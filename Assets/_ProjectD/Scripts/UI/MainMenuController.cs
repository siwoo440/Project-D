using UnityEngine; // Unity 기본 기능
using UnityEngine.UI; // UI 버튼 기능

public class MainMenuController : MonoBehaviour // 메인 메뉴 기능 관리
{
    private const string HasSaveDataKey = "HasSaveData"; // 임시 저장 데이터 확인 키

    [SerializeField] private Button continueButton; // 이어하기 버튼 참조

    private bool HasSaveData => PlayerPrefs.GetInt(HasSaveDataKey, 0) == 1; // 저장 데이터 존재 여부

    private void Start() // 메인 메뉴 초기 상태 설정
    {
        RefreshContinueButton(); // 이어하기 버튼 상태 갱신
    }

    public void RequestNewGame() // 새 게임 요청 처리
    {
        if (HasSaveData) // 기존 저장 데이터 확인
        {
            if (UIManager.Instance == null) // UIManager 존재 확인
            {
                Debug.LogError("UIManager를 찾을 수 없습니다."); // UIManager 오류
                return; // 새 게임 처리 중단
            }

            UIManager.Instance.ShowConfirmation("NEW GAME", "START A NEW GAME AND OVERWRITE CURRENT PROGRESS?", StartNewGame); // 덮어쓰기 확인 팝업
            return; // 확인 입력 대기
        }

        StartNewGame(); // 새 게임 즉시 시작
    }

    public void ContinueGame() // 이어하기 처리
    {
        if (!HasSaveData) // 저장 데이터 존재 확인
        {
            if (UIManager.Instance != null) // UIManager 존재 확인
            {
                UIManager.Instance.ShowPopup("CONTINUE", "NO SAVE DATA WAS FOUND."); // 저장 데이터 없음 안내
            }

            return; // 이어하기 중단
        }

        if (SceneLoader.Instance == null) // SceneLoader 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // SceneLoader 오류
            return; // 씬 이동 중단
        }

        SceneLoader.Instance.LoadScene(SceneNames.Lobby); // 로비 씬 이동
    }

    public void OpenSettings() // 설정 팝업 열기
    {
        if (UIManager.Instance == null) // UIManager 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // UIManager 오류
            return; // 설정 처리 중단
        }

        UIManager.Instance.ShowPopup("SETTINGS", "SETTINGS WILL BE IMPLEMENTED LATER."); // 임시 설정 안내
    }

    public void OpenCredits() // 크레딧 팝업 열기
    {
        if (UIManager.Instance == null) // UIManager 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // UIManager 오류
            return; // 크레딧 처리 중단
        }

        UIManager.Instance.ShowPopup("CREDITS", "PROJECT D\nDEVELOPED BY SIWOO"); // 임시 크레딧 표시
    }

    public void RequestExit() // 게임 종료 요청
    {
        if (UIManager.Instance == null) // UIManager 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // UIManager 오류
            return; // 종료 처리 중단
        }

        UIManager.Instance.ShowConfirmation("EXIT", "EXIT THE GAME?", QuitGame); // 게임 종료 확인 팝업
    }

    private void StartNewGame() // 새 게임 시작
    {
        if (SceneLoader.Instance == null) // SceneLoader 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // SceneLoader 오류
            return; // 새 게임 처리 중단
        }

        PlayerPrefs.SetInt(HasSaveDataKey, 1); // 임시 저장 데이터 생성
        PlayerPrefs.Save(); // 저장 내용 즉시 반영
        SceneLoader.Instance.LoadScene(SceneNames.Lobby); // 로비 씬 이동
    }

    private void QuitGame() // 게임 종료
    {
        Debug.Log("게임 종료를 요청했습니다."); // 종료 요청 기록

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 Play Mode 종료
#else
        Application.Quit(); // 실행 파일 종료
#endif
    }

    private void RefreshContinueButton() // 이어하기 버튼 상태 갱신
    {
        if (continueButton == null) // 이어하기 버튼 참조 확인
        {
            Debug.LogError("이어하기 버튼이 연결되지 않았습니다."); // 버튼 참조 오류
            return; // 버튼 갱신 중단
        }

        continueButton.interactable = HasSaveData; // 저장 데이터에 따른 활성화
    }

    [ContextMenu("Test/Clear Save Data")] // 테스트 초기화 메뉴 등록
    private void ClearSaveDataForTest() // 임시 저장 데이터 삭제
    {
        PlayerPrefs.DeleteKey(HasSaveDataKey); // 임시 저장 데이터 키 삭제
        PlayerPrefs.Save(); // 삭제 내용 즉시 반영
        RefreshContinueButton(); // 이어하기 버튼 상태 갱신
        Debug.Log("임시 저장 데이터를 삭제했습니다."); // 초기화 완료 기록
    }
}
