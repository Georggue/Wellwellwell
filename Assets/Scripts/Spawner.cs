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
    public SpawnerType Type;
    private bool directionTrigger = false;
    private IEnumerator SpawnObject()
    {
        while (true)
        {
            
            switch (Type)
            {
                case SpawnerType.Food:
                    yield return new WaitForSeconds(5);
                    GameObject currentApple = Instantiate(Prefabs[1], transform.position, Quaternion.identity);
                    currentApple.SetActive(true);
                    break;
                case SpawnerType.Enemy:
                    yield return new WaitForSeconds(3);
                    int direction = (directionTrigger) ? -1 : 1;
                    GameObject current =  Instantiate(Prefabs[0], new Vector3(-8.5f* direction, 0.2f, 0),Quaternion.identity);
                    current.transform.localScale = Vector3.one;
                    current.SetActive(true);
                    current.GetComponent<EnemyMovement>().MoveDir.x = direction;
                    directionTrigger = !directionTrigger;
                    break;
                default:
                    break;
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
