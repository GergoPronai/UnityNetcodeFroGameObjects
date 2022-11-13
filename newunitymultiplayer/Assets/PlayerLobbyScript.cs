using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyScript : MonoBehaviour
{
    [Header("Player Stuff")]
    [Header("UI Stuff")]
    public TMPro.TextMeshProUGUI PlayerNameText;
    public TMPro.TextMeshProUGUI PlayerCharacterName;
    public TMPro.TextMeshProUGUI PlayerhealthText;
    [SerializeField] Sprite[] CharacterImages;

    public void getCharImage(CharacterChoices CharChosen)
    {
        switch (CharChosen)
        {
            case CharacterChoices.Barbarian:
                PlayerCharacterName.text = "Barbarian";
                break;
            case CharacterChoices.Knight:
                PlayerCharacterName.text = "Knight";
                break;
            case CharacterChoices.Mage:
                PlayerCharacterName.text = "Mage";
                break;
            case CharacterChoices.Rogue:
                PlayerCharacterName.text = "Rogue";
                break;
        }
    }
}
