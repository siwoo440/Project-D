using TMPro; // TMP 텍스트 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.Localization; // 현지화 문자열 기능

public class LobbyController : MonoBehaviour // 로비 화면 제어
{
    [Header("재화 표시")] // 재화 설정 구분
    [SerializeField] private TextMeshProUGUI currencyValueText; // 재화 값 텍스트
    [SerializeField] private int temporaryCurrencyAmount; // 임시 재화 값

    [Header("메인 메뉴 복귀 팝업")] // 복귀 팝업 설정 구분
    [SerializeField] private LocalizedString returnTitle; // 현지화 팝업 제목
    [SerializeField] private LocalizedString returnMessage; // 현지화 팝업 설명

    private void Start() // 로비 초기 표시
    {
        RefreshCurrencyDisplay(); // 재화 표시 갱신
    }

    public void OpenCommandHQ() // 지휘 본부 이동
    {
        OpenScene(SceneNames.CommandHQ); // 지휘 본부 씬 요청
    }

    public void OpenFormation() // 편성 화면 이동
    {
        OpenScene(SceneNames.Formation); // 편성 씬 요청
    }

    public void OpenStageSelect() // 스테이지 선택 이동
    {
        OpenScene(SceneNames.StageSelect); // 스테이지 선택 씬 요청
    }

    public void RequestReturnToMainMenu() // 메인 메뉴 복귀 확인
    {
        if (UIManager.Instance == null) // UI 관리자 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // 관리자 누락 오류
            return; // 복귀 요청 중단
        }

        string title = returnTitle.GetLocalizedString(); // 현지화 제목 조회
        string message = returnMessage.GetLocalizedString(); // 현지화 설명 조회

        UIManager.Instance.ShowConfirmation(title, message, ReturnToMainMenu); // 복귀 확인 팝업 표시
    }

    private void ReturnToMainMenu() // 메인 메뉴 복귀 실행
    {
        OpenScene(SceneNames.MainMenu); // 메인 메뉴 씬 요청
    }

    private void RefreshCurrencyDisplay() // 재화 표시 갱신
    {
        if (currencyValueText == null) // 텍스트 연결 확인
        {
            Debug.LogError("CurrencyValueText가 연결되지 않았습니다."); // 텍스트 누락 오류
            return; // 표시 갱신 중단
        }

        currencyValueText.text = temporaryCurrencyAmount.ToString(); // 임시 재화 값 표시
    }

    private void OpenScene(string sceneName) // 공통 씬 이동
    {
        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 로더 누락 오류
            return; // 씬 이동 중단
        }

        if (SceneLoader.Instance.IsLoading) // 로딩 상태 확인
        {
            return; // 중복 이동 방지
        }

        SceneLoader.Instance.LoadScene(sceneName); // 비동기 씬 이동 요청
    }
}