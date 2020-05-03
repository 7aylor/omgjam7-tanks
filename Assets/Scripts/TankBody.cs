using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour
{
    public event TankDeathHandler TankHasDied;
    public void Death()
    {
        if(TankHasDied != null)
        {
            TankHasDied();
        }
    }
}
