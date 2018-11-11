using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateViaKeyboard : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private Behaviour[] components;

    void Update ()
    {
		if (Input.GetKeyDown(key))
        {
            ActivateOrDisactivate();
        }
	}

    private void ActivateOrDisactivate()
    {
        foreach(Behaviour c in components)
        {
            c.enabled = !c.enabled;
        }
    }
}
