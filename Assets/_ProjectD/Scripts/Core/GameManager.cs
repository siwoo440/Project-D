using UnityEngine; // Unity 기본 기능

public class GameManager : MonoBehaviour // 공통 게임 상태 관리
{
    private const string LanguageSelectedKey = "LanguageSelected"; // 언어 선택 저장 키

    public static GameManager Instance { get; private set; } // 전역 접근 인스턴스

    public bool HasSelectedLanguage => PlayerPrefs.GetInt(LanguageSelectedKey, 0) == 1; // 언어 선택 완료 여부

    private void Awake() // 싱글턴 생성과 오브젝트 유지
    {
        if (Instance != null && Instance != this) // 기존 인스턴스 확인
        {
            Destroy(gameObject); // 중복 오브젝트 제거
            return; // 중복 초기화 중단
        }

        Instance = this; // 현재 인스턴스 등록
        DontDestroyOnLoad(gameObject); // 씬 변경 후 오브젝트 유지
    }

    public void CompleteLanguageSelection() // 언어 선택 완료 처리
    {
        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 관리자 누락 출력
            return; // 화면 이동 중단
        }

        PlayerPrefs.SetInt(LanguageSelectedKey, 1); // 선택 완료 상태 저장
        PlayerPrefs.Save(); // 변경 데이터 즉시 저장
        SceneLoader.Instance.LoadScene(SceneNames.MainMenu); // 메인 메뉴 이동
    }

    [ContextMenu("Test/Clear Language Selection")] // 테스트 메뉴 등록
    private void ClearLanguageSelectionForTest() // 언어 선택 기록 초기화
    {
        PlayerPrefs.DeleteKey(LanguageSelectedKey); // 선택 완료 키 삭제
        PlayerPrefs.Save(); // 삭제 결과 즉시 저장
        Debug.Log("언어 선택 테스트 기록을 삭제했습니다."); // 초기화 완료 출력
    }
}