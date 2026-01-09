using UnityEngine;
using UnityEngine.SceneManagement;
// リザルト画面のスクリプト

public class ResultScene : MonoBehaviour
{
    [SerializeField] private UIInputSystem inputUI; // 入力を受け取るスクリプト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputUI.nextAction.triggered)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
