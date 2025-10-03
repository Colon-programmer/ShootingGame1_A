using UnityEngine;

public class MisyaPerformance : PlayerController
{
    public override void CharacterDefaultSetting()
    {
        playerHighSpeed = 5.0f;
    }

    public override void SubShooterSetting()
    {
        // サブショットの位置を設定
        subshooterLayout[0, 0] = new Vector2(-1.1f, 0.0f);
        subshooterLayout[0, 1] = new Vector2(0.0f, 1.0f);

        subshooterLayout[1, 0] = new Vector2(1.1f, 0.0f);
        subshooterLayout[1, 1] = new Vector2(1.1f, 0.0f);

        subshooterLayout[2, 0] = new Vector2(-2.2f, 0.0f);
        subshooterLayout[2, 1] = new Vector2(-1.1f, 0.0f);

        subshooterLayout[3, 0] = new Vector2(2.2f, 0.0f);
        subshooterLayout[3, 1] = new Vector2(0.0f, -1.0f);
        // サブショットの発射口を生成
        subshooterObj[0] = Instantiate(subshooter, subshooterLayout[0, 0], Quaternion.identity, subShooterCenter.transform);
        subshooterObj[1] = Instantiate(subshooter, subshooterLayout[1, 0], Quaternion.identity, subShooterCenter.transform);
        subshooterObj[2] = Instantiate(subshooter, subshooterLayout[2, 0], Quaternion.identity, subShooterCenter.transform);
        subshooterObj[3] = Instantiate(subshooter, subshooterLayout[3, 0], Quaternion.identity, subShooterCenter.transform);

        // 最初はRigidbodyを取得してから無効化する
        for (int i = 0; i < 3; i++)
        {
            subshooterRigidbody[i] = subshooterObj[i].GetComponent<Rigidbody2D>();
            subshooterObj[i].SetActive(false);
        }
    }
}
