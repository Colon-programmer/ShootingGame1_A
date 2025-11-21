using UnityEngine;
using System.Collections;
// ステージ上の敵の動きを制御するスクリプト

public class EnemyMoveManager : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    private float enemyspeed;

    private int movepattern; // 敵の移動パターンを指定する変数

    [SerializeField] private GameObject enemybullet; // 敵の出す弾

    private GameObject playerposition;

    private bool attackflag = true; // 攻撃が終わったかを管理するフラグ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        // 自機を探す
        playerposition = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TestMove();
        StartCoroutine("TestAttack");
    }

    void TestMove()
    {
        enemyRb.AddForce(transform.up * enemyspeed, ForceMode2D.Impulse);
        if (enemyRb.linearVelocity.magnitude > enemyspeed)
        {
            enemyRb.linearVelocity = enemyRb.linearVelocity.normalized * enemyspeed;
        }
    }
    IEnumerator TestAttack()
    {
        if (attackflag)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject bullets = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

            bullets.GetComponent<EnemyBullets>().getenemybulletspeed = 6.0f;

            attackflag = false;
        }
    }
    /// <summary>
    /// 敵の移動パターンを指定する変数のゲッター
    /// </summary>
    public int getmovepattern
    {
        get { return this.movepattern; }
        set { this.movepattern = value; }
    }

    public float getenemyspeed
    {
        get { return this.enemyspeed; }
        set { this.enemyspeed = value; }
    }

}
