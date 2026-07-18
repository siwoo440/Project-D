using UnityEngine; // Unity 기본 기능

public class ReturnToLobbyController : MonoBehaviour // 로비 복귀 제어
{
    public void ReturnToLobby() // 로비 복귀 실행
    {
        if (SceneLoader.Instance == null) // 씬 로더 존재 확인
        {
            Debug.LogError("SceneLoader를 찾을 수 없습니다."); // 로더 누락 오류
            return; // 복귀 요청 중단
        }

        if (SceneLoader.Instance.IsLoading) // 로딩 상태 확인
        {
            return; // 중복 이동 방지
        }

        SceneLoader.Instance.LoadScene(SceneNames.Lobby); // 로비 씬 이동 요청
    }
}