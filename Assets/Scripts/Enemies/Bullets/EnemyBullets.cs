using UnityEngine;
// 敵の弾の性能を管理するスクリプト

public class EnemyBullets : MonoBehaviour
{
    public enum BulletType
    {
        /// <summary>
        /// 一定速度で直進する弾
        /// </summary>
        STRAIGHT = 0,
    }

    private Rigidbody2D enemybulletRb; // 敵の弾のRigidbody
    private float enemybulletspeed; // 敵の弾の弾速
    private float bulletarea = 6.5f; // 弾が存在できる範囲

    protected GameObject playerposition; // 自機の位置

    protected Vector2 enemybulletVec; // 敵の弾の移動方向

    private BulletType enemyBulletType; // 敵弾の移動の仕方を指定する変数

    [SerializeField] private GameObject scoreItme; // スコアアイテム

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        enemybulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (enemyBulletType)
        {
            case BulletType.STRAIGHT:
                StraightType();
                break;
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

    void StraightType()
    {
        // 向いている方向に移動する
        enemybulletRb.AddForce(enemybulletVec * enemybulletspeed, ForceMode2D.Impulse);
        if (enemybulletRb.linearVelocity.magnitude > enemybulletspeed)
        {
            enemybulletRb.linearVelocity = enemybulletRb.linearVelocity.normalized * enemybulletspeed;
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

    // ボム攻撃に触れたら消えるようにする
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bomb"))
        {
            Destroy(this.gameObject);
        }

        if (col.CompareTag("ShotEraser"))
        {
            Instantiate(scoreItme, this.gameObject.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Bomb"))
        {
            Destroy(this.gameObject);
        }

        if (col.CompareTag("ShotEraser"))
        {
            Instantiate(scoreItme, this.gameObject.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// 敵弾の移動の仕方を指定する変数のゲッター
    /// </summary>
    public BulletType getenemyBulletType
    {
        get { return this.enemyBulletType; }
        set { this.enemyBulletType = value; }
    }
}
