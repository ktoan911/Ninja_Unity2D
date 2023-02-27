using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    private Vector3 target;
    [SerializeField] private float speed;

    private void Start()
    {
        transform.position = aPoint.position;
        target = bPoint.position;

    }
    void Update()
    {

        //Di chuyển trơn chu từ a đến b với tốc độ speed
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);


        //Quay ngược lại a khi đã tới b
        if(Vector2.Distance(transform.position,bPoint.position) <0.1f)
        {
            target = aPoint.position;
        }

        if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }

    }

    // ===== CHỐNG NẢY =======
    //Va chạm cứng khi 2 object ko có trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            //Khi va chạm player sẽ là con của platform =>di chuyển theo platform
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //ko va cham => ko là con platform nữa
            collision.transform.SetParent(null);
        }
    }



}
