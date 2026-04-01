using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float spd = 5;
    public Transform center, ball;
    Rigidbody m_Rigidbody;
    

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponentInChildren<Rigidbody>();

    }   // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * spd * Time.fixedDeltaTime;

        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //m_Rigidbody.MovePosition(transform.position + m_Input * Time.fixedDeltaTime );
        transform.Translate(Vector3.forward * Time.fixedDeltaTime);
        //print($"Diff: {ball.transform.position.y - center.transform.position.y} ");
    }
}
