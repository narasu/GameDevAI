public class BTCondition : BTComposite
{
    private readonly BTBaseNode condition;
    private readonly BTBaseNode onSuccess;
    private readonly BTBaseNode onFailed;

    public BTCondition(string _name, BTBaseNode _condition, BTBaseNode _onSuccess, BTBaseNode _onFailed) : base(_name, _condition, _onSuccess, _onFailed)
    {
        condition = _condition;
        onSuccess = _onSuccess;
        onFailed = _onFailed;
    }

    protected override TaskStatus Run()
    {
        TaskStatus conditionStatus = condition.Tick(debug);
		switch (conditionStatus) 
		{
			case TaskStatus.Success:
				onFailed.OnTerminate();
				return onSuccess.Tick(debug);
			case TaskStatus.Failed:
				onSuccess.OnTerminate();
				return onFailed.Tick(debug);
		}
		return conditionStatus;
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
