using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool canMove = true;
    public bool isTransitioning = false;
    bool collisionAreDisabled = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        deathHandler = FindObjectOfType<DeathHandler>();
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
                deathHandler.StartSuccessSequence();
                break;
            default:
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
