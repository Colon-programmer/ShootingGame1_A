using UnityEngine;
using DG.Tweening;
// コウモリ雑魚の行動パターンのスクリプト
public class BatsMovePattern : EnemyMoveManager
{
    private Vector3 batsVec; // コウモリ雑魚の座標

    private Sequence batsPattern; // コウモリ雑魚の行動シーケンス

    private Tween moveX_01;
    private Tween moveY_01;

    public override void Start()
    {
        // 自機を探す
        playerobj = GameObject.FindWithTag("Player");

        enemyRb = GetComponent<Rigidbody2D>();
        batsVec = this.transform.position;
        switch (movepattern)
        {
            case 1:
                BatsMoving_01();
                break;
            case 2:
                BatsMoving_02();
                break;
        }
    }

    public override void FixedUpdate()
    {
        
    }

    void BatsMoving_01()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x + 1f, 0.5f);
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 0.5f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(BatsAttack_01);
    }

    void BatsMoving_02()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x - 1f, 0.5f);
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 0.5f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(BatsAttack_01);
    }

    void BatsAttack_01()
    {
        GameObject bullets_01 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

        bullets_01.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
        bullets_01.GetComponent<EnemyBullets>().getenemybulletspeed = 4.0f;
        // 自機の位置から移動方向を決める
        bullets_01.GetComponent<EnemyBullets>().getenemybulletVec = playerobj.transform.position - this.transform.position;


        GameObject bullets_02 = Instantiate(enemybullet, this.transform.position, Quaternion.identity);

        bullets_02.GetComponent<EnemyBullets>().getenemyBulletType = EnemyBullets.BulletType.STRAIGHT;
        bullets_02.GetComponent<EnemyBullets>().getenemybulletspeed = 3.0f;
        // 自機の位置から移動方向を決める
        bullets_02.GetComponent<EnemyBullets>().getenemybulletVec = playerobj.transform.position - this.transform.position;

    }

    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            moveX_01?.Kill();
            moveY_01?.Kill();
            batsPattern?.Kill();
        }
    }
}
