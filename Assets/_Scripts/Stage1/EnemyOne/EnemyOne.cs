using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
    private Animator anim;
    private Transform mySpawnPoint;

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
        anim = GetComponent<Animator>();
        anim.SetBool("WalkBackwards", false);
    }

    private void Update()
    {
        DestroyWhenArrivedAtStartPoint();
    }

    private void DestroyWhenArrivedAtStartPoint()
    {
        if (anim.GetBool("WalkBackwards") && Vector3.Distance(transform.position, mySpawnPoint.position) <= 0.1f)
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
            //Destroy(gameObject);
            GetComponent<MoveTo>().Goal = mySpawnPoint;
            anim.SetBool("WalkBackwards", true);
            Physics.IgnoreCollision(GetComponent<Collider>(), FindObjectOfType<CharacterController>().GetComponent<Collider>());
        }
    }
}
