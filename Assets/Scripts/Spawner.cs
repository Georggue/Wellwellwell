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
                    break;
                case SpawnerType.Enemy:
                    yield return new WaitForSeconds(3);
                    int direction = (directionTrigger) ? -1 : 1;
                    GameObject current =  Instantiate(Prefabs[0], new Vector3(-8.5f* direction, 0.2f, 0),Quaternion.identity);
                    current.transform.localScale = new Vector3(1.74f, 0.46f, 1.1f);
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
