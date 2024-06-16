using UnityEngine;

public class BTAnimTrigger : BTBaseNode
{
    private readonly Animator animator;
    private readonly int animHash;

    public BTAnimTrigger(Animator _animator, int _animHash) : base("AnimTrigger")
    {
        animator = _animator;
        animHash = _animHash;
    }

    protected override TaskStatus Run()
    {
        animator.SetTrigger(animHash);
        return TaskStatus.Success;
    }
}
