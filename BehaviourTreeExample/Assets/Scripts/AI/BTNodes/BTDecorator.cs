public abstract class BTDecorator : BTBaseNode
{
    private BTBaseNode parent;
    
    protected BTDecorator(BTBaseNode _parent)
    {
        parent = _parent;
    }
    
    public override TaskStatus Run()
    {
        return parent.Run();
    }
}
