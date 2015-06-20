using UnityEngine;
using System.Collections;

//Parent class for all Units
public class Unit : MonoBehaviour 
{
    //Unit's Type
    public enum UType
    {
        UNIT_DEFAULT,
        UNIT_MAGE
    } public UType UnitType = UType.UNIT_DEFAULT;

    //Unit's State
    public enum EState
    {
        IDLE,
        ATTACK,
        MOVE,
        FALL
    } public EState State = EState.IDLE;

    //Check if Unit has Collided with Unwalkable Objects
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "AOE")
        {
            if (!this.isPlayerUnit)
                Global.EngagingUnitList.Add(this);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "AOE")
        {
            for (short i = 0; i < Global.EngagingUnitList.Count; ++i)
            {
                if (Global.EngagingUnitList[i].UnitID == this.UnitID)
                {
                    Global.EngagingUnitList.RemoveAt(i);
                    break;
                }
            }
        }
    }

    //Each Unit has it's own Stats
    public UnitStats Stats;

    //Each Unit has it's own Model
    public Model theModel;

    //Each Unit has it's own Icon
    public Sprite Icon;

    //Each Unit has it's own Tag
    public Tag UnitTag; //Init with Unit Tag Prefab
    bool bInstantiated = false;

    //InfoDisplayer Prefab
    public InfoDisplayer InfoDisPrefab;

    //Check if class has been inherited
    protected bool Inherited = false;

    //Every Unit has its own unique ID
    public int UnitID = -1;
    static int UniqueID = 0;

    //Check if this Unit = Player Unit
    public bool isPlayerUnit = false;

    //Fall State Timer
    Timer.TimeBundle FallTimer;

    //Randomize Stats
    public virtual void RandomizeStats()
    {
        Debug.Log("Default Unit Stats Inited.");
        if (Stats != null)
            Stats.Set(1, Random.Range(300, 500),
                      Random.Range(150, 250), Random.Range(100, 200),
                      Random.Range(150, 250), Random.Range(100, 200),
                      Random.Range(0.7f, 1.2f));
    }

    //Self Init
    public void Init() 
    {
        //Set ID
        ++UniqueID;
        this.UnitID = UniqueID;

        //Set Fall Time
        FallTimer.Time = 0.7f;
        FallTimer.Index = Timer.GetExecuteID(FallTimer.Time);

        //Init Default Stats if class is not inherited
        if (!Inherited)
            RandomizeStats();

        //Init Game Object Tag
        if (theModel.gameObject.tag == null)
            theModel.gameObject.tag = this.gameObject.tag = "UNIT";
    }

	//Use this for initialization
	public void Start () 
    {
        Init();
	}

    //Update State Change
    void UpdateStateChange()
    {
    }

    //Execute State
    void ExecuteState()
    {
        switch (State)
        {
            case EState.IDLE:
                if (theModel.CurAnimationIndex != 0)
                    theModel.SetAnimation(0);
                break;
            case EState.ATTACK:
                break;
            case EState.FALL:
                if (Timer.ExecuteTime(FallTimer.Time, FallTimer.Index))
                    State = EState.IDLE;
                break;
            default:
                break;
        }
    }

    //Parent Update
    public void StaticUpdate()
    {
        //Cap Z Pos
        Vector3 CapZ = new Vector3(transform.position.x, transform.position.y, 0.0f);
        this.transform.position = CapZ;

        //Set Tag
        if (this.isPlayerUnit)
            theModel.gameObject.tag = this.gameObject.tag = "PLAYER_UNIT";
        else
            theModel.gameObject.tag = this.gameObject.tag = "UNIT";

        //Destroy if Dead
        if (this.Stats.HP <= 0.0f)
        {
            ++Global.EnemyKillCount;
            Destroy(this.gameObject);
        }

        //FSM
        UpdateStateChange();
        ExecuteState();

        //Detect if Unit has been Selected
        //if (InputScript.Instance.InputCollided(theModel.collider, true))
        //{
        //    Global.CurrentUnitID = this.UnitID;
        //    Selected = true;
        //    Movement.Instance.theUnit = this;

        //    if (InfoDisplayer.IsNull())
        //        InfoDisplayer.SetInstance(Instantiate(InfoDisPrefab, InfoDisPrefab.transform.position, Quaternion.identity) as InfoDisplayer);
        //    InfoDisplayer.Instance.SetUnit(this);
        //}
        //else if (Global.CurrentUnitID != this.UnitID)
        //    Selected = false;

        //Set Tag Pos
        //Vector3 TagPos = new Vector3(this.transform.position.x, this.transform.position.y+1.0f, -1);
        //if (!bInstantiated)
        //{
        //    UnitTag = Instantiate(UnitTag, TagPos, Quaternion.identity) as Tag;
        //    UnitTag.UnitNameTag = this.Stats.UnitName;
        //    bInstantiated = true;
        //}
        //UnitTag.transform.position = TagPos;
    }

    //Update is called once per frame
   public void Update() 
    {
        StaticUpdate();
	}
}
