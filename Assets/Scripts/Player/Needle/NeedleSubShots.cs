using UnityEngine;
//針装備選択時のサブショットを制御するスクリプト

public class NeedleSubShots : SubShooters
{
    private bool shotflag = true; // 撃てる状態かどうかのフラグ

    SpriteRenderer subshooterSprite;

    public override void Start()
    {
        base.Start();

        subshooterSprite = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
        if(shotflag)
        {
            subshooterSprite.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            subshooterSprite.color = new Color32(255, 255, 255, 0);
        }
    }
    public override void SubShotting()
    {
        if (shotflag)
        {
            Instantiate(subBullet,
                this.transform.position, Quaternion.identity, ShotsField.transform);
            subshottime = 0.0f;
        }
    }

    public bool getshotflag
    {
        get { return this.shotflag; }
        set { this.shotflag = value; }
    }
}
