using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOneSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private GameObject enemy;

    public void SetUp(GameObject enmy)
    {
        enemy = enmy;
    }

    public void SpawnEnemy()
    {
        //print("spawn");
        // Sorteia um spawn point
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        Transform spawnPoint = spawnPoints[ Random.Range(0, spawnPoints.Count) ];
        print(spawnPoint.gameObject.name);
        //enemy.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, enemy.transform.position.z);
        enemy.transform.position = spawnPoint.position;
        enemy.SetActive(true);
        enemy.GetComponent<NavMeshAgent>().enabled = true;
    }
}
