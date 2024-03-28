using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage = 40;
    public float moveSpeed = 1.5f;
    private float leftValue = 1f;
    public float destroySecond = 3f;

    // Start is called before the first frame update
    private void Start()
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
        if(isRight == true)
        {
            leftValue = 1f;
        }
        else
        {
            leftValue = -1f;
        }

        Invoke("Delete", destroySecond);
    }

    private void Delete()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 것이 적이었다면
        if(collision.gameObject.tag == "Enemies")
        {
            // 총알을 삭제한다.
            Delete();
        }
        if(collision.gameObject.tag == "Tilemap")
        {
            Delete();
        }
    }
}
