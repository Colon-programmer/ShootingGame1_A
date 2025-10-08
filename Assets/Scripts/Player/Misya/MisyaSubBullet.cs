using UnityEngine;

public class MisyaSubBullet : MonoBehaviour
{
    public enum SubBullets
    {
        Fast = 0,
        Slow = 1
    }

    Rigidbody2D subshotRb; // サブショットのRigidbody
    [SerializeField] private SubBullets subbullets;
    float fastsubshotspeed = 15.0f; // 高速時サブショットの弾速
    float slowsubshotspeed = 30.0f; // 低速時サブショットの弾速
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subshotRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 高速時
        if (subbullets == SubBullets.Fast)
        {
            subshotRb.AddForce(transform.up * fastsubshotspeed, ForceMode2D.Impulse);
            if (subshotRb.linearVelocity.magnitude > fastsubshotspeed)
            {
                subshotRb.linearVelocity = subshotRb.linearVelocity.normalized * fastsubshotspeed;
            }
            if (subshotRb.transform.position.y >= 6.0f)
            {
                Destroy(this.gameObject);
            }
        }
        // 低速時
        if (subbullets == SubBullets.Slow)
        {
            subshotRb.AddForce(transform.up * slowsubshotspeed, ForceMode2D.Impulse);
            if (subshotRb.linearVelocity.magnitude > slowsubshotspeed)
            {
                subshotRb.linearVelocity = subshotRb.linearVelocity.normalized * slowsubshotspeed;
            }
            if (subshotRb.transform.position.y >= 6.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
