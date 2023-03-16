using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class Dialogue : NetworkBehaviour
{

    public TextMeshProUGUI textMeshComponent;
    public string[] lines;
    public float text_Speed;
    private int index;

    

    public void StartDialogue()
    {
        if (IsLocalPlayer)
        {
            this.textMeshComponent.text = "Interact with Tutorial man?";
            this.index = 0;
            this.NextLine();
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            this.textMeshComponent.text += c;
            yield return new WaitForSeconds(this.text_Speed);
        }
    }
    void NextLine()
    {
        if (this.index < this.lines.Length-1)
        {
            this.index++;
            this.textMeshComponent.text = string.Empty;
            this.StartCoroutine(TypeLine());
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void OnMouseClickedButton()
    {
        if (this.textMeshComponent.text == this.lines[index])
        {
            this.NextLine();
        }
        else
        {
            this.StopAllCoroutines();
            this.textMeshComponent.text = this.lines[index];
        }
    }
}
