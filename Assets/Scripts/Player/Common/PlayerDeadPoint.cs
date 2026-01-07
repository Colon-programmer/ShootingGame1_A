using UnityEngine;
using UnityEngine.SceneManagement;
// 自機のやられ判定のスクリプト
// やられ判定にアイテムが触れるとそのアイテムを取得できる

public class PlayerDeadPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerobj;

    private bool deadflag = false; // trueなら攻撃や敵に触れてもやられなくなるフラグ
    private bool deadmotionflag = false; // 死亡後一定時間は動かせないようにするフラグ

    private bool gameoverflag = false; // ゲームオーバーフラグ

    private float invincibleTime = 0.0f; // 無敵になる秒数
    private float invincebleInterval = 3.0f; // 無敵時間を測る変数

    private float invincibleColorTime = 0.0f;
    private float invincibleColorInterval = 0.05f;
    private bool normalcolorflag = true;

    [SerializeField] private SpriteRenderer playerSprite;

    private GameObject playermanager; // シングルトンを持ったオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // シングルトンを取得
        playermanager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        // 自機がやられたとき
        if (deadflag)
        {
            invincibleTime += Time.deltaTime; // 無敵時間を測る
            invincibleColorTime += Time.deltaTime;
            // 無敵中に自機の色を点滅させる
            if (invincibleColorTime >= invincibleColorInterval)
            {
                // フラグを反転させることで自機の色を変更させる
                normalcolorflag = normalcolorflag ? false : true;
                invincibleColorTime = 0.0f;
            }
            // 時間が経ったら無敵を解除する
            if (invincibleTime >= invincebleInterval)
            {
                deadflag = false;
                invincibleTime = 0.0f;
                normalcolorflag = true; // 自機の色を戻す
            }
            InvicibleEffect();
        }
    }
    /// <summary>
    /// 自機の無敵演出の関数
    /// </summary>
    private void InvicibleEffect()
    {
        // 死亡から復活するまでは自機を見えなくする
        if (deadmotionflag)
        {
            playerSprite.color = new Color32(255, 255, 255, 0);
        }
        // 普通の色と無敵の色を繰り返し変更して無敵を演出する
        else if (normalcolorflag && !deadmotionflag)
        {
            playerSprite.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            playerSprite.color = new Color32(0, 255, 255, 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // パワーアイテムかどうかを調べる
        if (col.gameObject.TryGetComponent(out PowerItemEffect pItem))
        {
            // 現在のパワーを取得する
            int numpower = playerobj.GetComponent<PlayerController>().getshotPower;
            // パワーを加算する
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
            // プレイヤーの操作スクリプトにパワーの値の変更を反映させる
            playerobj.GetComponent<PlayerController>().getshotPower = numpower;
        }
        if (col.gameObject.TryGetComponent(out ScoreItemEffect sItem))
        {
            // 現在のスコアを取得する
            int numscore = playermanager.GetComponent<PlayerManager>().scorenum;
            // スコアを加算する
            numscore = numscore + sItem.scoreAmount;
            // 獲得したアイテムを消滅させる
            Destroy(col.gameObject);
            // シングルトンにスコアの変更を反映させる
            playermanager.GetComponent<PlayerManager>().scorenum = numscore;
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
