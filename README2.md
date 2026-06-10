# 2026 닷넷 개발자 데스크톱 개발

## WPF 실습

### 카페 키오스크 개발

- 사용 스펙
  - WPF (.NET 10.0)
  - MaterialDesign (MaterialDesignThemes)
  - MySQL + DBeaver

#### 프로젝트 생성

- WPFCafeKiosk
- NuGet Package, MaterialDesignThemes, MySqlConnector 설치
- MahApps.Metro.IconPacks 추가 설치

  ![alt text](image-35.png)

#### 프로젝트 구성

- WPF 머티리얼 디자인 적용
- 키오스크 UI 제작
- 메뉴 모델, 주문 모델 생성
- 메뉴버튼 하드코딩
- MySQL menu 테이블 생성
- DB에서 메뉴 조회
- 메뉴버튼 동적생성
- 주문목록, 총액 계산

#### MaterialDesign 적용

- App.xaml에 리소스딕셔너리 적용

#### MySQL DB, Table 생성

- cafekiosk 데이터베이스 생성
- menu 테이블 생성
- orders, order_detail 테이블 생성

```sql
CREATE TABLE menu
(
    menu_id INT PRIMARY KEY AUTO_INCREMENT,
    menu_name VARCHAR(100) NOT NULL,
    price INT NOT NULL,
    image_path VARCHAR(255),
    category VARCHAR(20),
    is_sale CHAR(1) DEFAULT 'Y'
);

CREATE TABLE orders
(
    order_id INT PRIMARY KEY AUTO_INCREMENT,
    order_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    total_count INT NOT NULL,
    total_amount INT NOT NULL
);

CREATE TABLE order_detail
(
    detail_id INT PRIMARY KEY AUTO_INCREMENT,
    order_id INT NOT NULL,
    menu_id INT NOT NULL,
    menu_name VARCHAR(100) NOT NULL,
    price INT NOT NULL,
    count INT NOT NULL,
    total_price INT NOT NULL,
    CONSTRAINT fk_order_detail_orders
        FOREIGN KEY (order_id)
        REFERENCES orders(order_id)
);
```

![alt text](image-37.png)

#### 모델 클래스

- MenuItem - DB menu테이블과 매핑
- OrderItem - 주문리스트 저장

#### 이미지 작업

- Pixabay등 사이트에서 다운로드 후 편집
- Images 폴더에 붙여넣기

![alt text](image-32.png)

#### MainWindow UI 작업 및 기본 이벤트

![alt text](image-33.png)

#### 메뉴 옵션 팝업창 작업

![alt text](image-34.png)

#### 기본 동작 이벤트 구현

https://github.com/user-attachments/assets/71a66993-b2c5-4dda-90c4-93534e6f6e6e

### 카페 키오스크 구현 리스트

- [X] 옵션 팝업창에서 수량 선택한 내용 주문담기 버튼 기능구현
- [X] 키오스크 리스트뷰 음료 리스트업
- [X] 선택한 상품, 결제버튼 비용, 갯수 연동
- [X] 전체 삭제 기능
- [X] 남은 시간 완료 후 전체내용 초기화
- [X] 홈 버튼 클릭 초기화
- [X] 메인창에서 옵션창으로 MenuId 전달
- [ ] DB 연동!! 로딩 메뉴 SELECT / 주문내역 INSERT
- [X] 메뉴 동적 바인딩!!
- [ ] DB저장 후 신용카드 결제 팝업(더미)

#### 옵션창 주문내역 확인

![alt text](image-36.png)

- `Tag="{Binding}"` - 객체 자체의미, OrderItem 객체 자체. 하위에서 MenuName, Count 등 사용 가능
- Margin, Padding 위치 순서 - Left, Top, Right, Bottom / Left&Right, Top&Bottom 순서
- CornerRadius 위치 순서 - TopLeft, TopRight, BottomRight, BottomLeft / TopLeft&BottomRight, TopLight&BottomLeft 순서

#### 실행결과

https://github.com/user-attachments/assets/041efd61-0fc9-421a-9cfe-0ac99181ddad

#### 로그확인 방법

- 프로젝트 속성 > 출력유형

    ![alt text](image-39.png)

- Windows 애플리케이션 -> 콘솔 애플리케이션으로 변경
- MessageBox.Show() 대신 Console.WriteLine()메서드로 변경

  ![alt text](image-38.png)
- 실행로그 확인

#### DB 주문내역 등록

- DatabaseHelper에 INSERT 처리 메서드 생성
- MainWindow.xaml.cs에 저장쿼리 실행 메서드 생성
- BtnPay_Click 이벤트 핸들러에 저장 메서드 추가

#### 최종 작업

- 프로젝트 속성 > 출력 유형, Windows 애플리케이션으로 변경
- 구성관리자 Debug -> Release로 변경 빌드
- 배포...

#### 전체 실행결과

### OPENAPI 연동앱 개발
