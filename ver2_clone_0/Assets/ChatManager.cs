using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public TMP_InputField inputField;
    private PlayerAttackInfosAndChosenAttackNumbers localinfos;
    Queue<string> Chat = new Queue<string>();
    private int maxMessages=30;
    public static ChatManager Instance;
    void Start()
    {
        Instance = this;
        
        localinfos = GameObject.FindGameObjectWithTag("CharacterCustomizer").GetComponent<PlayerAttackInfosAndChosenAttackNumbers>();
    }

    // Update is called once per frame
    public void OnEnter(string text)
    {
        Chat.Enqueue(localinfos.PlayerName + ">> " + text);
        if (Chat.Count>maxMessages)
        {
            Chat.Dequeue();
        }
        chatText.text = string.Join("\n", Chat);
        inputField.text = "";
    }
}
