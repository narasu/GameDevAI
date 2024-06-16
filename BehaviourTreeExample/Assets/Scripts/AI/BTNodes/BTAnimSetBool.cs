using UnityEngine;

public class BTAnimSetBool : BTBaseNode
{
    private readonly Animator animator;
    private readonly int animHash;
    private readonly bool value;

    public BTAnimSetBool(Animator _animator, int _animHash, bool _value) : base("AnimSetBool")
    {
        animator = _animator;
        animHash = _animHash;
        value = _value;
    }

    protected override TaskStatus Run()
    {
        animator.SetBool(animHash, value);
        return TaskStatus.Success;
    }
}
