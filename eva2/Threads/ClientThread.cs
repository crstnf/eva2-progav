using eva2Model.DAL;
using eva2Model.DTO;
using SocketUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2.Threads
{
    public class ClientThread
    {
        private ClientSocket clientSocket;
        private ILecturaDAL LecturaDAL = LecturaDALFactory.CreateDAL();

        private IMedidorDAL MedConsumoDAL = MedidorConsumoDALFactory.CreateDAL();
        private IMedidorDAL MedTraficoDAL = MedidorTraficoDALFactory.CreateDAL();

        private IMedidorDAL[] DALs = new IMedidorDAL[3];

        private string logfile = Directory.GetCurrentDirectory()
        + Path.DirectorySeparatorChar + "logfile.txt";

        private bool canUpdate;
        public ClientThread(ClientSocket clientSocket)
        {
            DALs[(int)Medidor.Tipo.Consumo] = MedConsumoDAL;
            DALs[(int)Medidor.Tipo.Trafico] = MedTraficoDAL;
            this.clientSocket = clientSocket;
        }

        public void LogandSendError(int nro_medidor, String msg)
        {
            String fecha = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            Console.WriteLine(msg);
            File.AppendAllText(logfile, "[" + fecha + "] " + msg + "\n");
            String returnmsg = fecha + "|" + nro_medidor + "|ERROR";
            clientSocket.Write(returnmsg);
            clientSocket.CloseConnection();
            canUpdate = false;
        }

        public Medidor.Tipo parseTipo(String tipo)
        {
            if (tipo == "trafico") return Medidor.Tipo.Trafico;
            else if (tipo == "consumo") return Medidor.Tipo.Consumo;
            else return Medidor.Tipo.Desconocido;
        }

        public bool isMedidorValid(Medidor.Tipo tipo, int medidorID)
        {
            bool response = false;
            List<Medidor> medidores = DALs[(int)tipo].ObtenerMedidores();
            foreach (Medidor medidor in medidores)
            {
                if (medidor.Id == medidorID) response = true;
            }

            return response;
        }

        public String getUnidadMedida(Medidor.Tipo tipo)
        {
            if (tipo == Medidor.Tipo.Consumo) return "kwH";
            else if (tipo == Medidor.Tipo.Trafico) return "Vehiculos";
            else return null;
        }
        public String createWaitResponse()
        {
            String fechaServidor = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            return fechaServidor + "|WAIT";
        }

        public void Run()
        {
            Lectura lectura = new Lectura();

            // Handshake 
            // --- Handshake integrity
            String[] handshake = null;
            try { 
                handshake = clientSocket.Read().Trim().Split('|'); 
            }
            catch (Exception ex)
            {
                LogandSendError(0, "Invalid handshake packet");
                return;
            }

            if (handshake.Length != 3)
            {
                LogandSendError(0, "Invalid handshake packet length");
                return;
            }

            // --- Tipo (necesario para verificar medidor)
            lectura.Tipo = parseTipo(handshake[2]);
            if (lectura.Tipo == Medidor.Tipo.Desconocido)
            {
                LogandSendError(0, "Invalid medidor type");
                return;
            }

            // --- Medidor
            try
            {
                lectura.MedidorID = Int32.Parse(handshake[1]);
            }
            catch (Exception ex)
            {
                LogandSendError(0, "Error while parsing medidor ID - ex: " );
                return;
            }

            if (!isMedidorValid(lectura.Tipo, lectura.MedidorID))
            {
                LogandSendError(lectura.MedidorID, "Invalid Medidor ID for this Tipo");
                return;
            }

            // --- Fecha
            DateTime? fecha = null;
            try
            {
                fecha = DateTime.ParseExact(handshake[0], "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                LogandSendError(0, "Invalid date");
                return;
            }

            TimeSpan time_threshold = new TimeSpan(0, 30, 0);
            if (fecha < DateTime.Now.Subtract(time_threshold) || fecha == null)
            {
                LogandSendError(lectura.MedidorID, "Invalid, greater than 30 minutes");
                return;
            }

            // Wait response
            canUpdate = true;
            clientSocket.Write(createWaitResponse());

            // Update
            // --- Update integrity
            String[] update = null;
            try { update = clientSocket.Read().Trim().Split('|'); }
            catch (Exception ex)
            {
                LogandSendError(0, "Invalid update packet");
                return;
            }

            if (update == null)
            {
                LogandSendError(lectura.MedidorID, "Invalid update packet parameter");
                return;
            }

            if (update[update.Length - 1] != "UPDATE")
            {
                LogandSendError(lectura.MedidorID, "Invalid update packet parameter");
                return;
            }

            // --- Medidor
            try
            {
                int UpdateMedidorID = Int32.Parse(update[0]);
                if (UpdateMedidorID != lectura.MedidorID)
                {
                    LogandSendError(lectura.MedidorID, "Invalid medidor ID, previous was " + lectura.MedidorID + " but new is " + UpdateMedidorID);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogandSendError(0, "Error while parsing medidor ID on Update");
                return;
            }

            // --- Fecha
            try { 
                lectura.Fecha = DateTime.ParseExact(update[1], "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture); 
            }
            catch (Exception ex) { 
                LogandSendError(0, "Invalid date");
                return;
            }

            // --- Tipo 
            Medidor.Tipo UpdateTipo = parseTipo(update[2]);
            if (lectura.Tipo != UpdateTipo)
            {
                LogandSendError(0, "Invalid medidor type, previous was " + lectura.Tipo + " but new is " + UpdateTipo);
                return;
            }

            // --- Valor
            try { 
                lectura.Valor = Int32.Parse(update[3]); 
            } catch (Exception ex) { 
                LogandSendError(0, "Error while parsing valor on Update"); 
                return;
            }

            if (lectura.Valor < 0 || lectura.Valor > 1000) 
            {
                LogandSendError(lectura.MedidorID, "Invalid valor value");
                    return;
            }

            // -- Estado, si es que existe. Caso contrario asumimos que la lectura es correcta.
            Lectura.EstadoLectura estado = Lectura.EstadoLectura.OK;
            if (update.Length == 6 && update[4] != "UPDATE")
            {
                try { 
                    estado = (Lectura.EstadoLectura)Int32.Parse(update[4]); 
                }
                catch (Exception ex) { 
                    LogandSendError(0, "Error while parsing estado on Update"); 
                    return; 
                }
            }
            lectura.Estado = estado;


            // --- Unidad Medida
            lectura.UnidadMedida = getUnidadMedida(lectura.Tipo);

            // Lock & Update
            if (canUpdate) lock (LecturaDAL) LecturaDAL.RegistrarLectura(lectura);

            clientSocket.CloseConnection();
        }
    }
}
