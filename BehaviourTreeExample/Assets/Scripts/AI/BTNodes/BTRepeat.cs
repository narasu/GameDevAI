public class BTRepeat : BTDecorator
{
    private int currentIndex;
    private readonly int numRepeats;

    public BTRepeat(BTBaseNode _child, int _numRepeats) : base("Repeat", _child)
    {
        numRepeats = _numRepeats;
    }

    protected override TaskStatus Run()
    {
        TaskStatus childStatus = child.Tick(debug);
        currentIndex++;
        if (currentIndex == numRepeats)
        {
            currentIndex = 0;
            return childStatus;
        }

        return TaskStatus.Running;
    }

}
