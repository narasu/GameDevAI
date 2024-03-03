/// <summary>
/// Base class for any BT node with a single child that responds to the result of its child in some way.
/// </summary>

public abstract class BTDecorator : BTBaseNode
{
    protected BTBaseNode child;
    
    protected BTDecorator(BTBaseNode _child)
    {
        child = _child;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
        child.OnExit(_status);
    }

    public override void OnTerminate()
    {
        base.OnTerminate();
        child.OnTerminate();
    }
}
