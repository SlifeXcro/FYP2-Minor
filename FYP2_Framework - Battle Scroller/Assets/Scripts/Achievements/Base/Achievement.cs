using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

// *** Parent Virtual Achievement Class *** //
public class Achievement : MonoBehaviour 
{
    //Achievement's Type
    public enum A_Type
    {
        ACHV_KILL_3_ENEMIES,
        ACHV_EXECUTED_FIRST_COMBO,
        ACHV_COUNT //Total no. of Achievement Types
    } public A_Type Type;

    //Achievement's Tier
    public enum A_Tier
    {
        TIER_BRONZE,
        TIER_SILVER,
        TIER_GOLD,
        TIER_TOTAL
    } public A_Tier Tier = A_Tier.TIER_BRONZE;

    //Increment Tier
    public void IncrementTier()
    {
        //Final Tier
        if ((int)Tier == (int)A_Tier.TIER_TOTAL - 1)
            return;

        //Increment Tier
        Tier = (A_Tier)((int)++Tier);
    }

    //Return's Tier Name
    protected string TierName()
    {
        switch (Tier)
        {
            case A_Tier.TIER_BRONZE:
                return "Tier: Bronze";
            case A_Tier.TIER_SILVER:
                return "Tier: Silver";
            case A_Tier.TIER_GOLD:
                return "Tier: Gold";
        }
        return null;
    }

    //Returns 'True' if Achievement has been unlocked
    public virtual bool isUnlocked() 
    {
        if (Condition() && !this.b_Unlocked)
            return true;
        return false; 
    }
    protected bool b_Unlocked = false;

    //Condition for Unlocking
    public virtual bool Condition() { return false; }

    //Displaying of Achievement
    public Image AchievementBar;

    //Display Achievement 
    public void DisplayAchievement()
    {
        //Check if Achievement has been unlocked
        if (this.isUnlocked())
        {
            Debug.Log(DisplayMessage);

            //Set Display Message
            if (AchievementBar != null && AchievementBar.GetComponentInChildren<Text>() != null)
                AchievementBar.GetComponentInChildren<Text>().text = DisplayMessage;

            //Increment Tier
            IncrementTier();
        }
    }

    //Reset Achievement (Reset variables to prevent constant adding)
    public virtual void RebootAchievement() { Reset(); }
    public void Reset() 
    { 
        if (isUnlocked() && (int)this.Tier >= (int)A_Tier.TIER_TOTAL-1) 
            this.b_Unlocked = true; 
    }

    //Achievement's Bonus (What happens after an Achievement is acquired)
    public virtual void UnlockAchievement() { }

    // *** Variables *** //
    protected string DisplayMessage = "Uninitialised Achievement! (Parent)";

    //Set Display Message
    protected virtual void SetMessage() { }

    //Update
    protected void StaticUpdate()
    {
        SetMessage();
    }

    //Subscribe to Events
    void OnEnable()
    {
        AchievementEventHandler.ShowAchievement += DisplayAchievement;
        AchievementEventHandler.ExecuteAchievement += UnlockAchievement;
        AchievementEventHandler.ResetAchievement += RebootAchievement;
    }

    //Un-Subscribe from Events
    void OnDisable()
    {
        AchievementEventHandler.ShowAchievement -= DisplayAchievement;
        AchievementEventHandler.ExecuteAchievement -= UnlockAchievement;
        AchievementEventHandler.ResetAchievement -= RebootAchievement;
    } 
}
