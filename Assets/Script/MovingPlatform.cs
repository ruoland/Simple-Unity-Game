using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public List<Vector3> targetVectors = new List<Vector3>();
    private int currentTaget = 0;
    private bool isBackward = false;
    public float waitSecond = 1F;

    void Start()
    {
        StartCoroutine(CoMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator CoMove()
    {
        // 현재 위치값을 변경된 값으로 치환한다.
        transform.position = Vector3.MoveTowards(transform.position, targetVectors[currentTaget], moveSpeed * Time.deltaTime);

        // 목표지점에 도달했을 때
        Vector3 checkTarget = targetVectors[currentTaget];


        //1 프레임 대기
        yield return null;

        if (Vector3.Distance(checkTarget, transform.position) < 0.1f)
        {
            //대기시간을 가진다
            yield return new WaitForSeconds(waitSecond);

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
}
