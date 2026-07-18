using System.Collections; // 코루틴 기능
using TMPro; // TextMeshPro 기능
using UnityEngine; // Unity 기본 기능
using UnityEngine.EventSystems; // UI 선택 관리
using UnityEngine.UI; // UI 이미지 기능

[RequireComponent(typeof(CanvasGroup))] // CanvasGroup 필수 구성
public class LoadingScreen : MonoBehaviour // 공통 로딩 화면 관리
{
    [SerializeField] private Image progressFillImage; // 진행률 채움 이미지
    [SerializeField] private TMP_Text progressText; // 진행률 표시 텍스트
    [SerializeField, Min(0.0f)] private float fadeDuration = 0.25f; // 페이드 소요 시간

    private CanvasGroup canvasGroup; // 화면 투명도와 입력 관리

    private void Awake() // 초기 로딩 화면 설정
    {
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup 참조 저장
        canvasGroup.alpha = 0.0f; // 초기 화면 숨김
        canvasGroup.interactable = false; // 로딩 UI 조작 차단
        canvasGroup.blocksRaycasts = false; // 초기 입력 차단 해제
        SetProgress(0.0f); // 초기 진행률 설정
    }

    public IEnumerator Show() // 로딩 화면 표시
    {
        SetProgress(0.0f); // 진행률 초기화
        canvasGroup.interactable = false; // 로딩 UI 조작 차단
        canvasGroup.blocksRaycasts = true; // 아래쪽 UI 입력 차단

        if (EventSystem.current != null) // EventSystem 존재 확인
        {
            EventSystem.current.SetSelectedGameObject(null); // 현재 선택 UI 해제
        }

        yield return FadeCanvas(1.0f); // 화면 페이드 인
    }

    public IEnumerator Hide() // 로딩 화면 숨김
    {
        yield return FadeCanvas(0.0f); // 화면 페이드 아웃

        canvasGroup.interactable = false; // 로딩 UI 조작 차단
        canvasGroup.blocksRaycasts = false; // 아래쪽 UI 입력 허용
        SetProgress(0.0f); // 다음 로딩용 초기화
    }

    public void SetProgress(float progress) // 진행률 화면 반영
    {
        float clampedProgress = Mathf.Clamp01(progress); // 진행률 범위 제한

        if (progressFillImage != null) // 채움 이미지 연결 확인
        {
            progressFillImage.fillAmount = clampedProgress; // 이미지 채움 비율 적용
        }

        if (progressText != null) // 진행률 텍스트 연결 확인
        {
            int progressPercent = Mathf.RoundToInt(clampedProgress * 100.0f); // 백분율 정수 변환
            progressText.text = $"{progressPercent}%"; // 백분율 텍스트 표시
        }
    }

    private IEnumerator FadeCanvas(float targetAlpha) // CanvasGroup 페이드 처리
    {
        float startAlpha = canvasGroup.alpha; // 현재 투명도 저장

        if (fadeDuration <= 0.0f) // 즉시 전환 조건 확인
        {
            canvasGroup.alpha = targetAlpha; // 목표 투명도 즉시 적용
            yield break; // 코루틴 종료
        }

        float elapsedTime = 0.0f; // 경과 시간 초기화

        while (elapsedTime < fadeDuration) // 페이드 시간 동안 반복
        {
            elapsedTime += Time.unscaledDeltaTime; // 시간 배율 무관 경과 시간
            float fadeProgress = Mathf.Clamp01(elapsedTime / fadeDuration); // 페이드 진행률 계산
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, fadeProgress); // 투명도 보간
            yield return null; // 다음 프레임 대기
        }

        canvasGroup.alpha = targetAlpha; // 최종 투명도 보정
    }
}