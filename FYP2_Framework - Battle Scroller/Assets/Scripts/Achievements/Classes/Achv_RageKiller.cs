using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// *** Rage Killer Achievement *** //
public class Achv_RageKiller : Achievement
{
    // *** Inherited Virtual Functions *** //
    public override bool Condition()
    {
        switch (this.Tier)
        {
            case A_Tier.TIER_BRONZE:
                return (Global.EnemyKillCount >= 1);
            case A_Tier.TIER_SILVER:
                return (Global.EnemyKillCount >= 2);
            case A_Tier.TIER_GOLD:
                return (Global.EnemyKillCount >= 3);
        }
        return false;
    }
    public override void UnlockAchievement()
    {
        //For Example:
        //Inventory.Instance.AddItem(Item.ItemType.ITEM_KNIFE);
        // ^ What the player gains after unlocking Achievement
        //Different Achievement has different bonuses
        //Thus the virtual function approach
    }
    public override void RebootAchievement()
    {
        Reset();
        //Global.EnemyKillCount = 0;
    }
    protected override void SetMessage()
    {
        switch (this.Tier)
        {
            case A_Tier.TIER_BRONZE:
                this.DisplayMessage = "Achievement: Rage Killer! (1st Enemy) | " + this.TierName();
                break;
            case A_Tier.TIER_SILVER:
                this.DisplayMessage = "Achievement: Rage Killer! (2 Enemies) | " + this.TierName(); ;
                break;
            case A_Tier.TIER_GOLD:
                this.DisplayMessage = "Achievement: Rage Killer! (3 Enemies) | " + this.TierName(); ;
                break;
        }
    }

    //Initialisation
    public Achv_RageKiller()
    {
        this.Type = A_Type.ACHV_KILL_3_ENEMIES;
    }

    //Update Func
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}
