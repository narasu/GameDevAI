using System.Collections.Generic;
using UnityEngine;


public class Blackboard : MonoBehaviour
{
    [SerializeReference] public List<BaseSharedVariable> baseSharedVariables = new List<BaseSharedVariable>();

    private Dictionary<string, object> dictionary = new Dictionary<string, object>();

    public T GetVariable<T>(string name) where T : BaseSharedVariable
    {
        if (dictionary.ContainsKey(name))
        {
            return dictionary[name] as T;
        }
        return null;
    }

    public void AddVariable(string name, BaseSharedVariable variable)
    {
        dictionary.Add(name, variable);
    }

    [ContextMenu("Add FloatVariable")]
    public void AddFloatVariable()
    {
        baseSharedVariables.Add(new FloatVariable());
    }

    [ContextMenu("Add GameObjectVariable")]
    public void AddGameObjectVariable()
    {
        baseSharedVariables.Add(new GameObjectVariable());
    }
}
