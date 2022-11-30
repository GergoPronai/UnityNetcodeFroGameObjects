using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyScript : MonoBehaviour
{
    [Header("Player Stuff")]
    [Header("UI Stuff")]
    public TMPro.TextMeshProUGUI PlayerNameText;
    [SerializeField] Sprite[] CharacterImages;
    [SerializeField] private UnityEngine.UI.Image CharacterImage;
    public GameObject instantiateObj;
    
    public void getCharImage(CharacterChoices CharChosen)
    {
        switch (CharChosen)
        {
            case CharacterChoices.Barbarian:
                CharacterImage.sprite =CharacterImages[0];
                break;
            case CharacterChoices.Knight:
                CharacterImage.sprite = CharacterImages[1];
                break;
            case CharacterChoices.Mage:
                CharacterImage.sprite = CharacterImages[2];
                break;
            case CharacterChoices.Rogue:
                CharacterImage.sprite = CharacterImages[3];
                break;
        }
    }
    public void InstanAttackInfos(List<AttackInfo> attackInfos)
    {
        foreach (AttackInfo item in attackInfos)
        {
            GameObject instanOBJ = Instantiate(instantiateObj, transform.GetChild(3).transform);
            instanOBJ.GetComponent<UseAttackScript>().storedAttackInfo = item;
            instanOBJ.GetComponent<UseAttackScript>().name = item.Name;
        }
    }
}
