public enum Policy { RequireAll, RequireOne }

/// <summary>
/// This composite node runs multiple children in parallel.
/// Its return value depends on the policies specified in the constructor.
/// </summary>

public class BTParallel : BTComposite
{
    private readonly Policy successPolicy;
    private readonly Policy failPolicy;
    private readonly bool terminateChildren;

    public BTParallel(string _name, Policy _successPolicy, Policy _failPolicy, params BTBaseNode[] _children) : base(_name, _children)
    {
        successPolicy = _successPolicy;
        failPolicy = _failPolicy;
        terminateChildren = false;
    }

    public BTParallel(string _name, Policy _successPolicy, Policy _failPolicy, bool _terminateChildren, params BTBaseNode[] _children) : base(_name, _children)
    {
        successPolicy = _successPolicy;
        failPolicy = _failPolicy;
        terminateChildren = _terminateChildren;
    }

    protected override TaskStatus Run()
    {
        int failCount = 0;
        int successCount = 0;

        for (int i = 0; i < children.Length; i++)
        {
            BTBaseNode node = children[i];
            TaskStatus childStatus = node.Tick(debug);

            switch (childStatus)
            {
                default:
                    continue;
                case TaskStatus.Failed:
                    failCount++;
                    if (failPolicy == Policy.RequireOne)
                    {
                        if (terminateChildren)
                        {
                            foreach (BTBaseNode n in children)
                            {
                                n.OnTerminate();
                            }
                        }
                        return TaskStatus.Failed;
                    }
                    break;

                case TaskStatus.Success:
                    successCount++;
                    if (successPolicy == Policy.RequireOne)
                    {
                        if (terminateChildren)
                        {
                            foreach (BTBaseNode n in children)
                            {
                                n.OnTerminate();
                            }
                        }
                        return TaskStatus.Success;
                    }
                    break;
            }
        }

        if (failPolicy == Policy.RequireAll && failCount == children.Length)
        {
            if (terminateChildren)
            {
                foreach (BTBaseNode n in children)
                {
                    n.OnTerminate();
                }
            }
            return TaskStatus.Failed;
        }

        if (successPolicy == Policy.RequireAll && successCount == children.Length)
        {
            if (terminateChildren)
            {
                foreach (BTBaseNode n in children)
                {
                    n.OnTerminate();
                }
            }
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }
    
    
}
