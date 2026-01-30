using UnityEngine;
// ステージ上の敵の動きを制御するスクリプト

public class EnemyMoveManager : MonoBehaviour
{
    protected Rigidbody2D enemyRb;

    protected float enemyspeed;

    protected int movepattern; // 敵の移動パターンを指定する変数

    protected GameObject playerobj; // 自機のオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// 雑魚敵を消す関数
    /// </summary>
    protected void MobDelete()
    {
        Destroy(this.gameObject);
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
