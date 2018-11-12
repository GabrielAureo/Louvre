using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}
