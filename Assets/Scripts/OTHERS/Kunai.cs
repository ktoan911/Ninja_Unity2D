using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject hitVFXPrefab;
    
    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 4f);
    }

    public void OnDespawn()
    {
        
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {

            collision.GetComponent<Characters>().OnHit(30f);
            GameObject hitVFX = Instantiate(hitVFXPrefab,transform.position,transform.rotation);
            OnDespawn();
        }
    }
}
