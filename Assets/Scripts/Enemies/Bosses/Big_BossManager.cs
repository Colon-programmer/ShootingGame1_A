using UnityEngine;
// ステージボスの動きを管理するスクリプト

public class Big_BossManager : MonoBehaviour
{
    protected Rigidbody2D big_boss_rb;

    protected GameObject playerobj; // 自機

    // 体力関連
    protected int big_boss_hp = 3000; // ボスの体力
    protected int big_boss_maxhp; // ボスの最大体力
    [SerializeField] protected GameObject big_boss_hp_gauge; // 中ボスの体力ゲージ
    protected GameObject gamecanvas;

    protected sbyte big_boss_patten = 2; // HPが0になると1つ減りこれが0になるとやられるようにする

    protected sbyte shottingpatten = 0; // 弾の出現パターンを指定する変数

    protected float reAttackTime = 0.0f; // 次の攻撃までに時間を測る
    protected float reAttackInterval; // 次の攻撃までに掛かる時間

    protected bool cancelflag = false;

    protected GameObject scorecountobj; // スコアカウントオブジェクト

    [SerializeField] protected GameObject shotEraserobj; // 全画面弾消しオブジェクト

    protected GameObject enemySpawnobj; // 敵を出現させるオブジェクト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        gamecanvas = GameObject.Find("GameCanvas");

        // 親子関係
        big_boss_hp_gauge.transform.SetParent(gamecanvas.transform, false);

        // スコアカウンターを取得
        scorecountobj = GameObject.Find("ScoreCounterObject");

        enemySpawnobj = GameObject.Find("EnemySpawn");

        big_boss_rb.GetComponent<Rigidbody2D>();

        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");
    }
}
