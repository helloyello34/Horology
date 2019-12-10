using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, transform.right);


        if (hit.collider)
        {
            lr.SetPosition(1, new Vector3(hit.distance * 4, 0, 0));
        }

        //if (Physics.Raycast(transform.position, transform.right, out hit)) 
        //{
        //    Debug.Log(hit.distance);
        //    //if (hit.collider)
        //    //{
        //    //    lr.SetPosition(1, new Vector3(0, 0, hit.distance));
        //    //}
        //}
    }
}
