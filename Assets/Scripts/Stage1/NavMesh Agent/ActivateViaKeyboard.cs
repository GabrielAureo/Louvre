using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateViaKeyboard : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private Behaviour[] components;

    //private MoveTo moveToScript;
    //private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        //moveToScript = GetComponent<MoveTo>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
    }

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

        //moveToScript.enabled = !moveToScript.enabled;
        //navMeshAgent.enabled = !navMeshAgent.enabled;
    }
}
