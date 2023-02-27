using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "enemy")
        {
            collision.GetComponent<Characters>().OnHit(30f);
            
        }
    }
}
