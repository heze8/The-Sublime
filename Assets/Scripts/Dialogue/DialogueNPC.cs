using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueNPC : MonoBehaviour
{    
    [SerializeField]
    public DialogueNode dialog;

    private DialogueNode currDialogNode;
    private List<string> currSentences;
    private int index;
    
    private string currSpoken;
    private bool choicesMode;
    [HideInInspector]
    public DialogueObject output;
    
    private void Start()
    {
        choicesMode = false;
        currDialogNode = dialog;
        currSentences = currDialogNode.sentences;
        output = DialogueObject.CreateDialogueObject(gameObject, "");
        GetNextDialogueChoices();
        wait = true;
    }

    private bool wait;
    [SerializeField] private Color colorChoices;

    private void Update()
    {
        if (!choicesMode)
        {
            if (wait)
            {
                StartCoroutine(Wait(3f, GetNextDialogueChoices));
            }
        }
    }

    IEnumerator Wait(float time, Action call)
    {
        wait = false;
        yield return new WaitForSeconds(time);
        wait = true;
        call.Invoke();
    }

    private void Say(string dialog, Color color = new Color())
    {
        currSpoken = dialog;
        output.Say(currSpoken, color);
    }
    
    private void GetNextDialogueChoices()
    {

        if (currSentences.Count > index)
        {
            Say(currSentences[index]);
            index++;
        }
        else
        {
            if (currDialogNode.children.Count == 0)
            {
                Say("No more choices");
            
                return;
            }
            else
            {
                StringBuilder choices = new StringBuilder();
                // choicesString= new List<string>();
                foreach (var choice in currDialogNode.choices)
                {
                    // var aggregate = child.sentences.Aggregate("", (x, y) => x + " " + y);
                    choices.Append(choice);
                    choices.Append("\n");
                }
                Say(choices.ToString(), colorChoices);
                choicesMode = true;
            }

        }
        
    }
    

    public void SpokenAt(String dialogueSpoken)
    {
        Debug.Log(dialogueSpoken);
        if (!choicesMode) return;
        int i = 0;
        foreach (var choice in currDialogNode.choices)
        {
            int indexOfError = DialogueManager.GetIndexOfError(dialogueSpoken, choice);
            if (indexOfError == -1)
            {
                ReceiveChoice(i);
                return;
            }

            i++;
        }
        Say("?");
        
    }

    private void ReceiveChoice(int i)
    {
        index = 0;
        currDialogNode = currDialogNode.children[i];
        currSentences = currDialogNode.sentences;
        GetNextDialogueChoices();
    }
}
