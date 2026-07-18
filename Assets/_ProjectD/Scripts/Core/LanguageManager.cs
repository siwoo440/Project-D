using System.Collections; // 코루틴 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.Localization; // Locale 식별 기능
using UnityEngine.Localization.Settings; // Localization 설정 기능

public class LanguageManager : MonoBehaviour // 게임 언어 상태 관리
{
    public static LanguageManager Instance { get; private set; } // 전역 접근 인스턴스

    private bool isInitialized; // 초기화 완료 상태
    private bool isSelectingLanguage; // 언어 선택 처리 상태

    public bool IsInitialized => isInitialized; // 외부 확인용 초기화 상태

    private void Awake() // 싱글턴 생성과 유지 처리
    {
        if (Instance != null && Instance != this) // 기존 인스턴스 확인
        {
            Destroy(gameObject); // 중복 오브젝트 제거
            return; // 초기화 중단
        }

        Instance = this; // 현재 인스턴스 등록
        DontDestroyOnLoad(gameObject); // 씬 전환 후 유지
    }

    public IEnumerator InitializeAsync() // Localization 초기화와 언어 복원
    {
        if (isInitialized) // 기존 초기화 확인
        {
            yield break; // 중복 초기화 중단
        }

        yield return LocalizationSettings.InitializationOperation; // Localization 초기화 대기

        string initialLanguageCode = "en"; // 기본 언어 코드 설정

        if (GameManager.Instance != null && GameManager.Instance.HasSelectedLanguage) // 저장 언어 존재 확인
        {
            initialLanguageCode = GameManager.Instance.SavedLanguageCode; // 저장 언어 코드 불러오기
        }

        if (!TryApplyLocale(initialLanguageCode)) // 초기 Locale 적용 확인
        {
            TryApplyLocale("en"); // English 대체 적용
        }

        isInitialized = true; // 초기화 완료 저장
        Debug.Log($"Localization 초기화 완료: {initialLanguageCode}"); // 초기화 완료 기록
    }

    public void SelectLanguage(string languageCode) // 언어 선택 요청
    {
        if (isSelectingLanguage) // 중복 선택 확인
        {
            Debug.LogWarning("이미 언어 선택을 처리하고 있습니다."); // 중복 요청 경고
            return; // 추가 요청 중단
        }

        StartCoroutine(SelectLanguageAsync(languageCode)); // 비동기 언어 선택 시작
    }

    private IEnumerator SelectLanguageAsync(string languageCode) // 언어 선택 적용과 화면 이동
    {
        isSelectingLanguage = true; // 선택 처리 상태 활성화

        if (!isInitialized) // 초기화 완료 확인
        {
            yield return InitializeAsync(); // Localization 초기화 대기
        }

        if (!TryApplyLocale(languageCode)) // 선택 Locale 적용 확인
        {
            isSelectingLanguage = false; // 선택 처리 상태 해제
            yield break; // 선택 처리 중단
        }

        if (GameManager.Instance == null) // 게임 관리자 존재 확인
        {
            Debug.LogError("GameManager를 찾을 수 없습니다."); // 게임 관리자 오류
            isSelectingLanguage = false; // 선택 처리 상태 해제
            yield break; // 선택 처리 중단
        }

        GameManager.Instance.CompleteLanguageSelection(languageCode); // 언어 저장과 메인 메뉴 이동

        yield return null; // 중복 클릭 방지용 한 프레임 대기

        isSelectingLanguage = false; // 선택 처리 상태 해제
    }

    private bool TryApplyLocale(string languageCode) // Locale 검색과 적용
    {
        if (string.IsNullOrWhiteSpace(languageCode)) // 언어 코드 확인
        {
            Debug.LogError("적용할 언어 코드가 비어 있습니다."); // 언어 코드 오류
            return false; // 적용 실패 반환
        }

        Locale selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(languageCode)); // 언어 코드 Locale 검색

        if (selectedLocale == null) // Locale 검색 결과 확인
        {
            Debug.LogError($"등록된 Locale을 찾을 수 없습니다: {languageCode}"); // Locale 미등록 오류
            return false; // 적용 실패 반환
        }

        LocalizationSettings.SelectedLocale = selectedLocale; // 선택 Locale 적용
        Debug.Log($"언어 적용 완료: {languageCode}"); // 언어 적용 기록
        return true; // 적용 성공 반환
    }
}