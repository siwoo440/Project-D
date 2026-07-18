using System.Collections; // 코루틴 기능
using UnityEngine; // Unity 기본 기능

public class GameBootstrap : MonoBehaviour // 게임 시작 흐름 관리
{
    [SerializeField, Min(0.0f)] private float splashDuration = 2.0f; // 로고 표시 시간

    private void Awake() // 초기화 오브젝트 유지
    {
        DontDestroyOnLoad(gameObject); // 시작 흐름 오브젝트 유지
    }

    private IEnumerator Start() // 초기 화면 흐름 실행
    {
        if (GameManager.Instance == null) // 게임 관리자 존재 확인
        {
            Debug.LogError("GameManager를 찾을 수 없습니다."); // 관리자 누락 출력
            yield break; // 시작 흐름 중단
        }

        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 로더 누락 출력
            yield break; // 시작 흐름 중단
        }

        if (LanguageManager.Instance == null) // 언어 관리자 존재 확인
        {
            Debug.LogError("LanguageManager를 찾을 수 없습니다."); // 언어 관리자 오류
            yield break; // 초기화 중단
        }

        yield return LanguageManager.Instance.InitializeAsync(); // Localization 초기화 대기
        yield return SceneLoader.Instance.LoadSceneAsync(SceneNames.Splash); // 스플래시 씬 로딩

        yield return new WaitForSecondsRealtime(splashDuration); // 로고 표시 시간 대기

        string nextSceneName = GameManager.Instance.HasSelectedLanguage // 언어 선택 기록 확인
            ? SceneNames.MainMenu // 선택 완료 시 메인 메뉴
            : SceneNames.LanguageSelect; // 미선택 시 언어 선택

        yield return SceneLoader.Instance.LoadSceneAsync(nextSceneName); // 다음 화면 비동기 로딩

        Destroy(gameObject); // 시작 흐름 오브젝트 제거
    }
}