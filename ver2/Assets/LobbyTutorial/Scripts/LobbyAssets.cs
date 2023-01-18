using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAssets : MonoBehaviour {



    public static LobbyAssets Instance { get; private set; }


    [SerializeField] private Sprite Barbarian;
    [SerializeField] private Sprite Knight;
    [SerializeField] private Sprite Mage;
    [SerializeField] private Sprite Rogue;


    private void Awake() {
        Instance = this;
    }

    public Sprite GetSprite(CharacterChoices playerCharacter) {
        switch (playerCharacter) {
            default:
            case CharacterChoices.Barbarian:   return Barbarian;
            case CharacterChoices.Knight:    return Knight;
            case CharacterChoices.Mage:   return Mage;
            case CharacterChoices.Rogue:   return Rogue;
        }
    }

}