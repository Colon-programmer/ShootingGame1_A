using UnityEngine;
// ゲーム全体の設定を管理するスクリプト

public class GameManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
