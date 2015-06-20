using UnityEngine;
using System.Collections;

public class Mage : Unit 
{
    // *** Inherited Virtual Functions *** //
    public void RandomizeStats()
    {
        Debug.Log("Mage Stats Inited.");
        Stats.Set(1, Random.Range(500, 700),
                  Random.Range(900, 1100), Random.Range(150, 220),
                  Random.Range(190, 270), Random.Range(150, 220),
                  Random.Range(1.1f, 1.75f), "Mage", "Tsunayoshi");
    }

    //Use this for initialization
    void Start()
    {
        //Class has been inherited
        Inherited = true;

        //Init from Parent Class
        this.Init();

        //Init Unit's Type
        this.UnitType = UType.UNIT_MAGE;

        //Init Stats
        this.RandomizeStats();
    }

    //Update is called once per frame
    void Update()
    {
        //Update from Parent Class
        this.StaticUpdate();
    }
}
