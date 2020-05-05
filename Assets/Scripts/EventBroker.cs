using System;
using UnityEngine;

public class EventBroker
{
    public static event Action<int> PlayerDamaged; //events when the player is damaged
    public static event Action PlayerDied; //events when the player dies
    public static event Action<int> SpawnersChanged; //events when spawners are created or destroyed
    public static event Action<int> EnemiesChanged; //events when enemies are spawned or killed
    public static event Action<string> DisplayMessage; //events to display message

    public static void InvokePlayerDamaged(int damage)
    {
        if (PlayerDamaged != null)
            PlayerDamaged(damage);
    }

    public static void InvokePlayedDied()
    {
        if (PlayerDied != null)
            PlayerDied();
    }

    public static void InvokeSpawnersChanged(int changeAmount)
    {
        if (SpawnersChanged != null)
            SpawnersChanged(changeAmount);
    }

    public static void InvokeEnemiesChanged(int changeAmount)
    {
        if (EnemiesChanged != null)
            EnemiesChanged(changeAmount);
    }

    public static void InvokeDisplayMessage(string newMessage)
    {
        if (DisplayMessage != null)
            DisplayMessage(newMessage);
    }
}
