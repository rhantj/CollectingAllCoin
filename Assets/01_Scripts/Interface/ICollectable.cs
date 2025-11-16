using System;

public interface ICollectable
{
    public static event Action OnCollected;
    public void Collected();

    public static void CollectedInvoke()
    {
        OnCollected?.Invoke();
    }
}