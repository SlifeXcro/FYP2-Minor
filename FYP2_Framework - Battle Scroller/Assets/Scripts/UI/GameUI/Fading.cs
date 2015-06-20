using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fading : MonoBehaviour 
{
    //Singleton Structure
    protected static Fading mInstance;
    public static Fading Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObj = new GameObject();
                mInstance = tempObj.AddComponent<Fading>();
                Destroy(tempObj);
            }
            return mInstance;
        }
    }

    //Fade Canvas
    public Canvas FadePrefab;
    Canvas FadeSprite;

    // *** Variables *** //
    float f_Alpha = 1.0f, f_Speed = 0.7f;
    public bool isFaded = false;

    //Fade Type
    public enum E_FadeType
    {
        FADE_IN,
        FADE_OUT
    } public E_FadeType Type;

    //Instantiate
    void InstantiateFade()
    {
        if (FadePrefab != null)
        {
            FadeSprite = Instantiate(FadePrefab, FadePrefab.transform.position, Quaternion.identity) as Canvas;
            //FadeSprite.transform.parent = this.transform;
        }
    }

    //Initialisation
    void Start()
    {
        //Set Instance
        if (mInstance == null)
            mInstance = this;

        isFaded = false;
        InstantiateFade();
    }

    //Fade
    void StartFade(bool Mode)
    {
        //Fade-In
        if (Mode)
        {
            if (FadeSprite.GetComponentInChildren<Image>().color.a > 0.0f)
                f_Alpha -= Time.deltaTime * f_Speed;
            else
            {
                isFaded = true;
                Destroy(FadeSprite.gameObject);
            }
        }

        //Fade-Out
        else
        {
            if (FadeSprite.GetComponentInChildren<Image>().color.a < 1.0f)
                f_Alpha += Time.deltaTime * f_Speed;
            else 
                isFaded = true;
        }
    }

    //Reset
    public void ResetFade(bool Mode)
    {
        //Reset Flag
        isFaded = false;

        //Instantiate the destroyed Fade Object
        if (FadeSprite == null)
            InstantiateFade();

        //Fade-In
        if (Mode)
        {
            this.Type = E_FadeType.FADE_IN;
            FadeSprite.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            this.Type = E_FadeType.FADE_OUT;
            FadeSprite.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

	//Update Func
	void Update () 
    {
        //Fading
        if (FadeSprite != null)
        {
            //Fade's Type
            switch (Type)
            {
                case E_FadeType.FADE_IN:
                    StartFade(true);
                    break;
                case E_FadeType.FADE_OUT:
                    StartFade(false);
                    break;
            }

            //Set Alpha
            FadeSprite.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f, f_Alpha);
        }
	}
}
