using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTComposite
{
    private int currentIndex = 0;
    public BTSequence(params BTBaseNode[] _children) : base(_children) {}

    public override TaskStatus Run()
    {
        for(; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus status = children[currentIndex].Run();

            switch (status)
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;
                case TaskStatus.Failed:
                    currentIndex = 0;
                    return TaskStatus.Failed;
                case TaskStatus.Success: break;
            }
        }
        currentIndex = 0;
        return TaskStatus.Success;
    }
}