# 프로젝트 D 개발 일지

---

## 7일차 : 한국어·영어 현지화 기반 구현

### 개발 날짜

2026년 7월 18일

---

### 개발 목표

Unity Localization을 사용하여 한국어와 영어 Locale을 구성하고, 최초 실행 언어 선택과 선택 언어 저장 및 복원 기능을 구현한다.

---

### 개발 내용

1. Localization Settings를 생성했다.

   - 프로젝트 현지화 설정 구성
   - 비동기 초기화 방식 적용
   - English 기본 Locale 설정
   - Specific Locale Selector 설정

2. 지원 Locale을 추가했다.

   - English Locale 등록
   - Korean Locale 등록
   - Locale Identifier en·ko 사용

3. 한국어 TextMeshPro Font Asset을 구성했다.

   - Noto Sans KR 폰트 추가
   - Dynamic Atlas Population 설정
   - Multi Atlas Textures 활성화
   - TextMeshPro Fallback Font 등록

4. UI Text String Table을 생성했다.

   - language_select_title 추가
   - main_menu_title 추가
   - 영어 문장 작성
   - 한국어 문장 작성

5. LanguageManager를 구현했다.

   - Localization 비동기 초기화
   - 저장 언어 코드 복원
   - Locale 검색과 적용
   - Korean·English 언어 선택
   - 중복 언어 선택 방지

6. GameManager의 언어 저장 기능을 확장했다.

   - 언어 선택 완료 상태 저장
   - 선택 언어 코드 저장
   - 테스트 초기화 시 두 저장값 삭제

7. GameBootstrap 초기화 순서를 수정했다.

   - Splash 이전 Localization 초기화
   - 저장 Locale 적용 후 화면 흐름 시작

8. LanguageSelect 화면을 수정했다.

   - 임시 CONTINUE 버튼 제거
   - 한국어 버튼 추가
   - English 버튼 추가
   - 실제 Locale 선택 기능 연결

9. LanguageSelect와 MainMenu 제목을 현지화했다.

   - Localize String Event 적용
   - 선택 Locale에 따른 TMP 텍스트 갱신

---

### 실행 흐름

최초 실행:

Bootstrap
→ Localization 초기화
→ English 기본 적용
→ Splash
→ LanguageSelect
→ 언어 선택
→ 선택 언어 저장
→ MainMenu

재실행:

Bootstrap
→ Localization 초기화
→ 저장 언어 복원
→ Splash
→ MainMenu

---

### 생성 및 수정 파일

생성:

Assets/_ProjectD/Scripts/Core/LanguageManager.cs
Assets/_ProjectD/Localization/Localization Settings.asset
Assets/_ProjectD/Localization/Locales/English Locale.asset
Assets/_ProjectD/Localization/Locales/Korean Locale.asset
Assets/_ProjectD/Localization/Tables/UI Text Shared Data.asset
Assets/_ProjectD/Localization/Tables/UI Text_en.asset
Assets/_ProjectD/Localization/Tables/UI Text_ko.asset
Assets/_ProjectD/Fonts/NotoSansKR/NotoSansKR SDF.asset

수정:

Assets/_ProjectD/Scripts/Core/GameManager.cs
Assets/_ProjectD/Scripts/Core/GameBootstrap.cs
Assets/_ProjectD/Scripts/UI/LanguageSelectController.cs
Assets/_ProjectD/Scenes/Core/Bootstrap.unity
Assets/_ProjectD/Scenes/Menu/LanguageSelect.unity
Assets/_ProjectD/Scenes/Menu/MainMenu.unity
Packages/manifest.json
Packages/packages-lock.json

---

### 테스트 결과

- Localization 초기화 확인
- English Locale 적용 확인
- Korean Locale 적용 확인
- 한국어 버튼 동작 확인
- English 버튼 동작 확인
- 선택 언어 저장 확인
- 저장 언어 재실행 복원 확인
- 최초 실행 LanguageSelect 표시 확인
- 재실행 LanguageSelect 건너뛰기 확인
- LanguageSelect 제목 현지화 확인
- MainMenu 제목 현지화 확인
- 한국어 TMP 출력 확인
- 로딩 화면 연동 확인
- Unity Console Error 0건

---

### 개발 결과

프로젝트 D의 한국어·영어 현지화 기반과 실제 언어 선택 흐름을 완성했다.

최초 실행에서는 LanguageSelect에서 언어를 선택하고, 선택 결과는 PlayerPrefs에 저장된다. 이후 실행부터는 저장된 Locale을 Splash 이전에 적용하고 MainMenu로 직접 이동한다.

UI 문장은 String Table에서 언어별로 관리할 수 있으며, TextMeshPro의 Localize String Event를 통해 Locale 변경 시 자동으로 갱신된다.

---

### 다음 개발 계획

8일차에는 실제 Splash 화면 흐름과 MainMenu 기본 UI 기능을 구현한다.

- Splash 표시 시간과 스킵 조건 정리
- MainMenu 버튼 구조 구성
- 새 게임 버튼 구현
- 이어하기 버튼 상태 처리
- 설정 팝업 연결
- 크레딧 팝업 연결
- 게임 종료 확인 팝업 연결