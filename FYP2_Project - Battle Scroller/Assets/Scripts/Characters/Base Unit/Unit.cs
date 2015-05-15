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

    //Check if Unit has Collided with Unwalkable Objects
    void OnTriggerEnter(Collider col)
    {
       
    }
    void OnTriggerExit(Collider col)
    {
       
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

    //Randomize Stats
    public virtual void RandomizeStats()
    {
        Debug.Log("Default Unit Stats Inited.");
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

        //Init Default Stats if class is not inherited
        if (!Inherited)
            RandomizeStats();

        //Init Game Object Tag
        theModel.gameObject.tag = this.gameObject.tag = "UNIT";
    }

	//Use this for initialization
	void Start () 
    {
        Init();
	}

    //Parent Update
    public void StaticUpdate()
    {
        //Cap Z Pos
        Vector3 CapZ = new Vector3(transform.position.x, transform.position.y, 0.0f);
        this.transform.position = CapZ;

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
	void Update () 
    {
        StaticUpdate();
	}
}
