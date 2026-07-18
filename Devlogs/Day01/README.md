\# Project D 개발 일지



\## 1일차: 프로젝트 규격 및 개발 기반 구축



\### 개발 목표



Unity 6000.3.9f1과 Universal Render Pipeline을 사용하여 프로젝트 D의 기본 프로젝트를 생성하고, 이후 시스템 개발에 공통으로 사용할 프로젝트 설정, 폴더 구조, Assembly Definition과 Git 저장소를 구성한다.



\### 개발 환경



\- 엔진: Unity 6000.3.9f1

\- 템플릿: Universal 3D

\- 렌더링: Universal Render Pipeline, `PC\_RPAsset`

\- 플랫폼: Windows PC 64비트

\- 기본 해상도: 1920×1080

\- 최소 대응 해상도: 1280×720

\- 지원 화면비: 16:9, 16:10, 21:9

\- 목표 프레임: 60FPS

\- 스크립트 언어: C#

\- 버전 관리: Git, GitHub



\### 구현 내용



\- 프로젝트 D Unity 프로젝트 생성

\- Unity 버전을 6000.3.9f1로 확정

\- Universal 3D 템플릿 적용

\- Quality 설정의 Render Pipeline Asset에서 `PC\_RPAsset` 연결 확인

\- Windows PC 64비트 빌드 프로필 설정

\- 기본 실행 해상도를 1920×1080으로 설정

\- 창 크기 변경 허용 설정

\- Color Space를 Linear로 설정

\- Version Control Mode를 Visible Meta Files로 설정

\- Asset Serialization Mode를 Force Text로 설정

\- `Assets/\_ProjectD` 기준 프로젝트 폴더 구조 생성

\- Runtime 스크립트용 `ProjectD.Runtime` Assembly Definition 생성

\- Editor 스크립트용 `ProjectD.Editor` Assembly Definition 생성

\- `ProjectD.Editor`에서 `ProjectD.Runtime` 참조 설정

\- Unity 자동 생성 파일을 제외하는 `.gitignore` 생성

\- Git 로컬 저장소 생성

\- 기본 브랜치를 `main`으로 설정

\- 1일차 프로젝트 파일 첫 커밋 준비



\### 생성 파일



\- `Assets/\_ProjectD/Scripts/Runtime/ProjectD.Runtime.asmdef`

\- `Assets/\_ProjectD/Scripts/Editor/ProjectD.Editor.asmdef`

\- `.gitignore`

\- `DevLogs/Day01/README.md`



\### 테스트 결과



\- Unity 6000.3.9f1에서 프로젝트가 정상적으로 열리는 것을 확인했다.

\- Quality 설정에 `PC\_RPAsset`이 연결된 것을 확인했다.

\- Universal Render Pipeline이 정상적으로 적용된 것을 확인했다.

\- 기본 Sample Scene이 정상적으로 실행되는 것을 확인했다.

\- 머티리얼이 분홍색으로 표시되는 URP 오류가 없는 것을 확인했다.

\- Windows PC 64비트 빌드 프로필이 활성화된 것을 확인했다.

\- 기본 해상도가 1920×1080으로 설정된 것을 확인했다.

\- `ProjectD.Runtime`과 `ProjectD.Editor`가 오류 없이 컴파일되는 것을 확인했다.

\- `.gitignore`에서 Library, Temp, Obj, Logs와 UserSettings 폴더가 제외되는 것을 확인했다.

\- Assets, Packages, ProjectSettings와 `.meta` 파일이 Git 추적 대상에 포함되는 것을 확인했다.

\- Unity Console Error 0건을 확인했다.



\### 미구현 항목



\- Input System 패키지 설치와 설정

\- TextMeshPro 필수 리소스 확인

\- Localization 패키지 설치와 설정

\- Unity Test Framework 확인

\- Cinemachine 사용 여부 확인

\- Bootstrap과 공통 관리자 구현

\- 공통 씬 생성과 Build Profiles 등록

\- 씬 전환 기능

\- 입력 관리 기능

\- 오디오 관리 기능

\- 현지화 기능

\- 저장 및 불러오기 기능

\- 실제 전투 시스템



\### 다음 개발 방향



2일차에는 Input System, TextMeshPro, Localization, Unity Test Framework와 Cinemachine의 사용 여부를 확인하고 필요한 패키지를 설치한다. 설치 후 에디터를 다시 실행하여 패키지 충돌과 Console 오류가 없는지 검사한다.



\### 커밋 제목



`1일차 : 프로젝트 기반 구축`

