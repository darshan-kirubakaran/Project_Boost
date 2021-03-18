using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem mainEngineParticals;
    [SerializeField] ParticleSystem SuccessParticals;
    [SerializeField] ParticleSystem DeathParticals;

    Rigidbody rigidBody;
    AudioSource audioSource;

    GameSession gameSession;
    SceneLoader sceneLoader;
    DeathHandler deathHandler;
    Timer timer;
    LevelButtonManager levelButtonManager;

    public bool canMove = true;
    public bool isTransitioning = false;
    bool collisionAreDisabled = false;
    int GameNumber = 1;
    string currentsceneName;

    void Start()
    {
        currentsceneName = SceneManager.GetActiveScene().name;

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        deathHandler = FindObjectOfType<DeathHandler>();
        timer = FindObjectOfType<Timer>();
        levelButtonManager = FindObjectOfType<LevelButtonManager>();

        FindObjectOfType<GameSession>().AddToNumberOfGamesPlayed(GameNumber);
    }

    void Update()
    {
        ProcessInput();

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            sceneLoader.LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionAreDisabled = !collisionAreDisabled;
        }
    }

    private void ProcessInput()
    {
        if(canMove)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionAreDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                // Stoping Timer...
                timer.StopTimer();

                //Checking if Highsore...
                if (PlayerPrefs.HasKey(currentsceneName) == false)
                {
                    PlayerPrefs.SetFloat(currentsceneName, timer.Returnmsec());
                }
                else if(PlayerPrefs.GetFloat(currentsceneName) > timer.Returnmsec())
                {
                    PlayerPrefs.SetFloat(currentsceneName, timer.Returnmsec());
                }
                deathHandler.StartSuccessSequence();
                break;
            default:
                // Stoping Timer...
                timer.StopTimer();

                deathHandler.StartDeathSequence();
                break;

        }
    }

    private void RespondToThrustInput()
    {
        if (CrossPlatformInputManager.GetButton("Fire") && canMove)
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticals.Stop();
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) // So it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticals.Play();
        print("Is playing VFX");
    }

    private void RespondToRotateInput()
    {
        rigidBody.angularVelocity = Vector3.zero;

        var dirX = Input.acceleration.x * rcsThrust * Time.deltaTime;

        transform.Rotate(-Vector3.forward * dirX);
    }
}
