using UnityEngine;
// 雑魚敵のステータスを管理するスクリプト

public class MobEnemiesManager : MonoBehaviour
{
    protected int enemyHp; // 雑魚敵の体力

    protected float appearanceArea = 5.5f; // 雑魚敵の出現範囲
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    void EnemyDestrol()
    {
        Destroy(this.gameObject);
    }
}
