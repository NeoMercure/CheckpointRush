using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public bool IsReached {get; set;} = false;

    [SerializeField] List<ParticleSystem> particleSystems;

    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.CheckCheckpoint(this.gameObject);
        }
    }

    void Update()
    {

    }

    public void StopHighlight()
    {
        foreach(ParticleSystem pS in particleSystems)
        {
            pS.Stop();
        }
    }

    // Start effect Highlight
    public void StartHiglight()
    {
        foreach(ParticleSystem pS in particleSystems)
        {
            pS.Play();
        }
    }

}
