
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueTree
{
    public DialogueNode startNode;
}

[Serializable]
public struct DialogueNode 
{
    public List<string> sentences;
    public List<string> choices;
    public List<DialogueNode> children;
}
