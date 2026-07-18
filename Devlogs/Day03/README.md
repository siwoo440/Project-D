# 프로젝트 D 개발 일지

---

## 3일차 : 공통 씬 구성

### 개발 날짜

2026년 7월 18일

---

### 개발 목표

프로젝트 D의 전체 화면 구조에 필요한 공통 씬을 생성하고, Windows Build Profile의 Scene List에 등록한다.

---

### 개발 내용

1. 공통 씬 폴더 구조를 확인하고 정리했다.

   - `Scenes/Core`
   - `Scenes/Menu`
   - `Scenes/Battle`
   - `Scenes/Test`

2. 다음 공통 씬 9개를 생성했다.

   - `Bootstrap`
   - `Splash`
   - `LanguageSelect`
   - `MainMenu`
   - `Lobby`
   - `CommandHQ`
   - `Formation`
   - `StageSelect`
   - `Battle`

3. 각 씬에 실행 확인용 임시 UI를 배치했다.

   - 전체 화면 배경
   - 현재 씬 이름을 표시하는 TextMeshPro 텍스트
   - UI 입력 처리를 위한 EventSystem
   - 해상도 대응을 위한 Canvas Scaler

4. Canvas 기준 해상도를 `1920 × 1080`으로 설정했다.

5. 각 씬의 임시 텍스트를 영어로 작성했다.

6. EventSystem에 `Input System UI Input Module`이 적용된 것을 확인했다.

7. Windows Build Profile의 Scene List에 모든 씬을 등록했다.

8. 게임 시작용 `Bootstrap` 씬을 Build Index 0으로 설정했다.

---

### Scene List 등록 순서

| Build Index | 씬 이름 | 역할 |
|---:|---|---|
| 0 | Bootstrap | 게임 시스템 초기화 |
| 1 | Splash | 게임 로고 표시 |
| 2 | LanguageSelect | 최초 실행 언어 선택 |
| 3 | MainMenu | 메인 메뉴 |
| 4 | Lobby | 중심 로비 |
| 5 | CommandHQ | 캐릭터 및 자원 관리 |
| 6 | Formation | 전투 편성 |
| 7 | StageSelect | 스테이지 선택 |
| 8 | Battle | 실제 전투 진행 |

---

### 생성된 폴더 구조

Assets/_ProjectD/Scenes
├─ Core
│  ├─ Bootstrap.unity
│  └─ Splash.unity
├─ Menu
│  ├─ LanguageSelect.unity
│  ├─ MainMenu.unity
│  ├─ Lobby.unity
│  ├─ CommandHQ.unity
│  ├─ Formation.unity
│  └─ StageSelect.unity
├─ Battle
│  └─ Battle.unity
└─ Test

---

### 테스트 결과

- 공통 씬 9개 생성 완료
- 모든 씬 개별 실행 확인
- 각 씬의 이름 표시 확인
- Canvas와 EventSystem 구성 확인
- Scene List 등록 순서 확인
- Bootstrap Build Index 0 설정 확인
- Unity Console Error 0건
- Unity 프로젝트 저장 및 재실행 확인

---

### 개발 결과

게임의 주요 화면을 구현할 기본 씬 구조를 완성했다.

현재는 각 씬을 개별적으로 실행할 수 있지만, 씬 사이를 이동하는 기능은 구현하지 않았다. 이후 공통 관리자와 씬 로딩 시스템을 구현하여 전체 게임 흐름을 연결할 예정이다.

---

### 다음 개발 계획

4일차에는 다음 기능을 구현한다.

- GameBootstrap 구현
- GameManager 구현
- SceneLoader 구현
- 비동기 씬 로딩 구현
- Bootstrap에서 Splash로 이동
- 최초 실행 여부에 따른 LanguageSelect와 MainMenu 분기
- 씬 전환 중 중복 입력 방지