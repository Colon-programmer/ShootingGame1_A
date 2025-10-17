using UnityEngine;

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
    void Update()
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
}
