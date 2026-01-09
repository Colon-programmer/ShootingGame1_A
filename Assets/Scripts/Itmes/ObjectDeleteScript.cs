using UnityEngine;
// 出現してから一定時間でオブジェクトを消すスクリプト

public class ObjectDeleteScript : MonoBehaviour
{
    [SerializeField] private float destroyTime; // インスペクターで消えるまでの猶予を設定する
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
