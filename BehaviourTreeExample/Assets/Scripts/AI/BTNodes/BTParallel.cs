public enum Policy { RequireAll, RequireOne }
public class BTParallel : BTComposite
{
    private Policy successPolicy;
    private Policy failPolicy;

    public BTParallel(Policy _successPolicy, Policy _failPolicy, params BTBaseNode[] _children) : base(_children)
    {
        successPolicy = _successPolicy;
        failPolicy = _failPolicy;
    }

    protected override TaskStatus Run()
    {
        int failCount = 0;
        int successCount = 0;
        
        foreach (BTBaseNode node in children)
        {
            TaskStatus childStatus = node.Tick();

            switch (childStatus)
            {
                case TaskStatus.Running:
                    continue;
                case TaskStatus.Failed:
                    failCount++;
                    if (failPolicy == Policy.RequireOne)
                    {
                        return TaskStatus.Failed;
                    }
                    break;
                
                case TaskStatus.Success:
                    successCount++;
                    if (successPolicy == Policy.RequireOne)
                    {
                        return TaskStatus.Success;
                    }
                    break;
            }
        }
        if (failPolicy == Policy.RequireAll && failCount == children.Length)
        {
            return TaskStatus.Failed;
        }

        if (successPolicy == Policy.RequireAll && successCount == children.Length)
        {
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }
}
