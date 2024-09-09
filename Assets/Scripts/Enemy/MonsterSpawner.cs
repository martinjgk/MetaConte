using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnLocation;
    public int monsterCount = 5;
    private List<GameObject> monsters = new List<GameObject>();
    private float respawnTime = 30.0f;
    private bool isSpawning = false;

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        Debug.Log("Spawning monsters.");
        for (int i = 0; i < monsterCount; i++)
        {
            GameObject monster = Instantiate(monsterPrefab, spawnLocation.position, Quaternion.identity);
            monsters.Add(monster);
            monster.GetComponent<Monster>().OnDeath += HandleMonsterDeath;
        }
        isSpawning = false;
    }

    void HandleMonsterDeath(GameObject monster)
    {
        Debug.Log("Handling monster death.");
        monsters.Remove(monster);

        if (monsters.Count == 0 && !isSpawning)
        {
            Debug.Log("All monsters dead. Starting respawn timer.");
            StartCoroutine(RespawnMonsters());
        }
    }

    IEnumerator RespawnMonsters()
    {
        isSpawning = true;
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Respawning monsters after delay.");
        SpawnMonsters();
    }
}
