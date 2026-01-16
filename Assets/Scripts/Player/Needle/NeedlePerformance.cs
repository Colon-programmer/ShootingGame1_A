using UnityEngine;
// 針集中型の選択時の自機の操作を管理するスクリプト

public class NeedlePerformance : PlayerController
{
    float subshooterspeed = 2.0f; // 円運動のスピード
    float fastsubshooterRadius = 1.0f; // 高速移動時の円運動の半径
    float slowsubshooterRadius = 0.5f; // 低速移動時の円運動の半径

    Vector2[] needleshooterLayout = new Vector2[4];

    
    public override void CharacterDefaultSetting()
    {
        playerHighSpeed = 5.5f;
    }

    public override void SubShooterSetting()
    {
        needleshooterLayout[0] = new Vector2(0.0f, 1.0f);
        needleshooterLayout[1] = new Vector2(-1.0f, 0.0f);
        needleshooterLayout[2] = new Vector2(0.0f, -1.0f);
        needleshooterLayout[3] = new Vector2(1.0f, 0.0f);

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
        // パワーが4.0以上の時
        if (shotPower >= 400)
        {
            
        }
        // パワーが足りていないときは対応したサブショットを閉鎖する
        else
        {
            
        }
        // パワーが3.0以上の時
        if (shotPower >= 300)
        {
            
        }
        else
        {
           
        }
        // パワーが2.0以上の時
        if (shotPower >= 200)
        {
            
        }
        else
        {
            
        }
        // パワーが1.0以上の時
        if (shotPower >= 100)
        {
            
        }
        else
        {
            
        }
    }

    public override void SubShooterPositionning()
    {
        //sbyte power; // パワーの数値の偶奇を受け渡す変数
        //sbyte movetype; // 自機の今の移動が高速か低速かを受け渡す変数
        //float shotPowerFloat = shotPower / 100.0f; // 浮動小数点の方が計算しやすいの変換する
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
