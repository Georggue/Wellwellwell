using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject tutorialFoodSpawner;
    private GameObject tutorialEnemySpawner;
    private void ResetGame()
    {
        //todo reset all spawners, ulf, rudi pos, clean up gameobjects
    }

    private void EndGame()
    {
        //todo Show Win/Loss, Trigger potential animations, Trigger reset on space/after x seconds
    }

    public void StartTutorial()
    {

    }

    private void StartGame()
    {
    }

    private void TriggerTutorialApple()
    {
       tutorialFoodSpawner.GetComponentInChildren<Spawner>().SpawnSingleObject(Spawner.SpawnerType.Food);
    }
    private void TriggerTutorialEnemy()
    {
        tutorialEnemySpawner.GetComponentInChildren<Spawner>().SpawnSingleObject(Spawner.SpawnerType.Enemy);
    }
    // Use this for initialization
    void Start()
    {
        var tutorial = GameObject.FindObjectOfType<TutorialManager>();
        tutorial.OnControlsLearned += StartTutorial;
        tutorial.NewAppleRequested += TriggerTutorialApple;
        tutorial.NewEnemyRequested += TriggerTutorialEnemy;
        tutorialFoodSpawner = GameObject.FindWithTag("TutorialFood");
        tutorialEnemySpawner = GameObject.FindWithTag("TutorialEnemy");
    }

    // Update is called once per frame
    void Update()
    {
    }
}