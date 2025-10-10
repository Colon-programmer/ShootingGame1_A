using UnityEngine;
// 雑魚敵のステータスを管理するスクリプト

public class MobEnemiesManager : MonoBehaviour
{
    protected int enemyHp; // 雑魚敵の体力

    protected float appearanceTime; // 雑魚敵の出現時間(0になると自動で消えるようにする)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShots"))
        {
            enemyHp -= other.GetComponent<PlayerAttackAmounts>().damageAmount;
        }
    }
}
