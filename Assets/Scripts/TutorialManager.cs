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

    public UnityAction OnControlsLearned = delegate { };
    public UnityAction OnAppleLearned = delegate { };
    public UnityAction NewAppleRequested = delegate { };
    public UnityAction NewEnemyRequested = delegate { };
    // Use this for initialization
    void Start()
    {
        var cleanup = GameObject.FindObjectOfType<CleanUp>();
        var rudiBehaviour = GameObject.FindObjectOfType<RudiBehaviour>();
        rudiBehaviour.AppleEaten += OnAppleLearned;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
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
            OnControlsLearned();
            initGame();
        }
    }
}