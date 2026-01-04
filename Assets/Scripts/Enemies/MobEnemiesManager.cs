using UnityEngine;
// 雑魚敵のステータスを管理するスクリプト

public class MobEnemiesManager : MonoBehaviour
{
    public int enemyHp; // 雑魚敵の体力

    protected float appearanceArea = 5.5f; // 雑魚敵の出現範囲

    [SerializeField] private GameObject dropitme; // 倒した時に落とすアイテム
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHp = 1; // 生成時に値を受け取りますが念のため0以上の初期値を設定
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= appearanceArea ||
            this.transform.position.x <= -appearanceArea ||
            this.transform.position.y >= appearanceArea ||
            this.transform.position.y <= -appearanceArea)
        {
            EnemyDestrol();
        }
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
            enemyHp -= other.GetComponent<PlayerAttackAmounts>().damageAmount;
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
    }
}
