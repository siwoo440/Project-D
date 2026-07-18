using UnityEngine; // Unity 기본 기능

public class PopupTestController : MonoBehaviour // 공통 팝업 테스트 제어
{
    public void OpenTestPopup() // 테스트 팝업 열기
    {
        if (UIManager.Instance == null) // UIManager 존재 확인
        {
            Debug.LogError("UIManager를 찾을 수 없습니다."); // UIManager 오류
            return; // 팝업 요청 중단
        }

        UIManager.Instance.ShowPopup("NOTICE", "COMMON POPUP TEST"); // 테스트 팝업 표시
    }
}