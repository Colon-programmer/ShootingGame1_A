using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine.SceneManagement;
// ステージ1のステージボスのスクリプト

public class Big_Boss_Stage_1 : Big_BossManager
{
    private Sequence spwanMove; // スポーンしてすぐの移動シーケンス
    private Tween big_boss_moveX_01;
    private Tween big_boss_moveY_01;

    private CancellationToken canceler;

    private CancellationTokenSource token;

    [SerializeField] private GameObject bullet_01; // 小さい赤弾
    [SerializeField] private GameObject bigbullet_01; // 大きい赤弾

    [SerializeField] private GameObject shooterobj; // 扇形弾幕の発射オブジェクト

    private float[,] bigbullet_01_pos_x; // 大きい赤弾の出現位置のテンプレート
    public override void Start()
    {
        base.Start();

        // CancellationTokenSourceの生成  
        token = new CancellationTokenSource();

        // CancellationTokenをCancellationTokenSourceから取得  
        canceler = token.Token;

        // 弾の出現位置の初期化
        BulletpositionPreseter();

        big_boss_maxhp = big_boss_hp;

        spwanMove = DOTween.Sequence();
        big_boss_moveX_01 = transform.DOMoveX(this.transform.position.x - 5f, 0.5f);
        big_boss_moveY_01 = transform.DOMoveY(this.transform.position.y - 2f, 0.5f);

        spwanMove.Join(big_boss_moveX_01);
        spwanMove.Join(big_boss_moveY_01);

        reAttackInterval = 1.5f;
    }

    private void Update()
    {
        if (big_boss_hp <= 0)
        {
            // 攻撃パターン切り替え時に敵弾を全て消す
            Instantiate(shotEraserobj, new Vector2(0, 0), Quaternion.identity);
            switch (big_boss_patten)
            {
                //case 4:
                //    big_boss_patten -= 1; // 攻撃パターンを変える
                //    big_boss_hp = 5000; // 新しい体力を設定する
                //    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                //    break;
                //case 3:
                //    big_boss_patten -= 1; // 攻撃パターンを変える
                //    big_boss_hp = 3000; // 新しい体力を設定する
                //    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                //    break;
                case 2:
                    token.Cancel(); // 攻撃をキャンセル
                    big_boss_patten -= 1; // 攻撃パターンを変える
                    reAttackTime = 0.0f; // 攻撃のインターバルをリセットする
                    reAttackInterval = 0.5f; // 攻撃間隔を変える
                    big_boss_hp = 5000; // 新しい体力を設定する
                    big_boss_maxhp = big_boss_hp; // 新しく設定した体力を最大体力として設定する
                    scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
                    break;
                case 1:
                    scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
                    enemySpawnobj.GetComponent<EnemySpawnManager>().gettimercountstoper = false;
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
        switch (big_boss_patten)
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

                GameObject nolmar_02;

                nolmar_02 = Instantiate(shooterobj,
                    new Vector2(x_pos + bigbullet_01_pos_x[shottingpatten, i], y_pos),
                    Quaternion.identity);
                nolmar_02.GetComponent<SearchShooter>().FanShapeSearchShot(bullet_01, 5.0f, 1, 20.0f, 1, 1);

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

        nolmar.GetComponent<SearchShooter>().FanShapeSearchShot(bigbullet_01, 4.0f, 2, 20.0f, 1, 1);
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

    private void Stage_01_Clear()
    {
        SceneManager.LoadScene("StageClear");
    }

    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            big_boss_moveX_01?.Kill();
            big_boss_moveY_01?.Kill();
            spwanMove?.Kill();
        }
    }
}
