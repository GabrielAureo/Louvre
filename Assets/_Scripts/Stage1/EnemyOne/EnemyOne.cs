using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("colisão com player");
            collision.gameObject.GetComponent<Killable>().Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("LightedArea"))
        {
            print("área iluminada");
            Restart();
        }
    }

    private void Restart()
    {
        gameObject.SetActive(false);
    }

}
