using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // ¿Ãµø 
        float h = Input.GetAxis("Horizontal");

        if (h == 0)
        {
            GetComponent<Animator>().SetBool("isRun", false);
        }
        else if(h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Move(h);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Move(h);
        }
    }

    private void Move(float h)
    {
        GetComponent<Animator>().SetBool("isRun", true);
        Vector3 pos = new Vector3();
        pos.x = h * moveSpeed * Time.deltaTime;
        transform.Translate(pos);
    }
}
