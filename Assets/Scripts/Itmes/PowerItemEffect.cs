using UnityEngine;
// パワーを増やすアイテムのスクリプト

public class PowerItemEffect : MonoBehaviour
{
    [SerializeField] public float powerAmount; // 取ると取得できるパワーの量
    private float itemspeed = 3.0f; // アイテムの落下速度
    private Rigidbody2D itemrigidbody; // アイテムのRigidbody
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        itemrigidbody.linearVelocity = new Vector2(0.0f, -itemspeed);

        if (itemrigidbody.transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
