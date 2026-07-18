using System.Collections; // 코루틴 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.InputSystem; // 새 입력 시스템 기능


public class GameBootstrap : MonoBehaviour // 게임 시작 흐름 관리
{
    [SerializeField, Min(0.0f)] private float minimumSplashDuration = 1.0f; // 최소 스플래시 표시 시간
    [SerializeField, Min(0.0f)] private float maximumSplashDuration = 3.0f; // 최대 스플래시 표시 시간

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

        yield return WaitForSplashInput(); // 스플래시 표시와 스킵 입력 대기

        string nextSceneName = GameManager.Instance.HasSelectedLanguage // 언어 선택 기록 확인
            ? SceneNames.MainMenu // 선택 완료 시 메인 메뉴
            : SceneNames.LanguageSelect; // 미선택 시 언어 선택

        yield return SceneLoader.Instance.LoadSceneAsync(nextSceneName); // 다음 화면 비동기 로딩

        Destroy(gameObject); // 시작 흐름 오브젝트 제거
    }

    private IEnumerator WaitForSplashInput() // 스플래시 시간과 스킵 입력 처리
    {
        float elapsedTime = 0.0f; // 경과 시간 초기화
        float safeMaximumDuration = Mathf.Max(maximumSplashDuration, minimumSplashDuration); // 최대 시간 보정

        while (elapsedTime < safeMaximumDuration) // 최대 표시 시간까지 반복
        {
            elapsedTime += Time.unscaledDeltaTime; // 시간 배율 무관 경과 시간

            bool keyboardPressed = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame; // 키보드 입력 확인
            bool mousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame; // 마우스 왼쪽 입력 확인
            bool canSkip = elapsedTime >= minimumSplashDuration; // 최소 표시 시간 확인
            bool skipRequested = keyboardPressed || mousePressed; // 전체 스킵 입력 확인

            if (canSkip && skipRequested) // 스킵 가능 상태와 입력 확인
            {
                Debug.Log("Splash 건너뛰기 입력을 확인했습니다."); // 스킵 입력 기록
                yield break; // 스플래시 대기 종료
            }

            yield return null; // 다음 프레임 대기
        }
    }

}