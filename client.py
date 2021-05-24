import socket, datetime

IP = "127.0.0.1"
PORT = 2500

def getTimeFormatted():
    return datetime.datetime.now().strftime("%Y-%m-%d-%H-%M-%S")

def connect():
    client_socket = socket.socket()
    client_socket.connect((IP, PORT))
    return client_socket

def sendHandshake(client_socket):
    handshake = "{}|{}|{}\n".format(getTimeFormatted(), numeroMedidor, tipo)
    client_socket.send(handshake.encode())
    data = client_socket.recv(1024).decode()
    return data

def sendUpdate(client_socket):
    update = "{}|{}|{}|{}|UPDATE\n".format(numeroMedidor, getTimeFormatted(), tipo, valor)
    client_socket.send(update.encode())
    data = client_socket.recv(1024).decode()
    return data

if __name__ == "__main__":
    opt = input("Tipo de medidor:\n1.- Consumo\n2.- Trafico\nOpcion: ")
    if opt == "1":
        tipo = "consumo"
    elif opt == "2":
        tipo = "trafico"
    else:
        exit()

    while True:
        numeroMedidor = input("Numero medidor: ")
        clientSocket = connect()
        handshakeResponse = sendHandshake(clientSocket)
        if "WAIT" in handshakeResponse:
            print("El servidor esta esperando una update")
            valor = input("Inserte valor de la medicion: ")
            updateResponse = sendUpdate(clientSocket)
            print(updateResponse)
        elif "ERROR" in handshakeResponse:
            print("Hubo un error de comunicacion")
