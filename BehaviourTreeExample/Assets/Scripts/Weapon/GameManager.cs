using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Utility.ProvideTransformArrayFromType<WeaponPickup>();
        
    }
}
