public abstract class BTDecorator : BTBaseNode
{
    protected BTBaseNode child;
    
    protected BTDecorator(BTBaseNode _child)
    {
        child = _child;
    }
}
