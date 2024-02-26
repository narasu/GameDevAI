public class BTCondition : BTBaseNode
{
    private BTBaseNode condition;
    private BTBaseNode onSuccess;
    private BTBaseNode onFailure;

    public BTCondition(BTBaseNode _condition, BTBaseNode _onSuccess, BTBaseNode _onFailure)
    {
        condition = _condition;
        onSuccess = _onSuccess;
        onFailure = _onFailure;
    }

    protected override TaskStatus Run()
    {
        TaskStatus conditionStatus = condition.Tick();

        switch (conditionStatus)
        {
            case TaskStatus.Success:
                return onSuccess.Tick();
            case TaskStatus.Failed:
                return onFailure.Tick();
        }

        return TaskStatus.Running;
    }

    public override void OnExit(TaskStatus _status)
    {
        condition.OnExit(_status);
        onSuccess.OnExit(_status);
        onFailure.OnExit(_status);
        base.OnExit(_status);
    }
}
