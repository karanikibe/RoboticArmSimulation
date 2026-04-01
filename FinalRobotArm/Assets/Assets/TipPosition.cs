using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPosition : MonoBehaviour
{
    public GameObject Target, Motorbase;

    // Update is called once per frame
    void Update()
    {
        print($"Tip: {transform.position - Motorbase.transform.position } Target: {Target.transform.position}");
        print("Distance: " + Vector3.Distance(transform.position, Target.transform.position));
    }
}
