import cv2
from ultralytics import YOLO

# ESP32-CAM 주소
URL = 'http://192.168.0.13/stream'  # 기본웹 80포트, stream 81포트

# YOLO 모델
model = YOLO('yolo11n.pt')

cap = cv2.VideoCapture(URL)

if not cap.isOpened():
    print('ESP32-CAM 연결 실패')
    exit()

print('ESP32-CAM 연결 성공')
print('YOLO 객체 인식 시작')

while True:
    ret, frame = cap.read()

    if not ret:
        print('영상 수신 실패')
        break

    # YOLO 객체 인식
    # imgsz=320 입력이미지를 320으로 픽셀 줄이기
    results = model(frame, imgsz=320, verbose=False)

    # 결과 이미지
    result_frame = results[0].plot()

    cv2.imshow('ESP32-CAM YOLO', result_frame)

    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()