using UnityEngine;
// 自機のメインショットの性能を管理をするスクリプト

public class PlayerMeinBullet : MonoBehaviour
{
    Rigidbody2D meinshotRb; // メインショットのRigidbody
    float meinshotspeed = 20.0f; // メインショットの弾速

    int DamageAmount = 50; // ショットのダメージ量
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meinshotRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        meinshotRb.AddForce(transform.up * meinshotspeed, ForceMode2D.Impulse);
        if (meinshotRb.linearVelocity.magnitude > meinshotspeed)
        {
            meinshotRb.linearVelocity = meinshotRb.linearVelocity.normalized * meinshotspeed;
        }
        if (meinshotRb.transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 敵にヒットした時にヒットした弾を消す
        if (col.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
