using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

public enum PositionItCanBeUsedIn
{

    None,
    First,
    Second,
    Third,
    Fourth,
    All
}
public enum WeaponType
{

    None,
    Melee_SingleTarget,
    Projectile_SingleTarget,
    Magic_SingleTarget,
    Melee_MultiTarget,
    Projectile_MultiTarget,
    Magic_MultiTarget,
    Heal_SingleTarget,
    Attack_Self,
    Heal_MultiTarget
}
public enum StatInflicted
{
    None,
    Stun,
    Poison,
    bleed,
    Move_Self,
    Move_Target,
    Remove_Stats,
    DMG_up,
    Remove_all,
    Dmg_down,
    Acc_down,
    Acc_up
}

[Serializable]
public class AttackInfo
{
    public string Name;
    public string Description;
    public PositionItCanBeUsedIn Position;
    public WeaponType weaponType;
    public int numberOfTargetsIfApplicable;
    public float Damage;
    public float Accuracy;
    public float CritChance;
    public StatInflicted[] AffectedStats;
    public int AffectStatAmountIfApplicable;
    public StatInflicted[] StatRemovedIfApplicable;

}
