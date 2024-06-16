using UnityEngine;

public class BTPlaySound : BTBaseNode
{
    private readonly AudioSource source;
    private readonly AudioClip clip;

    public BTPlaySound(AudioSource _source, AudioClip _clip) : base("PlaySound")
    {
        source = _source;
        clip = _clip;
    }

    protected override TaskStatus Run()
    {
        source.PlayOneShot(clip);
        return TaskStatus.Success;
    }
}
