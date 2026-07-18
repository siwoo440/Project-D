# 프로젝트 D 개발 일지

---

## 4일차 : 게임 초기화 및 씬 전환 구현

### 개발 날짜

2026년 7월 18일

---

### 개발 목표

게임 실행 시 Bootstrap을 시작점으로 사용하고, Splash를 거쳐 최초 실행 여부에 따라 LanguageSelect 또는 MainMenu로 이동하는 초기 실행 구조를 구현한다.

---

### 개발 내용

1. SceneNames 클래스를 추가했다.

   - 공통 씬 이름 상수 관리
   - 문자열 직접 입력으로 발생할 수 있는 오타 방지
   - 이후 씬 전환 기능 확장 기반 구성

2. SceneLoader를 구현했다.

   - LoadSceneAsync 기반 비동기 씬 로딩
   - 중복 로딩 요청 방지
   - 빈 씬 이름 검사
   - Scene List 등록 여부 검사
   - 씬 로딩 완료 기록 출력

3. GameManager를 구현했다.

   - 싱글턴 방식의 전역 접근 구조
   - DontDestroyOnLoad를 통한 씬 간 유지
   - 언어 선택 완료 여부 관리
   - PlayerPrefs 기반 최초 실행 기록 저장
   - 테스트용 언어 선택 기록 초기화 기능

4. GameBootstrap을 구현했다.

   - 게임 시작 시 Splash 자동 이동
   - Splash 표시 시간 처리
   - 언어 선택 기록에 따른 다음 씬 결정
   - 초기화 완료 후 GameBootstrap 제거

5. LanguageSelectController를 구현했다.

   - 임시 CONTINUE 버튼 입력 처리
   - 언어 선택 완료 상태 저장
   - MainMenu 이동 요청

6. Bootstrap 씬에 다음 오브젝트를 배치했다.

   - GameManager
   - SceneLoader
   - GameBootstrap

7. LanguageSelect 씬에 임시 확인 버튼을 추가했다.

---

### 실행 흐름

최초 실행:

Bootstrap
→ Splash
→ LanguageSelect
→ MainMenu

두 번째 실행 이후:

Bootstrap
→ Splash
→ MainMenu

---

### 생성된 스크립트

Assets/_ProjectD/Scripts
├─ Core
│  ├─ SceneNames.cs
│  ├─ SceneLoader.cs
│  ├─ GameManager.cs
│  └─ GameBootstrap.cs
└─ UI
   └─ LanguageSelectController.cs

---

### 테스트 결과

- Bootstrap 시작 확인
- Splash 비동기 이동 확인
- Splash 약 2초 표시 확인
- 최초 실행 LanguageSelect 이동 확인
- CONTINUE 버튼 MainMenu 이동 확인
- 재실행 MainMenu 직접 이동 확인
- GameManager 씬 간 유지 확인
- SceneLoader 씬 간 유지 확인
- GameBootstrap 초기화 후 제거 확인
- 중복 씬 로딩 방지 확인
- Unity Console Error 0건

---

### 개발 결과

프로젝트 D의 공통 게임 초기화 구조와 비동기 씬 전환 기반을 완성했다.

게임은 Bootstrap을 시작점으로 사용하며, Splash 표시 후 언어 선택 기록에 따라 LanguageSelect 또는 MainMenu로 자동 이동한다.

현재 LanguageSelect의 버튼은 분기 테스트를 위한 임시 기능이다. 실제 한국어와 영어 선택 및 Localization Locale 적용은 이후 현지화 구현 단계에서 추가한다.

---

### 다음 개발 계획

5일차에는 공통 UI 전환 화면과 로딩 표시 기능을 구현한다.

- LoadingCanvas 구성
- 검은색 페이드 인·아웃
- 씬 로딩 진행률 표시
- 씬 전환 중 입력 차단
- SceneLoader와 전환 UI 연결
- 해상도 변경 대응 확인