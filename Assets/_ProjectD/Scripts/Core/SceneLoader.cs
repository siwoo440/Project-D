using System.Collections; // 코루틴 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.SceneManagement; // 씬 관리 기능

public class SceneLoader : MonoBehaviour // 비동기 씬 로딩 관리
{
    public static SceneLoader Instance { get; private set; } // 전역 접근 인스턴스

    [SerializeField] private LoadingScreen loadingScreen; // 공통 로딩 화면 참조
    [SerializeField, Min(0.0f)] private float minimumLoadingDuration = 0.5f; // 최소 로딩 표시 시간

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

    public IEnumerator LoadSceneAsync(string sceneName) // 로딩 화면 포함 씬 이동
    {
        if (isLoading) // 중복 로딩 확인
        {
            Debug.LogWarning("이미 다른 씬을 불러오는 중입니다."); // 중복 요청 경고
            yield break; // 코루틴 중단
        }

        if (string.IsNullOrWhiteSpace(sceneName)) // 빈 씬 이름 확인
        {
            Debug.LogError("불러올 씬 이름이 비어 있습니다."); // 빈 이름 오류
            yield break; // 코루틴 중단
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName)) // Scene List 등록 확인
        {
            Debug.LogError($"Scene List에서 씬을 찾을 수 없습니다: {sceneName}"); // 미등록 씬 오류
            yield break; // 코루틴 중단
        }

        isLoading = true; // 로딩 상태 활성화
        
        if (UIManager.Instance != null) // UIManager 존재 확인
        {
            UIManager.Instance.ClosePopup(); // 활성 팝업 닫기
        }

        if (loadingScreen != null) // 로딩 화면 연결 확인
        {
            yield return loadingScreen.Show(); // 로딩 화면 페이드 인
        }

        float loadingStartTime = Time.realtimeSinceStartup; // 로딩 시작 시간 저장

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single); // 비동기 씬 로딩 요청

        if (loadOperation == null) // 로딩 요청 생성 확인
        {
            Debug.LogError($"씬 로딩 요청을 생성하지 못했습니다: {sceneName}"); // 요청 생성 오류

            if (loadingScreen != null) // 로딩 화면 연결 확인
            {
                yield return loadingScreen.Hide(); // 오류 후 로딩 화면 숨김
            }

            isLoading = false; // 로딩 상태 해제
            yield break; // 코루틴 중단
        }

        loadOperation.allowSceneActivation = false; // 자동 씬 활성화 대기

        while (loadOperation.progress < 0.9f) // 씬 데이터 로딩 중 반복
        {
            float normalizedProgress = Mathf.Clamp01(loadOperation.progress / 0.9f); // 표시용 진행률 계산

            if (loadingScreen != null) // 로딩 화면 연결 확인
            {
                loadingScreen.SetProgress(normalizedProgress); // 진행률 화면 반영
            }

            yield return null; // 다음 프레임 대기
        }

        if (loadingScreen != null) // 로딩 화면 연결 확인
        {
            loadingScreen.SetProgress(1.0f); // 진행률 100퍼센트 표시
        }

        float elapsedLoadingTime = Time.realtimeSinceStartup - loadingStartTime; // 실제 로딩 시간 계산

        if (elapsedLoadingTime < minimumLoadingDuration) // 최소 표시 시간 확인
        {
            float remainingTime = minimumLoadingDuration - elapsedLoadingTime; // 남은 대기 시간 계산
            yield return new WaitForSecondsRealtime(remainingTime); // 최소 표시 시간 대기
        }

        loadOperation.allowSceneActivation = true; // 새로운 씬 활성화 허용

        while (!loadOperation.isDone) // 씬 활성화 완료 전 반복
        {
            yield return null; // 다음 프레임 대기
        }

        if (loadingScreen != null) // 로딩 화면 연결 확인
        {
            yield return loadingScreen.Hide(); // 새 씬 위에서 페이드 아웃
        }

        isLoading = false; // 로딩 상태 해제
        Debug.Log($"씬 로딩 완료: {sceneName}"); // 로딩 완료 기록
    }
}