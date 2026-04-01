using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects;
    private bool hasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(CollectableSpawn.spawned)
        {
            objects = GameObject.FindGameObjectsWithTag("enemy");

            if(objects.Length == 0)
            {
                
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        if (hasEnded == false)
        {
            //print($"Ending game at {Time.time}");
            hasEnded = true;
            Invoke("Restart", 1f);
        }
    }
    void Restart()
    {
        CollectableSpawn.spawned = false;
        CollectableSpawn.spawnNow = true;
        SceneManager.LoadScene(0);
        
    }
}
