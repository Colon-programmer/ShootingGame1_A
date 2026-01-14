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
    private float enemybulletangle = -90.0f; // 敵の弾の角度
    private Vector3 enemybulletvelocity; // 敵の弾の移動量
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
    void Update()
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
        //enemybulletRb.AddForce(enemybulletVec * enemybulletspeed, ForceMode2D.Impulse);
        //if (enemybulletRb.linearVelocity.magnitude > enemybulletspeed)
        //{
        //    enemybulletRb.linearVelocity = enemybulletRb.linearVelocity.normalized * enemybulletspeed;
        //}

        // X方向の移動量を設定する
        enemybulletvelocity.x = enemybulletspeed * Mathf.Cos(enemybulletangle * Mathf.Deg2Rad);

        // Y方向の移動量を設定する
        enemybulletvelocity.y = enemybulletspeed * Mathf.Sin(enemybulletangle * Mathf.Deg2Rad);

        // 弾の向きを設定する
        float zAngle = Mathf.Atan2(enemybulletvelocity.y, enemybulletvelocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);

        // 毎フレーム、弾を移動させる
        transform.position += enemybulletvelocity * Time.deltaTime;
    }

    public float getenemybulletangle
    {
        get { return this.enemybulletangle; }
        set { this.enemybulletangle = value; }
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

    /// <summary>
    /// 敵弾の移動の仕方を指定する変数のゲッター
    /// </summary>
    public BulletType getenemyBulletType
    {
        get { return this.enemyBulletType; }
        set { this.enemyBulletType = value; }
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
}
