using UnityEngine;
using System.Collections;

// *** Combo Master Achievement *** //
public class Achv_ComboMaster : Achievement
{
    // *** Inherited Virtual Functions *** //
    public override bool Condition()
    {
        switch (this.Tier)
        {
            case A_Tier.TIER_BRONZE:
                return (Global.ExecuteFirstComboCheck == 1);
            case A_Tier.TIER_SILVER:
                return (Global.ExecuteFirstComboCheck == 3);
            case A_Tier.TIER_GOLD:
                return (Global.ExecuteFirstComboCheck == 6);
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
    }
    protected override void SetMessage()
    {
        switch (this.Tier)
        {
            case A_Tier.TIER_BRONZE:
                this.DisplayMessage = "Achievement: Combo Master! (1st Combo) | " + this.TierName();
                break;
            case A_Tier.TIER_SILVER:
                this.DisplayMessage = "Achievement: Combo Master! (3 Combos!) | " + this.TierName();
                break;
            case A_Tier.TIER_GOLD:
                this.DisplayMessage = "Achievement: Combo Master! (6 Combos!) | " + this.TierName();
                break;
        }
    }

    //Initialisation
    public Achv_ComboMaster()
    {
        this.Type = A_Type.ACHV_EXECUTED_FIRST_COMBO;
    }

    //Update Func
    void Update()
    {
        //Update from Parent
        this.StaticUpdate();
    }
}

