using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    //public float delayTimeDestroy = 5;
    //public float currentTime = 0;

    private void Update()
    {
        //if (currentTime > delayTimeDestroy)
        //{
        //    SelftDespawn();
        //}
        //currentTime += Time.deltaTime;

        Invoke(nameof(SelftDespawn), 5);
    }
    private void SelftDespawn()
    {
        Destroy(gameObject);
    }

}
