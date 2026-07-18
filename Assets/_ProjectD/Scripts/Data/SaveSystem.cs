using System; // 예외 처리 기능
using System.IO; // 파일 입출력 기능
using UnityEngine; // Unity 기본 기능

public static class SaveSystem // JSON 저장 시스템
{
    private const string SaveFileName = "ProjectD_Save.json"; // 저장 파일 이름

    public static string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName); // 저장 파일 전체 경로
    public static bool HasSaveData => File.Exists(SaveFilePath); // 저장 파일 존재 상태

    public static bool Save(PlayerSaveData data) // 게임 데이터 저장
    {
        if (data == null) // 저장 데이터 존재 확인
        {
            Debug.LogError("저장할 PlayerSaveData가 없습니다."); // 데이터 누락 오류
            return false; // 저장 실패 반환
        }

        try // 파일 저장 예외 처리
        {
            Directory.CreateDirectory(Application.persistentDataPath); // 저장 폴더 생성
            string json = JsonUtility.ToJson(data, true); // 데이터를 JSON으로 변환
            File.WriteAllText(SaveFilePath, json); // JSON 파일 기록

            Debug.Log($"게임 저장 완료: {SaveFilePath}"); // 저장 완료 로그
            return true; // 저장 성공 반환
        }
        catch (Exception exception) // 저장 오류 처리
        {
            Debug.LogError($"게임 저장 실패: {exception.Message}"); // 저장 실패 로그
            return false; // 저장 실패 반환
        }
    }

    public static bool TryLoad(out PlayerSaveData data) // 게임 데이터 불러오기
    {
        data = null; // 출력 데이터 초기화

        if (!HasSaveData) // 저장 파일 존재 확인
        {
            Debug.LogWarning("불러올 저장 파일이 없습니다."); // 파일 누락 경고
            return false; // 불러오기 실패 반환
        }

        try // 파일 불러오기 예외 처리
        {
            string json = File.ReadAllText(SaveFilePath); // JSON 파일 읽기

            if (string.IsNullOrWhiteSpace(json)) // 빈 파일 확인
            {
                Debug.LogError("저장 파일이 비어 있습니다."); // 빈 파일 오류
                return false; // 불러오기 실패 반환
            }

            data = JsonUtility.FromJson<PlayerSaveData>(json); // JSON 데이터 변환

            if (data == null) // 변환 결과 확인
            {
                Debug.LogError("저장 데이터를 변환할 수 없습니다."); // 변환 실패 오류
                return false; // 불러오기 실패 반환
            }

            Debug.Log($"게임 불러오기 완료: {SaveFilePath}"); // 불러오기 완료 로그
            return true; // 불러오기 성공 반환
        }
        catch (Exception exception) // 불러오기 오류 처리
        {
            Debug.LogError($"게임 불러오기 실패: {exception.Message}"); // 불러오기 실패 로그
            data = null; // 실패 데이터 초기화
            return false; // 불러오기 실패 반환
        }
    }

    public static bool DeleteSaveData() // 저장 데이터 삭제
    {
        if (!HasSaveData) // 저장 파일 존재 확인
        {
            return true; // 삭제 완료 상태 반환
        }

        try // 파일 삭제 예외 처리
        {
            File.Delete(SaveFilePath); // 저장 파일 삭제
            Debug.Log("게임 저장 데이터가 삭제되었습니다."); // 삭제 완료 로그
            return true; // 삭제 성공 반환
        }
        catch (Exception exception) // 삭제 오류 처리
        {
            Debug.LogError($"저장 데이터 삭제 실패: {exception.Message}"); // 삭제 실패 로그
            return false; // 삭제 실패 반환
        }
    }
}