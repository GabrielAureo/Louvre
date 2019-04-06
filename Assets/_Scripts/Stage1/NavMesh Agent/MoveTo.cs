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
		UpdateGoal();

		//agent.SetDestination(goal.position);

		StartCoroutine(UpdateGoalTime());
    }

    public IEnumerator UpdateGoalTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
			//print(agent.pathStatus);

			UpdateGoal();

			//agent.ResetPath();
			//agent.SetDestination(goal.position);
		}
    }

	public void UpdateGoal()
	{
		agent.CalculatePath(goal.position, path);
		agent.SetPath(path);
	}

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateGoalTime());
    }
}