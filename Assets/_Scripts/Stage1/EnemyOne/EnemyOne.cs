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
            collision.gameObject.GetComponent<Killable>().Die();

            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<MoveTo>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LightedArea"))
        {
            Restart();
        }
    }

    private void Restart()
    {
        gameObject.SetActive(false);
    }

}
