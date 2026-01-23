using UnityEngine;
// 自機狙いの敵弾の発射機のスクリプト

public class SearchShooter : MonoBehaviour
{
    private int shotpattern; // 弾の発射パターン

    private GameObject playerobj; // 自機のオブジェクト

    private Transform searchshootertransform;

    private GameObject searchbulletobj; // 敵の出す弾

    private int searchbulletinterval; // 発射間隔
    private int searchbullettimer; // 時間を測る変数
    private int searchbulletdeletetime; // 発射機が消える時間

    private float searchbulletspeed; // 弾速
    private float searchbulletangle; // 発射角度
    private sbyte searchbulletcount; // 同時発射数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        Vector3 dir = (this.transform.position - playerobj.transform.position);

        searchshootertransform = this.transform;

        searchshootertransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        searchbullettimer = 0;
    }
    private void FixedUpdate()
    {
        searchbullettimer += 1; // 発射間隔をカウントする
        searchbulletdeletetime -= 1; // 消えるまでの時間をカウントする

        if (searchbullettimer >= searchbulletinterval)
        {
            switch (shotpattern)
            {
                case 1:
                    GameObject normal = Instantiate(searchbulletobj, this.transform.position, Quaternion.identity);

                    normal.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                    normal.GetComponent<EnemyBullets>().getenemybulletspeed = searchbulletspeed;
                    // 自機の位置から移動方向を決める
                    normal.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z; // eulerAnglesで発射機の向いている方向を取得する

                    break;
                case 2:
                    GameObject fanshape_1 = Instantiate(searchbulletobj, this.transform.position, Quaternion.identity);
                    fanshape_1.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                    fanshape_1.GetComponent<EnemyBullets>().getenemybulletspeed = searchbulletspeed;
                    // 自機の位置から移動方向を決める
                    fanshape_1.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z; // eulerAnglesで発射機の向いている方向を取得する

                    GameObject fanshape_2;
                    GameObject fanshape_3;

                    for (int i = 1; i <= searchbulletcount; i++)
                    {
                        fanshape_2 = Instantiate(searchbulletobj, this.gameObject.transform.position, Quaternion.identity);

                        fanshape_2.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                        fanshape_2.GetComponent<EnemyBullets>().getenemybulletspeed = searchbulletspeed;
                        // 自機の位置から移動方向を決める
                        fanshape_2.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z + searchbulletangle * i;

                        fanshape_3 = Instantiate(searchbulletobj, this.gameObject.transform.position, Quaternion.identity);

                        fanshape_3.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                        fanshape_3.GetComponent<EnemyBullets>().getenemybulletspeed = searchbulletspeed;
                        // 自機の位置から移動方向を決める
                        fanshape_3.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z - searchbulletangle * i;
                    }

                    break;
            }
            searchbullettimer = 0;
        }

        if (searchbulletdeletetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// 一発を自機に向かって撃つ関数
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="speed"></param>
    /// <param name="interval"></param>
    /// <param name="dlt"></param>
    public void NormalSearchShot(GameObject bullet, float speed, int interval, int dlt)
    {
        searchbulletobj = bullet;
        searchbulletspeed = speed;
        searchbulletinterval = interval;
        searchbullettimer = interval;
        searchbulletdeletetime = dlt;
        shotpattern = 1;
    }
    /// <summary>
    /// 扇形の弾幕を出す関数
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="speed"></param>
    /// <param name="count"></param>
    /// <param name="angle"></param>
    /// <param name="interval"></param>
    /// <param name="dlt"></param>
    public void FanShapeSearchShot(GameObject bullet, float speed, sbyte count, float angle, int interval, int dlt)
    {
        searchbulletobj = bullet;
        searchbulletspeed = speed;
        searchbulletcount = count;
        searchbulletangle = angle;
        searchbulletinterval = interval;
        searchbullettimer = interval;
        searchbulletdeletetime = dlt;
        shotpattern = 2;
    }
}
