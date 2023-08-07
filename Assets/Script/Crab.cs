using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Crab : MonoBehaviour
{
    private int currentHP = 100;
    public int maxHP = 100;

    public int damage = 1;

    public float moveSpeed = 1.5f;
    public List<Vector3> targetVectors = new List<Vector3>();
    private int currentTaget = 0;
    private bool isBackward = false;
    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isWalk", true);
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit == false)
        {
            Move();
        }
    }

    private void Move()
    {
        // 목표지점까지 도달하는 코드
        Vector3 dir = new Vector3();
        // 목표지점까지 위치를 설정하는 코드 X축만
        dir.x = Vector3.MoveTowards(transform.position, targetVectors[currentTaget], moveSpeed * Time.deltaTime).x;
        // Y축은 현재 위치 그대로
        dir.y = transform.position.y;

        // 현재 위치값을 변경된 값으로 치환한다.
        transform.position = dir;

        // 목표지점에 도달했을 때
        Vector3 checkTarget = targetVectors[currentTaget];
        checkTarget.y = transform.position.y;
        if (Vector3.Distance(checkTarget, transform.position) < 0.1f)
        {
            if (isBackward == true)
            {
                // 처음목적지에 도달하면 앞으로 가기
                if (currentTaget == 0)
                {
                    isBackward = false;
                    currentTaget = currentTaget + 1;
                }
                // 처음목적지에 도달하지 않았다면 전 목적지로
                else
                {
                    currentTaget = currentTaget - 1;
                }
            }
            else
            {
                // 최종목적지에 도달하면 뒤로 돌아가기
                if (currentTaget == (targetVectors.Count - 1))
                {
                    isBackward = true;
                    currentTaget = currentTaget - 1;
                }
                // 최종목적지에 도달하지 않았다면 다음 목적지로
                else
                {
                    currentTaget = currentTaget + 1;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 현재 충돌한 오브젝트가 총알인지 검사한다
        if (collision.gameObject.tag == "Shot")
        {
            isHit = true;
            // 총알에서 데미지를 내 현재 HP에서 -한다.
            currentHP = currentHP - collision.gameObject.GetComponent<Shot>().damage;
            //GetComponent<Animator>().SetBool("isWalk", false);
            if (currentHP <= 0)
            {
                // 죽는다.
                // 이 오브젝트를 삭제한다.
                GetComponent<Animator>().SetBool("isDeath", true);
                //GameObject.Destroy(gameObject);
                Invoke("Delete", 0.6f);
            }
            else
            {
                // 아직 안죽음
                GetComponent<Animator>().SetTrigger("isHit");
                Invoke("OffHit", 0.4f);
            }
        }
    }

    private void OffHit()
    {
        isHit = false;
    }

    private void Delete()
    {
        GameObject.Destroy(gameObject);
    }
}
