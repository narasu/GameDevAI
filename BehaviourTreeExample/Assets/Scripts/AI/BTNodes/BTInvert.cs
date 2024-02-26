public class BTInvert : BTDecorator
{
    public BTInvert(BTBaseNode _child) : base(_child) { }

    protected override TaskStatus Run()
    {
        TaskStatus childStatus = child.Tick();

        switch (childStatus)
        {
            case TaskStatus.Success:
                return TaskStatus.Failed;
            case TaskStatus.Failed:
                return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
