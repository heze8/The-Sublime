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
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMesh;

    /// <summary>
    /// Create dialogue object in parent location
    /// </summary>
    /// <returns></returns> the dialogue game object
    public static GameObject CreateDialogueObject(GameObject parent, string sentence, float duration, GameObject dialogueObj)
    {
        var instantiate = Instantiate(dialogueObj, parent.transform.position, parent.transform.rotation, parent.transform);
        instantiate.GetComponent<TextMeshPro>().text = sentence;
        var dialogueObject = instantiate.GetComponent<DialogueObject>();
        DOTween.To(() => dialogueObject.opacity, x => dialogueObject.opacity = x, 0, duration).SetEase(Ease.InCubic);
        Destroy(instantiate, duration);
        return instantiate;
    }
    
    public void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    public void Update()
    {
        var spriteRendererSize = new Vector2(textMesh.renderedWidth, textMesh.renderedHeight);
        if (spriteRendererSize.x < 0 || spriteRendererSize.y < 0)
        {
            spriteRendererSize = new Vector2();
        }
        spriteRenderer.size = spriteRendererSize;
        Debug.Log(spriteRendererSize);
        var color = spriteRenderer.color;
        color.a = opacity;
        spriteRenderer.color = color;
        
        color = textMesh.color;
        color.a = opacity;
        textMesh.color = color;
    }
}
