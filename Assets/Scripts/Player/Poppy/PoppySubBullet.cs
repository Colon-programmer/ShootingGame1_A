using UnityEngine;
// ポピーを選択した時の自機のサブショットの性能を管理をするスクリプト

public class PoppySubBullet : MonoBehaviour
{
    private Rigidbody2D subshotRb; // サブショットのRigidbody
    private float subshotspeed = 5.0f; // サブショットの弾速
    private float maxspeed = 40.0f; // サブショットの最高速
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subshotRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        subshotRb.AddForce(transform.up * subshotspeed, ForceMode2D.Force);
        if (subshotRb.linearVelocity.magnitude > maxspeed)
        {
            subshotRb.linearVelocity = subshotRb.linearVelocity.normalized * maxspeed;
        }
        if (subshotRb.transform.position.y >= 6.0f)
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
