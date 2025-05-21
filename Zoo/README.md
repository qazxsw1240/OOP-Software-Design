# OOP-Software-Design
객체지향적 소프트웨어 설계 스터디

![클래스구조](스터디1차시.drawio(2).png)

## 1차시 "직접 해보기"
1. 각 클래스로 생성된 개체는 무슨 역할과 책임을 맡을지 설명한다.

- Zoo.Species
    - 역할 : 동물의 종 표현
    - 책임 : Species 이름 유효성 검사, Species Equals 비교, Species - String 암묵적 변환(짜피 state가 string _value 밖에 없으니까), Species 간 연산자 오버라이딩 
- Zoo.Animal
    - 역할 : 동물 한 마리의 정보를 표현
    - 책임 : 동물 고유 ID, 이름, 종(Zoo.Species) 정보 보관(readonly), Animal 끼리의 Equals 비교
- Zoo.AnimalCollection
    - 역할 : 여러 Animal 개체 관리
    - 책임 : Animal 개체들 List에 보관(readonly), Animal 개체 또는 개체들 List에 추가, 삭제, 탐색
- Zoo.Application
    - 역할 : 애플리케이션 진입점.
    - 책임 : Console 기반 UI 출력
- Zoo.Application.ApplicationInputResult
    - 역할 : 사용자의 입력을 캡슐화 하고 검증
    - 책임 : 사용자 입력값 보관(readonly), 프로그램 종료 요청 검사, ApplicationInputResult - String 암묵적 변환

2. 개체의 상태와 동작은 다른 개체와 어떻게 상호작용하는지 설명한다.
- Zoo.Species
    -`Animal` 개체의 field로 사용
    - string과 변환가능
- Zoo.Animal
    - `Species`를 통해 종 정보를 가짐
    - `AnimalCollection`에 저장
- Zoo.AnimalCollection
    - `Animal` 개체 저장, 관리
    - `Application`에 `_animals`에 대한 툴 제공
- Zoo.Application
    - `AnimalCollection`를 가지고 동물 데이터 관리
- Zoo.Application.ApplicationInputResult
    - `Application`의 입력 처링 사용

3. 개체의 캡슐화가 어떻게 이뤄져 있는지 설명할 수 있어야 한다.
- Zoo.Species
    - _value를 private readonly로 선언
    - 유효성 검증을 통해 잘못된 데이터 진입 차단
- Zoo.Animal
    - 모든 내부 state를 private readonly로 선언
    - Immutable Object
- Zoo.AnimalCollection
    - 내부 컬렉션을 private으로 보호
    - 컬렉션 조작은 메서드를 통해서만 가능
    - 조회 시 복사본을 반환
- Zoo.Application
    - 사용자 입력 처리를 내부 클래스로 캡슐화

	1. 캡슐화가 적용된 부분이 무엇을 염두에 둔 것인지 생각해 보고 설명한다.
        - 유효하지 않은 종 이름 생성 방지
        - 객체 생성 후 상태 변경 방지
        - 잘못된 입력 처리 로직 통합
4. (추가) 이 프로그램 코드는 어떤 기능을 확장하는 데에 염두했는지 생각해 보고 설명한다.
    - `Animal` 클래스는 id, name, Species뿐만이 아닌 age, gender와 같은 속성으로 확장가능
    - `AnimalCollection` 새로운 검색 조건이 필요할 때 메서드만 추가하면 됨
	1. 이 프로그램 코드는 어떤 기능을 확장하지 않도록 염두했는지 생각해 보고 설명한다.
        - 동물 고유 정보 변경
        - 동물의 state 동적 추가

5. 기타
    `Zoo.Animal`에서 `Equals` 재정의 부분에서
    ```csharp
    Id == other.Id
    && Name == other.Name
    && Species == other.Species;
    ```
    이래 되어있는데, 그냥 ID만 비교해도 되지 않을..까?