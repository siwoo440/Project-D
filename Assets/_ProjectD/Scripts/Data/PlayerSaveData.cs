using System; // 직렬화 속성 기능

[Serializable] // JSON 직렬화 허용
public class PlayerSaveData // 플레이어 저장 데이터
{
    public int saveVersion = 1; // 저장 형식 버전
    public int credits = 0; // 보유 크레딧
}