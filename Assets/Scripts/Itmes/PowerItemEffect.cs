using UnityEngine;
// パワーを増やすアイテムのスクリプト

public class PowerItemEffect : MonoBehaviour
{
    [SerializeField] public int powerAmount; // 取ると取得できるパワーの量
    private float itemspeed = 3.0f; // アイテムの落下速度
    private float maxitemspeed = 6.0f; // 自機に吸い込まれる時の最大速度
    private Rigidbody2D itemrigidbody; // アイテムのRigidbody
    private bool changemove = false;
    private GameObject playerposition; // 自機の位置
    private Vector2 itemVec; // アイテムの移動方向
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemrigidbody = GetComponent<Rigidbody2D>();
        // 自機を探す
        playerposition = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (changemove)
        {
            // 自機の位置から移動方向を決める
            itemVec = (playerposition.transform.position - this.transform.position);
            // 向いている方向に移動する
            itemrigidbody.AddForce(itemVec * itemspeed, ForceMode2D.Impulse);
            if (itemrigidbody.linearVelocity.magnitude > maxitemspeed)
            {
                itemrigidbody.linearVelocity = itemrigidbody.linearVelocity.normalized * maxitemspeed;
            }

        }
        else
        {
            itemrigidbody.linearVelocity = new Vector2(0.0f, -itemspeed);

            if (itemrigidbody.transform.position.y <= -5.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 自機周辺の判定に触れると自機に吸い込まれる状態になる
        if (col.CompareTag("ItemArea"))
        {
            changemove = true;
            itemrigidbody.linearVelocity = Vector2.zero;
        }
    }
}
