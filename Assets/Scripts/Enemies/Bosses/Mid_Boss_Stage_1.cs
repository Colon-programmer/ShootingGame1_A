using UnityEngine;
using System.Collections;
using DG.Tweening;
// ステージ1の中ボスのスクリプト

public class Mid_Boss_Stage_1 : MonoBehaviour
{
    private Rigidbody2D mid_boss_rb_01;

    private Sequence spwanMove; // スポーンしてすぐの移動シーケンス

    private Tween mid_boss_moveX_01;
    private Tween mid_boss_moveY_01;

    [SerializeField] private GameObject smallbullet_01;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mid_boss_rb_01 = GetComponent<Rigidbody2D>();

        spwanMove = DOTween.Sequence();
        mid_boss_moveX_01 = transform.DOMoveX(this.transform.position.x - 5.5f, 0.5f);
        mid_boss_moveY_01 = transform.DOMoveY(this.transform.position.y - 2f, 0.5f);

        spwanMove.Join(mid_boss_moveX_01);
        spwanMove.Join(mid_boss_moveY_01);

        spwanMove.OnComplete(BaseAttack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BaseAttack()
    {
        float x = -2.5f;
        float y = -0.5f;
        GameObject nolmar;
        for (int i = 0; i < 10; i++)
        {
            nolmar = Instantiate(smallbullet_01, new Vector2(x, y), Quaternion.identity);

            nolmar.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

            x += 0.5f;
            y += 0.1f;
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
