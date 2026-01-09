using UnityEngine;
using System.Collections;
// ステージ上の敵の動きを制御するスクリプト

public class EnemyMoveManager : MonoBehaviour
{
    protected Rigidbody2D enemyRb;

    protected float enemyspeed;

    protected int movepattern; // 敵の移動パターンを指定する変数

    [SerializeField] protected GameObject enemybullet; // 敵の出す弾

    protected GameObject playerobj; // 自機のオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        StartCoroutine("TestAttack");
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
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
    IEnumerator TestAttack()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject bullets = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

        bullets.GetComponent<EnemyBullets>().getenemybulletspeed = 6.0f;
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
