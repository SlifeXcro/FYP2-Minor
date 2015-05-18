using UnityEngine;
using System.Collections;

public class Attack_Kick : AttackScript
{
    //Use this for initialization
    void Start()
    {
        //Init from Parent
        this.Init();

        //Set Type
        this.AttackType = AType.ATTACK_KICK;

        //Set Key
        this.AttackKey = KeyCode.X;

        //Set Damage
        this.Damage = 20.0f;

        //Set Anim Index
        this.AnimationIndex = 3;

        //Set Anim Time
        this.AnimationTime = 0.5f;
    }

    //Update is called once per frame
    public override void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}
