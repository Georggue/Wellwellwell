using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefabs;

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
                    yield return new WaitForSeconds(5);
                    SpawnApple();
                    break;
                case SpawnerType.Enemy:
                    yield return new WaitForSeconds(3);
                    SpawnEnemy();
                    break;
                default:
                    break;
            }
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