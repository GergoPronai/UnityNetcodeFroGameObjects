using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInfosAndChosenAttackNumbers : MonoBehaviour
{


    public List<AttackInfo> attackInfos = new List<AttackInfo>();
    public int CharChosen_ChosenAttacks_1 = 0;
    public int CharChosen_ChosenAttacks_2 = 0;
    public int CharChosen_ChosenAttacks_3 = 0;
    public CharacterChoices character;
    public string PlayerName="";
    public int PlayerHealth=0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
