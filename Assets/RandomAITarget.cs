using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RandomAITarget : MonoBehaviour
{
    private AICharacterControl _aiCharacterControl;

    public Transform[] targets;
    // Start is called before the first frame update
    void Start()
    {
        _aiCharacterControl = GetComponent<AICharacterControl>();
        StartCoroutine(SetRandom(0f));
    }

    // Update is called once per frame
    IEnumerator SetRandom(float time)
    {
        yield return new WaitForSeconds(time);
        _aiCharacterControl.target = targets[Random.Range(0, targets.Length)];
        StartCoroutine(SetRandom(Random.Range(2f, 4f)));
    }
}
