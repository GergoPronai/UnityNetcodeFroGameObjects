using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textMeshComponent;
    public string[] lines;
    public float text_Speed;
    private int index;

    void Start()
    {
        textMeshComponent.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textMeshComponent.text += c;
            yield return new WaitForSeconds(text_Speed);
        }
    }
    void NextLine()
    {
        if (index< lines.Length-1)
        {
            index++;
            textMeshComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void OnMouseClickedButton()
    {
        if (textMeshComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textMeshComponent.text = lines[index];
        }
    }
}
