using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float destroySecond = 3;
    private float leftValue = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = moveSpeed * Time.deltaTime * leftValue;
        transform.Translate(pos);
    }

    public void Instance(bool isRight)
    {
        if (isRight)
        {
            leftValue = 1f;
        }
        else
        {
            leftValue = -1F;
        }

        Invoke("Destroy", destroySecond);
    }

    private void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
