using UnityEngine;
// ミーシャ選択時の自機の操作を管理するスクリプト

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
        // パワーが1以上で使えるサブショット
        fastsubshooterLayout[0, 0] = new Vector2(-0.8f, 0.0f);
        fastsubshooterLayout[0, 1] = new Vector2(0.0f, 1.0f);
        // パワーが2以上で使えるサブショット
        fastsubshooterLayout[1, 0] = new Vector2(0.8f, 0.0f);
        fastsubshooterLayout[1, 1] = new Vector2(0.8f, 0.0f);
        // パワーが3以上で使えるサブショット
        fastsubshooterLayout[2, 0] = new Vector2(-1.6f, 0.0f);
        fastsubshooterLayout[2, 1] = new Vector2(-0.8f, 0.0f);
        // パワーが4以上で使えるサブショット
        fastsubshooterLayout[3, 0] = new Vector2(1.6f, 0.0f);
        fastsubshooterLayout[3, 1] = new Vector2(0.0f, -1.0f);
        // 低速時
        // パワーが1以上で使えるサブショット
        slowsubshooterLayout[0, 0] = new Vector2(-0.25f, 0.0f);
        slowsubshooterLayout[0, 1] = new Vector2(0.0f, 0.0f);
        // パワーが2以上で使えるサブショット
        slowsubshooterLayout[1, 0] = new Vector2(0.25f, 0.0f);
        slowsubshooterLayout[1, 1] = new Vector2(0.5f, 0.0f);
        // パワーが3以上で使えるサブショット
        slowsubshooterLayout[2, 0] = new Vector2(-0.75f, 0.0f);
        slowsubshooterLayout[2, 1] = new Vector2(-0.5f, 0.0f);
        // パワーが4以上で使えるサブショット
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
        sbyte power; // パワーの数値の偶奇を受け渡す変数
        sbyte movetype; // 自機の今の移動が高速か低速かを受け渡す変数
        // 低速時
        if (controllerManager.slowAction.IsPressed())
        {
            movetype = 1;
            if (Mathf.Floor(shotPower) == 0 || Mathf.Floor(shotPower) % 2 == 0)
            {
                subshooterObj[0].GetComponent<MisyaSubShots>().changeshotangle = false;
                power = 0;
                PositionningForLoop(power, movetype);
            }
            else if (Mathf.Floor(shotPower) % 2 == 1)
            {
                subshooterObj[0].GetComponent<MisyaSubShots>().changeshotangle = true;
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
                // 真ん中に配置される事が有るサブショットはパワーが偶数の時は斜めにも弾を飛ばすようにする
                subshooterObj[0].GetComponent<MisyaSubShots>().changeshotangle = false;
                power = 0;
                PositionningForLoop(power, movetype);
            }
            else if (Mathf.Floor(shotPower) % 2 == 1)
            {
                // 真ん中に配置される事が有るサブショットはパワーが奇数の時は全て真っ直ぐに弾を飛ばすようにする
                subshooterObj[0].GetComponent<MisyaSubShots>().changeshotangle = true;
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
