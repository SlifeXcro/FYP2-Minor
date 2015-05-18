using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// *** COMBAT MANAGER CLASS *** //
// ***    AUTHOR: SLIFE     *** //

// --- Manages Attacks
// --- Combos
// --- Timers
// --- Attack Key Inputs (Chains)

public class CombatManager : MonoBehaviour 
{
    //Singleton Structure
    protected static CombatManager mInstance;
    public static CombatManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<CombatManager>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    //Combo Bar
    public Image ComboBar;
    public string ComboString = null;

    //Combos
    public bool ComboActive = false;
    float ComboTime = 2.0f;
    short ComboTimerIndex;

    //List of Attacks (Combos)
    public List<AttackScript> ListOfAttacks = new List<AttackScript>();
    public AttackScript CurrentAttack = null;

    //Attacking Flag
    public bool isAttacking = false;

	//Use this for initialization
	void Start () 
    {
        //Set Instance
        mInstance = this;

        //Set Timer
        ComboTimerIndex = Timer.GetExecuteID(ComboTime);

        //Set Text to NULL
        if (ComboBar != null)
            ComboBar.GetComponentInChildren<Text>().text = null;
	}

    //Convert Key to String
    public string ConvertKeyToString(KeyCode Key)
    {
        switch (Key)
        {
            case KeyCode.Z:
                return "A";
            case KeyCode.X:
                return "B";
        }
        return null;
    }

    public void DealDamageToEngagedEntities()
    {
        //Damage
        for (short i = 0; i < Global.EngagingUnitList.Count; ++i)
        {
            if (CurrentAttack != null && Global.EngagingUnitList[i].State != Unit.EState.FALL)
            {
                Global.EngagingUnitList[i].Stats.TakePhysicalDamage(Movement.Instance.theUnit.Stats,
                                                                    CurrentAttack.Damage);
                Global.EngagingUnitList[i].theModel.SetAnimation(4);
                Global.EngagingUnitList[i].State = Unit.EState.FALL;
            }
        }
    }

    //Attacks
    void Attack(AttackScript theAttack)
    {
        theAttack.ToggleAnimation();
        CurrentAttack = theAttack;

        Movement.Instance.theUnit.State = Unit.EState.ATTACK;
    }
	
	//Update is called once per frame
	void Update () 
    {
        //Timer for Combos
        ComboActive = !Timer.ExecuteTime(ComboTime, ComboTimerIndex);
        if (!ComboActive)
            ComboString = null;

        //Set Text
        if (ComboBar != null)
            ComboBar.GetComponentInChildren<Text>().text = ComboString;

	    //Loop through List of Attacks
        bool AllIdle = true, ProceedToReset = false;
        for (short i = 0; i < ListOfAttacks.Count; ++i)
        {
            bool ProceedToNonComboAttack = true,
                 ProceedToAttack = false;

            //Detect Combo Attacks
            if (ListOfAttacks[i].isCombo)
            {
                //Combo Detected, Execute Combo
                if (ListOfAttacks[i].executeCombo)
                {
                    ProceedToNonComboAttack = ListOfAttacks[i].executeCombo = false;
                    ListOfAttacks[i].ResetCombo();
                    ProceedToAttack = true;
                    string TempString = ComboString;
                    ComboString = "Executing Combo! (" + TempString + ")";
                }
            }

            //No Combos
            if (ProceedToNonComboAttack)
            {
                //Check for Key Input
                if (Input.GetKeyDown(ListOfAttacks[i].AttackKey))
                {
                    ProceedToAttack = true;

                    bool bAdd = true;
                    if (ComboString != null && ComboString.Length > 3 && ComboString.Substring(0, 3) == "Exe")
                        bAdd = false;

                    if (bAdd)
                        ComboString += ConvertKeyToString(ListOfAttacks[i].AttackKey);
                }
            }

            //Set Flag to false if any one attack isAnimating
            if (ListOfAttacks[i].isAnimating)
                AllIdle = false;

            //Execute the Attack
            if (ProceedToAttack)
            {
                if (!ListOfAttacks[i].isAnimating)
                {
                    isAttacking = true;
                    AllIdle = false;
                    Attack(ListOfAttacks[i]);
                    ListOfAttacks[i].isAnimating = true;
                    ListOfAttacks[i].finishedAnimating = false;
                }
            }

            //Reset flags upon completion of Attacking Animation
            if (ListOfAttacks[i].finishedAnimating)
            {
                ListOfAttacks[i].isAnimating = false;
                ListOfAttacks[i].finishedAnimating = false;
                ProceedToReset = true;
            }
        }

        //Makes sure no attacks are animating
        if (ProceedToReset && AllIdle)
            isAttacking = false;

        //Deal Damage
        if (isAttacking)
            DealDamageToEngagedEntities();
	}
}
