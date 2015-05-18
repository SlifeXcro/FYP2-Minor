using UnityEngine;
using System.Collections;

public class Attack_Combo_Multi : AttackScript
{
    //Use this for initialization
    void Start()
    {
        //Set Combo Flag
        this.isCombo = true;

        //Init from Parent
        this.Init();

        //Set Type
        this.AttackType = AType.ATTACK_COMBO_MULTI;

        //Set Keys (Combo Chains)
        ListOfKeys.Add(KeyCode.Z);
        ListOfKeys.Add(KeyCode.Z);
        ListOfKeys.Add(KeyCode.Z);
        ListOfKeys.Add(KeyCode.X);

        //Set Damage
        this.Damage = 45.0f;

        //Set Anim Index
        this.AnimationIndex = 6;

        //Set Anim Time
        this.AnimationTime = 2.0f;
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}

