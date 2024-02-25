using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTComposite : BTBaseNode
{
    protected BTBaseNode[] children;

    protected BTComposite(params BTBaseNode[] _children)
    {
        children = _children;
    }
}