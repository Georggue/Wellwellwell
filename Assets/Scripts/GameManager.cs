using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject tutorialFoodSpawner;
    public List<GameObject> Spawners;
    public Text EndText;
    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3);
        EndText.gameObject.SetActive(false);
        foreach (var spawner in Spawners)
        {
            spawner.SetActive(false);
        }
        var rudi = GameObject.FindObjectOfType<RudiBehaviour>();
        rudi.Reset();
        var ulf = GameObject.FindObjectOfType<PlayerController>();
        ulf.Reset();
        var water = GameObject.FindObjectOfType<WaterRising>();
        water.Reset();
        //todo reset all spawners, ulf, rudi pos, clean up gameobjects
    }

    private void EndGame(bool win)
    {
        EndText.gameObject.SetActive(true);
        if (win)
        {
            
            EndText.text = "You Win!";
        }
        else
        {
            EndText.text = "You Die! :'( ";
        }
        StartCoroutine(ResetGame());
        //todo Show Win/Loss, Trigger potential animations, Trigger reset on space/after x seconds
    }

    

    private void StartGame()
    {
        foreach (var spawner in Spawners)
        {
            spawner.SetActive(true);
            spawner.GetComponentInChildren<Spawner>().StartSpawnLoop();
        }
    }

    private void TriggerTutorialApple()
    {
       tutorialFoodSpawner.SetActive(true);
       tutorialFoodSpawner.GetComponentInChildren<Spawner>().SpawnSingleObject(Spawner.SpawnerType.Food);
    }
   
    // Use this for initialization
    void Start()
    {
        var tutorial = GameObject.FindObjectOfType<TutorialManager>();
        tutorial.NewAppleRequested += TriggerTutorialApple;
        tutorial.TutorialDone += StartGame;
        var rudi = GameObject.FindObjectOfType<RudiBehaviour>();
        rudi.GameEnd += EndGame;

    }

    // Update is called once per frame
    void Update()
    {
    }
}