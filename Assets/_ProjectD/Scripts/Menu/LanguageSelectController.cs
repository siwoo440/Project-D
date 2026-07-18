using UnityEngine; // Unity 기본 기능

public class LanguageSelectController : MonoBehaviour // 언어 선택 화면 관리
{
    public void ConfirmTemporarySelection() // 임시 언어 선택 완료
    {
        if (GameManager.Instance == null) // 게임 관리자 존재 확인
        {
            Debug.LogError("GameManager를 찾을 수 없습니다."); // 관리자 누락 출력
            return; // 선택 처리 중단
        }

        GameManager.Instance.CompleteLanguageSelection(); // 선택 완료 상태 저장
    }
}