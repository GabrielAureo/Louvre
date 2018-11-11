using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(EnemyOne))]
public class EnemyOneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Transform> spawnPoints;

    public void SpawnEnemy()
    {
        if (spawnPoints.Count > 0)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            //Transform spawnPoint = spawnPoints[ Random.Range(0, spawnPoints.Count) ];
            Transform spawnPoint = GetNearestSpawnPoint();
            print(spawnPoint.gameObject.name);
            transform.position = spawnPoint.position;
            gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    // Retorna o spawn point mais próximo do player e que não seja visível para ele
    private Transform GetNearestSpawnPoint()
    {
        Transform retSPoint = spawnPoints[0];

        foreach(Transform t in spawnPoints)
        {
            if (Vector3.Distance(t.position, player.transform.position) < Vector3.Distance(retSPoint.position, player.transform.position) &&
                Vector3.Angle(player.GetComponentInChildren<Camera>().gameObject.transform.forward, t.position - player.transform.position) > 45)
            {
                retSPoint = t;
            }
        }

        return retSPoint;
    }
}
