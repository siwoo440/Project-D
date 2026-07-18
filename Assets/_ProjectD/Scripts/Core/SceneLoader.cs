using System.Collections; // 코루틴 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.SceneManagement; // 씬 관리 기능

public class SceneLoader : MonoBehaviour // 비동기 씬 로딩 관리
{
    public static SceneLoader Instance { get; private set; } // 전역 접근 인스턴스

    private bool isLoading; // 현재 로딩 상태

    public bool IsLoading => isLoading; // 외부 확인용 로딩 상태

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

    public void LoadScene(string sceneName) // 외부 요청용 씬 로딩
    {
        if (isLoading) // 중복 로딩 확인
        {
            Debug.LogWarning($"이미 씬을 로딩하고 있습니다: {sceneName}"); // 중복 요청 경고
            return; // 중복 요청 중단
        }

        StartCoroutine(LoadSceneAsync(sceneName)); // 비동기 로딩 시작
    }

    public IEnumerator LoadSceneAsync(string sceneName) // 비동기 씬 로딩 처리
    {
        if (isLoading) // 다른 로딩 작업 확인
        {
            Debug.LogWarning($"이미 씬을 로딩하고 있습니다: {sceneName}"); // 중복 작업 경고
            yield break; // 코루틴 종료
        }

        if (string.IsNullOrWhiteSpace(sceneName)) // 빈 씬 이름 확인
        {
            Debug.LogError("불러올 씬 이름이 비어 있습니다."); // 잘못된 이름 출력
            yield break; // 코루틴 종료
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName)) // Scene List 등록 확인
        {
            Debug.LogError($"Scene List에서 씬을 찾을 수 없습니다: {sceneName}"); // 미등록 씬 출력
            yield break; // 코루틴 종료
        }

        isLoading = true; // 로딩 상태 활성화

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single); // 비동기 씬 로딩 생성

        if (loadOperation == null) // 로딩 작업 생성 확인
        {
            Debug.LogError($"씬 로딩 작업 생성에 실패했습니다: {sceneName}"); // 생성 실패 출력
            isLoading = false; // 로딩 상태 해제
            yield break; // 코루틴 종료
        }

        while (!loadOperation.isDone) // 로딩 완료 여부 확인
        {
            yield return null; // 다음 프레임까지 대기
        }

        isLoading = false; // 로딩 상태 해제
        Debug.Log($"씬 로딩 완료: {sceneName}"); // 로딩 완료 출력
    }
}