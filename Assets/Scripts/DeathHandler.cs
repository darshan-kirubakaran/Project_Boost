using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] int numberOfGamesForAds = 2;

    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem mainEngineParticals;
    [SerializeField] ParticleSystem SuccessParticals;
    [SerializeField] ParticleSystem DeathParticals;

    AudioSource audioSource;

    GameSession gameSession;
    LevelController levelController;
    Rocket rocket;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameSession = FindObjectOfType<GameSession>();
        levelController = FindObjectOfType<LevelController>();
        rocket = FindObjectOfType<Rocket>();
    }

    public void StartSuccessSequence()
    {
        rocket.isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticals.Play();

        if (PlayerPrefs.GetInt("GamesPlayed") % numberOfGamesForAds == 0)
        {
            audioSource.Stop();
            //FindObjectOfType<Ads>().DisplayVideoAD();
        }

        levelController.HandleWinCondition();
    }

    public void StartDeathSequence()
    {
        rocket.isTransitioning = true;
        rocket.canMove = false;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        DeathParticals.Play();

        if (PlayerPrefs.GetInt("GamesPlayed") % numberOfGamesForAds == 0)
        {
            audioSource.Stop();
            //FindObjectOfType<Ads>().DisplayVideoAD();
        }

        gameSession.DeacreaseLives();
    }
}
