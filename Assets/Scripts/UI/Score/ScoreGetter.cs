using UnityEngine;
using TMPro;
// スコアを取得するスクリプト

public class ScoreGetter : MonoBehaviour
{
    private GameObject playermanager; // シングルトンを持ったオブジェクト
    [SerializeField ] private TextMeshProUGUI scoretext;

    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playermanager = GameObject.Find("PlayerManager");
        scoretext.text = "SCORE : " + playermanager.GetComponent<PlayerManager>().scorenum.ToString();
        score = playermanager.GetComponent<PlayerManager>().scorenum;
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "SCORE : " + score.ToString();
        playermanager.GetComponent<PlayerManager>().scorenum = score;

    }

    public int getscore
    {
        get { return this.score; }
        set { this.score = value; }
    }
}
