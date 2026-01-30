using UnityEngine;
using DG.Tweening;
using System.Collections;
// コウモリ雑魚の行動パターンのスクリプト
public class BatsMovePattern : EnemyMoveManager
{
    private Vector3 batsVec; // コウモリ雑魚の座標

    private Sequence batsPattern; // コウモリ雑魚の行動シーケンス

    private Tween moveX_01;
    private Tween moveY_01;

    private Tween move_tween;

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
            case 3:
                StartCoroutine("BatsMoving_03");
                break;
            case 4:
                StartCoroutine("BatsMoving_04");
                break;
            case 5:
                StartCoroutine("BatsMoving_05");
                break;
            case 6:
                StartCoroutine("BatsMoving_06");
                break;
            default:
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

    IEnumerator BatsMoving_03()
    {
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 1f);

        yield return new WaitForSeconds(1.1f);

        BatsAttack_01();

        yield return new WaitForSeconds(0.1f);

        moveX_01 = transform.DOMoveX(-6f, 3f);

        if (this.transform.position.x <= -5.1f)
        {
            MobDelete();
        }
    }

    IEnumerator BatsMoving_04()
    {
        moveY_01 = transform.DOMoveY(batsVec.y - 1f, 1f);

        yield return new WaitForSeconds(1.1f);

        BatsAttack_01();

        yield return new WaitForSeconds(0.1f);

        moveX_01 = transform.DOMoveX(6f, 3f);
        if (this.transform.position.x >= 5.1f)
        {
            move_tween.Kill();
            MobDelete();
        }
    }

    IEnumerator BatsMoving_05()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x + 14f, 4.0f);
        moveY_01 = transform.DOMoveY(batsVec.y - 12f, 4.0f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(MobDelete);

        yield return new WaitForSeconds(0.5f);

        BatsAttack_02();

        yield return new WaitForSeconds(1.0f);

        BatsAttack_02();
    }

    IEnumerator BatsMoving_06()
    {
        batsPattern = DOTween.Sequence();
        moveX_01 = transform.DOMoveX(batsVec.x - 14f, 4.0f);
        moveY_01 = transform.DOMoveY(batsVec.y - 12f, 4.0f);

        batsPattern.Join(moveX_01);
        batsPattern.Join(moveY_01);

        batsPattern.OnComplete(MobDelete);

        yield return new WaitForSeconds(0.5f);

        BatsAttack_02();

        yield return new WaitForSeconds(1.0f);

        BatsAttack_02();
    }

    void BatsAttack_01()
    {
        GameObject shot_1 = Instantiate(shooterobj, this.transform.position, Quaternion.identity);

        shot_1.GetComponent<SearchShooter>().NormalSearchShot(red_bullet, 4.0f, 5, 10);
    }

    void BatsAttack_02()
    {
        GameObject shot_02 = Instantiate(shooterobj, this.transform.position, Quaternion.identity);

        shot_02.GetComponent<SearchShooter>().FanShapeSearchShot(red_bullet, 4.0f, 1, 15.0f, 1, 1);
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
            move_tween?.Kill();
        }
    }
}
