using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    private struct TutorialText
    {
        public string Text;
        public float DisplayTime;
    }
    
    public Text UiText;
    private List<TutorialText> TutorialStrings = new List<TutorialText>();
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool spacePressed = false;
    private bool startedGame = false;
    private CleanUp cleanup;
    private RudiBehaviour rudiBehaviour;
    public UnityAction OnAppleLearned = delegate { };
    public UnityAction NewAppleRequested = delegate { };
    public UnityAction TutorialDone = delegate { };
    public UnityAction AppleRot = delegate { };
    // Use this for initialization
    private int appleCounter = 0;
    public bool TutorialActive = true;
    public bool MenuActive = true;
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
            StopAllCoroutines();
            StartCoroutine(DisplayTutorialTexts(3,6,true));
            TutorialActive = false;
        }
       

    }

    private void HandleAppleRot()
    {
        if (TutorialActive)
        {
            NewAppleRequested();
        }
    }
    void Start()
    {
        Instance = this;
        cleanup = GameObject.FindObjectOfType<CleanUp>();
        rudiBehaviour = GameObject.FindObjectOfType<RudiBehaviour>();
        rudiBehaviour.AppleEaten += HandleTutorialApples;
        AppleRot += HandleAppleRot;
        cleanup.AppleDestroyed += () => { NewAppleRequested(); };

        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f,Text= "Rudi fell into the well! OH NOES!" });
        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f, Text = "Move left and right with arrow keys, jump with space" });
        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f, Text = "Push apples down the well before they go bad" });
        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f, Text = "Enemies eat apples and hurt Rudi!" });
        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f, Text = "Tackle or squish enemies to defeat them!" });
        TutorialStrings.Add(new TutorialText{ DisplayTime = 3f, Text = "Hurry up, the water is rising!" });
        
       
    }

    public void StartTutorial()
    {
        MenuActive = false;
        StartCoroutine(DisplayTutorialTexts(0, 2, false));
    }
    private IEnumerator DisplayTutorialTexts(int from, int to, bool last)
    {
        for (int i = from; i < to; i++)
        {
            UiText.text = TutorialStrings[i].Text;
            yield return new WaitForSeconds(TutorialStrings[i].DisplayTime);
        };
        if(last) UiText.gameObject.SetActive(false);
    }
  
    // Update is called once per frame
    void Update()
    {
        if (MenuActive) return;
        
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
       
        if (!startedGame && (leftPressed || rightPressed) && spacePressed)
        {
            startedGame = true;
            NewAppleRequested();
            StopAllCoroutines();
            StartCoroutine(DisplayTutorialTexts(2, 3, false));

        }
    }
}