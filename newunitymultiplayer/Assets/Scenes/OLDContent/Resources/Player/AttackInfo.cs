using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;

public enum PositionItCanBeUsedIn
{
    First,
    Second,
    Third,
    Fourth,
    All,
    None
}
public enum WeaponType
{
    Melee_SingleTarget,
    Projectile_SingleTarget,
    Magic_SingleTarget,
    Melee_MultiTarget,
    Projectile_MultiTarget,
    Magic_MultiTarget,
    Heal_SingleTarget,
    Attack_Self,
    Heal_MultiTarget,
    None
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
    Acc_up,
}

[Serializable]
public struct AttackInfo
{
    public FixedString32Bytes Name;
    public FixedString32Bytes Description;
    public PositionItCanBeUsedIn Position;
    public WeaponType weaponType;
    public int numberOfTargetsIfApplicable;
    public int Damage;
    public int Accuracy;
    public int CritChance;
    public StatInflicted[] AffectedStats;
    public int AffectStatAmountIfApplicable;
    public StatInflicted[] StatRemovedIfApplicable;

    public AttackInfo(FixedString32Bytes name, FixedString32Bytes description, PositionItCanBeUsedIn position, WeaponType weaponType, int numberOfTargetsIfApplicable, int damage, int accuracy, int critChance, StatInflicted[] affectedStats, int affectStatAmountIfApplicable, StatInflicted[] statRemovedIfApplicable)
    {
        Name = name;
        Description = description;
        Position = position;
        this.weaponType = weaponType;
        this.numberOfTargetsIfApplicable = numberOfTargetsIfApplicable;
        Damage = damage;
        Accuracy = accuracy;
        CritChance = critChance;
        AffectedStats = affectedStats;
        AffectStatAmountIfApplicable = affectStatAmountIfApplicable;
        StatRemovedIfApplicable = statRemovedIfApplicable;
    }
}
