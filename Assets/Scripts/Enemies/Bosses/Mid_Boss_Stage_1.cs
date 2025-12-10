using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        // 弾の出現位置の初期化
        BulletpositionPreseter();

        //GameObjectが破棄された時にキャンセルを飛ばすトークンを作成
        var token = this.GetCancellationTokenOnDestroy();

        mid_boss_rb_01 = GetComponent<Rigidbody2D>();

        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        spwanMove = DOTween.Sequence();
        mid_boss_moveX_01 = transform.DOMoveX(this.transform.position.x - 5.5f, 0.5f);
        mid_boss_moveY_01 = transform.DOMoveY(this.transform.position.y - 2f, 0.5f);

        spwanMove.Join(mid_boss_moveX_01);
        spwanMove.Join(mid_boss_moveY_01);

        await UniTask.Delay(500);

        await BaseAttack();

        await UniTask.Delay(500);

        await BaseAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async UniTask BaseAttack()
    {
        // 上から下へ降る自機外し大弾
        float x_pos = playerobj.transform.position.x; // 左右の出現位置は自機に依存するようにする
        float y_pos = 4.0f; // 上下の出現位置は固定
        GameObject nolmar;
        for (int i = 0; i < 4; i++)
        {
            nolmar = Instantiate(bigbullet_01, new Vector2(x_pos + bigbullet_01_pos_x[0,i], y_pos), Quaternion.identity);

            nolmar.GetComponent<EnemyBullets>().getenemybulletVec = Vector2.down; // ベクトルを下方向

            nolmar.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

            await UniTask.Delay(300);
        }
    }
    /// <summary>
    /// 弾の出現位置のテンプレを設定する
    /// </summary>
    private void BulletpositionPreseter()
    {
        bigbullet_01_pos_x = new float[3, 4] {
            { 1.0f, -2.0f, 2.0f, -1.0f },
            { -1.0f, 1.0f, -2.0f, 2.0f },
            { 2.0f, -2.0f, 1.0f, -1.0f },
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
