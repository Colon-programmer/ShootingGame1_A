using UnityEngine;
// ゲーム画面に自機を生成するスクリプト

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefab;
    private GameObject playerManager; // プレイヤーの情報を持つスクリプトオブジェクト
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        switch (playerManager.GetComponent<PlayerManager>().playerSelect)
        {
            case PlayerManager.PLAYER.MISYA:
                Instantiate(playerPrefab[0], this.transform.position, Quaternion.identity);
                break;
            case PlayerManager.PLAYER.NEEDLE:
                Instantiate(playerPrefab[1], this.transform.position, Quaternion.identity);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
