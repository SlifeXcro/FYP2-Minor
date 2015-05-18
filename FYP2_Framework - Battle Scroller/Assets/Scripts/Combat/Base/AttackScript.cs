using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// *** DYNAMIC ATTACKS CLASS *** //
// ***     AUTHOR: SLIFE     *** //

// --- Caters for Attacking Combo Chains
// --- Couples with Combo Manager Class 
// --- Animation Handling Implemented

public class AttackScript : MonoBehaviour 
{
    //Attack Type
    public enum AType
    {
        ATTACK_DEFAULT,
        ATTACK_PUNCH,
        ATTACK_KICK,
        ATTACK_KICK_COMBO,
        ATTACK_COMBO_MULTI
    } public AType AttackType = AType.ATTACK_DEFAULT;

    //Attack Key
    public KeyCode AttackKey = KeyCode.Tab; //Default

    //Combo Attacks
    public bool isCombo = false, executeCombo = false;
    public List<KeyCode> ListOfKeys = new List<KeyCode>(); //Combo Keys
    public List<KeyCode> StorageList = new List<KeyCode>(); //Storage List 
    public void ResetCombo()
    {
        StorageList.Clear();
        this.executeCombo = false;
    }

    //Area Of Effect
    public Collider AOE;

    //Attack's Damage
    public float Damage = 0.0f;

    //Animating Flag
    public bool isAnimating = false,
                finishedAnimating = false;

    //Length (in Seconds) of Attacking Animation
    protected float AnimationTime = 0.5f;

    //Animation Index
    protected short AnimationIndex = 1,
                    AnimationTimeIndex;

    //Animation
    public void ToggleAnimation()
    {
        Movement.Instance.theUnit.theModel.SetAnimation(AnimationIndex);
        this.finishedAnimating = false;
    }

    //Parent Init
    protected void Init()
    {
        AnimationTimeIndex = Timer.GetExecuteID(AnimationTime);
    }

	//Use this for initialization
	void Start () 
    {
        Init();
	}

    //Combo Update
    protected void ComboUpdate()
    {
        //Timer for Combo
        if (CombatManager.Instance.ComboActive)
        {
            //Loop Through Keys
            for (short i = 0; i < ListOfKeys.Count; ++i)
            {
                if (Input.GetKeyDown(ListOfKeys[i]))
                {
                    StorageList.Add(ListOfKeys[i]);
                    break;
                }
            }
        }

        //Reset Input
        else
            this.ResetCombo();

        //Compare Keys for Combo
        for (short i = 0; i < ListOfKeys.Count; ++i)
        {
            if (StorageList.Count < ListOfKeys.Count)
                break;
            if (ListOfKeys[i] != StorageList[i])
                break;
            if (i == ListOfKeys.Count - 1)
                this.executeCombo = true;
        }
    }

    //Parent Update
    protected void StaticUpdate()
    {
        //Check state of current Animation
        if (this.isAnimating && Timer.ExecuteTime(this.AnimationTime, AnimationTimeIndex))
        {
            this.finishedAnimating = true;
            this.isAnimating = false;
        }

        //Set AOE Pos
        Transform ModelTransform = Movement.Instance.theUnit.theModel.transform;
        if (ModelTransform.localScale.x < 0) //LEFT
            AOE.transform.position = new Vector3(ModelTransform.position.x - AOE.bounds.size.x, ModelTransform.position.y, AOE.transform.position.z);
        else                                 //RIGHT
            AOE.transform.position = new Vector3(ModelTransform.position.x + AOE.bounds.size.x, ModelTransform.position.y, AOE.transform.position.z);

        //Combo Update
        if (this.isCombo)
            ComboUpdate();
    }
	
	//Update is called once per frame
	public virtual void Update () 
    {
        StaticUpdate();
	}
}
