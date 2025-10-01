using UnityEngine;
using UnityEngine.UI;

public class PlayerMeinShots : MonoBehaviour
{
    Rigidbody2D meinshotRb; // メインショットのRigidbody
    float meinshotspeed = 30.0f; // メインショットの弾速
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meinshotRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        meinshotRb.linearVelocity = new Vector2(0, meinshotspeed);
        if (meinshotRb.transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
