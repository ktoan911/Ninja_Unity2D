using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();


            //ALT ENTER để tạo phương thức SavePoint nhanh
            //F12 để truy cập nhanh vào phương thức
            player.SavePoint();
          
        }
    }
}
