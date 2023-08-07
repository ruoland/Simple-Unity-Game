using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public int damage = 2;
    public GameObject startPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // 1. 플레이어의 HP를 떨어트린다
            player.OnHit(damage);
            // 2. 플레이어를 시작 지점으로 되돌린다
            collision.transform.position = startPoint.transform.position;

        }
    }
   
}
