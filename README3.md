# 2026 닷넷 개발자 데스크톱 개발

## 2.Unity 실습

### 2-1. 유니티 학습

- https://learn.unity.com/ 튜토리얼대로 따라하기
- Keijiro Takahasi Github : https://github.com/keijiro
- 이전 버전 https://unity.com/kr/releases/editor/archive 확인 다운로드 설치

#### Get Started With Unity

- Tutorial 순서대로 따라하기

    ![alt text](image-58.png)

- 1번 챕터 완료 후

    ![alt text](image-59.png)

### 2-1. Essential PathWay

- 가장 짧은 시간에 Unity 학습할 수 있는 튜토리얼

#### Essentials PathWay Template

![alt text](image-60.png)

- 템플릿 다운로드 우선

- 프로젝트명, 프로젝트 위치 선택 후 프로젝트 생성

#### 화면/시점 이동

- 방향키, WSAD
- Mouse Right, Wheel Up Down
- Flythrough Mode : Mouse Right + WSAD / EQ

- Object 선택 후 F 클릭(또는 오브젝트 더블 클릭)

#### Pan Tool

- 오브젝트 위치, 회전, 크기 등을 조절할 수 있는 아이콘 툴바

- View, Move, Rotate, Scale, Rect, Transform까지 여섯개 아이콘
- 단축키 : Q, W, E, R, T, Y

![alt text](image-61.png)

#### 오브젝트 위치(Position), 회전(Rotation), 크기(Scale) 조정

- Inspector에서 Position x, y, z값을 입력 또는 마우스로 좌우 드래그 형태로 변경
- Rotation, Scale도 동일하게 적용

![alt text](image-62.png)

![alt text](image-63.png)

#### Kid's Room 꾸미기

- 방 오브젝트
- 침대, 카페트, 협탁, 알람시계, 침실 조명 등 위치 및 

![alt text](image-64.png)

#### Material

- 오브젝트 재질 표현 객체
- Material 객체 생성 후 Inspector에서 조정

![alt text](image-65.png)

- Material 객체를 Ball 객체에 드래그

![alt text](image-66.png)

#### RigidBody

- 물리역학 기능 제공 컴포넌트
- Ball 선택 후 Inspector에서 Add Component 버튼 클릭

![alt text](image-67.png)

#### Physics Material

- 물체가 충돌할 때 마찰력, 반발력을 설정하는 자산
- Bounciness : 1 완전 탄성 충돌
    - 0.1(쇠구슬), 0.7(축구공), 0.9(고무공)

![alt text](image-68.png)

![alt text](image-69.png)

#### Ramp Object 추가

- 위치, 회전 지정
- Mesh Colider 컴포넌트 추가

![alt text](image-70.png)

#### Block 객체 생성

- Cube로 생성
- Scale x:0.1, y:0.25, z:0.1로 설정. Ball이 튕겨서 닿는 위치
- Rigid Body 추가

#### 카메라 시점 변환

- Flythrough 모드로 이동 후
- 카메라 오브젝트 선택
- Ctrl + Shift + F : 현 카메라 시점을 플레이 카메라 시점으로 변경

#### 프리팹 변경

- Prefabs 폴더 내에 기존 Object 드래그하면 Prefab으로 변경

![alt text](image-71.png)

#### Block 쌓기

- Pivot을 Center로 변경 후
- 프리팹 Block을 쌓아올림

![alt text](image-72.png)

#### 프리팹 편집모드

- 프로젝트 창의 프리팹을 더블클릭
- Inspector 수정 후
- RigidBody > mass를 1보다 작게 수정(0.1)
- 충돌하는 물체의 mass의 상대적 반응
- Hierachy 창의 < 버튼 클릭

![alt text](image-73.png)

#### 라이트, 스카이박스 조정

- 라이트
    - y, z축으로 낮밤조정 가능
    - Emission > Color 조정 빛 색상조절
    - Emission > Light Appearance, Filter and Temperature 선택 후 빛의 온도를 조정

    ![alt text](image-74.png)

- 스카이박스
    - 하늘 전체 배경 변경
    - Materials > Skyboxes의 Material을 Scene뷰에 드래그

#### PlayMode 구분짓기

- Preferences > Colors > Play mode tints 색상을 어두운 색으로 변경
- Play시 UI 색상이 Edit모드와 다르게 표시

![alt text](image-75.png)


#### 피벗기능

- Object를 쌓을 때 v를 누르면 Object의 기준점 변경됨

![alt text](image-76.png)

#### Chapter 2

![alt text](image-77.png)

#### Audio Effect

- 냄비 프리팹 선택 후 가스레인지 위 위치
- Audio Source 컴포넌트 추가
- Audio Generator 선택 후 Loop 체크
- Spatial Blend : 2D -> 3D 

![alt text](image-80.png)

#### Unity 오브젝트 복사

- Ctrl + D : 선택한 오브젝트 바로 복사

#### 단축키

- 메뉴 Edit > Shortcuts

    ![alt text](image-81.png)

#### 배경음악, 새소리

- 계층창에서 Audio Source 선택

- 알맞은 사운드 Audio Generator에서 선택

- 시작하면서 바로 음악 플레이하고 싶으면
    - Play on Awake 체크

- 새소리처럼 랜덤하게 플레이하고 싶으면
    - Play on Awake 체크 해제
    - PlaySoundAtRandomIntervals 스크립트 추가
    - Min/Max Seconds에 랜덤시간 지정

#### Programming

- 유니티 개발 시 가장 핵심!

- Player 오브젝트 위치, 회전, 크기 조정

- PlayerController 스크립트 생성 후 Player에 드래그

- 입력시스템 변경
    - Project Settings > Player > Other Settings > `Active Input Handling`을 Old 또는 `Both`로 변경 후 에디터 재시작

#### 카메라 플레이어 Child 지정

- Main Camera, Player 하위에 드래그
- 카메라 위치 Reset 후 위치, 회전 수정

![alt text](image-82.png)

- 방 아래 Cube까지 화면에 출력될 경우 위치 조정 잘해줘야 플레이 시 카메라 진동 X

#### 플레이모드 변수값 변경

- Speed : 5.0f, RotationSpeed : 120.0f
- 플레이 시 이동속도가 빠름
- 플레이모드 변수값 수정하면서 알맞은 속도 확인
- Speed : 0.3f, RotationSpeed : 70.0f 이 적당함
- Inspector에 지정된 스크립트 리셋

![alt text](image-83.png)

#### 아이템 코인 오브젝트

- Prefabs 폴더에서 Collectible Coin 드래그, 위치, 사이즈 조정
- Collectable.cs 스크립트 생성

```cs
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("회전 설정")]
    [Tooltip("프레임당 회전 속도")]
    [Range(0, 10)]
    public float rotationSpeed = 0.5f;

    [Tooltip("아이템 획득시 이펙트 지정")]
    public GameObject collectEffect;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);  // 매 프레임마다 y축을 0.5f씩 회전
    }

    // 물체끼리 충돌이 발생했을 때 이벤트 처리
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);    // 코인 삭제

        Instantiate(collectEffect, transform.position, transform.rotation);
    }
}
```

![alt text](image-84.png)

#### 아이템 획득 기능 추가

- Coin에 Box Colider > `Is Trigger` 체크
- 충돌은 발생하지 않고, 충돌감지 기능 활성화

![alt text](image-85.png)

- Collectable.cs에 onTriggerEnter 메서드 추가

#### 실행화면

https://github.com/user-attachments/assets/bcf01f87-cd24-4f25-9590-164d4d6bc289

#### 점프 기능 추가

- PlayController.cs에 공용변수, Update() 추가

```cs
[Tooltip("점프 강도")]
public float jumpForce = 5.0f;

// 입력처리, 카메라, ... Frame별 실행
// LateUpdate() : Update() 후에 실행되는 메서드. 카메라 추적
private void Update()
{
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
```

![alt text](image-86.png)

- Play 후 Space 누르면 점프 확인

#### 생성형 AI 활용 밤낮처리 추가

- 프롬포트로 요청
```text
유니티에서 Directional Light를 조정해서 밤낮으로 바뀌는 스크립트를 작성해줘. 20초에 한번씩 해가지고 다시 뜨도록 만들어줘. DayNightCycle.cs로 만들어줘
```

```cs
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Directional Light")]
    public Light sun;

    [Header("Cycle Settings")]
    [Tooltip("낮→밤→낮 한 바퀴 도는 시간(초)")]
    public float cycleDuration = 20f;

    private float currentTime;

    private void Update()
    {
        if (sun == null) return;

        // 시간 증가
        currentTime += Time.deltaTime;

        // 0 ~ 1 반복
        float normalizedTime = (currentTime % cycleDuration) / cycleDuration;

        // 0 ~ 360도 회전
        float sunAngle = normalizedTime * 360f;

        transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);

        // 빛 세기 조절
        float intensity = Mathf.Clamp01(
            Mathf.Cos((normalizedTime - 0.25f) * Mathf.PI * 2f)
        );

        sun.intensity = intensity;
    }
}
```

- Directional Light 오브젝트에 할당
- 변수 Sun에 Directional Light 할당

![alt text](image-87.png)

- Tutorial 스크립트

```cs
[Header("회전 속도 설정")]
public float rotationSpeed = 1f;

[Header("시간 설정")]
[Tooltip("하루(24시간)가 지나는데 걸리는 실제 시간(초)")]
public float dayDuration = 60f;

private float timePassed = 0.0f;

void Start()
{
    rotationSpeed = Mathf.Abs(rotationSpeed);
}

void Update()
{
    float angleToRotate =
        (360.0f / dayDuration) * Time.deltaTime;

    transform.Rotate(
        Vector3.right,
        angleToRotate * rotationSpeed);

    timePassed += Time.deltaTime;

    if (timePassed >= dayDuration)
    {
        timePassed = 0.0f;
    }
}
```

TODO - 이미지 추가

#### 방문 열기 기능

- DoorOpener.cs생성
- Door 루트오브젝트에 스크립트 지정
- Box Collider 추가 Is Trigger 체크 후 위치, 크기 수정
- 튜토리얼에 있는 스크립트 붙여넣기
- Player 객체에 Tag 콤보박스에서 `Player` 태그를 선택

![alt text](image-88.png)

#### 코인 획득 사운드 추가

- Collectable.cs에 소스 추가

```cs
[Header("이펙트 사운드")]
public AudioClip pickupSound;

// 물체끼리 충돌이 발생했을 때 이벤트 처리
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.postion);    // 뾰롱소리

        Destroy(gameObject);    // 코인 삭제

        Instantiate(collectEffect, transform.position, transform.rotation); // 파티클 이펙트 실행
    }
}
```

- prefab의 코인을 선택 후 Script 내 pickupSound 설정

![alt text](image-89.png)

#### 배포하기

- UI(Canvas) 메뉴에서 선택

![alt text](image-90.png)

#### 빌드 시 사용할 Scene 리스트 설정

- 메뉴 > File > Build Profiles
- 필요한 씬을 Scene List에 추가

![alt text](image-91.png)

- 플레이어 셋팅 작업
    - Company Name, Product Name, Version, Default Icon
    - Resolution > Windowed, Width, Height 설정

![alt text](image-92.png)

- Build Profiles > Build 또는 Build And Run 버튼 클릭 빌드 진행

![alt text](image-93.png)

![alt text](image-94.png)

- 메뉴 클릭 Scene 이동, ESC키로 메뉴 리턴

- 유니티 UI Camvus > Button Inspector 속성
- On Click 이벤트

![alt text](image-95.png)

---

### 2-2. 3D 모델 불러오기

#### 렌더링 파이프라인

- 오브젝트 생성, 카메라 확인, 빛 계산, 그림자 계산, 재질 생성/계산, 후처리 후 모니터 출력 등의 순서과정

#### Built-In / SRP

- Built-In : Unity가 렌더링 방식 고정. 수정 어려움
- SRP(Scriptable Render Pipeline) : Unity는 뼈대만 제공, 개발자가 원하는 렌더링을 추가하는 방식

#### 프로젝트 구분

- 렌더링 파이프라인 종류 3가지 구분

|종류|성능|그래픽품질|모바일/VR지원|
|:---:|:---:|:---:|:---:|
|Built-In|보통|보통|보통|
|URP|좋음|좋음|좋음|
|HDRP|낮음(고사양)|매우좋음|제한적|

- 기본적으로 Built-In으로 학습

![alt text](image-96.png)

- 에셋스토어에서 제공하는 에셋의 RP종류 확인하고 사용할 것

#### 3D 모델 활용방법

![alt text](image-97.png)

- 유니티에서는 3D 모델링이 아주 제한적
- 3D 모델 활용방법
    - Blender 무료 3D 모델링 툴에서 작업한 모델 import
    - 3D Max, Rhino 상용 모델링 툴 작업 모델 import
    - Unity Asset Store에서 제공하는 3D 모델 import
    - 생성형 AI로 모델 생성 import

#### 3D 모델 가져오기

- https://www.cgtrader.com
- https://poly.pizza/ (low polygon model)
- https://sketchfab.com/

![alt text](image-98.png)

- 호환되는 파일 포맷
    - `FBX` : Autodesk 3d(AutoCAD) 교환 포맷. Unity 가장 호환
    - `OBJ` : 범용 정적 모델 포맷, Unity 사용 가능
    - STL : 3D프린터용 포맷, 비추천
    - BLEND : Blender 원본 파일. 애니메이션 기능 포함. 가능(Blender 설치)
    - 3DS : 구형 AutoDesk 3D Studio 모델, 사용 가능

- 스케치팹 사이트 > Conveyor Belt 검색 > 로그인 후 검색 다운로드

- 압축해제 후 fbx, 텍스처를 프로젝트 Assets 폴더 아래에 복사

![alt text](image-99.png)

- Models 폴더에 위치한 Conveyor를 Scene뷰에 드래그

![alt text](image-100.png)

#### 생산품 박스

- Cube 오브젝트로 생성
- 구글에서 `Plastic Normal Map` 검색
- 텍스쳐 이미지 Assets > Textures 아래 위치에 저장
- Material 생성, Base Map 앞 사각형에 텍스쳐를 드래그(색상 X) 
- Rigid Body 추가

![alt text](image-101.png)

#### 컨베이어 벨트 물리컴포넌트 적용

- 컨베이어 벨트 전체가 아닌 Belt에만 Collider 추가
    - Mesh Collider : 3D 모델 폴리곤 메시 갯수만큼 충돌영역지정하기 때문에 리소스 부하
    - `Box Collider` : 큐브형태로 충돌영역 지정. 부하적음 

#### 컨베이어 벨트 스크립트 작성

- ConveyorBelt.cs 스크립트 생성
- 충돌이 감지되는 동안 물체이동 로직

```cs
using UnityEngine;  // 유니티 엔진 클래스

// MonoBehaviour C# 스크립트가 기본적으로 상속받는 핵심 클래스
// 개발자 코드가 유니티 엔진과 인터랙티브하게 소통할 수 있도록
// 오브젝트에 컴포넌트로 연결, 동작을 제어
public class ConveyorBelt : MonoBehaviour
{
    [Header("물체이동 방향")]
    public Vector3 moveDirection = Vector3.right;

    [Header("물체이동 속도")]
    public float speed = 2.0f;

    // 매 프레임마다 두 충돌 영역이 접촉하고 있는 동안 발생하는 이벤트 핸들러
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody; // 충돌 감지된 오브젝트에 리지드바디 가져오기

        if (rb != null)
        {
            rb.linearVelocity = moveDirection.normalized * speed;   // 이동방향으로 속도만큼 이동
        }
    }
}

```

- 컨베이어 오브젝트 중 Collider 컴포넌트 적용한 벨트에 스크립트 할당
- 플레이 테스트 후 방향 변경

![alt text](image-102.png)

#### Box Spawner 생성

- 박스를 일정시간마다 하나씩 생성하도록 하는 기능
- Product 박스, Conveyor를 프리팹으로 이동

![alt text](image-103.png)

- EmptyObject 생성, 위치를 이전 Product 위치로 지정
- BoxSpawner.cs 스크립트

```cs
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [Header("프리팹 지정")]
    public GameObject prdPrefab;

    [Header("생성 간격")]
    public float interval = 3.0f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;    // HW 성능별 FPS 고정

        if (timer >= interval)
        {
            timer = 0;

            // instant 예제, 샘플
            // Quaternion.identity 회전값 없는 상태
            Instantiate(prdPrefab, 
                        transform.position, 
                        Quaternion.identity);   
        }
    }
}
```

- Spawner 빈오브젝트에 스크립트 할당

#### 실행결과

https://github.com/user-attachments/assets/a562599f-92e3-4811-8c33-7a24ede90116










### 2-3. Unity Factory

- Unity Technologies Japan에서 제공하는 무료 HDRP 공장 시뮬레이션 에셋
- 공장 건물부터 컨베이어라인, 로봇팔, 작업자, 조명 등 제공
- https://assetstore.unity.com/ 에서 `Unity Factory` 검색

#### 프로젝트 생성

- HighDefinition 3D(HDRP) 프로젝트 생성
- My Asset에서 Unity Factory를 검색 후 Import

![alt text](image-78.png)

- Import 후 오류 발생
    - Spline Container 오류
        - Package Manager > Unity Registy, Splines 검색 후 설치

    - InputSystem 오류
        - 키보드, 마우스 입력 시스템이 Unity 6부터 변경
        - 예전 방식 입력시스템 사용
        - Project Settings > Player > Other Settings > `Active Input Handling`을 Old 또는 `Both`로 변경 후 에디터 재시작

    TODO - 비활성화하기 전 이미지추가

- Global Volume 오브젝트, 사용체크 비활성화

    ![alt text](image-79.png)
