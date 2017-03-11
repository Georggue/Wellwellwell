using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public float TimeToSpawn;
    public enum SpawnerType
    {
        Food,
        Enemy
    }

    private bool spawnerActive = false;
    public SpawnerType Type;
    private bool directionTrigger = false;

    public void ToggleSpawner()
    {
        spawnerActive = !spawnerActive;
    }

    private void SpawnApple()
    {
        GameObject currentApple = Instantiate(Prefabs[1], transform.position, Quaternion.identity);
        currentApple.SetActive(true);
    }

    private void SpawnEnemy()
    {
        int direction = (directionTrigger) ? -1 : 1;
        GameObject current = Instantiate(Prefabs[0], new Vector3(-8.5f * direction, 0.2f, 0),
            Quaternion.identity);
        if (direction == 1)
        {
            current.GetComponentInChildren<MeshRenderer>().transform.localEulerAngles = new Vector3(-90,-50,-180);
        }
    
        current.transform.localScale = Vector3.one;
        current.SetActive(true);
        current.GetComponent<EnemyMovement>().MoveDir.x = direction;
        directionTrigger = !directionTrigger;
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            switch (Type)
            {
                case SpawnerType.Food:
                    SpawnApple();
                    break;
                case SpawnerType.Enemy:
                    SpawnEnemy();
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(TimeToSpawn);
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    public void SpawnSingleObject(SpawnerType type)
    {
        switch (type)
        {
            case SpawnerType.Enemy:
                SpawnEnemy();
                break;
            case SpawnerType.Food:
                SpawnApple();
                break;
            default:
                break;
        }

    }

    public void StartSpawnLoop()
    {
        StartCoroutine(SpawnObject());
    }


    // Update is called once per frame
    void Update()
    {
    }
}