using UnityEngine;
using UnityEngine.UI;
using TMPro;
// タイトルでの操作の処理を管理するスクリプト

public class TitleScript : MonoBehaviour
{
    public enum Title
    {
        title,
        charaselect,
        setting
    }
    [SerializeField] private UIInputSystem inputUI; // 入力を受け取るスクリプト

    [SerializeField] private TextMeshProUGUI[] titleTexts; // タイトル画面で選べる選択肢
    [SerializeField] private Image titleselectFrame; // タイトル画面で今どれを選んでいるのかを表示する額縁
    private sbyte titleselectAmount; // タイトル画面での選択肢の数
    private sbyte titleselectNumber; // タイトル画面で今どれを選んでいるのかを指定する変数

    [SerializeField] private TextMeshProUGUI[] charaselects; // 自機の選択肢
    [SerializeField] private Image charaselectFrame; // 自機選択画面で今どれを選んでいるのかを表示する額縁
    private sbyte charaselectAmount; // 自機選択画面での選択肢の数
    private sbyte charaselectNumber; // 自機選択画面で今どれを選んでいるのかを指定する変数

    private Title selectSceneNum; // どの選択画面なのかを取得する変数

    [SerializeField] private TextMeshProUGUI[] settingselects; // 設定で選べる選択肢
    [SerializeField] private Image settingselectFrame; // 設定画面で今どれを選んでいるのかを表示する額縁
    private sbyte settingselectAmount; // 設定画面での選択肢の数
    private sbyte settingselectNumber; // 設定画面で今どれを選んでいるのかを指定する変数

    [SerializeField] private GameObject titleCanvas; // タイトル画面のキャンバス
    [SerializeField] private GameObject charaselectCanvas; // 自機選択画面のキャンバス
    [SerializeField] private GameObject setttingCanvas; // 設定画面のキャンバス

    private GameObject playerManager; // プレイヤーの情報を持つスクリプトをオブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        selectSceneNum = Title.title;
        CanvasDisplay(selectSceneNum);
        // 各画面に表示されている選択肢の数を取得する
        titleselectAmount = (sbyte)titleTexts.Length;
        charaselectAmount = (sbyte)charaselects.Length;
        settingselectAmount = (sbyte)settingselects.Length;
        // 最初に選ばれた状態にする項目を設定する
        titleselectFrame.rectTransform.localPosition = titleTexts[0].rectTransform.localPosition;
        titleselectNumber = 0;
        charaselectFrame.rectTransform.localPosition = charaselects[0].rectTransform.localPosition;
        charaselectNumber = 0;
        settingselectFrame.rectTransform.localPosition = settingselects[0].rectTransform.localPosition;
        settingselectNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CanvasDisplay(selectSceneNum);
        // タイトル画面での操作
        if (selectSceneNum == Title.title)
        {
            // 上方向ボタンを押されたとき
            if (inputUI.upAction.triggered)
            {
                titleselectNumber -= 1;
                if (titleselectNumber < 0)
                {
                    titleselectNumber = (sbyte)(titleselectAmount - 1);
                }
            }
            // 下方向ボタンを押されたとき
            if (inputUI.downAction.triggered)
            {
                titleselectNumber += 1;
                if (titleselectNumber > (sbyte)(titleselectAmount - 1))
                {
                    titleselectNumber = 0;
                }
            }
            if (inputUI.nextAction.triggered)
            {
                switch (titleselectNumber)
                {
                    case 0:
                        selectSceneNum = Title.charaselect;
                        break;
                    case 1:
                        selectSceneNum = Title.setting;
                        break;
                    case 2:
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false; // エディターの場合は再生を停止
#else
                        Application.Quit(); // スタンドアロン形式の場合はアプリケーションを終了
#endif
                        break;
                }

            }
            // 額縁を動かす
            titleselectFrame.rectTransform.localPosition = titleTexts[titleselectNumber].rectTransform.localPosition;
        }
        // 自機選択画面での操作
        else if (selectSceneNum == Title.charaselect)
        {
            // 上方向ボタンを押されたとき
            if (inputUI.upAction.triggered)
            {
                charaselectNumber -= 1;
                if (charaselectNumber < 0)
                {
                    charaselectNumber = (sbyte)(charaselectAmount - 1);
                }
            }
            // 下方向ボタンを押されたとき
            if (inputUI.downAction.triggered)
            {
                charaselectNumber += 1;
                if (charaselectNumber > (sbyte)(charaselectAmount - 1))
                {
                    charaselectNumber = 0;
                }
            }
            if (inputUI.nextAction.triggered)
            {
                switch (charaselectNumber)
                {
                    case 0:
                        playerManager.GetComponent<PlayerManager>().playerSelect = PlayerManager.PLAYER.MISYA;
                        break;
                    case 1:
                        playerManager.GetComponent<PlayerManager>().playerSelect = PlayerManager.PLAYER.NEEDLE;
                        break;
                }
                playerManager.GetComponent<PlayerManager>().GameSceneMove(1);
            }
            if (inputUI.backAction.triggered)
            {
                selectSceneNum = Title.title;
            }
            // 額縁を動かす
            charaselectFrame.rectTransform.localPosition = charaselects[charaselectNumber].rectTransform.localPosition;
        }
        // 設定画面での操作
        else if (selectSceneNum == Title.setting)
        {
            // 上方向ボタンを押されたとき
            if (inputUI.upAction.triggered)
            {
                settingselectNumber -= 1;
                if (settingselectNumber < 0)
                {
                    settingselectNumber = (sbyte)(settingselectAmount - 1);
                }
            }
            // 下方向ボタンを押されたとき
            if (inputUI.downAction.triggered)
            {
                settingselectNumber += 1;
                if (settingselectNumber > (sbyte)(settingselectAmount - 1))
                {
                    settingselectNumber = 0;
                }
            }

            if (settingselectNumber == 0)
            {
                if (inputUI.leftAction.triggered)
                {
                    playerManager.GetComponent<PlayerManager>().lifedefault -= 1;
                    if (playerManager.GetComponent<PlayerManager>().lifedefault < 0)
                    {
                        playerManager.GetComponent<PlayerManager>().lifedefault = 9;
                    }
                }
                else if (inputUI.rightAction.triggered)
                {
                    playerManager.GetComponent<PlayerManager>().lifedefault += 1;
                    if (playerManager.GetComponent<PlayerManager>().lifedefault > 9)
                    {
                        playerManager.GetComponent<PlayerManager>().lifedefault = 0;
                    }
                }                
            }

            else if (settingselectNumber == 1)
            {
                if (inputUI.leftAction.triggered)
                {
                    playerManager.GetComponent<PlayerManager>().bombdefault -= 1;
                    if (playerManager.GetComponent<PlayerManager>().bombdefault < 0)
                    {
                        playerManager.GetComponent<PlayerManager>().bombdefault = 9;
                    }
                }
                else if (inputUI.rightAction.triggered)
                {
                    playerManager.GetComponent<PlayerManager>().bombdefault += 1;
                    if (playerManager.GetComponent<PlayerManager>().bombdefault > 9)
                    {
                        playerManager.GetComponent<PlayerManager>().bombdefault = 0;
                    }
                }
            }

            settingselects[0].text = "初期LIFE数　：　←" +
                playerManager.GetComponent<PlayerManager>().lifedefault.ToString() + "→";
            settingselects[1].text = "初期BOMB数　：　←" +
                playerManager.GetComponent<PlayerManager>().bombdefault.ToString() + "→";

            if (inputUI.nextAction.triggered)
            {
                switch (settingselectNumber)
                {
                    case 2:
                        selectSceneNum = Title.title;
                        break;
                }

            }
            if (inputUI.backAction.triggered)
            {
                selectSceneNum = Title.title;
            }

            // 額縁を動かす
            settingselectFrame.rectTransform.localPosition = settingselects[settingselectNumber].rectTransform.localPosition;
        }
    }
    /// <summary>
    /// 変数によって表示する画面を切り替える関数
    /// </summary>
    /// <param name="select"></param>
    void CanvasDisplay(Title select)
    {
        titleCanvas.SetActive(false);
        charaselectCanvas.SetActive(false);
        setttingCanvas.SetActive(false);
        if (selectSceneNum == Title.title)
        {
            // タイトル画面を表示する
            titleCanvas.SetActive(true);
        }
        else if (selectSceneNum == Title.charaselect)
        {
            // 自機選択画面を表示する
            charaselectCanvas.SetActive(true);
        }
        else if (selectSceneNum == Title.setting)
        {
            // 設定画面を表示する
            setttingCanvas.SetActive(true);
        }
    }
}
