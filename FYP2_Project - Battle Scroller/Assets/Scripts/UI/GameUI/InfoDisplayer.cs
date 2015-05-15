using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoDisplayer : MonoBehaviour 
{
    //Singleton Structure
    protected static InfoDisplayer mInstance;
    public static InfoDisplayer Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<InfoDisplayer>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }
    public static bool IsNull()
    {
        if (mInstance == null)
            return true;
        return false;
    }
    public static void SetInstance(InfoDisplayer Instance)
    {
        mInstance = Instance;
    }

    //Unit's Icon
    public SpriteRenderer UnitIcon = new SpriteRenderer();

    //Name - Class - Level - Bounty (Removed) - Tag
    public Text[] UnitInfo = new Text[5];

    //Hp - PhyAtk - MagAtk - PhyDef - MagDef - Crit
    public Text[] UnitStatsText = new Text[6];

    //Set Info Text
    public void SetInfo(string Name, string Class, short Level)
    {
        UnitInfo[0].text = "[ " + Name + " ]";
        UnitInfo[4].text = "// " + Class + " //";
        UnitInfo[2].text = "Level: " + Level;
    }

    //Set Stats Text
    public void SetStats(UnitStats Stats)
    {
        UnitStatsText[0].text = "HP: " + (int)((Stats.HP / Stats.MAX_HP) * 100) + "%";
        UnitStatsText[1].text = "Phy. Atk: " + Stats.Physical_Attack;
        UnitStatsText[2].text = "Mag. Atk: " + Stats.Magical_Attack;
        UnitStatsText[3].text = "Phy. Def: " + Stats.Physical_Defense;
        UnitStatsText[4].text = "Mag. Def: " + Stats.Magical_Defense;
        UnitStatsText[5].text = "Critical: " + Stats.Critical;
    }

    //Set Unit
    public void SetUnit(Unit theUnit)
    {
        UnitIcon.sprite = theUnit.Icon;
        SetInfo(theUnit.Stats.UnitName, theUnit.Stats.UnitType, theUnit.Stats.Level);
        SetStats(theUnit.Stats);
    }

	//Use this for initialization
	void Start () 
    {
	    //Set Instance
        mInstance = this;
	}
	
	//Update is called once per frame
	void Update () 
    {
	    
	}
}
