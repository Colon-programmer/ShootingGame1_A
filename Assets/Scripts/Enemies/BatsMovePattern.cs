using UnityEngine;
using DG.Tweening;
// コウモリ雑魚の行動パターンのスクリプト
public class BatsMovePattern : EnemyMoveManager
{
    private Sequence batsPattern; // コウモリ雑魚の行動シーケンス

    private Tween moveX_01;
    private Tween moveY_01;

    public override void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(transform.position.x + 1f, 0.5f);
        moveY_01 = transform.DOMoveY(transform.position.y - 1f, 0.5f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(BatsAttack_01);
    }

    public override void FixedUpdate()
    {
        
    }

    void BatsAttack_01()
    {
        GameObject bullets_01 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

        bullets_01.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;

        GameObject bullets_02 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

        bullets_02.GetComponent<EnemyBullets>().getenemybulletspeed = 3.0f;
    }

    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            moveX_01?.Kill();
            moveY_01?.Kill();
        }
    }
}
