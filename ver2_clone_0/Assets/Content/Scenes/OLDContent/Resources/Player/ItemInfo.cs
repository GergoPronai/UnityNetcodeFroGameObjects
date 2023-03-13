using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    StatBoost,
    Healing,
    Plot
}
[Serializable]
public class ItemInfo
{
    public Sprite ItemIcon;
    public int StackSize=1;
    public string Name;
    public string Description;
    public float Value;
    public float Cost;
    public string StatThatMightBeBoosted = null;
    public ItemType ItemType;
    public GameObject WeaponParticleSystem = null;
}