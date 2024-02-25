using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { Success, Failed, Running, Inactive }
public abstract class BTBaseNode
{
    private TaskStatus status = TaskStatus.Inactive;
    public abstract TaskStatus Run();

    protected virtual void OnEnter() {}
    protected virtual void OnExit(TaskStatus _status) {}
    
    public TaskStatus Tick()
    {
        if (status != TaskStatus.Running)
        {
            OnEnter();
        }   
        status = Run();
        if (status != TaskStatus.Running)
        {
            OnExit(status);
        }

        return status;
    }
}
