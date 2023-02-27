using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    public float speed;

    void Start()
    {
       // target = FindObjectOfType<===PLAYER===>().transform;
       
        //kéo thả trực tiếp object player vào target 
    }

    // Update is called once per frame
    void LateUpdate() // lateupdate là hàm sau hàm update nên theo update nên có thể giật nên p đồng bộ bằng fixedupdate
    {

        //Cộng offset do nếu ko có thì cam sẽ dính vào player phải để cam cách player 1 khoảng nhất định
        transform.position = Vector3.Lerp(transform.position,target.position + Offset, Time.deltaTime*speed);


    }


}
