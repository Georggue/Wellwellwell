using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public AudioSource Squish;
    public AudioSource Dotz;
    public AudioSource Jump;

    public AudioSource RudiEatsGood;
    public AudioSource RudiEatsBad;
    public AudioSource Drowning;

    public AudioSource WinSound;
    public AudioSource LoseSound;

    public AudioSource AppleDropDotz;
    public AudioSource AppleEatenByEnemy;

    public AudioSource EarthWormAppears;

    public enum Sound
    {
     Squish,
     Dotz,
     Jump,
     RudiEatsGood,
     RudiEatsBad,
     Drowning,

     WinSound,
     LoseSound,

     AppleDropDotz,
     AppleEatenByEnemy,

     EarthWormAppears

}

    public void PlaySound(Sound sound)
    {
        AudioSource source = null;
        switch (sound)
        {
            case Sound.Squish:
                source = Squish;
                break;
            case Sound.Dotz:
                source = Dotz;
                break;
            case Sound.Jump:
                source = Jump;
                break;
            case Sound.RudiEatsGood:
                source = RudiEatsGood;
                break;
            case Sound.RudiEatsBad:
                source = RudiEatsBad;
                break;
            case Sound.Drowning:
                source = Drowning;
                break;
            case Sound.WinSound:
                source = WinSound;
                break;
            case Sound.LoseSound:
                source = LoseSound;
                break;
            case Sound.AppleDropDotz:
                source = AppleDropDotz;
                break;
            case Sound.AppleEatenByEnemy:
                source = AppleEatenByEnemy;
                break;
            case Sound.EarthWormAppears:
                source = EarthWormAppears;
                break;
            default:
                source = null;
                break;
        }
        if(source != null)
         PlaySound(source);
    }
    private void PlaySound(AudioSource sound)
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }
    // Use this for initialization
    public static SoundManager Instance;

    void Start()
    {
        Instance = this;
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

  


}