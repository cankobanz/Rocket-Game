using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandling : MonoBehaviour
{
    [SerializeField] float delay = 1;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip chrashSound;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem chrashParticle;


    AudioSource audioSource;

    bool isTransitioning=false;
    bool collisionDisabled = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled){return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartChrashSequence();
                break;
        }
        
    }
    void StartChrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(chrashSound);
        chrashParticle.Play();
        GetComponent<Movement>().enabled=false;
       Invoke("ReloadLevel", delay);
    }
    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    private void ReloadLevel()
    {
        isTransitioning = false;
        int currentSceneIndex= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void LoadNextLevel()
    {
        isTransitioning = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
