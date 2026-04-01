using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class CollectableSpawn : MonoBehaviour
{
    public GameObject Enemy;
    public static bool spawned = false, spawnNow = false;
    Vector3 pos;
    [SerializeField] private int spwanNumber = 3;
    private float w , h ;
    void start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        h = (MazeRenderer.h * 0.5f) - 0.2f;
        w = (MazeRenderer.w * 0.5f) - 0.2f; 
        if (Input.GetKeyDown(KeyCode.Space) || spawnNow)
        {
            SpawnEnemy();
            spawnNow = false;
        }
    }
    public void SpawnEnemy()
    {
        for (int i = 0; i < spwanNumber; i++)
        {
            pos = new Vector3(Random.Range(-w, w), 0, Random.Range(-h, h));
            Instantiate(Enemy, pos, Quaternion.identity);


        }
        spawned = true;
    }
}
