using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueObject : MonoBehaviour
{
    //0 to 1
    public float opacity;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public TextMeshPro textMesh;

    private Color originalColor;

    /// <summary>
    /// Create dialogue object in parent location
    /// </summary>
    /// <returns></returns> the dialogue game object
    public static DialogueObject CreateDialogueObject(GameObject parent, string sentence, float duration = -1)
    {
        
        var dialogPrefab = DialogueManager.Instance.dialogPrefab;
        var instantiate = Instantiate(dialogPrefab, parent.transform.position, parent.transform.rotation, parent.transform);

        var dialogueObject = instantiate.GetComponent<DialogueObject>();
        dialogueObject.textMesh = instantiate.GetComponentInChildren<TextMeshPro>();
        dialogueObject.spriteRenderer = instantiate.GetComponentInChildren<SpriteRenderer>();

        dialogueObject.textMesh.text = sentence;
        
        //destroy obj if duration is +ve
        if (duration > 0)
        {
            DOTween.To(() => dialogueObject.opacity, x => dialogueObject.opacity = x, 0, duration).SetEase(Ease.InCubic);
            Destroy(instantiate, duration);
        }
      
        return dialogueObject;
    }
    
    public void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMeshPro>();
        originalColor = textMesh.color;
    }

    public void Update()
    {
        var spriteRendererSize = new Vector2(textMesh.renderedWidth, textMesh.renderedHeight);
        if (spriteRendererSize.x < 0 || spriteRendererSize.y < 0)
        {
            spriteRendererSize = new Vector2();
        }
        spriteRenderer.size = spriteRendererSize;
        var color = spriteRenderer.color;
        color.a = opacity;
        spriteRenderer.color = color;
        
        color = textMesh.color;
        color.a = opacity;
        textMesh.color = color;
    }

    public void Say(string currDialogue, Color color = new Color())
    {
        if (color != new Color())
        {
            //color added
            textMesh.color = color;
        }
        else
        {
            textMesh.color = originalColor;
        }
        textMesh.text = currDialogue;
    }
}
