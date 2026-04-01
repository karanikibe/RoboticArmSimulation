using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool Collid = false;

    //private void OnTriggerEnter(Collider other)
    //{
    //    //print($"Triggered by {other.tag}");
    //    if (other.tag == "obstacle")
    //        Collid = true;
    //    else
    //        Collid = false;

    //}
    private void Update()
    {
        CollisionChecker();
    }
    public void CollisionChecker()
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, 0.25f, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].tag == "obstacle")
            {
                if(transform.position.x<4)
                {
                    transform.position += new Vector3(1, 0, 0);
                }
                else
                    transform.position += new Vector3(0, 0, 1);

            }
                
                //Debug.LogError(" Coll: " + transform.position + " at:" + hitColliders[i].name);
                
        }
        //return Collid;
    }
}
