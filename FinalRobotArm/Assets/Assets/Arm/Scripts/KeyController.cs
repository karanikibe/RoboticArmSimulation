using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public JointManager _root, _joint1, _joint2;

    public float MoveSpeed = 10;
    public GameObject tipp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        //Motor 1 rotations
        _root.RotateJoint(horz * Time.deltaTime * MoveSpeed);
        //_root.transform.Rotate(new Vector3(0, 1, 0) * horz * Time.deltaTime * MoveSpeed, Space.Self);

        // Motor 2 rots
        _joint1.RotateJoint(vert * Time.deltaTime * MoveSpeed);
        //_joint1.transform.Rotate(new Vector3(0, 1, 0) * vert * Time.deltaTime * MoveSpeed, Space.Self);
        

        if (Input.GetKey(KeyCode.R))
        {
            _joint2.RotateJoint( -1 * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.T))
        {
            _joint2.RotateJoint(1 * Time.deltaTime * MoveSpeed);
            
        }



        //print($"Tip: { tipp.transform.InverseTransformPoint(Vector3.zero)}");
    }
}
