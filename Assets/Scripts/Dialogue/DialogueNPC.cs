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
    private List<string> choicesString;
    private bool choicesMode;
    [HideInInspector]
    public DialogueObject output;
    private List<Char> alphabet = new List<Char>{'a', 'b', 'c','d','e'};
    
    private void Start()
    {
        choicesMode = false;
        currDialogNode = dialog;
        currSentences = currDialogNode.sentences;
        output = DialogueObject.CreateDialogueObject(gameObject, "");
        GetNextDialogueChoices();

    }

    private void Say(string dialog)
    {
        currSpoken = dialog;
        output.Say(currSpoken);
    }
    private void GetNextDialogueChoices()
    {
        if (currSentences.Count == index)
        {
            index++;
            Say(currSentences[index]);
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
                Say(choices.ToString());
                choicesMode = true;
            }

        }
        
    }



    private void GenerateResponse()
    {
        var text = new StringBuilder();
        for (var i = 0; i < Random.Range(5, 20); i++)
        {
            foreach (Char c in alphabet)
            {
                text.Append(c, Random.Range(1, 5));
            }
        }
        Say(text.ToString());
    }
    

    public void SpokenAt(String dialogueSpoken)
    {
        if (!choicesMode) return;
        int i = 0;
        foreach (var choice in currDialogNode.choices)
        {
            int indexOfError = DialogueManager.GetIndexOfError(dialogueSpoken, choice);
            if (indexOfError == -1)
            {
                ReceiveChoice(i);
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

    }
}
