using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// *** MOVEMENT SCIPTING *** //
// ***   AUTHOR: SLIFE   *** //

// --- Uses Raycast to debug line of Movement
// --- Analog Movement Implemented

public class Movement : MonoBehaviour
{
    //Singleton Structure
    protected static Movement mInstance;
    public static Movement Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<Movement>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    List<KeyCode> ListOfMovementKeys = new List<KeyCode>();

    bool isMoving = false;                                          // Check if Unit is Moving
    bool facingLeft = false, flipped = false;                       // Check for the player sprite direction
    public float MovementSpeed = 5.0f;                              // Toggle this value in Editor to increase or decrease movement speed
    public Unit theUnit;                                            // Unit Class
    public Map theMap;                                              // Current Map (for Collision detection)
    KeyCode CurrentKey = KeyCode.V;                                 // Current Key
    Vector3 PlayerLastPos;                                          // Player's Last Position

    void OnTriggerEnter(Collider col)
    {

    }
    void OnTriggerExit(Collider col)
    {

    }

    //Use this for initialization
    void Start()
    {
        //Init Instance
        mInstance = this;

        //Set Movement Keys
        ListOfMovementKeys.Add(KeyCode.LeftArrow);
        ListOfMovementKeys.Add(KeyCode.RightArrow);
        ListOfMovementKeys.Add(KeyCode.UpArrow);
        ListOfMovementKeys.Add(KeyCode.DownArrow);
        ListOfMovementKeys.Add(KeyCode.A);
        ListOfMovementKeys.Add(KeyCode.D);
        ListOfMovementKeys.Add(KeyCode.W);
        ListOfMovementKeys.Add(KeyCode.S);

        //Set Last Pos
        PlayerLastPos = theUnit.transform.position;

        //Set Cur Unit
        theUnit.isPlayerUnit = true;

#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_ANDROID
        MovementSpeed *= 0.85f;
#endif
    }

    //Movement 
    void Move(KeyCode Key)
    {
        if (!CombatManager.Instance.isAttacking)
            theUnit.theModel.SetAnimation(1);

        theUnit.State = Unit.EState.MOVE;

        switch (Key)
        {
            case KeyCode.LeftArrow:
            case KeyCode.A:
                theUnit.transform.Translate(-MovementSpeed * Time.deltaTime, 0, 0);

                //Flip
                if (!flipped && theUnit.theModel.transform.localScale.x > 0)
                {
                    flipped = true;
                    Vector3 tempScale = theUnit.theModel.transform.localScale;
                    tempScale.x *= -1;
                    theUnit.theModel.transform.localScale = tempScale; //Flip Player's Model
                }
                break;
            case KeyCode.RightArrow:
            case KeyCode.D:
                theUnit.transform.Translate(MovementSpeed * Time.deltaTime, 0, 0);

                //Flip
                if (!flipped && theUnit.theModel.transform.localScale.x < 0)
                {
                    flipped = true;
                    Vector3 tempScale = theUnit.theModel.transform.localScale;
                    tempScale.x *= -1;
                    theUnit.theModel.transform.localScale = tempScale; //Flip Player's Model
                }
                break;
            case KeyCode.UpArrow:
            case KeyCode.W:
                if (!(theUnit.theModel.CollisionRegions.CollidedUnwalkable && CurrentKey == Key))
                    theUnit.transform.Translate(0, MovementSpeed * Time.deltaTime, 0);
                if (!theUnit.theModel.CollisionRegions.CollidedUnwalkable)
                    CurrentKey = Key;
                break;
            case KeyCode.DownArrow:
            case KeyCode.S:
                if (!(theUnit.theModel.CollisionRegions.CollidedUnwalkable && CurrentKey == Key))
                    theUnit.transform.Translate(0, -MovementSpeed * Time.deltaTime, 0);
                if (!theUnit.theModel.CollisionRegions.CollidedUnwalkable)
                    CurrentKey = Key;
                break;
            default:
                isMoving = false;
                break;
        }
    }

    public static bool RayCastMovement(Vector3 Pos, Vector3 Dir, float Dist)
    {
        // *** RAYCASTING OF MOVEMENT *** //
        //Check if Path is Clear
        bool ClearPath = true;

        //Raycast Line of Movement
        RaycastHit[] Hit = Physics.RaycastAll(Pos, Dir, Dist);

        //Check if Raycast line has hit unwalkable objects
        if (Hit != null)
        {
            foreach (RaycastHit hit in Hit)
            {
                //Stop Movement
                if (hit.collider.tag == "UNWALKABLE")
                    ClearPath = false;
            }
        }

        //Debug Line
        if (ClearPath)
            Debug.DrawRay(Pos, Dir, Color.green);
        else
            Debug.DrawRay(Pos, Dir, Color.red);

        return !ClearPath;
        // *** END OF RAYCASTING *** //
    }

    //Update is called once per frame
    void Update()
    {
        //Set Camera Pan Unit
        if (CameraPan.Instance.theUnit != null && CameraPan.Instance.theUnit != this.theUnit)
            CameraPan.Instance.theUnit = this.theUnit;

        bool AllKeysClear = true;
#if UNITY_EDITOR || UNITY_STANDALONE
        //Check if Unit is Moving
        for (short i = 0; i < ListOfMovementKeys.Count; ++i)
        {
            if (Input.GetKey(ListOfMovementKeys[i]))
            {
                AllKeysClear = false;

                //Move Unit
                Move(ListOfMovementKeys[i]);
            }
            else if (Input.GetKeyUp(ListOfMovementKeys[i]))
                flipped = false;
        }
        isMoving = !AllKeysClear;
#endif

        if (Analog.Instance.Move)
        {
            theUnit.State = Unit.EState.MOVE;

            //RayCast
            Vector3 theDir = new Vector3(Analog.Instance.GetTravelDir().x, Analog.Instance.GetTravelDir().y, 0);
            bool ClearPath = !RayCastMovement(theUnit.theModel.CollisionRegions.transform.position, theDir, 1.0f);

            isMoving = true;

            if (!CombatManager.Instance.isAttacking)
                theUnit.theModel.SetAnimation(1);

            //Only Move when path is clear
            if (ClearPath)
                theUnit.transform.Translate(Analog.Instance.GetTravelDir() * MovementSpeed * Time.deltaTime * 1.1f);
            else if (isMoving)
                theUnit.transform.Translate(Analog.Instance.GetTravelDir().x * MovementSpeed * Time.deltaTime * 1.1f, 0, 0);

            //Flip
            if (Analog.Instance.GetTravelDir().x < 0)
            {
                flipped = true;
                Vector3 tempScale = theUnit.theModel.transform.localScale;

                if (tempScale.x > 0)
                    tempScale.x *= -1;

                theUnit.theModel.transform.localScale = tempScale; //Flip Player's Model
            }

            //Flip
            else if (Analog.Instance.GetTravelDir().x > 0)
            {
                flipped = true;
                Vector3 tempScale = theUnit.theModel.transform.localScale;

                if (tempScale.x < 0)
                    tempScale.x *= -1;

                theUnit.theModel.transform.localScale = tempScale; //Flip Player's Model
            }
        }
        else if (AllKeysClear)
        {
            isMoving = false;
            theUnit.transform.Translate(Vector3.zero);
        }

        //Set Player Sprite & Animation to IDLE if Game is Paused
        //if ((!Global.GamePause || Global.StopMovement) && !CombatManager.Instance.isAttacking)
        //    theUnit.theModel.SetAnimation(0);

        //Reset Movement Vector3 & Flags
        if (!isMoving || Global.StopMovement /*|| theUnit.CollidedUnwalkable*/)
        {
            //if (Global.FreeCam)
            //{
                flipped = false;
                PlayerLastPos = theUnit.transform.position;

                if (!CombatManager.Instance.isAttacking)
                {
                    theUnit.theModel.SetAnimation(0);
                    theUnit.State = Unit.EState.IDLE;
                }
            //}
        }
    }
}
