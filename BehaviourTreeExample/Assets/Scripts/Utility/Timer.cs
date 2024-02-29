using UnityEngine;

public class Timer
{
    private float length;
    private float currentTime;

    public Timer(float _length)
    {
        length = _length;
    }

    public void Run(bool _fixedDelta, out bool _expired)
    {
        if (currentTime >= length)
        {
            _expired = true;
            currentTime = .0f;
            return;
        }

        currentTime += _fixedDelta ? Time.fixedDeltaTime : Time.deltaTime;
        _expired = false;
    }
}