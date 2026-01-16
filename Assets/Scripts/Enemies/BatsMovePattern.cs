using UnityEngine;
using DG.Tweening;
// コウモリ雑魚の行動パターンのスクリプト
public class BatsMovePattern : EnemyMoveManager
{
    private Vector3 batsVec; // コウモリ雑魚の座標

    private Sequence batsPattern; // コウモリ雑魚の行動シーケンス

    private Tween moveX_01;
    private Tween moveY_01;

    private Tween moveY_02;

    [SerializeField] private GameObject shooterobj;

    [SerializeField] private GameObject red_bullet;

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

    void BatsMoving_01()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x + 1f, 0.5f);
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 0.5f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(BatsAttack_01);

        batsVec = this.transform.position;
        moveY_02 = transform.DOMoveY(batsVec.y + 1f, 1.0f).SetDelay(3);

        moveY_02.OnComplete(MobDelete);
    }

    void BatsMoving_02()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x - 1f, 0.5f);
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 0.5f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(BatsAttack_01);

        batsVec = this.transform.position;
        moveY_02 = transform.DOMoveY(batsVec.y + 1f, 1.0f).SetDelay(3);

        moveY_02.OnComplete(MobDelete);
    }

    void BatsAttack_01()
    {
        GameObject shot_1 = Instantiate(shooterobj, this.transform.position, Quaternion.identity);

        shot_1.GetComponent<SearchShooter>().NormalSearchShot(red_bullet, 4.0f, 5, 10);
    }

    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            //?.Kill();(テンプレ)
            moveX_01?.Kill();
            moveY_01?.Kill();
            batsPattern?.Kill();
            moveY_02?.Kill();
        }
    }
}
