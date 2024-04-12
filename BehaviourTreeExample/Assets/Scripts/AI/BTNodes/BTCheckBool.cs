public class BTCheckBool : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string blackboardBool;
    private readonly TaskStatus ifTrue;
    private readonly TaskStatus ifFalse;

    public BTCheckBool(
        Blackboard _blackboard,
        string _blackboardBool,
        TaskStatus _ifTrue = TaskStatus.Success,
        TaskStatus _ifFalse = TaskStatus.Failed) : base("CheckBool")
    {
        blackboard = _blackboard;
        blackboardBool = _blackboardBool;
        ifTrue = _ifTrue;
        ifFalse = _ifFalse;
    }

    protected override TaskStatus Run()
    {
        bool isTrue = blackboard.GetVariable<bool>(blackboardBool);

		return isTrue ? ifTrue : ifFalse;
    }
}