# 프로젝트 D 개발 일지

---

## 6일차 : 공통 UI 관리자와 팝업 구현

### 개발 날짜

2026년 7월 18일

---

### 개발 목표

게임 전체에서 공통으로 사용할 UIManager를 구현하고, 공통 팝업과 UI 입력 차단 상태를 관리한다.

---

### 개발 내용

1. UIManager를 구현했다.

   - 싱글턴 기반 전역 접근
   - DontDestroyOnLoad 기반 씬 간 유지
   - 공통 팝업 표시와 닫기
   - 팝업 제목과 내용 변경
   - ESC 입력 처리
   - 팝업 상태 관리

2. GameplayBlocked 상태를 구현했다.

   - 공통 팝업 활성화 상태 확인
   - SceneLoader 로딩 상태 확인
   - 팝업 또는 로딩 중 전체 게임 입력 차단
   - 이후 이동과 카메라 입력에서 사용할 공통 상태 제공

3. 공통 팝업 UI를 구성했다.

   - 전체 화면 반투명 배경
   - 팝업 패널
   - 제목 텍스트
   - 내용 텍스트
   - 닫기 버튼
   - 배경 UI 입력 차단

4. 새 Input System 기반 ESC 입력을 구현했다.

   - Keyboard.current 존재 확인
   - escapeKey 입력 확인
   - 로딩 중 ESC 처리 차단
   - 팝업 활성화 상태에서 닫기 처리

5. SceneLoader와 UIManager를 연결했다.

   - 씬 로딩 시작 전 활성 팝업 닫기
   - 로딩 화면을 공통 팝업보다 앞에 표시
   - 씬 전환 후 팝업 잔존 방지

6. MainMenu에 임시 팝업 테스트 버튼을 추가했다.

---

### UI 차단 구조

일반 상태:

GameplayBlocked = false

팝업 열림:

IsPopupOpen = true
GameplayBlocked = true

씬 로딩 중:

SceneLoader.IsLoading = true
GameplayBlocked = true

팝업과 로딩 종료:

GameplayBlocked = false

---

### 생성 및 수정 파일

생성:

Assets/_ProjectD/Scripts/UI/UIManager.cs
Assets/_ProjectD/Scripts/UI/PopupTestController.cs

수정:

Assets/_ProjectD/Scripts/Core/SceneLoader.cs
Assets/_ProjectD/Scenes/Core/Bootstrap.unity
Assets/_ProjectD/Scenes/Menu/MainMenu.unity

---

### 테스트 결과

- UIManager 생성 확인
- UIManager 씬 간 유지 확인
- 공통 팝업 열기 확인
- 공통 팝업 닫기 확인
- CLOSE 버튼 동작 확인
- ESC 팝업 닫기 확인
- 팝업 뒤쪽 UI 입력 차단 확인
- GameplayBlocked 팝업 상태 반영 확인
- GameplayBlocked 로딩 상태 반영 확인
- SceneLoader 팝업 자동 닫기 확인
- 로딩 화면 표시 순서 확인
- Unity Console Error 0건

---

### 개발 결과

프로젝트 D의 전체 UI 상태를 관리하는 UIManager와 공통 팝업 기반을 완성했다.

공통 팝업이 활성화되거나 씬을 로딩하는 동안 GameplayBlocked가 활성화되며, 이후 플레이어 이동과 카메라 입력을 일관되게 차단할 수 있다.

공통 팝업은 제목과 내용을 전달받아 표시하며, 닫기 버튼 또는 ESC 키로 닫을 수 있다.

---

### 다음 개발 계획

7일차에는 실제 언어 선택과 Localization 기반을 구현한다.

- Localization Settings 생성
- Korean Locale 추가
- English Locale 추가
- 기본 언어 설정
- 언어 선택 버튼 구현
- 선택 언어 PlayerPrefs 저장
- 재실행 시 저장 언어 복원
- 임시 CONTINUE 버튼 교체