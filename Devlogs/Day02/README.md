# Project D 개발 일지

## 2일차: 필수 패키지 구성 및 충돌 확인

### 개발 목표

Unity 6000.3.9f1에서 프로젝트 D의 입력, UI 텍스트, 현지화, 자동 테스트와 카메라 시스템에 사용할 필수 패키지를 구성한다. 각 패키지를 개별적으로 설치하고 에디터 재실행과 Console 검사를 진행하여 이후 개발을 시작할 수 있는 안정적인 패키지 환경을 확보한다.

### 개발 환경

- 엔진: Unity 6000.3.9f1
- 템플릿: Universal 3D
- 렌더링: Universal Render Pipeline, `PC_RPAsset`
- 플랫폼: Windows PC 64비트
- 입력: Unity Input System
- UI 텍스트: TextMeshPro
- 현지화: Unity Localization
- 테스트: Unity Test Framework
- 카메라: Cinemachine
- 버전 관리: Git, GitHub

### 구현 내용

- Unity Package Manager의 Unity Registry 연결 확인
- 프리릴리스 패키지를 제외하고 Unity 호환 정식 버전 사용
- Input System 패키지 설치 상태 확인 및 설치
- Active Input Handling을 `Input System Package (New)`로 설정
- Input System 적용을 위한 Unity Editor 재실행
- TextMeshPro 설치 상태 확인
- TMP Essential Resources 임포트
- TMP Examples & Extras 임포트 제외
- Localization 패키지 설치
- Localization 의존 패키지 자동 설치 확인
- Test Framework 패키지 설치 상태 확인
- Test Runner 창 실행 확인
- Cinemachine 패키지 설치
- Cinemachine 메뉴 생성 확인
- 패키지별 설치 후 Unity Console 검사
- 설치 완료 후 프로젝트 재실행 검사
- `manifest.json`과 `packages-lock.json` 변경 내용 확인

### 생성 및 변경 파일

- `Packages/manifest.json`
- `Packages/packages-lock.json`
- `ProjectSettings/ProjectSettings.asset`
- `Assets/TextMesh Pro/Resources`
- `DevLogs/Day02/README.md`

### 테스트 결과

- Input System 패키지가 정상적으로 설치된 것을 확인했다.
- Active Input Handling이 `Input System Package (New)`로 설정된 것을 확인했다.
- Unity Editor 재실행 후 Input System 설정이 유지되는 것을 확인했다.
- TMP Essential Resources가 정상적으로 임포트된 것을 확인했다.
- Localization 패키지가 정상적으로 설치된 것을 확인했다.
- Test Runner 창이 정상적으로 열리는 것을 확인했다.
- EditMode와 PlayMode 테스트 목록이 표시되는 것을 확인했다.
- Cinemachine 메뉴가 정상적으로 표시되는 것을 확인했다.
- 기본 Sample Scene이 정상적으로 실행되는 것을 확인했다.
- 패키지 설치 후 URP의 `PC_RPAsset` 연결이 유지되는 것을 확인했다.
- 패키지 설치 과정에서 의존성 충돌이 발생하지 않은 것을 확인했다.
- Unity Console Error 0건을 확인했다.

### 미구현 항목

- Input Actions 에셋과 Action Map 생성
- 키보드·마우스 입력 바인딩
- UI 입력 차단 기능
- 한국어·영어 Locale 생성
- String Table과 Asset Table 생성
- 한국어·영어 폰트 에셋 제작
- EditMode와 PlayMode 테스트 코드
- Cinemachine Camera 생성과 설정
- 공통 씬 생성
- Bootstrap과 씬 전환 기능
- 오디오와 저장 시스템
- 실제 전투 시스템

### 다음 개발 방향

3일차에는 Bootstrap, Splash, LanguageSelect, MainMenu, Lobby, CommandHQ, Formation, StageSelect와 Battle 공통 씬을 생성한다. 각 씬을 프로젝트의 Scenes 폴더에 구분하여 저장하고 Build Profiles의 Scene List에 등록한 뒤 모든 씬이 개별적으로 오류 없이 실행되는지 확인한다.

### 커밋 제목

`2일차 : 필수 패키지 구성`
