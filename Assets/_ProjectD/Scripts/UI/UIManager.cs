using TMPro; // TextMeshPro 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.EventSystems; // UI 선택 관리
using UnityEngine.Events; // 확인 동작 콜백 기능
using UnityEngine.InputSystem; // 새 입력 시스템 기능

public class UIManager : MonoBehaviour // 공통 UI 상태 관리
{
    public static UIManager Instance { get; private set; } // 전역 접근 인스턴스

    [SerializeField] private CanvasGroup popupCanvasGroup; // 공통 팝업 그룹
    [SerializeField] private TMP_Text popupTitleText; // 팝업 제목 텍스트
    [SerializeField] private TMP_Text popupMessageText; // 팝업 내용 텍스트
    [SerializeField] private GameObject popupCloseButtonObject; // 일반 닫기 버튼 오브젝트
    [SerializeField] private GameObject popupConfirmButtonObject; // 확인 버튼 오브젝트
    [SerializeField] private GameObject popupCancelButtonObject; // 취소 버튼 오브젝트

    private bool isPopupOpen; // 팝업 활성화 상태
    private UnityAction pendingConfirmAction; // 대기 중인 확인 동작
    public bool IsPopupOpen => isPopupOpen; // 외부 확인용 팝업 상태

    public bool GameplayBlocked // 전체 게임 입력 차단 여부
    {
        get // 현재 차단 상태 반환
        {
            bool isSceneLoading = SceneLoader.Instance != null && SceneLoader.Instance.IsLoading; // 씬 로딩 상태
            return isPopupOpen || isSceneLoading; // 통합 입력 차단 상태
        }
    }

    private void Awake() // 싱글턴 생성과 초기 상태 설정
    {
        if (Instance != null && Instance != this) // 기존 인스턴스 확인
        {
            Destroy(gameObject); // 중복 오브젝트 제거
            return; // 초기화 중단
        }

        Instance = this; // 현재 인스턴스 등록
        DontDestroyOnLoad(gameObject); // 씬 전환 후 유지
        ApplyPopupState(false); // 팝업 초기 숨김
        SetPopupButtonMode(false); // 일반 팝업 버튼 상태 적용
    }

    private void Update() // ESC 입력 확인
    {
        if (Keyboard.current == null) // 키보드 존재 확인
        {
            return; // 입력 확인 중단
        }

        if (!Keyboard.current.escapeKey.wasPressedThisFrame) // ESC 입력 확인
        {
            return; // ESC 미입력 처리
        }

        if (SceneLoader.Instance != null && SceneLoader.Instance.IsLoading) // 씬 로딩 상태 확인
        {
            return; // 로딩 중 ESC 처리 차단
        }

        if (isPopupOpen) // 팝업 열림 확인
        {
            ClosePopup(); // 현재 팝업 닫기
        }
    }

    public void ShowPopup(string title, string message) // 일반 공통 팝업 열기
    {
        if (SceneLoader.Instance != null && SceneLoader.Instance.IsLoading) // 씬 로딩 상태 확인
        {
            Debug.LogWarning("씬 로딩 중에는 팝업을 열 수 없습니다."); // 팝업 요청 경고
            return; // 팝업 열기 중단
        }

        if (popupCanvasGroup == null || popupTitleText == null || popupMessageText == null) // UI 참조 확인
        {
            Debug.LogError("UIManager의 팝업 참조가 연결되지 않았습니다."); // 참조 오류 출력
            return; // 팝업 열기 중단
        }

        pendingConfirmAction = null; // 기존 확인 동작 제거
        popupTitleText.text = title; // 팝업 제목 적용
        popupMessageText.text = message; // 팝업 내용 적용
        SetPopupButtonMode(false); // 일반 닫기 버튼 표시

        if (EventSystem.current != null) // EventSystem 존재 확인
        {
            EventSystem.current.SetSelectedGameObject(null); // 기존 UI 선택 해제
        }

        ApplyPopupState(true); // 팝업 활성화
        Debug.Log("일반 공통 팝업을 열었습니다."); // 팝업 열림 기록
    }
    public void ShowConfirmation(string title, string message, UnityAction confirmAction) // 확인 공통 팝업 열기
    {
        if (SceneLoader.Instance != null && SceneLoader.Instance.IsLoading) // 씬 로딩 상태 확인
        {
            Debug.LogWarning("씬 로딩 중에는 확인 팝업을 열 수 없습니다."); // 팝업 요청 경고
            return; // 팝업 열기 중단
        }

        if (confirmAction == null) // 확인 동작 존재 확인
        {
            Debug.LogError("확인 팝업에 실행할 동작이 없습니다."); // 확인 동작 오류
            return; // 팝업 열기 중단
        }

        if (popupCanvasGroup == null || popupTitleText == null || popupMessageText == null) // UI 참조 확인
        {
            Debug.LogError("UIManager의 팝업 참조가 연결되지 않았습니다."); // 참조 오류 출력
            return; // 팝업 열기 중단
        }

        pendingConfirmAction = confirmAction; // 확인 동작 저장
        popupTitleText.text = title; // 확인 팝업 제목 적용
        popupMessageText.text = message; // 확인 팝업 내용 적용
        SetPopupButtonMode(true); // 확인과 취소 버튼 표시

        if (EventSystem.current != null) // EventSystem 존재 확인
        {
            EventSystem.current.SetSelectedGameObject(null); // 기존 UI 선택 해제
        }

        ApplyPopupState(true); // 팝업 활성화
        Debug.Log("확인 공통 팝업을 열었습니다."); // 팝업 열림 기록
    }
    public void ClosePopup() // 공통 팝업 닫기
    {
        if (!isPopupOpen) // 팝업 닫힘 상태 확인
        {
            return; // 중복 닫기 중단
        }

        pendingConfirmAction = null; // 대기 확인 동작 제거
        ApplyPopupState(false); // 팝업 비활성화
        Debug.Log("공통 팝업을 닫았습니다."); // 팝업 닫힘 기록
    }
    public void ConfirmPopup() // 확인 팝업 동작 실행
    {
        if (!isPopupOpen || pendingConfirmAction == null) // 팝업과 확인 동작 확인
        {
            return; // 확인 처리 중단
        }

        UnityAction confirmAction = pendingConfirmAction; // 실행할 동작 임시 저장

        pendingConfirmAction = null; // 대기 확인 동작 제거
        ApplyPopupState(false); // 팝업 비활성화
        Debug.Log("확인 팝업 동작을 실행합니다."); // 확인 실행 기록
        confirmAction.Invoke(); // 저장된 확인 동작 실행
    }


    private void SetPopupButtonMode(bool useConfirmationButtons) // 팝업 버튼 구성 변경
    {
        if (popupCloseButtonObject != null) // 닫기 버튼 참조 확인
        {
            popupCloseButtonObject.SetActive(!useConfirmationButtons); // 일반 팝업에서만 표시
        }

        if (popupConfirmButtonObject != null) // 확인 버튼 참조 확인
        {
            popupConfirmButtonObject.SetActive(useConfirmationButtons); // 확인 팝업에서만 표시
        }

        if (popupCancelButtonObject != null) // 취소 버튼 참조 확인
        {
            popupCancelButtonObject.SetActive(useConfirmationButtons); // 확인 팝업에서만 표시
        }
    }

    private void ApplyPopupState(bool shouldOpen) // 팝업 표시 상태 적용
    {
        isPopupOpen = shouldOpen; // 팝업 상태 저장

        if (popupCanvasGroup == null) // CanvasGroup 참조 확인
        {
            return; // 화면 적용 중단
        }

        popupCanvasGroup.alpha = shouldOpen ? 1.0f : 0.0f; // 팝업 투명도 적용
        popupCanvasGroup.interactable = shouldOpen; // 팝업 조작 상태 적용
        popupCanvasGroup.blocksRaycasts = shouldOpen; // 배경 입력 차단 적용
    }
}