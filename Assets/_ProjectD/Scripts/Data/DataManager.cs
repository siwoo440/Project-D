using UnityEngine; // Unity 기본 기능

public class DataManager : MonoBehaviour // 현재 게임 데이터 관리
{
    public static DataManager Instance { get; private set; } // 전역 데이터 관리자

    public PlayerSaveData CurrentData { get; private set; } // 현재 플레이 데이터
    public bool HasLoadedData => CurrentData != null; // 데이터 로드 상태
    public int Credits => CurrentData != null ? CurrentData.credits : 0; // 현재 크레딧 값

    private void Awake() // 데이터 관리자 초기화
    {
        if (Instance != null && Instance != this) // 중복 관리자 확인
        {
            Destroy(gameObject); // 중복 오브젝트 제거
            return; // 초기화 중단
        }

        Instance = this; // 전역 인스턴스 등록
        DontDestroyOnLoad(gameObject); // 씬 전환 유지

        Debug.Log("DataManager 생성 완료"); // 관리자 생성 확인
    }

    private void OnDestroy() // 관리자 제거 처리
    {
        if (Instance == this) // 현재 인스턴스 확인
        {
            Instance = null; // 전역 인스턴스 해제
        }
    }

    public bool StartNewGame() // 새 게임 데이터 생성
    {
        CurrentData = new PlayerSaveData(); // 기본 데이터 생성

        if (SaveGame()) // 초기 데이터 저장
        {
            return true; // 새 게임 성공 반환
        }

        CurrentData = null; // 저장 실패 데이터 제거
        return false; // 새 게임 실패 반환
    }

    public bool LoadGame() // 저장 게임 불러오기
    {
        if (!SaveSystem.TryLoad(out PlayerSaveData loadedData)) // 저장 파일 불러오기
        {
            CurrentData = null; // 현재 데이터 초기화
            return false; // 불러오기 실패 반환
        }

        CurrentData = loadedData; // 불러온 데이터 등록
        return true; // 불러오기 성공 반환
    }

    public bool SaveGame() // 현재 게임 저장
    {
        if (CurrentData == null) // 현재 데이터 존재 확인
        {
            Debug.LogError("저장할 현재 게임 데이터가 없습니다."); // 데이터 누락 오류
            return false; // 저장 실패 반환
        }

        return SaveSystem.Save(CurrentData); // 현재 데이터 파일 저장
    }

    public bool AddCredits(int amount) // 크레딧 추가
    {
        if (CurrentData == null) // 현재 데이터 존재 확인
        {
            Debug.LogError("크레딧을 추가할 게임 데이터가 없습니다."); // 데이터 누락 오류
            return false; // 추가 실패 반환
        }

        if (amount <= 0) // 추가 값 유효성 확인
        {
            Debug.LogWarning("추가할 크레딧은 1 이상이어야 합니다."); // 잘못된 값 경고
            return false; // 추가 실패 반환
        }

        CurrentData.credits += amount; // 크레딧 값 증가

        if (SaveGame()) // 변경 데이터 저장
        {
            return true; // 추가 성공 반환
        }

        CurrentData.credits -= amount; // 저장 실패 값 복구
        return false; // 추가 실패 반환
    }

    public void ClearLoadedData() // 메모리 데이터 초기화
    {
        CurrentData = null; // 현재 데이터 제거
    }

#if UNITY_EDITOR
    [ContextMenu("테스트/크레딧 100 추가")] // 테스트 메뉴 등록
    private void AddCreditsForTest() // 테스트 크레딧 추가
    {
        AddCredits(100); // 크레딧 100 추가
    }

    [ContextMenu("테스트/저장 경로 출력")] // 경로 확인 메뉴 등록
    private void LogSavePathForTest() // 저장 경로 출력
    {
        Debug.Log($"저장 파일 경로: {SaveSystem.SaveFilePath}"); // 저장 경로 로그
    }
#endif
}