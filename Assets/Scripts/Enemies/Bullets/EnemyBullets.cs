using UnityEngine;
// 敵の弾の性能を管理するスクリプト

public class EnemyBullets : MonoBehaviour
{
    private Rigidbody2D enemybulletRb; // 敵の弾のRigidbody
    private float enemybulletspeed; // 敵の弾の弾速
    private float bulletarea = 6.5f; // 弾が存在できる範囲

    protected GameObject playerposition; // 自機の位置

    protected Vector2 enemybulletVec; // 敵の弾の移動方向

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        enemybulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 向いている方向に移動する
        enemybulletRb.AddForce(enemybulletVec * enemybulletspeed, ForceMode2D.Impulse);
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
    /// <summary>
    /// 自機外しの場合、弾の進行方向を別スクリプトから取得する
    /// </summary>
    public Vector2 getenemybulletVec
    {
        get { return this.enemybulletVec; }
        set { this.enemybulletVec = value; }
    }
}
