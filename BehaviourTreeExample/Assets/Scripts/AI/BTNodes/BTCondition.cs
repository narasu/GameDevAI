/// <summary>
/// This decorator node takes the result of its condition node
/// and executes one of two child nodes based on the result.
/// </summary>

public class BTCondition : BTBaseNode
{
    private BTBaseNode condition;
    private BTBaseNode onSuccess;
    private BTBaseNode onFailure;

    public BTCondition(BTBaseNode _condition, BTBaseNode _onSuccess, BTBaseNode _onFailure) : base("condition")
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

        return conditionStatus;
    }
}
