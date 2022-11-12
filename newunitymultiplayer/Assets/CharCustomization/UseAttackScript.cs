using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
enum TargetAmount
{
    Single,
    Two,
    Three,
    All
}
public class UseAttackScript : NetworkBehaviour
{
    public TMPro.TextMeshProUGUI AttackName;
    public AttackInfo storedAttackInfo;
    private TargetAmount amountofTargets;
    // Start is called before the first frame update
    void Start()
    {
        AttackName.text = storedAttackInfo.Name.Value.ToString();
    }

    public void WhatDo()
    {
        switch (storedAttackInfo.weaponType)
        {
            case WeaponType.Melee_SingleTarget:
                amountofTargets = TargetAmount.Single;
                break;
            case WeaponType.Projectile_SingleTarget:
                amountofTargets = TargetAmount.Single;
                break;
            case WeaponType.Magic_SingleTarget:
                amountofTargets = TargetAmount.Single;
                break;
            case WeaponType.Melee_MultiTarget:
                if (storedAttackInfo.numberOfTargetsIfApplicable == 2)
                {
                    amountofTargets = TargetAmount.Two;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable == 3)
                {
                    amountofTargets = TargetAmount.Three;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable > 3)
                {
                    amountofTargets = TargetAmount.All;
                }
                break;
            case WeaponType.Projectile_MultiTarget:
                break;
            case WeaponType.Magic_MultiTarget:
                if (storedAttackInfo.numberOfTargetsIfApplicable == 2)
                {
                    amountofTargets = TargetAmount.Two;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable == 3)
                {
                    amountofTargets = TargetAmount.Three;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable > 3)
                {
                    amountofTargets = TargetAmount.All;
                }
                break;
            case WeaponType.Heal_SingleTarget:
                amountofTargets = TargetAmount.Single;
                break;
            case WeaponType.Attack_Self:
                break;
            case WeaponType.Heal_MultiTarget:
                if (storedAttackInfo.numberOfTargetsIfApplicable==2)
                {
                    amountofTargets = TargetAmount.Two;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable == 3)
                {
                    amountofTargets = TargetAmount.Three;
                }
                else if (storedAttackInfo.numberOfTargetsIfApplicable > 3)
                {
                    amountofTargets = TargetAmount.All;
                }
                break;
        }
    }
    void SelectTarget()
    {
        switch (amountofTargets)
        {
            case TargetAmount.Single:
                break;
            case TargetAmount.Two:
                break;
            case TargetAmount.Three:
                break;
            case TargetAmount.All:
                break;
            default:
                break;
        }
    }
}
