using UnityEngine;

public class Timer
{
    private float length;
    private float currentTime;
    private bool isStarted;

    public Timer(float _length, bool _startImmediately = true)
    {
        length = _length;
        isStarted = _startImmediately;
    }

    public void Start()
    {
        isStarted = true;
    }

    public void Run(bool _fixedDelta, out bool _expired)
    {
        if (!isStarted)
        {
            _expired = false;
            return;
        }
        if (currentTime >= length)
        {
            _expired = true;
            currentTime = .0f;
            isStarted = false;
            return;
        }

        currentTime += _fixedDelta ? Time.fixedDeltaTime : Time.deltaTime;
        _expired = false;
    }

    public void Reset()
    {
        currentTime = .0f;
    }

    public void Stop()
    {
        isStarted = false;
        currentTime = .0f;
    }
}