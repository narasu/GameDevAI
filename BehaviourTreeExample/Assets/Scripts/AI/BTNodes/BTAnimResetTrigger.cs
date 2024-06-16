using UnityEngine;

public class BTAnimResetTrigger : BTBaseNode
{
    private readonly Animator animator;
    private readonly int animHash;

    public BTAnimResetTrigger(Animator _animator, int _animHash) : base("AnimTrigger")
    {
        animator = _animator;
        animHash = _animHash;
    }

    protected override TaskStatus Run()
    {
        animator.ResetTrigger(animHash);
        return TaskStatus.Success;
    }
}
