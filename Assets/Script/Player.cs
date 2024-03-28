using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentHP = 3;
    public int maxHP = 3;

    public float moveSpeed = 0.6f;
    public float jumpForce = 0.5f;
    public GameObject shotObject = null;
    public Transform shotPositionR = null;
    public Transform shotPositionL = null;
    private bool jumping = false;

    private bool isHit = false;
    private bool isDeath = false;
    public float hitSecond = 0.6f;

  

    // Start is called before the first frame update
    private void Start()
    {
        currentHP = maxHP;
        UIManager.instance.ShowHP(currentHP, maxHP);
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDeath == false)
        {
            Moving();

            Jump();

            Shoting();

            UIManager.instance.ShowHP(currentHP,maxHP);

        }
    }

    private void Moving()
    {
        // 이동
        float h = Input.GetAxis("Horizontal");
        if (h == 0)
        {
            // 이동을 안하는 경우
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
    }

    private void Move(float h)
    {
        GetComponent<Animator>().SetBool("isRun", true);
        Vector3 pos = new Vector3();
        pos.x = h * moveSpeed * Time.deltaTime;
        transform.Translate(pos);
    }

    private void Jump()
    {
        // 점프
        if (jumping == false)
        {
            bool jump = Input.GetButtonDown("Jump");

            if (jump)
            {
                Vector2 jumpVector = new Vector2();
                jumpVector.y = jumpForce;

                GetComponent<Animator>().SetBool("isRun", false);
                GetComponent<Animator>().SetBool("isJump", true);
                GetComponent<Rigidbody2D>().AddForce(jumpVector);
                jumping = true;
            }
        }
    }

    private void Shoting()
    {
        // 총쏘기
        bool shot = Input.GetButtonDown("Fire1");
        if (shot == true)
        {
            GetComponent<Animator>().SetTrigger("isShot");

            // 총알 생성
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                shotPositionR.GetComponent<AudioSource>().Play();
                GameObject bullet = GameObject.Instantiate(shotObject, shotPositionR.position, shotPositionR.rotation);
                bullet.GetComponent<Shot>().Instance(true);
            }
            else
            {
                shotPositionL.GetComponent<AudioSource>().Play();
                GameObject.Instantiate(shotObject, shotPositionL.position, shotPositionL.rotation).GetComponent<Shot>().Instance(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            // 바닥에 닿았을 때
            jumping = false;
            GetComponent<Animator>().SetBool("isRun", false);
            GetComponent<Animator>().SetBool("isJump", false);
        }
        else if (collision.gameObject.tag == "Enemies")
        {
            OnHit(collision.gameObject.GetComponent<Crab>().damage); 
        }
    }

    public void OnHit(int damage)
    {
        isHit = true;
        // 적에게 닿았을 때
        currentHP = currentHP - damage;

        gameObject.layer = 9;

        // 맞았을 때에는 일정 시간 무적상태가 되야함
        if (currentHP <= 0)
        {
            isDeath = true;
            // 죽음
            GetComponent<Animator>().SetBool("isDeath", true);
            Invoke("ShowGameOver", 1.5f);
        }
        else
        {
            GetComponent<Animator>().SetTrigger("isHit");
            Invoke("OffHit", hitSecond);
        }
    }
    private void OffHit()
    {
        isHit = false;
        gameObject.layer = 3;
    }

    private void ShowGameOver()
    {
        UIManager.instance.ShowGameOver();
    }

}
