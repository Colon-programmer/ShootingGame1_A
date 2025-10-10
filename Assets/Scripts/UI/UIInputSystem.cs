using UnityEngine;
using UnityEngine.InputSystem;
// UIを操作する時の入力を受け付けるスクリプト

public class UIInputSystem : MonoBehaviour
{
    public InputAction nextAction; // 主に決定する入力
    public InputAction backAction; // 主に戻る入力
    public InputAction upAction; // 上方向の入力
    public InputAction downAction; // 下方向の入力
    public InputAction leftAction; // 左方向の入力
    public InputAction rightAction; // 右方向の入力
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // UIに関わる入力を受け付けるようにする
        nextAction = InputSystem.actions.FindAction("Next");
        backAction = InputSystem.actions.FindAction("Back");
        upAction = InputSystem.actions.FindAction("Up");
        downAction = InputSystem.actions.FindAction("down");
        leftAction = InputSystem.actions.FindAction("left");
        rightAction = InputSystem.actions.FindAction("right");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
