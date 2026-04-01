using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public ParticleSystem sparks;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print($"Colliding with {collision.collider}");
        if(collision.collider.tag == "Player")
        {
            //var part = transform.Find("Sparks");
            //part.gameObject.SetActive(true);
            //part.GetComponent<ParticleSystem>().Play();
            var spark = Instantiate(sparks, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Destroy(spark, 1.5f);
        }
    }
}
