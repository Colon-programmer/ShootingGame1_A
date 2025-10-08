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
        // 高速時
        fastsubshooterLayout[0, 0] = new Vector2(-1.1f, 0.0f);
        fastsubshooterLayout[0, 1] = new Vector2(0.0f, 1.0f);

        fastsubshooterLayout[1, 0] = new Vector2(1.1f, 0.0f);
        fastsubshooterLayout[1, 1] = new Vector2(1.1f, 0.0f);

        fastsubshooterLayout[2, 0] = new Vector2(-2.2f, 0.0f);
        fastsubshooterLayout[2, 1] = new Vector2(-1.1f, 0.0f);

        fastsubshooterLayout[3, 0] = new Vector2(2.2f, 0.0f);
        fastsubshooterLayout[3, 1] = new Vector2(0.0f, -1.0f);
        // 低速時
        slowsubshooterLayout[0, 0] = new Vector2(-0.25f, 0.0f);
        slowsubshooterLayout[0, 1] = new Vector2(0.0f, 0.0f);

        slowsubshooterLayout[1, 0] = new Vector2(0.25f, 0.0f);
        slowsubshooterLayout[1, 1] = new Vector2(0.5f, 0.0f);

        slowsubshooterLayout[2, 0] = new Vector2(-0.75f, 0.0f);
        slowsubshooterLayout[2, 1] = new Vector2(-0.5f, 0.0f);

        slowsubshooterLayout[3, 0] = new Vector2(0.75f, 0.0f);
        slowsubshooterLayout[3, 1] = new Vector2(0.0f,-1.0f);
        // サブショットの発射口を生成
        subshooterObj[0] = Instantiate(subshooter, fastsubshooterLayout[0, 0],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[0].GetComponent<MisyaSubShots>().subPosition = MisyaSubShots.LeftAndRight.Left;
        subshooterObj[1] = Instantiate(subshooter, fastsubshooterLayout[1, 0],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[1].GetComponent<MisyaSubShots>().subPosition = MisyaSubShots.LeftAndRight.Right;
        subshooterObj[2] = Instantiate(subshooter, fastsubshooterLayout[2, 0],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[2].GetComponent<MisyaSubShots>().subPosition = MisyaSubShots.LeftAndRight.Left;
        subshooterObj[3] = Instantiate(subshooter, fastsubshooterLayout[3, 0],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[3].GetComponent<MisyaSubShots>().subPosition = MisyaSubShots.LeftAndRight.Right;

        // 最初はRigidbodyを取得してから無効化する
        for (int i = 0; i < 4; i++)
        {
            subshooterRigidbody[i] = subshooterObj[i].GetComponent<Rigidbody2D>();
            
            subshooterObj[i].SetActive(false);
        }
    }

    public override void SubShooterPositionning()
    {
        sbyte power;
        sbyte movetype;
        // 低速時
        if (controllerManager.slowAction.IsPressed())
        {
            movetype = 1;
            if (Mathf.Floor(shotPower) == 0 || Mathf.Floor(shotPower) % 2 == 0)
            {
                power = 0;
                PositionningForLoop(power, movetype);
            }
            else if (Mathf.Floor(shotPower) % 2 == 1)
            {
                power = 1;
                PositionningForLoop(power, movetype);
            }
        }
        // 高速時
        else
        {
            movetype = 0;
            if (Mathf.Floor(shotPower) == 0 || Mathf.Floor(shotPower) % 2 == 0)
            {
                power = 0;
                PositionningForLoop(power, movetype);
            }
            else if (Mathf.Floor(shotPower) % 2 == 1)
            {
                power = 1;
                PositionningForLoop(power, movetype);
            }
        }
    }
    /// <summary>
    /// 受け取った数値をもとにサブショットの発射口の配置を変更する関数
    /// </summary>
    /// <param name="num_1"></param>
    /// <param name="num_2"></param>
    private void PositionningForLoop(sbyte num_1, sbyte num_2)
    {
        // 高速時
        if (num_2 == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                subshooterObj[i].transform.position =
                    fastsubshooterLayout[i, num_1] + new Vector2(subShooterCenter.transform.position.x,
                                                                subShooterCenter.transform.position.y);
            }
        }
        // 低速時
        else
        {
            for (int i = 0; i < 4; i++)
            {
                subshooterObj[i].transform.position =
                    slowsubshooterLayout[i, num_1] + new Vector2(subShooterCenter.transform.position.x,
                                                                subShooterCenter.transform.position.y); ;
            }
        }
    }
}
