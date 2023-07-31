using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public Vector3 targetA = new Vector3(0, 0, 0);
    public Vector3 targetB = new Vector3(0, 0, 0);
    public bool ADir = true;

    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //A 지점, B지점
        if(ADir)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB, moveSpeed * Time.deltaTime);
        }

        if (transform.position == targetA)
        {
            ADir = false;
        }
        else 
        {
            ADir = true;
        }
    }
}
