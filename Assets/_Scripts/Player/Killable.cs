﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public void Die()
    {
        print("die");
        GameManager.OnPlayerDied();
    }
}
