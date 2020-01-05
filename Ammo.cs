using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    //[SerializeField] int ammoCapacity = 7;
    
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
        public int currentAmmo;
        public int ammoInGun;
    }


    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoInGun;
    }
    public int GetAmmoInGun(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).currentAmmo;
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoInGun--;  
    }
    public void ReduceAmmoAmount(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }
    

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {       
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    public void ReloadMechanics(AmmoType ammoType)
    {       
        if (GetAmmoSlot(ammoType).ammoAmount >= 0)
        {
            if (GetAmmoSlot(ammoType).ammoAmount >= (GetAmmoSlot(ammoType).currentAmmo - GetAmmoSlot(ammoType).ammoInGun))
            {
                GetAmmoSlot(ammoType).ammoInGun = GetAmmoSlot(ammoType).currentAmmo;
                GetAmmoSlot(ammoType).ammoAmount = GetAmmoSlot(ammoType).ammoAmount - (GetAmmoSlot(ammoType).currentAmmo - GetAmmoSlot(ammoType).ammoInGun);
            }
            else
            {
                GetAmmoSlot(ammoType).ammoInGun = GetAmmoSlot(ammoType).ammoInGun + GetAmmoSlot(ammoType).ammoAmount;
                GetAmmoSlot(ammoType).ammoAmount = 0;
            }
        }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
