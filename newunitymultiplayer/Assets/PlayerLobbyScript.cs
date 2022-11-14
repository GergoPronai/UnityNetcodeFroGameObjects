using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyScript : MonoBehaviour
{
    [Header("Player Stuff")]
    [Header("UI Stuff")]
    public TMPro.TextMeshProUGUI PlayerNameText;
    public TMPro.TextMeshProUGUI PlayerCharacterName;
    [SerializeField] Sprite[] CharacterImages;
    [SerializeField] private UnityEngine.UI.Image CharacterImage;
    public bool privBool=false;

    
    public void getCharImage(CharacterChoices CharChosen)
    {
        switch (CharChosen)
        {
            case CharacterChoices.Barbarian:
                PlayerCharacterName.text = "Barbarian";
                CharacterImage.sprite =CharacterImages[0];
                break;
            case CharacterChoices.Knight:
                PlayerCharacterName.text = "Knight";
                CharacterImage.sprite = CharacterImages[1];
                break;
            case CharacterChoices.Mage:
                PlayerCharacterName.text = "Mage";
                CharacterImage.sprite = CharacterImages[2];
                break;
            case CharacterChoices.Rogue:
                PlayerCharacterName.text = "Rogue";
                CharacterImage.sprite = CharacterImages[3];
                break;
        }
    }
}
