using UnityEngine;
using System.Collections;

// *** BACKGROUND SCROLLING *** //
// ***     AUTHOR: SLIFE    *** //

// --- Translate Seamless BG from Prev Index to Last Index
// --- Provides an "Endless Scrolling" Effect

public class Background : MonoBehaviour 
{
    //Background Objects
    public SpriteRenderer[] BG = new SpriteRenderer[3];
    int CurIndex = 1;
	
    //Init
    void Start()
    {
        //Init Next BG Pos
        Vector3 TempPos = BG[CurIndex].transform.position;
        TempPos.x += BG[CurIndex].gameObject.collider.bounds.size.x;
        BG[CurIndex + 1].transform.position = TempPos;

        //Init Prev BG Pos
        TempPos = BG[CurIndex].transform.position;
        TempPos.x -= BG[CurIndex].gameObject.collider.bounds.size.x;
        BG[CurIndex - 1].transform.position = TempPos;
    }

	//Update is called once per frame
	void Update () 
    {
        //Set Positions
        float Cam_Pos = Camera.main.transform.position.x + Camera.main.rect.size.x * 0.5f,
              BG_Pos_Right = BG[CurIndex].transform.position.x + BG[CurIndex].gameObject.collider.bounds.size.x * 0.5f,
              BG_Pos_Left = BG[CurIndex].transform.position.x - BG[CurIndex].gameObject.collider.bounds.size.x * 0.5f;

        //Set Indices
        int NextIndex, PrevIndex;
        if (CurIndex + 1 > 2)
            NextIndex = 0;
        else
            NextIndex = CurIndex + 1;
        if (CurIndex - 1 < 0)
            PrevIndex = 2;
        else
            PrevIndex = CurIndex - 1;

        //Shift BG (Right)
        if (Cam_Pos >= BG_Pos_Right)
        {
            Vector3 NewPos = BG[PrevIndex].transform.position;
            NewPos.x += BG[PrevIndex].gameObject.collider.bounds.size.x + BG[CurIndex].gameObject.collider.bounds.size.x + BG[NextIndex].gameObject.collider.bounds.size.x;
            BG[PrevIndex].transform.position = NewPos;
            ++CurIndex;
            if (CurIndex > 2)
                CurIndex = 0;
        }

        //Shift BG (Left)
        if (Cam_Pos <= BG_Pos_Left)
        {
            Vector3 NewPos = BG[NextIndex].transform.position;
            NewPos.x -= BG[PrevIndex].gameObject.collider.bounds.size.x + BG[CurIndex].gameObject.collider.bounds.size.x + BG[NextIndex].gameObject.collider.bounds.size.x;
            BG[NextIndex].transform.position = NewPos;
            --CurIndex;
            if (CurIndex < 0)
                CurIndex = 2;
        }
	}
}
