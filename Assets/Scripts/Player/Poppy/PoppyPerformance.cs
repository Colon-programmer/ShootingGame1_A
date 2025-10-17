using UnityEngine;

public class PoppyPerformance : PlayerController
{
    private float positionResetInterval = 1.0f; // サブショットの位置固定状態の解除に掛かる時間
    private float resetTimer = 0.0f; // 位置固定状態の解除をする時間を測る
    public override void CharacterDefaultSetting()
    {
        playerHighSpeed = 5.5f;
    }

    public override void SubShooterSetting()
    {
        // サブショットの発射口を生成
        subshooterObj[0] = Instantiate(subshooter, new Vector2(-0.5f,-0.5f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[0].GetComponent<PoppySubShots>().subNumber = PoppySubShots.SubSetNumber.First;
        subshooterObj[1] = Instantiate(subshooter, new Vector2(0.5f, -0.5f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[1].GetComponent<PoppySubShots>().subNumber = PoppySubShots.SubSetNumber.First;
        subshooterObj[2] = Instantiate(subshooter, new Vector2(-1.0f, 0.0f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[2].GetComponent<PoppySubShots>().subNumber = PoppySubShots.SubSetNumber.Second;
        subshooterObj[3] = Instantiate(subshooter, new Vector2(1.0f, 0.0f),
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[3].GetComponent<PoppySubShots>().subNumber = PoppySubShots.SubSetNumber.Second;

        // 最初はRigidbodyを取得してから無効化する
        for (int i = 0; i < 4; i++)
        {
            subshooterRigidbody[i] = subshooterObj[i].GetComponent<Rigidbody2D>();

            subshooterObj[i].SetActive(false);
        }
    }

    public override void PlayerMove()
    {
        var playermoveValue = controllerManager.moveingAction.ReadValue<Vector2>();

        if (controllerManager.slowAction.IsPressed())
        {
            // 低速移動
            // 本体
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerSlowSpeed, playermoveValue.y * playerSlowSpeed);
            // サブショットの位置を固定する
            resetTimer = 0.0f;
        }
        else
        {
            // 高速移動
            // 本体
            playerRb.linearVelocity =
            new Vector2(playermoveValue.x * playerHighSpeed, playermoveValue.y * playerHighSpeed);
            // 時間が経ったらサブショットの発射口を自機に追尾させる
            if (resetTimer >= positionResetInterval)
            {
                subShooterCenter.transform.position = new Vector2(playerRb.position.x,
                                                            playerRb.position.y);
            }
            // 時間を測る
            else
            {
                resetTimer += Time.deltaTime;
            }
        }
    }

    public override void ShotPowerCheck()
    {
        // パワーが4.0以上の時
        if (shotPower >= 4.0f)
        {
            // サブショットの発射間隔を早くする
            subshooterObj[2].GetComponent<PoppySubShots>().getsubshotinterval = 0.2f;
            subshooterObj[3].GetComponent<PoppySubShots>().getsubshotinterval = 0.2f;
        }
        else
        {
            // サブショットの発射間隔を遅くする
            subshooterObj[2].GetComponent<PoppySubShots>().getsubshotinterval = 0.4f;
            subshooterObj[3].GetComponent<PoppySubShots>().getsubshotinterval = 0.4f;
        }
        // パワーが3.0以上の時
        if (shotPower >= 3.0f)
        {
            // サブショットを開放する
            subshooterObj[2].SetActive(true);
            subshooterObj[3].SetActive(true);
        }
        else
        {
            // パワーが足りていないときは対応したサブショットを閉鎖する
            subshooterObj[2].SetActive(false);
            subshooterObj[3].SetActive(false);
        }
        // パワーが2.0以上の時
        if (shotPower >= 2.0f)
        {
            // サブショットの発射間隔を早くする
            subshooterObj[0].GetComponent<PoppySubShots>().getsubshotinterval = 0.2f;
            subshooterObj[1].GetComponent<PoppySubShots>().getsubshotinterval = 0.2f;
        }
        else
        {
            // サブショットの発射間隔を遅くする
            subshooterObj[0].GetComponent<PoppySubShots>().getsubshotinterval = 0.4f;
            subshooterObj[1].GetComponent<PoppySubShots>().getsubshotinterval = 0.4f;
        }
        // パワーが1.0以上の時
        if (shotPower >= 1.0f)
        {
            // サブショットを開放する
            subshooterObj[0].SetActive(true);
            subshooterObj[1].SetActive(true);
        }
        else
        {
            // パワーが足りていないときは対応したサブショットを閉鎖する
            subshooterObj[0].SetActive(false);
            subshooterObj[1].SetActive(false);
        }
    }
}
