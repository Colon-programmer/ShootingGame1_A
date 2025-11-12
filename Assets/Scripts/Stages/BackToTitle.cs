using UnityEngine;
using UnityEngine.SceneManagement;
// タイトルに戻るスクリプト

public class BackToTitle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pキーでタイトルに戻る
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
