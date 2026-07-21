# 2026 닷넷 개발자 토이프로젝트

## 웹 통합 토이 프로젝트

### 공공데이터 통합 플랫폼

- [공공데이터 통합 플랫폼](./TOYPROJECT1.md)

### WPF MVVM 활용

- [MVVM 패턴 학습 + 앱개발](./TOYPROJECT2.md)

### 깃허브 대문 작성

- [GitHub 대문 ReadMe](./TOYPROJECT3.md)

### AI 비전 검사 시스템

- [Python AI + WebAPI + MQTT/WebSocket 연계](./TOYPROJECT4.md)

### 스마트팩토리 MES 미니 플랫폼

- 컨베이어벨트 조별 + MQTT + (WPF) + Unity 연계

### IoT 스마트홈 통합 플랫폼

- MQTT WPF + WebAPI + Unity 연계

### Unity ProductApp 기능 개선

- 각 상품 클릭시 3D 박스와 연계
- 로봇팔 오브젝트 연계

### 실시간 채팅 시스템 + 챗봇 기능

- Python AI + SignalR API

### 취업처 설명

- 사람인, 잡코리아 확인
    - 분야 입력(임베디드)
    - 신입 선택
    - 학력 미선택

### 네트워크 연결 설정

- 여러사람이 같이 한 PC(서버) 공유할 수 있도록 공유기/라우터 설정

- 네트워크 연결 설정 방법

    - 사용 중 공유기 정보확인

        ![alt text](image-263.png)

    - 현재 포트포워딩 상태

        ![alt text](image-265.png)

    - MQTT 포트포워드 설정 지정

        ![alt text](image-266.png)

    - 이후 설정 저장

        ![alt text](image-268.png)

    - 윈도우(OS) 방화벽 포트 연결 허용 설정

        1. Windows 검색 > 방화벽 상태 확인 > 고급설정 > 인바운드 규칙 > 새 규칙

            ![alt text](image-272.png)

        2. 새 인바운드 규칙 마법사
            - 규칙 종류 : 포트
            - TCP/특정로컬포트 : 1883
            - 연결허용
            - 도메인/개인/공용 선택
            - 이름 : MQTT Port Open

        3. 설정 완료

            ![alt text](image-274.png)

    - 외부 IP로 접속 확인

        ![alt text](image-264.png)

#### MQTT 브로커 접속

- MQTT Explorer에서 외부 MQTT 브로커 연결 정보 설정

    ![alt text](image-275.png)

- MQTT Explorer에서 토픽을 지정하여 메시지 Publish 확인

    ![alt text](image-267.png)

