using UnityEngine;

public class SearchShooter : MonoBehaviour
{
    private int shotpattern; // 弾の発射パターン

    private GameObject playerobj; // 自機のオブジェクト

    private Transform shootertransform;

    private GameObject enemybullet; // 敵の出す弾

    private sbyte fanshapecount; // 扇形に弾を出すときの左右それぞれの弾の数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        Vector3 dir = (this.transform.position - playerobj.transform.position);

        shootertransform = this.transform;

        shootertransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        Debug.Log(this.transform.eulerAngles);

        switch (shotpattern)
        {
            case 1:
                GameObject bullets_01 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

                bullets_01.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                bullets_01.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
                // 自機の位置から移動方向を決める
                bullets_01.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z; // eulerAnglesで発射機の向いている方向を取得する

                GameObject bullets_02 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

                bullets_02.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                bullets_02.GetComponent<EnemyBullets>().getenemybulletspeed = 3.0f;
                // 自機の位置から移動方向を決める
                bullets_02.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z;
                break;
            case 2:
                GameObject bullets_04 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);
                bullets_04.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                bullets_04.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
                // 自機の位置から移動方向を決める
                bullets_04.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z; // eulerAnglesで発射機の向いている方向を取得する

                GameObject bullet_05;
                GameObject bullet_06;

                for (int i = 1; i <= fanshapecount; i++)
                {
                    bullet_05 = Instantiate(enemybullet, this.gameObject.transform.position, Quaternion.identity);

                    bullet_05.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                    bullet_05.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
                    // 自機の位置から移動方向を決める
                    bullet_05.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z + 20 * i;

                    bullet_06 = Instantiate(enemybullet, this.gameObject.transform.position, Quaternion.identity);

                    bullet_06.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                    bullet_06.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
                    // 自機の位置から移動方向を決める
                    bullet_06.GetComponent<EnemyBullets>().getenemybulletangle += this.transform.eulerAngles.z - 20 * i;
                }

                break;
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }

    public int getshotpattren
    {
        get { return this.shotpattern; }
        set { this.shotpattern = value; }
    }

    public GameObject getenemybullet
    {
        get { return this.enemybullet; }
        set { this.enemybullet = value; }
    }

    public sbyte getfanshapecount
    {
        get { return this.fanshapecount; }
        set { this.fanshapecount = value; }
    }
}
