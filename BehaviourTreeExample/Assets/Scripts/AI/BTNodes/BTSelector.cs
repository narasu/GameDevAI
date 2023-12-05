using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTComposite
{
    private int currentIndex = 0;
    public BTSelector(params BTBaseNode[] _children) : base(_children) {}
    public override TaskStatus Run()
    {
        for(; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus status = children[currentIndex].Run();
            switch(status)
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;
                case TaskStatus.Success:
                    currentIndex = 0;
                    return TaskStatus.Success;
                case TaskStatus.Failed: break;
            }
        }
        currentIndex = 0;
        return TaskStatus.Failed;
    }
}