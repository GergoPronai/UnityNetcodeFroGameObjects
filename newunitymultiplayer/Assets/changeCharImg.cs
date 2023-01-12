using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class changeCharImg : MonoBehaviour
{
    public Image playerimg;
    // Start is called before the first frame update
    void Start()
    {
        playerimg.sprite = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<AttackListHolder>().characterImage.sprite;
    }

}
