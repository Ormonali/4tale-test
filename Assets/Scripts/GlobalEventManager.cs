using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class GlobalEventManager
{
    public static UnityEvent<bool> OnCoinCollision = new UnityEvent<bool>();

    public static void SendOnCoinCollision()
    {
        OnCoinCollision.Invoke(true);
    }
}