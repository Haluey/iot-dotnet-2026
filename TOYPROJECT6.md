# 토이 프로젝트 6

## 컨베이어벨트 공정관리 시스템 2

### ESP32-CAM

#### 개요

![alt text](image-327.png)

**Ai-Thinker ESP32-CAM**

ESP32 기반 프로세서 사용, WiFi, 블루투스를 지원하는 아두이노 호환보드

업로드 모듈을 사용하지 않을 경우, 아래와 같이 브레드보드, USB모듈을 직접 연결해야 함

![alt text](image-328.png)

#### 기본사양 일부

- Bluetooth 4.2, BLE
- WiFi 802.11 전부가능
- USB b타입 지원
- microSD 4G까지 지원 - 사진 및 데이터 저장
- 외부 안테나 연결 가능

#### 활용처

- 카메라 촬영
- 실시간 영상 스트리밍
- Wi-Fi 통신
- 자체 웹 서버 기능
- 간단한 영상/물체 감지 - Arduino TinyML
- IoT 기능도 포함
- UART - Arduino, Raspberry Pi와 시리얼 통신

##### ESP32-CAM 사용이유

- 라즈베리파이 직접 카메라를 장착하려면 - RPi Camera 또는 USB 웹캠 가능
- 컨베이어벨트 등 산업장비에 설치, 독립적으로 스트리밍을 가능하게 하기 위해서 사용
- 저사양으로 테스트용으로 사용. 실제 산업현장용은 고비용 고사양

![alt text](image-329.png)

#### 개발환경 설정

Arduino IDE 나 Visual Stduio Code - PlatformIO 확장으로 사용 등 여러방법 존재

##### VS Code - PlatformIO IDE

- VS Code 확장 
- Install
- Python 설치되어 있지않으면 병행 설치됨
- 새로 리로드 필요

##### PlatformIO IDE 프로젝트 생성

1. PlatformIO 아이콘 클릭(pio)
2. Quick Access에서 New Project 선택

    - 프로젝트 명, Board : AI Thinker ESP32-CAM, Framework : Arduino 선택

    - Finish

![alt text](image-330.png)

##### PlatformIO 프로젝트 구조

- 프로젝트 폴더 구조
    - include : 헤더파일 위치
    - lib : 외부라이브러리 저장
    - src : cpp파일 위치
    - test : 단위 테스트용
    - platformio.ini : 사용할 보드 설정

    ![alt text](image-331.png)

- 프로젝트 태스크 구조
    - Build : 빌드 컴파일
    - Upload : 보드 업로드
    - Monitor : 시리얼 모니터
    - Upload and Monitor : 업로드 후 시리얼 모니터 오픈
    - Clean / Full Clean : 소스 정리
    - Devices : 보드 정보 확인

    ![alt text](image-341.png)

    - 윈도우 장치 관리자에서 시리얼포트 확인, 라즈베리파이에서는 /dev/ttyUSB*

##### ESP32-CAM 동작확인

- [platformio.ini](./toyproject/ToyProjects06/platformio_part/test_esp32cam/platformio.ini) 작성 - 버전 변경(6.6.0) 후 저장하면 프로젝트 재구성 시간소요

    ![alt text](image-340.png)

- 기본동작 소스 테스트

    ```cpp
    #include <Arduino.h>

    void setup() {
    Serial.begin(115200);

    delay(2000);

    Serial.println();
    Serial.println("ESP32-CAM START");
    }

    void loop() {
    Serial.println("ESP32 alive!");

    delay(1000);
    }
    ```

- PlatformIO 프로젝트 태스크 > Build 클릭
- Build 성공하면 [SUCCESS] 출력
- 최초 Upload시 Tool Manager 다운로드 설치 시간소요

    ![alt text](image-332.png)

- 업로드 %가 표시

    ![alt text](image-333.png)

- 프로젝트 태스크 > Monitor 클릭

- ESP32-CAM보드 > RST 버튼 클릭 초기화

    ![alt text](image-334.png)

- 기본명령어
    - 빌드 : `platformio run`
    - 실제 : `platformio.exe run --environment esp32cam `
    - 빌드 + 업로드 : `platformio run --target upload`
    - 시리얼 모니터 : `platformio device monitor`
    - Clean : `platformio run --targer clean`

##### ESP32-CAM 웹서버 예제

- [소스](./toyproject/ToyProjects06/platformio_part/test_esp32cam/src/main.cpp)

- 빌드, 업로드 후 모니터 확인

    ![alt text](image-335.png)

- 테스트

    ![alt text](image-336.png)

- 특이사항
    - ESP32-CAM 저사양으로 한번에 여러개 접속 불가능
    - WiFi 2.4GHz만 지원(5G 이상 접속 불가)
    - 웹브라우저 오픈 + Python YOLO 동시에 처리하면 스트리밍 끊김
    - TinyML이라는 머신러닝 라이브러리로 AI 처리 가능 - 느려서 사용 어려움
    - ESP32-CAM은 영상만 스트리밍. 물체인식 등은 라즈베리파이에서 Python으로 처리

#### Python OpenCV, YOLO 연계

- 기본적인 [OpenCV 소스](./toyproject/ToyProjects06/raspberrypi_part/test_opencv.py) 동작 확인
- 기본적인 [YOLO 소스](./toyproject/ToyProjects06/raspberrypi_part/test_yolo.py) 동작 확인

    ![alt text](image-337.png)

- 색상으로 인식할 모델 생성 또는 검색

#### ESP32-CAM 전원만 인가

![alt text](image-338.png)

ESP32-CAM 동작확인

#### 라즈베리파이 + ESP32-CAM

- 윈도우에서 ESP32-CAM 빌드, 업로드한 보드가 라즈베리파이에서 동작 실패
- 컬러센서에서 인식하는 부분에 카메라 위치

![alt text](image-339.png)

### 물체인식 기능 추가

#### Raspberry Pi Global Python에 PIP 라이브러리 설치방법

- 라즈비안 Wormbook부터 글로벌 Python은 PIP로 라이브러리 설치가 금지(방지)

    ```bash
    $ pip install ~
    error : externally-managed-environment
    ```

- 위 명령을 무시하고 설치하고자 하면

    ```bash
    ## 1번째 방법
    $ sudo rm /usr/lib/python3.13/EXTERNALLY-MANAGED
    # 삭제 후
    $ pip install ~

    ## 2번째 방법
    $ pip install ~ --break-system-packages
    ```

#### Raspberry Pi YOLO 설치 시 주의점

- YOLO 설치 (Python 가상환경)을 아래와 같이하면

    ```bash
    (.venv)$ pip install opencv-python 
    (.venv)$ pip install ultralytics # YOLO 설치하면서 PyTorch와 같이 설치 
    ```

    - YOLO로 자동설치되는 PyTorch는 GPU버전이 설치됨
    - ARM64 버전에 Nvidia Jetson Nano들은 GPU가 설치되어 있음
    - MicroSD 32GB에서는 pip캐시 저장용량, ssd tmp 드라이브 용량이 모자람

- Raspberry Pi에서 YOLO를 설치하려면 아래의 명령으로 진행할 것

    ```bash
    (.venv)$ pip install opencv-python
    (.venv)$ pip install torch torchvision --index-url https://download.pytorch.org/whl/cpu 
    (.venv)$ pip install ultralytics # YOLO만 설치
    ```

#### YOLO 물체인식

- ESP32-CAM으로 컬러센서 대신 물체인식 변경
- YOLO에서 사용할 커스터마이징 모델 훈련, 생성
- 현재 벨트상황에서 훈련시킬 물체 사진 캡쳐
    - 최소 색상별(Red, Green, Blue) 100장 이상 캡쳐

#### ESP32-CAM 캡쳐 기능

- 보드 재부팅 현상 발생
- 보류

#### Python OpenCV 캡쳐 기능

- [소스](./toyproject/ToyProjects06/raspberrypi_part/test_capture.py)

![alt text](image-342.png)

#### YOLO Pretrained Model 생성

생산품 색상별 인식할 수 있는 YOLO 모델 생성해야 함

YOLO 커스텀 학습이 필요

1. 이미지 셋 준비 (색상별 100장 이상)
2. **라벨링**
3. `YOLO 형식에 맞게 export`
4. 데이터 셋 폴더 구성 - Train 폴더 / Validation 폴더
5. data.yaml 작성
6. YOLO로 학습

##### 라벨링 툴

- [Robotflow](https://roboflow.com/) - 유료 라벨링 사이트
- [cvat.ai](https://www.cvat.ai/) - 유료 라벨링 사이트. export 시 결재 팝업
- [labelmg](https://github.com/HumanSignal/labelImg) - 무료툴 Github 오픈소스

##### LabelImg툴 사용법

![alt text](image-343.png)

##### YOLO 학습 폴더(데이터셋) 구성

- YOLO 학습을 위한 데이터셋 구성
    - train : val - 8 : 2로
    - images > train, val 
    - labels > train, val 

    ![alt text](image-344.png)

    - data.yaml 작성

##### YOLO 학습

- `Fine-tuning` : 기존 yolo11n.pt 사전학습 모델을 가져와서 필요한 Red/Green/Blue 데이터로 재학습

- YOLO 사전학습 모델 기반으로 학습
    - data.yaml 절대경로로 작성
    - Ultralytics 패키지 폴더 setting.json 파일 내 
        - 윈도우 경우 C:\Users\User\AppData\Roaming\Ultralytics\setting.json
        - `dataset_dir`(훈련시킬 데이터셋 경로로 지정), weights_dir, runs_dir

    ```bash
    yolo detect train data=C:\SourceBank\iot-dotnet-2026\toyproject\ToyProjects06\raspberrypi_part\data.yaml model=yolo11n.pt epochs=100 imgsz=640
    ```

    - 훈련 진행중 화면

        ![alt text](image-345.png)

    - 결과 화면. 모델 파일 위치 확인

        ![alt text](image-346.png)

    - 훈련 중간 배치 이미지 확인

        ![alt text](image-347.png)

    - 훈련모델 물체인식 테스트

        ```powershell
        (venv) PS C:\...\iot-dotnet-2026> yolo detect predict model=../runs/detect/train-5/weights/best.pt source=.\toyproject\ToyProjects06\raspberrypi_part\dataset\images\val\capture_018.jpg

        Ultralytics 8.4.102  Python-3.12.10 torch-2.13.0+cu130 CUDA:0 (NVIDIA GeForce RTX 5060, 8151MiB)
        YOLO11n summary (fused): 101 layers, 2,582,737 parameters, 0 gradients, 6.3 GFLOPs

        image 1/1 C:\SourceBank\iot-dotnet-2026\toyproject\ToyProjects06\raspberrypi_part\dataset\images\val\capture_018.jpg: 480x640 1 red, 35.3ms
        Speed: 1.2ms preprocess, 35.3ms inference, 10.0ms postprocess per image at shape (1, 3, 480, 640)
        Results saved to C:\SourceBank\runs\detect\predict-5
        Learn more at https://docs.ultralytics.com/modes/predict
        VS Code: view Ultralytics VS Code Extension  at https://docs.ultralytics.com/integrations/vscode
        ```

    - YOLO CNN 코드 작성해서 모델 학습(TODO)

##### 라즈베리파이 실시간 확인

- best.pt파일 이전
- raspberrypi_yolo.py실행

    ![alt text](image-348.png)

- 실시간 물체인식 확인

### 기존 컨베이어벨트 키트와 통합

### 유니티에서 컨베이어벨트 제어

