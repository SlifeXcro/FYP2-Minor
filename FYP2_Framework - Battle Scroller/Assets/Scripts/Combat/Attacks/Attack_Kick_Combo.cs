using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack_Kick_Combo : AttackScript 
{
	//Use this for initialization
	void Start () 
    {
        //Set Combo Flag
        this.isCombo = true;

        //Init from Parent
        this.Init();

        //Set Type
        this.AttackType = AType.ATTACK_KICK_COMBO;

        //Set Keys (Combo Chains)
        ListOfKeys.Add(KeyCode.X);
        ListOfKeys.Add(KeyCode.X);
        ListOfKeys.Add(KeyCode.Z);

        //Set Damage
        this.Damage = 50.0f;

        //Set Anim Index
        this.AnimationIndex = 5;

        //Set Anim Time
        this.AnimationTime = 0.7f;
	}
	
	//Update is called once per frame
	void Update () 
    {
	    //Update from Parent
        this.StaticUpdate();
	}
}
