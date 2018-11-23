using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(EnemyOne))]
public class EnemyOneSpawner : MonoBehaviour
{
    private List<Transform> spawnPoints;

    private GameObject player;

    public GameObject Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public List<Transform> SpawnPoints
    {
        get
        {
            return spawnPoints;
        }

        set
        {
            spawnPoints = value;
        }
    }

    public void SpawnEnemy()
    {
        if (spawnPoints.Count > 0)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            Transform spawnPoint = GetNearestSpawnPoint();
            transform.position = spawnPoint.position;
            gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<EnemyOne>().MySpawnPoint = spawnPoint;
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
