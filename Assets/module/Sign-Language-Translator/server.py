import socket
import time

# 서버의 IP 주소와 포트 번호 설정
host = 'localhost'
port = 1398

# 소켓 객체 생성
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# 포트 사용 중이라 연결할 수 없는 경우를 대비하여 설정
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

# IP 주소와 포트를 서버에 바인드
server_socket.bind((host, port))

# 서버가 클라이언트의 접속을 허용하도록 설정
server_socket.listen(5)
print(f'server wait at port num: {port} ...')

# 클라이언트 연결 대기
clients = []
while len(clients) < 2:
    client_socket, addr = server_socket.accept()
    print(f'Connected at addr: {addr} ...')
    clients.append(client_socket)

# 데이터 중계
try:
    while True:
        data = clients[0].recv(1024).decode()  # 수화인식모듈로부터 메시지 수신
        if not data:
            break
        print(f'Received from sign recognition module: {data}')
        if len(clients) == 2:
            clients[1].sendall(data.encode())  # 받은 메시지를 클라이언트에 전송
            print('Sent the message to client')
except Exception as e:
    print(f'error: {e}')
except KeyboardInterrupt:
    print("close.")
finally:
    for client_socket in clients:
        client_socket.close()
    
server_socket.close()  # 서버 소켓을 닫음
