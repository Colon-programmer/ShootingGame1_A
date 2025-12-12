using UnityEngine;
using UnityEngine.SceneManagement;
// 自機のやられ判定のスクリプト

public class PlayerDeadPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerobj;

    private bool deadflag = false; // trueなら攻撃や敵に触れてもやられなくなるフラグ
    private bool deadmotionflag = false; // 死亡後一定時間は動かせないようにするフラグ

    private bool gameoverflag = false; // ゲームオーバーフラグ

    private float invincibleTime = 0.0f; // 無敵になる秒数
    private float invincebleInterval = 3.0f; // 無敵時間を測る変数

    private float invincibleColorTime = 0.0f;
    private float invincibleColorInterval = 0.2f;
    private bool normalcolor = true;

    [SerializeField] private SpriteRenderer playerSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 自機がやられたとき
        if (deadflag)
        {
            invincibleTime += Time.deltaTime; // 無敵時間を測る
            invincibleColorTime += Time.deltaTime;
            // 時間が経ったら無敵を解除する
            if (invincibleTime >= invincebleInterval)
            {
                deadflag = false;
                invincibleTime = 0.0f;
            }
            if (invincibleColorTime >= invincibleColorInterval)
            {
                normalcolor = normalcolor ? false : true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // パワーアイテムかどうかを調べる
        if (col.gameObject.TryGetComponent(out PowerItemEffect pItem))
        {
            // パワーを取得する
            int numpower = playerobj.GetComponent<PlayerController>().getshotPower;
            // パワーを増やす
            numpower = numpower + pItem.powerAmount;
            // パワーが上限値を超えないようにする
            if (numpower > playerobj.GetComponent<PlayerController>().getshotMaxPower)
            {
                numpower = playerobj.GetComponent<PlayerController>().getshotMaxPower;
            }
            // パワーの表示を変える
            playerobj.GetComponent<PlayerController>().
                getitemDisplayer.GetComponent<ItemNumManager>().getpowerNum = numpower;
            // 獲得したアイテムを消滅させる
            Destroy(col.gameObject);
            playerobj.GetComponent<PlayerController>().getshotPower = numpower;
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        // 無敵でない時
        if (!deadflag)
        {
            // 敵にぶつかった時または敵の攻撃に当たった時
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyShots")
            {
                // 残機が無いときにやられるとゲームオーバー
                if (playerobj.GetComponent<PlayerController>().getlifeNum <= 0)
                {
                    deadflag = true;
                    deadmotionflag = true;
                    SceneManager.LoadScene("TitleScene");
                }
                // まだ残機が残っている時は残機を減らす
                else
                {
                    deadflag = true;
                    deadmotionflag = true;
                    playerobj.GetComponent<PlayerController>().getlifeNum -= 1;
                }
            }
        }
    }

    public bool getdeadmotionflag
    {
        get { return this.deadmotionflag; }
        set { this.deadmotionflag = value; }
    }
}
