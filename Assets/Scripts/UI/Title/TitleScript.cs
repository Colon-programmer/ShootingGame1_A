using UnityEngine;
using UnityEngine.UI;
using TMPro;
// タイトルでの操作の処理を管理するスクリプト

public class TitleScript : MonoBehaviour
{
    [SerializeField] private UIInputSystem inputUI; // 入力を受け取るスクリプト

    [SerializeField] private TextMeshProUGUI[] titleTexts; // タイトル画面で選べる選択肢

    [SerializeField] private Image selectFrame; // 今どれを選んでいるのかを表示する額縁

    private sbyte titleselectAmount; // 選択肢の数

    private sbyte selectNumber; // 今どれを選んでいるのかを指定する変数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleselectAmount = (sbyte)titleTexts.Length;

        selectFrame.rectTransform.localPosition = titleTexts[0].rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputUI.upAction.WasPressedThisFrame())
        {
            selectNumber -= 1;
            if (selectNumber < 0)
            {
                selectNumber = (sbyte)(titleselectAmount - 1);
            }
        }
        if (inputUI.downAction.WasPressedThisFrame())
        {
            selectNumber += 1;
            if (selectNumber > (sbyte)(titleselectAmount - 1))
            {
                selectNumber = 0;
            }
        }
        selectFrame.rectTransform.localPosition = titleTexts[selectNumber].rectTransform.localPosition;

    }
}
