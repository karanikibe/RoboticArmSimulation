using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = startPos.position;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = RandomCircle(center, Random.Range(2f, 5));
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;

        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
