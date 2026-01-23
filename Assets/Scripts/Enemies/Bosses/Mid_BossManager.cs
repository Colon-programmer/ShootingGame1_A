using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
// 中ボスの動きを管理するスクリプト


public class Mid_BossManager : MonoBehaviour
{
    protected Rigidbody2D mid_boss_rb;

    protected Sequence spwanMove; // スポーンしてすぐの移動シーケンス

    protected GameObject playerobj; // 自機

    // 体力関連
    protected int mid_boss_hp = 3000; // 中ボスの体力
    protected int mid_boss_maxhp; // 中ボスの最大体力
    [SerializeField] protected GameObject mid_boss_hp_gauge; // 中ボスの体力ゲージ
    protected GameObject gamecanvas;

    protected sbyte mid_boss_patten = 2; // HPが0になると1つ減りこれが0になるとやられるようにする

    protected sbyte shottingpatten = 0; // 弾の出現パターンを指定する変数

    protected float reAttackTime = 0.0f; // 次の攻撃までに時間を測る
    protected float reAttackInterval; // 次の攻撃までに掛かる時間

    protected CancellationToken canceler;

    protected CancellationTokenSource token;

    protected bool cancelflag = false;

    protected GameObject scorecountobj; // スコアカウントオブジェクト

    [SerializeField] protected GameObject shotEraserobj; // 全画面弾消しオブジェクト

    protected GameObject enemySpawnobj; // 敵を出現させるオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        gamecanvas = GameObject.Find("GameCanvas");

        // 親子関係
        mid_boss_hp_gauge.transform.SetParent(gamecanvas.transform, false);

        // スコアカウンターを取得
        scorecountobj = GameObject.Find("ScoreCounterObject");

        enemySpawnobj = GameObject.Find("EnemySpawn");

        mid_boss_rb = GetComponent<Rigidbody2D>();

        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShots"))
        {
            int d_a = other.GetComponent<PlayerAttackAmounts>().damageAmount;

            mid_boss_hp -= d_a;
            scorecountobj.GetComponent<ScoreGetter>().getscore += d_a;
        }
    }

    protected void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Bomb"))
        {
            mid_boss_hp -= col.GetComponent<PlayerAttackAmounts>().damageAmount;
        }
    }

    public int getmid_boss_hp
    {
        get { return this.mid_boss_hp; }
        set { this.mid_boss_hp = value; }
    }

    public int getmid_boss_maxhp
    {
        get { return this.mid_boss_maxhp; }
        set { this.mid_boss_maxhp = value; }
    }
}
