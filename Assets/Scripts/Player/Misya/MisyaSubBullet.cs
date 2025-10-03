using UnityEngine;

public class MisyaSubBullet : MonoBehaviour
{
    Rigidbody2D subshotRb; // サブショットのRigidbody
    float subshotspeed = 15.0f; // サブショットの弾速
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subshotRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        subshotRb.linearVelocity = new Vector2(0, subshotspeed);
        if (subshotRb.transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
