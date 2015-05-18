using UnityEngine;
using System.Collections;

// *** UNIT STATS CLASS *** //
// ***   AUTHOR: SLIFE  *** //

// --- Stats Class with Formulated Calculations
// --- Physical and Magical Stats Implemented
// --- Critical Chance Implemented
// --- Damage Functions Implemented

public class UnitStats : MonoBehaviour 
{
    //Max Crit Cap
    const short MAX_CRITICAL = 3;

    //public string UnitName = "Default_Unit";
    public string UnitName, UnitType;
    public float HP = 0.0f, MAX_HP = 0.0f,
                 Physical_Attack = 0.0f,
                 Physical_Defense = 0.0f,
                 Magical_Attack = 0.0f,
                 Magical_Defense = 0.0f,
                 Critical = 0.0f;
    public short Level = 1;
    bool ConstStatsInited = false;

    public void Set(short Level, float HP,
                    float Physical_Attack, float Physical_Defense,
                    float Magical_Attack, float Magical_Defense,
                    float Critical, 
                    string UnitType = "Default_Unit",
                    string UnitName = "Default_Name")
    {
        this.Level = Level;
        this.HP = HP;
        if (!ConstStatsInited)
        {
            this.MAX_HP = HP;
            ConstStatsInited = true;
        }
        this.Physical_Attack = Physical_Attack;
        this.Physical_Defense = Physical_Defense;
        this.Magical_Attack = Magical_Attack;
        this.Magical_Defense = Magical_Defense;
        this.Critical = CMath.Round(Critical, 2);
        this.UnitType = UnitType;
        this.UnitName = UnitName;
    }

    public void TakePhysicalDamage(UnitStats Other, float Damage)
    {
        float AdditionalMultiplier = 1.0f, //Additional Damage Multiplier (Crit)
              CritRangeValue = Random.Range(0.0f, Critical); //Randomize a number from 0 to Critical

        //^ Critical Stat = ^ Critical Chance = ^ Additional Damage
        if (CritRangeValue > 0 && CritRangeValue < MAX_CRITICAL)
            AdditionalMultiplier += Critical;

        //Placeholder Formula
        this.HP -= ((Other.Physical_Attack - this.Physical_Defense * 0.2f) * AdditionalMultiplier) * 0.01f * Damage;
    }

    public void TakeMagicalDamage(UnitStats Other, float Damage)
    {
        float AdditionalMultiplier = 1.0f, //Additional Damage Multiplier (Crit)
              CritRangeValue = Random.Range(0.0f, Critical); //Randomize a number from 0 to Critical

        //^ Critical Stat = ^ Critical Chance = ^ Additional Damage
        if (CritRangeValue > 0 && CritRangeValue < MAX_CRITICAL)
            AdditionalMultiplier += Critical;

        //Placeholder Formula
        this.HP -= ((Other.Magical_Attack - this.Magical_Defense * 0.2f) * AdditionalMultiplier) * 0.01f * Damage;
    }

	//Use this for initialization
	void Start () 
    {
        //UnitName = Instantiate(Placeholder, this.transform.position, Quaternion.identity) as GUIText;
	}
	
	//Update is called once per frame
	void Update () 
    {
	    //Cap Critical
        if (this.Critical > MAX_CRITICAL)
            this.Critical = MAX_CRITICAL;

        //Cap Hp
        if (this.HP <= 0.0f)
            this.HP = 0.0f;
	}
}
