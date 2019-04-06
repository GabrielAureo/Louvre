using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
    private Animator anim;
    private Transform mySpawnPoint;
	private NavMeshAgent agent;
	private MoveTo moveTo;

	public Transform MySpawnPoint
    {
        get
        {
            return mySpawnPoint;
        }

        set
        {
            mySpawnPoint = value;
        }
    }

    private void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		moveTo = GetComponent<MoveTo>();
		anim = GetComponent<Animator>();
        anim.SetBool("WalkBackwards", false);
		
    }

    private void Update()
    {
        DestroyWhenArrivedAtStartPoint();

		anim.SetFloat("BlendX", agent.velocity.magnitude / 4);
	}

    private void DestroyWhenArrivedAtStartPoint()
    {
        if (anim.GetBool("WalkBackwards") && Vector3.Distance(transform.position, mySpawnPoint.position) <= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<MoveTo>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Animator>().enabled = false;

            collision.gameObject.GetComponent<Killable>().Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LightedArea"))
        {
			agent.isStopped = true;
			StartCoroutine(WalkBackwards());
		}
    }

	private IEnumerator WalkBackwards()
	{		
		yield return new WaitForSeconds(2f);

		agent.isStopped = false;
		StopCoroutine(moveTo.UpdateGoalTime());
		moveTo.Goal = mySpawnPoint;
		moveTo.UpdateGoal();
		anim.SetBool("walkbackwards", true);
		Physics.IgnoreCollision(GetComponent<Collider>(), FindObjectOfType<CharacterController>().GetComponent<Collider>());

	}
}
