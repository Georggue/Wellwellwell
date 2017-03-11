using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TutorialManager : MonoBehaviour
{
    
    public Text UiText;
    private List<string> TutorialStrings = new List<string>();
    public float ShowTextForSeconds;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool spacePressed = false;
    private bool startedGame = false;
    private CleanUp cleanup;
    private RudiBehaviour rudiBehaviour;
    public UnityAction OnAppleLearned = delegate { };
    public UnityAction NewAppleRequested = delegate { };
    public UnityAction TutorialDone = delegate { };
    // Use this for initialization
    private int appleCounter = 0;
 
    public void HandleTutorialApples()
    {
        appleCounter++;
        if (appleCounter < 2)
        {
            NewAppleRequested();
        }
        else
        {
            cleanup.AppleDestroyed -= NewAppleRequested;
            rudiBehaviour.AppleEaten -= HandleTutorialApples;
            TutorialDone();
        }
       

    }
    void Start()
    {
        cleanup = GameObject.FindObjectOfType<CleanUp>();
        rudiBehaviour = GameObject.FindObjectOfType<RudiBehaviour>();
        rudiBehaviour.AppleEaten += HandleTutorialApples;
        cleanup.AppleDestroyed += NewAppleRequested;
        TutorialStrings.Add("Rudi fell into the well! OH NOES!");
        TutorialStrings.Add(
            "Help Ulf save his brother by pushing apples into the well. Be careful not to fall into the well yourself!");
        TutorialStrings.Add("Hurry up, the water in the well is rising!");
        TutorialStrings.Add("Move left and right with arrow keys, jump with space");
        TutorialStrings.Add("Beware of evil predators, they eat apples and hurt Rudi!");
        TutorialStrings.Add("Enemies can be defeated by throwing them off the screen or jumping on them.");
        StartCoroutine(DisplayTutorialTexts());
    }

    private IEnumerator DisplayTutorialTexts()
    {
        foreach (var text in TutorialStrings)
        {
            UiText.text = text;
            yield return new WaitForSeconds(ShowTextForSeconds);
        }
    }
    private void initGame()
    {
        UiText.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            leftPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            rightPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
       
        if (!startedGame && leftPressed && rightPressed && spacePressed)
        {
            startedGame = true;
            NewAppleRequested();
            initGame();
        }
    }
}