public class BTSetBool : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string variable;
    private readonly bool value;

    public BTSetBool(Blackboard _blackboard, string _variable, bool _value) : base("SetBool")
    {
        blackboard = _blackboard;
        variable = _variable;
        value = _value;
    }

    protected override TaskStatus Run()
    {
        blackboard.SetVariable(variable, value);
        return TaskStatus.Success;
    }
}
