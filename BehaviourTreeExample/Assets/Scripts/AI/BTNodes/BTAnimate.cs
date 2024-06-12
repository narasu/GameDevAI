using UnityEngine;

public class BTAnimate : BTDecorator
{
    private readonly Animator animator;
    private readonly int animHash;

    public BTAnimate(Animator _animator, int _animHash, BTBaseNode _child) : base("Animate", _child)
    {
        animator = _animator;
        animHash = _animHash;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        animator.SetBool(animHash, true);
    }

    protected override TaskStatus Run()
    {
        TaskStatus childStatus = child.Tick(debug);
        if (childStatus != TaskStatus.Running)
        {
            animator.SetBool(animHash, false);
        }

        return childStatus;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
        animator.SetBool(animHash, false);
    }

    public override void OnTerminate()
    {
        base.OnTerminate();
        animator.SetBool(animHash, false);
    }
}
