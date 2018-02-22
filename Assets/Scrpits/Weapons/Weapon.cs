using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon {
	 public enum WeaponType { Wallbouncer, Scythe, Lance };

    public WeaponType NextType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Wallbouncer:
                return WeaponType.Scythe;
            case WeaponType.Scythe:
                return WeaponType.Lance;
            case WeaponType.Lance:
                return WeaponType.Wallbouncer;
            default: return WeaponType.Wallbouncer;
        }
    }
}
