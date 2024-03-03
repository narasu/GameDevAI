using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abstract base class representing a node in a behavior tree.
/// It provides the common functionality and structure for all types of behavior tree nodes.
/// </summary>
public enum TaskStatus { Success, Failed, Running, Inactive }
public abstract class BTBaseNode
{
    private TaskStatus status = TaskStatus.Inactive;
    protected abstract TaskStatus Run();
    private bool debug;

    protected virtual void OnEnter(bool _debug)
    {
        debug = _debug;
        if (debug)
        {
            Debug.Log(GetType());
        }
    }

    public virtual void OnExit(TaskStatus _status)
    {
        if (debug)
        {
            Debug.Log(GetType() + " " + status);
        }
    }

    public virtual void OnTerminate()
    {
        status = TaskStatus.Inactive;
    }
    
    public TaskStatus Tick()
    {
        if (status != TaskStatus.Running)
        {
            OnEnter(true);
        }   
        status = Run();
        if (status != TaskStatus.Running)
        {
            OnExit(status);
        }

        return status;
    }
}
