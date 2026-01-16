using UnityEngine;
// 雑魚敵のステータスを管理するスクリプト

public class MobEnemiesManager : MonoBehaviour
{
    public int enemyHp; // 雑魚敵の体力

    private GameObject dropitme; // 倒した時に落とすアイテム

    private GameObject scorecountobj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // スコアカウンターを取得
        scorecountobj = GameObject.Find("ScoreCounterObject");

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHp <= 0)
        {
            // アイテムを落とす
            Instantiate(dropitme, this.transform.position, Quaternion.identity);
            EnemyDestrol();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShots"))
        {
            int d_a = other.GetComponent<PlayerAttackAmounts>().damageAmount;

            enemyHp -= d_a;
            scorecountobj.GetComponent<ScoreGetter>().getscore += d_a;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Bomb"))
        {
            enemyHp -= col.GetComponent<PlayerAttackAmounts>().damageAmount;
        }
    }

    void EnemyDestrol()
    {
        Destroy(this.gameObject);

        scorecountobj.GetComponent<ScoreGetter>().getscore += 100;
    }

    public GameObject getdropitme
    {
        get { return this.dropitme; }
        set { this.dropitme = value; }
    }
}
