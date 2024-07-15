import os
import cv2
import mediapipe as mp
import numpy as np
from tensorflow.keras.models import load_model
import tensorflow as tf
import logging


# TensorFlow 경고 메시지 비활성화
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3' 
tf.get_logger().setLevel(logging.ERROR)

actions = ['water', 'down', 'fail']
seq_length = 30

model = load_model('models/model.keras')  # 모델 경로 수정

# MediaPipe hands model
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils
hands = mp_hands.Hands(
    max_num_hands=2,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5)

cap = cv2.VideoCapture(0)

seq = []
action_seq = []

while cap.isOpened():
    ret, img = cap.read()
    if not ret:  # 프레임 읽기에 실패한 경우 루프를 계속
        continue

    img0 = img.copy()

    img = cv2.flip(img, 1)
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    result = hands.process(img)
    img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)

    if result.multi_hand_landmarks is not None:
        for res in result.multi_hand_landmarks:
            joint = np.zeros((21, 4))
            for j, lm in enumerate(res.landmark):
                joint[j] = [lm.x, lm.y, lm.z, lm.visibility]

            v1 = joint[[0,1,2,3,0,5,6,7,0,9,10,11,0,13,14,15,0,17,18,19], :3]
            v2 = joint[[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20], :3]
            v = v2 - v1
            v = v / np.linalg.norm(v, axis=1)[:, np.newaxis]

            angle = np.arccos(np.einsum('nt,nt->n',
                v[[0,1,2,4,5,6,8,9,10,12,13,14,16,17,18],:],
                v[[1,2,3,5,6,7,9,10,11,13,14,15,17,18,19],:])) 

            angle = np.degrees(angle)

            d = np.concatenate([joint.flatten(), angle])

            seq.append(d)
            seq = seq[-seq_length:]  # 최신 seq_length 프레임만 유지

            mp_drawing.draw_landmarks(img, res, mp_hands.HAND_CONNECTIONS)

            if len(seq) < seq_length:  # 30 프레임에 대해 정보가 전부 충족을 한 경우에만 실행하도록
                continue

            input_data = np.expand_dims(np.array(seq, dtype=np.float32), axis=0)

            y_pred = model.predict(input_data).squeeze()

            i_pred = int(np.argmax(y_pred))
            conf = y_pred[i_pred]  # 제일 높은 예상치를 conf에 담음

            if conf < 0.95:
                action = 'fail'
            else:
                if len(result.multi_hand_landmarks) == 1 and actions[i_pred] == 'water':
                    action = 'water'
                elif len(result.multi_hand_landmarks) == 2 and actions[i_pred] == 'down':
                    action = 'down'
                else:
                    action = 'fail'

            action_seq.append(action)
            action_seq = action_seq[-5:]  # 최신 5개 예측만 유지

            if len(action_seq) < 5:  # 행동이 연속적으로 5번 인식되어야 실행 가능
                continue

            this_action = '?'
            if action_seq[-1] == action_seq[-2] == action_seq[-3]:
                this_action = action

            cv2.putText(img, f'{this_action.upper()}', org=(int(res.landmark[0].x * img.shape[1]), int(res.landmark[0].y * img.shape[0] + 20)), fontFace=cv2.FONT_HERSHEY_SIMPLEX, fontScale=1, color=(255, 255, 255), thickness=2)

    else:
        action_seq.append('fail')
        action_seq = action_seq[-5:]  # 최신 5개 예측만 유지

    cv2.imshow('img', img)
    if cv2.waitKey(1) == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()