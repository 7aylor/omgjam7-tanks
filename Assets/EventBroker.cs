using System;
using UnityEngine;

public class EventBroker
{
    public static event Action<int> PlayerDamaged;

    public static void InvokePlayerDamaged(int damage)
    {
        if (PlayerDamaged != null)
            PlayerDamaged(damage);
    }
}
