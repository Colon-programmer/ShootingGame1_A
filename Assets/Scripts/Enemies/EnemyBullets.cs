using UnityEngine;
// 敵の弾の性能を管理するスクリプト

public class EnemyBullets : MonoBehaviour
{
    Rigidbody2D enemybulletRb; // 敵の弾のRigidbody
    float enemybulletspeed; // 敵の弾の弾速
    float bulletarea = 6.5f; // 弾が存在できる範囲
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemybulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 向いている方向に移動する
        enemybulletRb.AddForce(transform.up * enemybulletspeed, ForceMode2D.Impulse);
        if (enemybulletRb.linearVelocity.magnitude > enemybulletspeed)
        {
            enemybulletRb.linearVelocity = enemybulletRb.linearVelocity.normalized * enemybulletspeed;
        }
        // 画面外に出たら消える
        if (enemybulletRb.transform.position.x >= bulletarea ||
            enemybulletRb.transform.position.x <= -bulletarea ||
            enemybulletRb.transform.position.y >= bulletarea ||
            enemybulletRb.transform.position.y <= -bulletarea)
        {
            Destroy(this.gameObject);
        }
    }

    public float getenemybulletspeed
    {
        get { return this.enemybulletspeed; }
        set { this.enemybulletspeed = value; }
    }
}
