using UnityEngine;
using System.Collections;

public class Attack_Punch : AttackScript 
{
	//Use this for initialization
	void Start () 
    {
	    //Init from Parent
        this.Init();

        //Set Type
        this.AttackType = AType.ATTACK_PUNCH;

        //Set Key
        this.AttackKey = KeyCode.Z;

        //Set Damage
        this.Damage = 15.0f;

        //Set Anim Index
        this.AnimationIndex = 2;

        //Set Anim Time
        this.AnimationTime = 0.5f;
	}
	
	//Update is called once per frame
	public override void Update () 
    {
	    //Update from Parent
        this.StaticUpdate();
	}
}
