using UnityEngine;
using UnityEngine.SceneManagement;
// 自機のやられ判定のスクリプト

public class PlayerDeadPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerobj;

    public bool deadflag = false;

    private bool gameoverflag = false; // ゲームオーバーフラグ

    private long shotPowerlong; // パワーの固定小数点

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log(playerobj.GetComponent<PlayerController>().getshotPower);

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
                    SceneManager.LoadScene("TitleScene");
                }
                // まだ残機が残っている時は残機を減らす
                else
                {
                    deadflag = true;
                    playerobj.GetComponent<PlayerController>().getlifeNum -= 1;

                }
            }
        }
    }
}
