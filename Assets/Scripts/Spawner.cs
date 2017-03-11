using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public float TimeToSpawn;
    private float initialTime;

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
        Vector3 applePos = transform.position + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-1f, 1f), 0);
        GameObject currentApple = Instantiate(Prefabs[1], applePos, Quaternion.identity);
        currentApple.SetActive(true);
    }

    private void SpawnEnemy()
    {
        float directionRand = Random.Range(-1f, 1f);
        float posRand = Random.Range(-1f, 1f);
        float ypos = (posRand > 0) ? 0.2f : -5f;

        int direction = (directionRand > 0) ? 1 : -1;
        GameObject current = Instantiate(Prefabs[0], new Vector3(-8.5f * direction, ypos, -1f),
            Quaternion.identity);
        if (direction == 1)
        {
            Vector3 rotations = new Vector3(-90, -50, -180);
            if (ypos < 0)
            {
                rotations.x += 45;
                current.GetComponent<Rigidbody2D>().gravityScale = 0;
                current.GetComponent<EnemyMovement>().MoveDir.y = 1f;
                current.layer = 11;
                var angle = Random.Range(-5f, 5f);
                rotations.x += angle;
                current.GetComponent<EnemyMovement>().MoveDir= current.GetComponent<EnemyMovement>().MoveDir.Rotate(angle);
            }
            current.GetComponentInChildren<MeshRenderer>().transform.localEulerAngles = rotations;
        }
        else if (direction == -1)
        {
            Vector3 rotations = new Vector3(-90, 50, -180);
            if (ypos < 0)
            {
                rotations.x += 45;
                current.GetComponent<Rigidbody2D>().gravityScale = 0;
                current.GetComponent<EnemyMovement>().MoveDir.y = 1f;
                var angle = Random.Range(-5f, 5f);
                rotations.x += angle;
                current.GetComponent<EnemyMovement>().MoveDir = current.GetComponent<EnemyMovement>().MoveDir.Rotate(angle);
                current.layer = 11;
            }
            current.GetComponentInChildren<MeshRenderer>().transform.localEulerAngles = rotations;
        }
        

        current.transform.localScale = Vector3.one;
        current.SetActive(true);
        current.GetComponent<EnemyMovement>().MoveDir.x = direction;
        directionTrigger = !directionTrigger;
    }

    private IEnumerator SpawnObject()
    {
        initialTime = TimeToSpawn;
        while (true)
        {
            switch (Type)
            {
                case SpawnerType.Food:
                    TimeToSpawn = initialTime + Random.Range(0, 4);
                    SpawnApple();
                    break;
                case SpawnerType.Enemy:
                    TimeToSpawn = initialTime + Random.Range(0, 2);
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