using UnityEngine; // Unity 기본 기능

public class LanguageSelectController : MonoBehaviour // 언어 선택 화면 제어
{
    public void SelectKorean() // 한국어 선택 처리
    {
        SelectLanguage("ko-KR"); // 한국어 Locale 요청
    }

    public void SelectEnglish() // 영어 선택 처리
    {
        SelectLanguage("en"); // 영어 Locale 요청
    }

    private void SelectLanguage(string languageCode) // 공통 언어 선택 요청
    {
        if (LanguageManager.Instance == null) // 언어 관리자 존재 확인
        {
            Debug.LogError("LanguageManager를 찾을 수 없습니다."); // 언어 관리자 오류
            return; // 선택 처리 중단
        }

        LanguageManager.Instance.SelectLanguage(languageCode); // 선택 언어 적용 요청
    }
}