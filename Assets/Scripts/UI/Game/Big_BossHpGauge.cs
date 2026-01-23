using UnityEngine;
using UnityEngine.UI;
// ステージボスの体力ゲージのスクリプト

public class Big_BossHpGauge : MonoBehaviour
{
    private RectTransform rectTransform;

    private Image bosshpimage;

    [SerializeField] private Big_BossManager bigBossScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(-200, 200);

        bosshpimage = GetComponent<Image>();

        bosshpimage.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float currenthp = Mathf.Clamp((float)bigBossScript.getbig_boss_hp, 0, (float)bigBossScript.getbig_boss_maxhp);
        bosshpimage.fillAmount = currenthp / bigBossScript.getbig_boss_maxhp;
    }
}
