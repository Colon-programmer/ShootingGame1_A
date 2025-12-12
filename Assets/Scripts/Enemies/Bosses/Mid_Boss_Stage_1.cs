using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
// ステージ1の中ボスのスクリプト

public class Mid_Boss_Stage_1 : MonoBehaviour
{
    private Rigidbody2D mid_boss_rb_01;

    private Sequence spwanMove; // スポーンしてすぐの移動シーケンス

    private Tween mid_boss_moveX_01;
    private Tween mid_boss_moveY_01;

    private GameObject playerobj; // 自機

    [SerializeField] private GameObject bigbullet_01; // 大きい赤弾

    private float[,] bigbullet_01_pos_x;

    private int mid_boss_hp = 3000; // 中ボスの体力

    private sbyte shottingpatten = 0; // 弾の出現パターンを指定する変数

    float reAttackTime = 0.0f; // 次の攻撃までに時間を測る
    float reAttackInterval; // 次の攻撃までに掛かる時間

    private CancellationToken canceler;

    private CancellationTokenSource token;

    private bool cancelflag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CancellationTokenSourceの生成  
        token = new CancellationTokenSource();

        // CancellationTokenをCancellationTokenSourceから取得  
        canceler = token.Token;

        // 弾の出現位置の初期化
        BulletpositionPreseter();

        mid_boss_rb_01 = GetComponent<Rigidbody2D>();

        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        spwanMove = DOTween.Sequence();
        mid_boss_moveX_01 = transform.DOMoveX(this.transform.position.x - 5.5f, 0.5f);
        mid_boss_moveY_01 = transform.DOMoveY(this.transform.position.y - 2f, 0.5f);

        spwanMove.Join(mid_boss_moveX_01);
        spwanMove.Join(mid_boss_moveY_01);

        reAttackInterval = 1.5f;
    }

    private void Update()
    {
        if (mid_boss_hp <= 0)
        {
            cancelflag = true;
            token.Cancel();
            Destroy(this.gameObject);
        }

    }

    async void FixedUpdate()
    {
        if (!cancelflag)
        {
            reAttackTime += Time.deltaTime;

            if (reAttackTime >= reAttackInterval)
            {
                reAttackTime = 0.0f;
                await BaseAttack();
            }
        }
    }

    async UniTask BaseAttack()
    {
        // 上から下へ降る自機外し大弾
        float x_pos = playerobj.transform.position.x; // 左右の出現位置は自機に依存するようにする
        float y_pos = 4.0f; // 上下の出現位置は固定
        GameObject nolmar;
        for (int i = 0; i < 4; i++)
        {
            if (cancelflag) break;

            nolmar = Instantiate(bigbullet_01,
                new Vector2(x_pos + bigbullet_01_pos_x[shottingpatten,i], y_pos), Quaternion.identity);

            nolmar.GetComponent<EnemyBullets>().getenemybulletVec = Vector2.down; // ベクトルを下方向

            nolmar.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

            await UniTask.Delay(300);
        }

        shottingpatten += 1; // 弾の出現パターンを変更する
        // 出現パターンが一周したら1パターン目に戻る
        if (shottingpatten >= 3)
        {
            shottingpatten = 0;
        }

    }
    /// <summary>
    /// 弾の出現位置のテンプレを設定する
    /// </summary>
    private void BulletpositionPreseter()
    {
        bigbullet_01_pos_x = new float[3, 4] {
            { 1.0f, 0.0f, 2.0f, -1.0f },
            { -1.0f, 1.0f, -2.0f, 2.0f },
            { 0.0f, -2.0f, 1.0f, -1.0f },
        };
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShots"))
        {
            mid_boss_hp -= other.GetComponent<PlayerAttackAmounts>().damageAmount;
        }
    }
    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            mid_boss_moveX_01?.Kill();
            mid_boss_moveY_01?.Kill();
            spwanMove?.Kill();
        }
    }
}
