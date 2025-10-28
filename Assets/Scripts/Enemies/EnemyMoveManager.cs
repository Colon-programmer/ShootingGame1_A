using UnityEngine;
// ステージ上の敵の動きを制御するスクリプト

public class EnemyMoveManager : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    private float enemyspeed = 10.0f;

    public int movepattern; // 敵の移動パターンを指定する変数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TestMove();
    }

    void TestMove()
    {
        enemyRb.AddForce(transform.up * enemyspeed, ForceMode2D.Impulse);
        if (enemyRb.linearVelocity.magnitude > enemyspeed)
        {
            enemyRb.linearVelocity = enemyRb.linearVelocity.normalized * enemyspeed;
        }
    }
    /// <summary>
    /// 敵の移動パターンを指定する変数のゲッター
    /// </summary>
    int getmovepattern
    {
        get { return this.movepattern; }
        set { this.movepattern = value; }
    }

}
