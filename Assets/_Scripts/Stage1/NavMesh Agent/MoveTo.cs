using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private Transform goal;

    private NavMeshAgent agent;
    private NavMeshPath path;

    public Transform Goal
    {
        get
        {
            return goal;
        }

        set
        {
            goal = value;
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        path = new NavMeshPath();
        agent.CalculatePath(goal.position, path);
        agent.SetPath(path);

        //agent.SetDestination(goal.position);

        StartCoroutine(UpdateGoal());
    }

    private IEnumerator UpdateGoal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //print(agent.pathStatus);

            agent.CalculatePath(goal.position, path);
            agent.SetPath(path);

            //agent.ResetPath();
            //agent.SetDestination(goal.position);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}