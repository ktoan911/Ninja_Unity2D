using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player : Characters
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float horizontal;

    

    [SerializeField][Range(0f, 2000f)] private float speed=5;

    [SerializeField] private float jumpForce;

   // public Transform ground;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttacked;
    private bool isDead ;


    public Vector3 savePoint;

    private int Coins;

    [SerializeField] private Kunai kunaiPrerfab;
    [SerializeField] private Transform throwPoint;
   
    
    [SerializeField] private GameObject attackArea;


    private void Start()
    {
        Coins = PlayerPrefs.GetInt("coin", 0); // chỉnh coin về giá trị mặc định bằng 0 khi start


        SavePoint();
        OnInit();            //tại sao phải gọi lại ?????
    }

    void Update()
    {
        
        isGrounded = CheckedGround();

        // ====== Dead Condition ======
        if(isDead)
        {
            base.OnDeath();
            
            return;
        }


        // ===== Atack Condition ========
        if(isAttacked)
        {
            rb.velocity= Vector2.zero;
            return;
        }

        // ======JUMP======= 

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }


            //trả về true khi gõ space
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
                return;
            }

            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");   // làm vậy để tránh trường hợp đang nhảy mà run thì nó chuyển anim
            }


            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
                return;
            }


            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
                return;
                
            }

                
        }




        //check fall
        if(!isGrounded && rb.velocity.y < 0f) // Khi ko đứng trên mặt đất và vận tốc bé hơn 0 do phi xuống dưới
        {
            
            ChangeAnim("fall");
            isJumping = false;
            return;
            
        }


        //========MOVING===========

        //-1 0 1 trai dungim phai
       // horizontal = Input.GetAxisRaw("Horizontal");

        //Ko su dung translate do ko tinh toan duoc vat li => xuyen qua tuong => su dung rigidbody



        if (Mathf.Abs(horizontal) > 0.1f) //float ko nên so sánh trực tiếp ==0 hoặc !=0 do có thể có sai số
        {
            


            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);


            // scale.x = -1 thi gameobhect se quay lại theo huong x 1 goc 180 độ....Tuy nhiên cách này không nên dùng vì không tối ưu được game
            //Khac phuc la doi rotation y =180 độ

           
            transform.rotation =Quaternion.Euler(new Vector3(0,horizontal > 0 ? 0:180, 0)); //horizontal >0 góc quay =0 (quay phải) ko thì ngược lại



        }
        else if(isGrounded) // neu ko co ham else if nay thi ngay ca khi o tren 0 no van sẽ nhảy vào trong else
        {
            ChangeAnim("idle");


            rb.velocity = Vector2.zero;


            
        }
    }




    private bool CheckedGround()
    {
        //Vector3 a = transform.position - ground.transform.position;

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.3f, Color.red);

        //raycast: chieu ra cac tia tu vi tri vat theo huong xuong duoi trong layerGround coi do dai tia la 1.3
        //transform.position la vi tri cua vat
        //vecto2.down la huong chieu tia
        //ham raycast neu cac tia ko chieu trung gi tra ket qua la null

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down , 1.2f  , layerGround);

        

        //// If it hits something... (now is collider)

        //if (hit.collider != null) return true;
        //else return false;


        //Another way

        return hit.collider != null;

       
    }


    public void Jump()
    {
        isJumping = true;

        ChangeAnim("jump");

        //Addforce tác dụng một lực lên Rigidbody này theo hướng lên của GameObjects này
        rb.AddForce(jumpForce * Vector2.up);
       
    
    }

    public void Attack()
    {
        
        isAttacked = true;

        ChangeAnim("attack");

        //Gọi hàm reset với độ trể là 0.5s
        Invoke(nameof(ResetToIdle), 0.5f);

        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
       
    }

    public void Throw()
    {
       
        isAttacked = true;

        ChangeAnim("throw");

        //Gọi hàm reset với độ trể là 0.5s
        Invoke(nameof(ResetToIdle), 0.5f);

        //ham khoi tao object
        Instantiate(kunaiPrerfab, throwPoint.position, throwPoint.rotation);
    }

    private void ResetToIdle()
    {
        isAttacked= false;
        
        ChangeAnim("idle");
    }

  
    //Hàm được gọi khi chết dể đưa vật về điểm checkpoint với các thông số được cài lại như ban đầu
    public override void OnInit()
    {
        ChangeAnim("idle");
        base.OnInit();
        isGrounded = false;
        isJumping = false;
        isAttacked = false;
        isDead = false;

        transform.position = savePoint;
        
        DeActiveAttack();

        UIManager.instance.SetCoin(Coins);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        OnInit();
    }

    public void SavePoint()
    {
        savePoint= transform.position;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }


    public void SetMove(float horizontal)
    {
        this.horizontal= horizontal;
    }


    //Collider thông thường dùng để phát hiện va chạm và ngăn chặn mọi tác động
    //xuyên qua Object của một Collider khác thì Trigger chỉ đóng vai trò phát
    //hiện ra sự va chạm nhưng vẫn cho Collider tạo nên sự va chạm đó đi xuyên
    //qua Object
    private void OnTriggerEnter2D(Collider2D collision)   // hàm va chạm 2d
    {
        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            Coins++;
            PlayerPrefs.SetInt("coin", Coins);
            UIManager.instance.SetCoin(Coins);
        }

        if (collision.tag == "deathzone")
        {
            ChangeAnim("die");
            isDead = true;
            Invoke(nameof(OnInit), 2f);

        }
    }
}
