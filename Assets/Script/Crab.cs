using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Crab : MonoBehaviour
{
    private int currentHP = 100;
    public int maxHP = 100;
    public List<Vector3> targetVectors = new List<Vector3>();

    private int currentTarget = 0;
    private bool isBackward = false;
    private bool isHit = false;
    public float moveSpeed = 1f;
   
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isWalk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
            Move();
    }
    
    private void Move()
    {
        //A 지점, B지점
        Vector3 dir = new Vector3();

        dir.x = Vector3.MoveTowards(transform.position, targetVectors[currentTarget], moveSpeed * Time.deltaTime).x;
        dir.y = transform.position.y;
        dir.z = transform.position.z;

        transform.position = dir;

        Vector3 checkTarget = targetVectors[currentTarget];
        checkTarget.y = transform.position.y;
        if (Vector3.Distance(checkTarget, transform.position) < 0.3F)
        {
            if (isBackward)
            {
                //최종 목적지에 도달하면 앞으로 가기
                if (currentTarget == 0)
                {
                    isBackward = false;
                    currentTarget++;
                }
                else
                {
                    currentTarget--;
                }
            }
            else
            {
                //최종 목적지 도착한다면 뒤로 가기
                if (currentTarget == (targetVectors.Count - 1))
                {
                    isBackward = true;
                }
                else
                {
                    currentTarget++;
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Shot")
        {
            isHit = true;

            Shot shot = collision.gameObject.GetComponent<Shot>();
            currentHP -= shot.attackDamage;
            if(currentHP <= 0)
            {
                GetComponent<Animator>().SetBool("isDeath", true);
                GameObject.Destroy(gameObject, 1F);
            }

            GetComponent<Animator>().SetTrigger("isHit");
            Invoke("OffHit", 0.5F);
        }
    }
    
    private void OffHit()
    {
        isHit = false;
    }
}
