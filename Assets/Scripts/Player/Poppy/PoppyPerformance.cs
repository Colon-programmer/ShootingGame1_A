using UnityEngine;

public class PoppyPerformance : PlayerController
{
    public override void CharacterDefaultSetting()
    {
        playerHighSpeed = 5.5f;
    }

    public override void SubShooterSetting()
    {
        // サブショットの発射口を生成
        subshooterObj[0] = Instantiate(subshooter, new Vector2(-0.5f,-0.5f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[1] = Instantiate(subshooter, new Vector2(0.5f, 0.5f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[2] = Instantiate(subshooter, new Vector2(-1.0f, 0.0f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[3] = Instantiate(subshooter, new Vector2(1.0f, 0.0f),
            Quaternion.identity, subShooterCenter.transform);
    }
}
