using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Checkpoint info")]
    [SerializeField] private int nbCheckpoint = 5;
    [SerializeField] private GameObject checkpointPrefab;
    [SerializeField] private Transform checkpointContener;
    [Space]
    [SerializeField] private List<GameObject> checkpoints = new();
    [HideInInspector]
    public int CurrentCheckpointIndex {get; private set;}= 0;

    [Space]
    [SerializeField] private LayerMask obstacleMask;

    [Header("Timer")]
    public float Timer {get; set;} = 60f;
    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;
    public bool IsAWin {get; private set;} = false;

    private int trackSize = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // // spawn all 5 checkpoints
        // for(int i = 0; i < nbCheckpoint; i++)
        // {
        //     CreateCheckpoint();
        // }

         // Turn off all checkpoint VFX
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.GetComponent<Checkpoint>().StopHighlight();
        }

        // Highlight only the first checkpoint
        if (checkpoints.Count > 0)
        {
            HiglightCheckpoint(0);
        }
    }

    // Create checkpoints function
    private void CreateCheckpoint()
    {
        // create new position on the floor
        Vector3 pos = GeneratePos();
        // Verify if there are no obstacle
        IsPositionAvailable(pos);

        // Instantiate CheckpointPrefab
        GameObject checkpoint = Instantiate(checkpointPrefab, pos, this.transform.rotation);
        checkpoint.transform.SetParent(checkpointContener);
    }

    private Vector3 GeneratePos()
    {
        float x = Random.Range(-trackSize, trackSize);
        float z = Random.Range(-trackSize, trackSize);

        return new Vector3(x,.5f,z);
    }

    private bool IsPositionAvailable(Vector3 _pos)
    {
        Debug.Log(!Physics.CheckSphere(_pos, 1, obstacleMask));
        return !Physics.CheckSphere(_pos, 1, obstacleMask);
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        if(!isGameOver)
        {
            Timer -= Time.deltaTime;

            if(Timer <= 0f)
            {
                Timer = 0;
                LoseGame();
            }
        }
    }

    private void LoseGame()
    {
        isGameOver = true;
        IsAWin = false;
        Debug.Log("You lose");
    }

    private void WinGame()
    {
        isGameOver = true;
        IsAWin = true;
        Debug.Log("You Win");
    }

    private void HiglightCheckpoint(int _id)
    {
        if(_id < checkpoints.Count)
        {
            checkpoints[_id].GetComponent<Checkpoint>().StartHiglight();
        }
    }

    public void CheckCheckpoint(GameObject _checkpoint)
    {
        if(CurrentCheckpointIndex <= checkpoints.Count && checkpoints[CurrentCheckpointIndex] == _checkpoint)
        {
            Debug.Log("Correct checkpoint");
            // Stop the current checkpoint's VFX
            _checkpoint.GetComponent<Checkpoint>().StopHighlight();

            // Increase current index to move to the next checkpoint
            CurrentCheckpointIndex++;

            if (IsAllCheckpointReached())
            {
                WinGame();
            }
            else
            {
                HiglightCheckpoint(CurrentCheckpointIndex);
            }
        }
        else
        {
            Debug.Log("Wrong one");
        }
    }

    private bool IsAllCheckpointReached()
    {
        return CurrentCheckpointIndex >= checkpoints.Count;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
