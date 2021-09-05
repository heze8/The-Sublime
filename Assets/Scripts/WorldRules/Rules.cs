using System.Collections.Generic;
using UnityEngine;

public abstract class Rules :MonoBehaviour
{
    public List<Entity> subjectedEntities;
    
    public void FixedUpdate()
    {
        ApplyRule(subjectedEntities);
    }

    protected abstract void ApplyRule(List<Entity> entities);

    protected abstract void ChangeRule();

}