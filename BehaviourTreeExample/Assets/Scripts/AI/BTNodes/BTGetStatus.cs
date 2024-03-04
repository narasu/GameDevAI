using System;

public class BTGetStatus : BTBaseNode
{
    
    private Blackboard blackboard;
    private string statusString;
    private readonly bool forceWaitForSuccess;

    public BTGetStatus(Blackboard _blackboard, string _statusString, bool _forceWaitForSuccess = false) : base("GetStatus")
    {
        blackboard = _blackboard;
        statusString = _statusString;
        forceWaitForSuccess = _forceWaitForSuccess;
    }

    protected override TaskStatus Run()
    {
        TaskStatus s = blackboard.GetVariable<TaskStatus>(statusString);
        
        if (forceWaitForSuccess)
        {
            return s == TaskStatus.Success ? TaskStatus.Success : TaskStatus.Running;
        }

        return TaskStatus.Failed;
    }
}