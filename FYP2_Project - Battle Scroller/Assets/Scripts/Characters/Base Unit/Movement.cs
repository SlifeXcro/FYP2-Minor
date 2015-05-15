using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_ANDROID
        MovementSpeed *= 0.4f;
#endif
    }

    //Movement 
    void Move(KeyCode Key)
    {
        theUnit.theModel.SetAnimation(1);
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
                if (!(theUnit.theModel.WalkCollisionRegion.CollidedUnwalkable && CurrentKey == Key))
                    theUnit.transform.Translate(0, MovementSpeed * Time.deltaTime, 0);
                if (!theUnit.theModel.WalkCollisionRegion.CollidedUnwalkable)
                    CurrentKey = Key;
                break;
            case KeyCode.DownArrow:
            case KeyCode.S:
                if (!(theUnit.theModel.WalkCollisionRegion.CollidedUnwalkable && CurrentKey == Key))
                    theUnit.transform.Translate(0, -MovementSpeed * Time.deltaTime, 0);
                if (!theUnit.theModel.WalkCollisionRegion.CollidedUnwalkable)
                    CurrentKey = Key;
                break;
            default:
                isMoving = false;
                break;
        }
    }

    public static bool RayCastMovement(Vector3 Pos)
    {
        // *** RAYCASTING OF MOVEMENT *** //
        //Check if Path is Clear
        bool ClearPath = true;

        //Set Distance
        float Dist = 0.0f;
        if (Analog.Instance.GetTravelDir().y > 0)
            Dist = 3.0f;
        else
            Dist = 7.0f;

        //Raycast Line of Movement
        RaycastHit[] Hit = Physics.RaycastAll(Pos, Analog.Instance.GetTravelDir(), Dist);

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

        return !ClearPath;
        // *** END OF RAYCASTING *** //
    }

    //Update is called once per frame
    void Update()
    {
        //Set Camera Pan Unit
        if (CameraPan.Instance.theUnit != null && CameraPan.Instance.theUnit != this.theUnit)
            CameraPan.Instance.theUnit = this.theUnit;

#if UNITY_EDITOR || UNITY_STANDALONE
        //Check if Unit is Moving
        bool AllKeysClear = true;
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
            bool ClearPath = !RayCastMovement(theUnit.theModel.WalkCollisionRegion.transform.position);

            isMoving = true;
            theUnit.theModel.SetAnimation(1);

            //Only Move when path is clear
            if (ClearPath)
                theUnit.transform.Translate(Analog.Instance.GetTravelDir() * MovementSpeed * Time.deltaTime * 10.0f);

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
        //if (!Global.GamePause || Global.StopMovement)
        //    theUnit.theModel.SetAnimation(0);

        //Reset Movement Vector3 & Flags
        if (!isMoving || Global.StopMovement /*|| theUnit.CollidedUnwalkable*/)
        {
            //if (Global.FreeCam)
            //{
            flipped = false;
            PlayerLastPos = theUnit.transform.position;
            theUnit.theModel.SetAnimation(0);
            //}
        }
    }
}
