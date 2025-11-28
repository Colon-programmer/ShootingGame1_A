using UnityEngine;
using UnityEngine.SceneManagement;
// 自機のやられ判定のスクリプト

public class PlayerDeadPoint : MonoBehaviour
{
    [SerializeField] private GameObject playerobj;

    public bool deadflag = false;

    private bool gameoverflag = false; // ゲームオーバーフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
