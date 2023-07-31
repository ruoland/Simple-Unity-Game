using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpFor = 400f;
    public GameObject shotObject = null;
    public Transform shotPositionL = null;
    public Transform shotPositionR = null;
    private bool isRight = false;
    private bool jumping = false;
    
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
        else if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Move(h);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Move(h);
            
        }

        isRight = !GetComponent<SpriteRenderer>().flipX;


        if (!jumping)
        { 
            bool jump = Input.GetButtonDown("Jump");

            if (jump)
            {
                GetComponent<Animator>().SetBool("isJump", true);
                GetComponent<Animator>().SetBool("isRun", false);
                Vector2 jumpForce = new Vector2();
                jumpForce.y = jumpFor;
                GetComponent<Rigidbody2D>().AddForce(jumpForce);
                jumping = true;
            }

        }

        //√—ΩÓ±‚
        bool shot = Input.GetButtonDown("Fire1");
        if (shot)
        {
            GetComponent<Animator>().SetTrigger("isShot");
            if (isRight) 
            {
                GameObject bullet = GameObject.Instantiate(shotObject, shotPositionR.position, shotPositionR.rotation);
                bullet.GetComponent<Shot>().Instance(isRight);

            }
            else
            {
                GameObject bullet = GameObject.Instantiate(shotObject, shotPositionL.position, shotPositionL.rotation);
                bullet.GetComponent<Shot>().Instance(isRight);

            }
            

        }


    }

    private void Move(float h)
    {
        GetComponent<Animator>().SetBool("isRun", true);
        Vector3 pos = new Vector3();
        pos.x = h * moveSpeed * Time.deltaTime;
        transform.Translate(pos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
        GetComponent<Animator>().SetBool("isJump", false);

    }
    
}
