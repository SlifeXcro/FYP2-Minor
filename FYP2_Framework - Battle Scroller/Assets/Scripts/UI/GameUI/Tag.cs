using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tag : MonoBehaviour 
{
    public Image Backdrop, Triangle;
    public Text UnitTag;
    public string UnitNameTag;
    public Color TextColor;
    public Sprite CurSprite;
    public Sprite[] Sprites = new Sprite[2];

	//Use this for initialization
	void Start () 
    {
        TextColor = new Color(0.353f, 1.0f, 0.404f); //Default Color 
	}
	
	//Update is called once per frame
	void Update () 
    {
        //Set Name & Color & BG
        UnitTag.color = TextColor;
        if (UnitTag.text != UnitNameTag)
            UnitTag.text = UnitNameTag;
        if (CurSprite != null && Backdrop.sprite != CurSprite)
            Backdrop.sprite = CurSprite;

        //Set Selection Triangle Pos
        if (Triangle.gameObject.activeSelf)
            Triangle.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.7f, this.transform.position.z);
	}
}
