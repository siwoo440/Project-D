# 프로젝트 D 개발 일지

---

## 8일차 : Splash와 MainMenu 기본 기능 구현

### 개발 날짜

2026년 7월 18일

---

### 개발 목표

Splash 화면의 표시·스킵 규칙을 구현하고, MainMenu의 새 게임·이어하기·설정·크레딧·종료 기능을 구성한다.

---

### 개발 내용

1. Splash 표시 규칙을 구현했다.

   - 최소 표시 시간 1초
   - 최대 표시 시간 3초
   - 키보드 임의 키 스킵
   - 마우스 왼쪽 클릭 스킵
   - TimeScale 영향을 받지 않는 시간 처리

2. Splash UI를 수정했다.

   - PROJECT D 임시 로고 표시
   - 스킵 안내 문장 추가
   - String Table 기반 한국어·영어 현지화

3. UIManager 확인 팝업을 확장했다.

   - 일반 팝업 CLOSE 버튼
   - 확인 팝업 CONFIRM 버튼
   - 확인 팝업 CANCEL 버튼
   - UnityAction 기반 확인 동작 저장
   - ESC 취소 처리
   - 확인 후 지정 동작 실행

4. MainMenuController를 구현했다.

   - 새 게임 시작
   - 이어하기 처리
   - 저장 데이터 여부 확인
   - 설정 팝업 연결
   - 크레딧 팝업 연결
   - 게임 종료 확인
   - Editor Play Mode 종료 처리

5. MainMenu UI를 구성했다.

   - 새 게임 버튼
   - 이어하기 버튼
   - 설정 버튼
   - 크레딧 버튼
   - 종료 버튼
   - Vertical Layout Group 기반 정렬

6. MainMenu 버튼을 현지화했다.

   - NEW GAME·새 게임
   - CONTINUE·이어하기
   - SETTINGS·설정
   - CREDITS·크레딧
   - EXIT·종료

7. 임시 저장 데이터 상태를 구현했다.

   - HasSaveData PlayerPrefs 저장
   - 저장 데이터가 없을 때 이어하기 비활성화
   - 새 게임 실행 후 이어하기 활성화
   - 테스트용 저장 데이터 초기화

---

### 실행 흐름

Splash:

최소 1초 표시
→ 키보드 또는 마우스 스킵 가능
→ 입력이 없으면 3초 후 자동 종료

새 게임:

저장 데이터 없음
→ 즉시 Lobby 이동

저장 데이터 있음
→ 덮어쓰기 확인 팝업
→ CONFIRM
→ Lobby 이동

이어하기:

저장 데이터 있음
→ Lobby 이동

종료:

EXIT
→ 종료 확인 팝업
→ CONFIRM
→ 게임 종료

---

### 생성 및 수정 파일

생성:

Assets/_ProjectD/Scripts/UI/MainMenuController.cs

수정:

Assets/_ProjectD/Scripts/Core/GameBootstrap.cs
Assets/_ProjectD/Scripts/UI/UIManager.cs
Assets/_ProjectD/Scenes/Core/Bootstrap.unity
Assets/_ProjectD/Scenes/Core/Splash.unity
Assets/_ProjectD/Scenes/Menu/MainMenu.unity
Assets/_ProjectD/Localization/Tables/UI Text_en.asset
Assets/_ProjectD/Localization/Tables/UI Text_ko-KR.asset
Assets/_ProjectD/Localization/Tables/UI Text Shared Data.asset

삭제:

Assets/_ProjectD/Scripts/UI/PopupTestController.cs

---

### 테스트 결과

- Splash 최소 표시 시간 확인
- Splash 최대 표시 시간 확인
- 키보드 스킵 확인
- 마우스 스킵 확인
- Splash 안내 현지화 확인
- MainMenu 버튼 배치 확인
- MainMenu 버튼 현지화 확인
- 이어하기 비활성화 확인
- 새 게임 Lobby 이동 확인
- 저장 데이터 생성 확인
- 새 게임 덮어쓰기 확인 팝업 확인
- 설정 팝업 확인
- 크레딧 팝업 확인
- 종료 확인 팝업 확인
- Editor Play Mode 종료 확인
- 로딩 화면 연동 확인
- Unity Console Error 0건

---

### 개발 결과

프로젝트 D의 실제 시작 화면 흐름과 MainMenu 기본 기능을 완성했다.

Splash는 최소 표시 시간이 지나면 키보드나 마우스로 건너뛸 수 있고, 입력이 없으면 자동으로 종료된다.

MainMenu는 새 게임, 이어하기, 설정, 크레딧, 종료 기능을 제공하며 새 게임 덮어쓰기와 종료에는 공통 확인 팝업을 사용한다.

현재 저장 데이터는 이어하기 버튼 검증을 위한 임시 PlayerPrefs 값이며, 실제 게임 진행 데이터는 이후 저장 시스템 구현 단계에서 교체한다.

---

### 다음 개발 계획

9일차에는 Lobby 기본 화면과 메뉴 이동 기능을 구현한다.

- Lobby 상단 재화 영역 구성
- 지휘 본부 이동 버튼
- 편성 이동 버튼
- 스테이지 선택 이동 버튼
- MainMenu 복귀 확인
- LobbyController 구현
- 씬 전환과 로딩 화면 연결