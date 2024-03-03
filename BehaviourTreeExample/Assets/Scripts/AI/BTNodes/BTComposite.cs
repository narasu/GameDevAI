using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for any BT node with multiple children.
/// </summary>

public abstract class BTComposite : BTBaseNode
{
    protected BTBaseNode[] children;

    protected BTComposite(params BTBaseNode[] _children)
    {
        children = _children;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
        foreach (BTBaseNode n in children)
        {
            n.OnExit(_status);
        }
    }
    
    public override void OnTerminate()
    {
        base.OnTerminate();
        foreach (BTBaseNode n in children)
        {
            n.OnTerminate();
        }
    }
}