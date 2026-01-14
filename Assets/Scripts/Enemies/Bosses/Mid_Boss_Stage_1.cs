using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine.SceneManagement;
// ステージ1の中ボスのスクリプト

public class Mid_Boss_Stage_1 : Mid_BossManager
{
    // 弾の動きのTween
    private Tween mid_boss_moveX_01;
    private Tween mid_boss_moveY_01;

    [SerializeField] private GameObject bigbullet_01; // 大きい赤弾
    [SerializeField] private GameObject bigSearchbullet;
    [SerializeField] private GameObject shooterobj; // 扇形弾幕の発射オブジェクト

    private float[,] bigbullet_01_pos_x; // 大きい赤弾の出現位置のテンプレート

    private float[,] bigbullet_01_vec; // 大きい赤弾のベクトルのテンプレート

    //[SerializeField] private GameObject scoreItme; // スコアアイテム

    [SerializeField] private GameObject shoteraserobj; // 全画面弾消しオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        // CancellationTokenSourceの生成  
        token = new CancellationTokenSource();

        // CancellationTokenをCancellationTokenSourceから取得  
        canceler = token.Token;

        // 弾の出現位置の初期化
        BulletpositionPreseter();

        mid_boss_rb = GetComponent<Rigidbody2D>();

        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        spwanMove = DOTween.Sequence();
        mid_boss_moveX_01 = transform.DOMoveX(this.transform.position.x - 5.5f, 0.5f);
        mid_boss_moveY_01 = transform.DOMoveY(this.transform.position.y - 2f, 0.5f);

        spwanMove.Join(mid_boss_moveX_01);
        spwanMove.Join(mid_boss_moveY_01);

        reAttackInterval = 1.5f;

        mid_boss_maxhp = mid_boss_hp;
    }

    private void Update()
    {
        if (mid_boss_hp <= 0)
        {
            switch (mid_boss_patten)
            {
                case 2:
                    token.Cancel(); // 攻撃をキャンセル
                    mid_boss_patten -= 1; // 攻撃パターンを変える
                    reAttackTime = 0.0f; // 攻撃のインターバルをリセットする
                    reAttackInterval = 1.0f; // 攻撃間隔を変える
                    mid_boss_hp = 5000; // 新しい体力を設定する
                    mid_boss_maxhp = mid_boss_hp; // 新しく設定した体力を最大体力として設定する
                    scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
                    Instantiate(shoteraserobj, new Vector2(0, 0), Quaternion.identity);
                    break;
                case 1:
                    cancelflag = true;
                    scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
                    SceneManager.LoadScene("StageClear");
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    async void FixedUpdate()
    {
        if (!cancelflag)
        {
            switch (mid_boss_patten)
            {
                case 2:
                    reAttackTime += Time.deltaTime;

                    if (reAttackTime >= reAttackInterval)
                    {
                        reAttackTime = 0.0f;
                        await BaseAttack(canceler);
                    }
                    break;
                case 1:
                    reAttackTime += Time.deltaTime;

                    if (reAttackTime >= reAttackInterval)
                    {
                        reAttackTime = 0.0f;
                        BaseAttack_02();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    async UniTask BaseAttack(CancellationToken cancal)
    {
        // 上から下へ降る自機外し大弾
        float x_pos = playerobj.transform.position.x; // 左右の出現位置は自機に依存するようにする
        float y_pos = 4.0f; // 上下の出現位置は固定
        GameObject nolmar;
        for (int i = 0; i < 4; i++)
        {
            // キャンセルトークンの状態を見る
            if (cancal.IsCancellationRequested)
            {
                return;
            }
            else
            {
                nolmar = Instantiate(bigbullet_01,
    new Vector2(x_pos + bigbullet_01_pos_x[shottingpatten, i], y_pos), Quaternion.identity);

                nolmar.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
                nolmar.GetComponent<EnemyBullets>().getenemybulletangle = -90.0f; // ベクトルを下方向
                nolmar.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

                await UniTask.Delay(300);
            }
        }

        shottingpatten += 1; // 弾の出現パターンを変更する
        // 出現パターンが一周したら1パターン目に戻る
        if (shottingpatten >= 3)
        {
            shottingpatten = 0;
        }

    }

    void BaseAttack_02()
    {
        GameObject nolmar;

        nolmar = Instantiate(shooterobj, this.transform.position, Quaternion.identity);

        nolmar.GetComponent<SearchShooter>().getshotpattren = 2;
        nolmar.GetComponent<SearchShooter>().getenemybullet = bigbullet_01;
        nolmar.GetComponent<SearchShooter>().getfanshapecount = 2;
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

        bigbullet_01_vec = new float[5, 2]{
            {-0.4f, -0.6f },
            {-0.2f, -0.8f },
            {0.0f, -1.0f },
            {0.2f, -0.8f },
            {0.4f, -0.6f },
        };
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
