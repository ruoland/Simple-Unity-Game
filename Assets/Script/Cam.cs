using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cam : MonoBehaviour
{
    public GameObject playerObj = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = new Vector3();

        pos.x = playerObj.transform.position.x;
        pos.y = playerObj.transform.position.y;
        pos.z = -20;
        transform.position = pos;
    }
}
