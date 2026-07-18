# 프로젝트 D 개발 일지 — 10일차

## 개발 주제

JSON 기반 게임 데이터 저장 및 불러오기 구조 구현

## 개발 목표

- 플레이어의 진행 데이터를 저장할 데이터 클래스를 만든다.
- 게임 데이터를 JSON 파일로 저장하고 다시 불러올 수 있게 한다.
- DataManager를 씬 전환 후에도 유지한다.
- MainMenu의 새 게임과 이어하기를 실제 저장 파일과 연결한다.
- Lobby에서 저장된 크레딧 값을 표시한다.

## 구현 내용

### PlayerSaveData

- 저장 형식 구분을 위한 `saveVersion` 필드를 추가했다.
- 플레이어의 보유 재화를 저장하는 `credits` 필드를 추가했다.
- Unity `JsonUtility`에서 사용할 수 있도록 직렬화 가능한 일반 클래스로 구성했다.

### SaveSystem

- 저장 파일 이름을 `ProjectD_Save.json`으로 지정했다.
- 저장 위치로 `Application.persistentDataPath`를 사용했다.
- JSON 저장, 불러오기, 저장 파일 존재 확인과 삭제 기능을 구현했다.
- 빈 파일이나 파일 접근 오류가 발생했을 때 Console에 오류를 남기도록 처리했다.

### DataManager

- 현재 불러온 `PlayerSaveData`를 관리하도록 구현했다.
- 새 게임 생성, 이어하기, 현재 데이터 저장과 크레딧 추가 기능을 구성했다.
- `Awake()`에서 싱글턴 인스턴스를 등록하고 `DontDestroyOnLoad`를 적용했다.
- Bootstrap에서 생성된 DataManager가 Lobby 이동 후 `DontDestroyOnLoad` 영역에 유지되는 것을 확인했다.

### MainMenu 연결

- 임시 PlayerPrefs 값 대신 실제 JSON 저장 파일의 존재 여부로 Continue 버튼을 제어했다.
- New Game 선택 시 기본 데이터를 생성하고 저장하도록 연결했다.
- Continue 선택 시 JSON 파일을 불러온 후 Lobby로 이동하도록 연결했다.
- 저장과 불러오기 실패 시 공통 오류 팝업을 표시하도록 구성했다.

### Lobby 연결

- Lobby의 임시 크레딧 필드를 제거했다.
- `DataManager.Credits` 값을 `CurrencyValueText`에 표시하도록 변경했다.
- 테스트 기능으로 크레딧 `100`을 지급한 후 JSON 파일의 `credits` 값이 정상적으로 변경된 것을 확인했다.

## 확인한 저장 데이터

테스트 후 `ProjectD_Save.json`에서 다음 값이 저장된 것을 확인했다.

```json
{
    "saveVersion": 1,
    "credits": 100
}
```

JSON 파일에 `credits: 100`이 기록되었으므로 DataManager의 값 변경과 SaveSystem의 파일 저장은 정상적으로 동작한다.

## 문제 확인 기록

### Lobby에서 DataManager가 보이지 않았던 문제

- DataManager는 Lobby 씬에 직접 배치되는 오브젝트가 아니다.
- Bootstrap에서 생성된 뒤 `DontDestroyOnLoad` 영역으로 이동한다.
- `Awake()`의 싱글턴 등록과 `DontDestroyOnLoad` 처리를 확인했다.
- Lobby 실행 중 `DontDestroyOnLoad` 영역에 DataManager가 유지되는 것을 확인했다.

### 크레딧 표시 확인

- 테스트로 크레딧 `100`을 지급했다.
- Lobby 숫자의 즉시 변경 여부와 별개로 JSON 파일에는 `100`이 정상적으로 저장되었다.
- 이번 확인 과정에서는 추가 코드 수정이나 파일 변경을 진행하지 않았다.
- 현재 Lobby UI는 씬에 진입할 때 저장된 크레딧 값을 갱신하는 구조다.

## 생성 및 변경 파일

- `Assets/_ProjectD/Scripts/Data/PlayerSaveData.cs`
- `Assets/_ProjectD/Scripts/Data/SaveSystem.cs`
- `Assets/_ProjectD/Scripts/Data/DataManager.cs`
- `Assets/_ProjectD/Scripts/UI/MainMenuController.cs`
- `Assets/_ProjectD/Scripts/UI/LobbyController.cs`
- `Assets/_ProjectD/Scenes/Core/Bootstrap.unity`
- `Assets/_ProjectD/Scenes/Menu/MainMenu.unity`
- Localization String Table 관련 에셋
- `DevLogs/Day10/README.md`

이번 크레딧 JSON 확인 단계에서 추가로 수정된 프로젝트 파일은 없다.

## 테스트 결과

- [x] Bootstrap에서 DataManager가 생성된다.
- [x] Lobby 이동 후 DataManager가 `DontDestroyOnLoad`에 유지된다.
- [x] 새 게임 데이터가 JSON 파일로 저장된다.
- [x] 테스트 크레딧 `100`이 JSON 파일에 기록된다.
- [x] 저장 파일 경로를 직접 확인했다.
- [x] 크레딧 저장 확인 과정에서 추가 코드 수정이 필요하지 않았다.
- [ ] Play Mode 종료 후 Continue를 통한 크레딧 복원 최종 확인

## 최종 결과

프로젝트 D의 기본 게임 데이터 저장 구조를 완성했다. DataManager가 씬 전환 후에도 유지되고, 플레이어의 크레딧 데이터가 JSON 파일에 정상적으로 기록되는 것을 확인했다. 이번 크레딧 확인 단계에서는 저장 데이터가 정상적으로 변경되어 추가 코드를 수정하지 않았다.

## 다음 작업

- Play Mode를 완전히 종료한 뒤 Continue를 통해 크레딧 `100`이 복원되는지 확인한다.
- 실제 게임 콘텐츠에서 크레딧을 획득하거나 소비하는 기능과 연결한다.
- UI를 즉시 갱신해야 하는 시점에는 크레딧 변경 이벤트를 연결한다.