using UnityEngine;
// 針集中型の選択時の自機の操作を管理するスクリプト

public class NeedlePerformance : PlayerController
{
    float subshooterspeed = 1.0f; // 円運動のスピード

    Vector2[] needleshooterLayout = new Vector2[4];

    
    public override void CharacterDefaultSetting()
    {
        playerHighSpeed = 5.5f;
    }

    public override void SubShooterSetting()
    {
        needleshooterLayout[0] = new Vector2(0.0f, 1.0f);
        needleshooterLayout[1] = new Vector2(0.0f, -1.0f);
        needleshooterLayout[2] = new Vector2(0.0f, 0.6f);
        needleshooterLayout[3] = new Vector2(0.0f, -0.6f);

        subshooterObj[0] = Instantiate(subshooter, needleshooterLayout[0],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[1] = Instantiate(subshooter, needleshooterLayout[1],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[2] = Instantiate(subshooter, needleshooterLayout[2],
            Quaternion.identity, subShooterCenter.transform);
        subshooterObj[3] = Instantiate(subshooter, needleshooterLayout[3],
            Quaternion.identity, subShooterCenter.transform);

        // 最初はRigidbodyとスプライトを取得してから不可視化する
        for (int i = 0; i < 4; i++)
        {
            subshooterRigidbody[i] = subshooterObj[i].GetComponent<Rigidbody2D>();
        }
    }

    public override void ShotPowerCheck()
    {
        if (controllerManager.slowAction.IsPressed())
        {
            subshooterObj[0].GetComponent<NeedleSubShots>().getshotflag = false;
            subshooterObj[1].GetComponent<NeedleSubShots>().getshotflag = false;
            subshooterObj[2].GetComponent<NeedleSubShots>().getshotflag = true;
            subshooterObj[3].GetComponent<NeedleSubShots>().getshotflag = true;
        }
        else
        {
            subshooterObj[0].GetComponent<NeedleSubShots>().getshotflag = true;
            subshooterObj[1].GetComponent<NeedleSubShots>().getshotflag = true;
            subshooterObj[2].GetComponent<NeedleSubShots>().getshotflag = false;
            subshooterObj[3].GetComponent<NeedleSubShots>().getshotflag = false;
        }
            for (int i = 0; i < 4; i++)
        {
            // パワーが4.0以上の時
            if (shotPower >= 400)
            {
                subshooterObj[i].GetComponent<SubShooters>().getsubshotinterval = 0.1f;
            }
            // パワーが3.0以上の時
            else if (shotPower >= 300)
            {
                subshooterObj[i].GetComponent<SubShooters>().getsubshotinterval = 0.15f;
            }
            // パワーが2.0以上の時
            else if (shotPower >= 200)
            {
                subshooterObj[i].GetComponent<SubShooters>().getsubshotinterval = 0.2f;
            }
            // パワーが1.0以上の時
            else if (shotPower >= 100)
            {
                subshooterObj[i].GetComponent<SubShooters>().getsubshotinterval = 0.25f;
            }
            // パワーが1.0未満の時
            else
            {
                subshooterObj[i].GetComponent<SubShooters>().getsubshotinterval = 0;
            }
        }
    }

    public override void SubShooterPositionning()
    {
        // 低速時
        if (controllerManager.slowAction.IsPressed())
        {
            for (int i = 0; i < 4; i++)
            {
                subshooterObj[i].transform.RotateAround(
                    subShooterCenter.transform.position,
                    Vector3.back * 0.5f,
                    360 / subshooterspeed * Time.deltaTime
                    );
            }
        }
        // 高速時
        else
        {
            for (int i = 0; i < 4; i++)
            {
                subshooterObj[i].transform.RotateAround(
                    subShooterCenter.transform.position,
                    Vector3.back,
                    360 / subshooterspeed * Time.deltaTime
                    );
            }
        }
    }

}
