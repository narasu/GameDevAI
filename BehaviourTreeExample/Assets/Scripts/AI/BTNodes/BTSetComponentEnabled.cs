using UnityEngine;

public class BTSetComponentEnabled<T> : BTBaseNode where T:MonoBehaviour
{
    private readonly T component;
    private readonly bool enabled;

    public BTSetComponentEnabled(T _component, bool _enabled) : base("SetComponentEnabled")
    {
        component = _component;
        enabled = _enabled;
    }

    protected override TaskStatus Run()
    {
        component.enabled = enabled;
        return TaskStatus.Success;
    }
}
