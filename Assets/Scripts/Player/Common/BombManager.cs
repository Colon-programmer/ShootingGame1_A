using UnityEngine;
// ボムの挙動を管理するスクリプト

public class BombManager : MonoBehaviour
{
    /*円形の判定が真上方向に進むような挙動のボム*/

    Rigidbody2D bombRb; // ボムのRigidbody
    float bombspeed = 1.0f; // ボムの移動速度

    float bombtime = 2.5f; // ボムの持続時間
    float bombcount; // ボムが消えるまでの時間をカウントする
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bombRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bombRb.AddForce(transform.up * bombspeed, ForceMode2D.Impulse);
        if (bombRb.linearVelocity.magnitude > bombspeed)
        {
            bombRb.linearVelocity = bombRb.linearVelocity.normalized * bombspeed;
        }

        bombcount += Time.deltaTime;
        // 持続時間が過ぎたら消える
        if (bombcount >= bombtime)
        {
            Destroy(this.gameObject);
        }
    }
}
