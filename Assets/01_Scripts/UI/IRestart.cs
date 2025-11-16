using System;

public static class RestartEvent
{
    public static event Action OnRestart;

    public static void RestartInvoke()
    {
        OnRestart?.Invoke();
    }
}