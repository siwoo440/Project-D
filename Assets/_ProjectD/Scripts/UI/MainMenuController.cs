using UnityEngine; // Unity 기본 기능
using UnityEngine.Localization; // 현지화 문자열 기능
using UnityEngine.UI; // UI 버튼 기능

public class MainMenuController : MonoBehaviour // 메인 메뉴 화면 제어
{
    [Header("메인 메뉴 버튼")] // 버튼 설정 구분
    [SerializeField] private Button continueButton; // 이어하기 버튼

    [Header("저장 오류 팝업")] // 저장 오류 설정 구분
    [SerializeField] private LocalizedString saveErrorTitle; // 저장 오류 제목
    [SerializeField] private LocalizedString saveErrorMessage; // 저장 오류 설명

    private void Start() // 메인 메뉴 초기화
    {
        RefreshContinueButton(); // 이어하기 버튼 상태 갱신
    }

    public void RequestNewGame() // 새 게임 요청
    {
        if (!SaveSystem.HasSaveData) // 기존 저장 파일 확인
        {
            StartNewGame(); // 새 게임 즉시 시작
            return; // 요청 처리 종료
        }

        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 팝업 요청 중단
        }

        UIManager.Instance.ShowConfirmation( // 새 게임 확인 팝업
            "NEW GAME", // 확인 팝업 제목
            "Existing save data will be overwritten. Start a new game?", // 확인 팝업 설명
            StartNewGame); // 확인 시 실행 메서드
    }

    public void ContinueGame() // 저장 게임 이어하기
    {
        if (!SaveSystem.HasSaveData) // 저장 파일 존재 확인
        {
            RefreshContinueButton(); // 버튼 상태 갱신
            return; // 이어하기 중단
        }

        if (DataManager.Instance == null) // 데이터 관리자 존재 확인
        {
            Debug.LogError("DataManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 이어하기 중단
        }

        if (!DataManager.Instance.LoadGame()) // 저장 파일 불러오기
        {
            ShowSaveErrorPopup(); // 불러오기 오류 팝업
            return; // 이어하기 중단
        }

        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 로더 누락 오류
            return; // 씬 이동 중단
        }

        SceneLoader.Instance.LoadScene(SceneNames.Lobby); // 로비 씬 이동
    }

    public void OpenSettings() // 설정 팝업 열기
    {
        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 팝업 요청 중단
        }

        UIManager.Instance.ShowPopup( // 설정 안내 팝업
            "SETTINGS", // 설정 팝업 제목
            "Settings will be implemented later."); // 설정 팝업 설명
    }

    public void OpenCredits() // 제작진 팝업 열기
    {
        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 팝업 요청 중단
        }

        UIManager.Instance.ShowPopup( // 제작진 정보 팝업
            "CREDITS", // 제작진 팝업 제목
            "PROJECT D\nDEVELOPED BY SIWOO"); // 제작진 팝업 설명
    }

    public void RequestExit() // 게임 종료 요청
    {
        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 종료 요청 중단
        }

        UIManager.Instance.ShowConfirmation( // 게임 종료 확인 팝업
            "EXIT", // 종료 팝업 제목
            "Exit the game?", // 종료 팝업 설명
            QuitGame); // 확인 시 종료 실행
    }

    private void StartNewGame() // 새 게임 시작
    {
        if (DataManager.Instance == null) // 데이터 관리자 존재 확인
        {
            Debug.LogError("DataManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 새 게임 중단
        }

        if (!DataManager.Instance.StartNewGame()) // 기본 데이터 생성 및 저장
        {
            ShowSaveErrorPopup(); // 저장 오류 팝업
            return; // 새 게임 중단
        }

        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 로더 누락 오류
            return; // 씬 이동 중단
        }

        SceneLoader.Instance.LoadScene(SceneNames.Lobby); // 로비 씬 이동
    }

    private void RefreshContinueButton() // 이어하기 버튼 상태 갱신
    {
        if (continueButton == null) // 버튼 연결 확인
        {
            Debug.LogError("ContinueButton이 연결되지 않았습니다."); // 버튼 누락 오류
            return; // 상태 갱신 중단
        }

        continueButton.interactable = SaveSystem.HasSaveData; // 저장 파일 기준 활성화
    }

    private void ShowSaveErrorPopup() // 저장 오류 팝업 표시
    {
        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 팝업 요청 중단
        }

        string title = saveErrorTitle.GetLocalizedString(); // 현지화 오류 제목
        string message = saveErrorMessage.GetLocalizedString(); // 현지화 오류 설명

        UIManager.Instance.ShowPopup(title, message); // 공통 오류 팝업 표시
    }

    private void QuitGame() // 게임 종료 실행
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 실행 종료
#else
        Application.Quit(); // 빌드된 게임 종료
#endif
    }

#if UNITY_EDITOR
    [ContextMenu("테스트/저장 데이터 삭제")] // 테스트 삭제 메뉴 등록
    private void DeleteSaveDataForTest() // 테스트 저장 데이터 삭제
    {
        if (!SaveSystem.DeleteSaveData()) // 저장 파일 삭제
        {
            ShowSaveErrorPopup(); // 삭제 오류 팝업
            return; // 삭제 작업 중단
        }

        if (DataManager.Instance != null) // 데이터 관리자 존재 확인
        {
            DataManager.Instance.ClearLoadedData(); // 메모리 데이터 초기화
        }

        RefreshContinueButton(); // 이어하기 버튼 상태 갱신
    }
#endif
}