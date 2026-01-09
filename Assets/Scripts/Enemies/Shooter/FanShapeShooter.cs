using UnityEngine;
// 扇型の敵弾のスクリプト

public class FanShapeShooter : MonoBehaviour
{
    private GameObject enemybullet; // 敵弾のオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 目的地に向かって一発と左右に扇形に撃つ関数
    /// obj = 発射する弾
    /// num = 左右に何発発射するか
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="num"></param>
    public void EnemyFanShapeShot(GameObject obj, int num)
    {
        GameObject bullet_1;
        GameObject bullet_2;
        GameObject bullet_3;

        bullet_1 = Instantiate(obj, this.gameObject.transform.position, Quaternion.identity);

        bullet_1.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
        for (int i = 0; i < num; i++)
        {
            bullet_2 = Instantiate(obj, this.gameObject.transform.position, Quaternion.identity);

            bullet_2.GetComponent<EnemyBullets>().getenemybulletVec += new Vector2(-0.2f * num, 0.2f * num);

            bullet_2.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

            bullet_3 = Instantiate(obj, this.gameObject.transform.position, Quaternion.identity);

            bullet_3.GetComponent<EnemyBullets>().getenemybulletVec += new Vector2(0.2f * num, -0.2f * num);

            bullet_3.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
        }

        Destroy(this.gameObject);
    }
}
