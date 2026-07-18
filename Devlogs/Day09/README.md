# 프로젝트 D 개발 일지 — 9일차

## 개발 목표

- `Lobby` 씬을 게임의 메뉴 허브로 구성한다.
- `CommandHQ`, `Formation`, `StageSelect` 씬으로 이동하는 버튼을 연결한다.
- 각 하위 화면에서 `Lobby`로 돌아오는 기본 이동 경로를 만든다.
- `MainMenu` 복귀 전에 공통 확인 팝업을 표시한다.
- 로비의 정적 UI 문구를 한국어와 영어로 현지화한다.
- 공통 로딩 화면을 거쳐 모든 씬이 정상적으로 전환되는지 확인한다.

## 개발 내용

### 로비 UI 구성

- 상단에 로비 제목과 임시 재화 표시 영역을 배치했다.
- 중앙에 지휘 본부, 편성, 스테이지 선택 버튼을 배치했다.
- 하단에 메인 메뉴 복귀 버튼을 배치했다.
- 실제 재화 데이터 시스템이 아직 없으므로 재화 값은 `LobbyController`의 임시 값으로 표시했다.

### 로비 화면 제어

- `LobbyController`를 생성했다.
- 각 메뉴 버튼을 `SceneLoader`와 연결했다.
- 메인 메뉴 복귀 시 `UIManager`의 공통 확인 팝업을 사용하도록 구성했다.
- 씬 로딩 중에는 중복 이동 요청을 무시하도록 처리했다.

### 하위 화면 복귀 기능

- `ReturnToLobbyController`를 생성했다.
- `CommandHQ`, `Formation`, `StageSelect` 씬에 로비 복귀 버튼을 추가했다.
- 각 복귀 버튼도 공통 `SceneLoader`를 사용하도록 연결했다.

### 현지화

- `UI Text` String Table Collection에 로비 관련 문자열을 추가했다.
- 한국어 테이블은 프로젝트에 등록된 Locale 코드인 `ko-KR`을 사용했다.
- 버튼과 제목에는 `Localize String Event`를 연결했다.
- 메인 메뉴 복귀 팝업 문구는 `LocalizedString` 참조로 연결했다.

## 생성 및 변경 파일

- `Assets/_ProjectD/Scripts/UI/LobbyController.cs`
- `Assets/_ProjectD/Scripts/UI/ReturnToLobbyController.cs`
- `Assets/_ProjectD/Scenes/Menu/Lobby.unity`
- `Assets/_ProjectD/Scenes/Menu/CommandHQ.unity`
- `Assets/_ProjectD/Scenes/Menu/Formation.unity`
- `Assets/_ProjectD/Scenes/Menu/StageSelect.unity`
- Localization String Table 관련 에셋
- `DevLogs/Day09/README.md`

## 테스트 결과

- [ ] `Bootstrap`에서 시작했을 때 `MainMenu → Lobby` 이동이 정상이다.
- [ ] `Lobby → CommandHQ → Lobby` 이동이 정상이다.
- [ ] `Lobby → Formation → Lobby` 이동이 정상이다.
- [ ] `Lobby → StageSelect → Lobby` 이동이 정상이다.
- [ ] 메인 메뉴 복귀 팝업의 취소 버튼이 정상 동작한다.
- [ ] 메인 메뉴 복귀 팝업의 확인 버튼이 `MainMenu`를 연다.
- [ ] 모든 씬 전환에서 공통 로딩 화면이 표시된다.
- [ ] 한국어와 영어 UI 문구가 정상적으로 표시된다.
- [ ] Console에 빨간색 Error가 없다.

## 발생한 문제와 해결

- 발생한 문제가 없다면 `특이 사항 없음`으로 기록한다.
- 오류가 있었다면 오류 문구, 원인, 수정 내용을 여기에 기록한다.

## 다음 작업

- 10일차에는 실제 게임 데이터를 관리할 기반 구조를 만들고, 임시 재화 값을 데이터 관리자와 연결한다.
- 저장 데이터의 생성, 불러오기, 초기화 흐름을 구체화한다.