using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;

    [SerializeField] Vector3 offset;

    private Transform target;

    private float hp;
    private float maxHp;

    void Update()
    {
        // Tạo hiệu ứng máu trôi từ từ xuống trong 5s bằng nội suy giữa fill.amount và hp/maxHp
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp,Time.deltaTime*5f);
        transform.position=target.position + offset;
    }

    public void OnInit(float maxHp,Transform target)
    {
        this.target = target;
        this.maxHp= maxHp;
        hp= maxHp;
        
        imageFill.fillAmount = 1; //100% fill amount thông số của image fill

    }

    public void SetHp(float hp)
    {
        this.hp = hp;

        //Không sử dụng cách này vì máu sẽ tụt 1 mạch xuống hp mà không có hiệu ứng trôi
        //imageFill.fillAmount = hp/maxHp; 
    }
}
