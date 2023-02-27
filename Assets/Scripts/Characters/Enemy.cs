using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Characters
{

    [SerializeField] private float attackRange;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody2D Erb;

    [SerializeField] private GameObject attackArea;

    private bool isRight = true;

    private Characters target;

    public Characters Target => target; //Tránh chọc code lung tung

    private IState currentState;

    
    private void Update()
    {
        
        if (currentState != null )

        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());

        DeActiveAttack();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();

        Destroy(healthBar.gameObject);

        Destroy(gameObject);
    }


    //Check state cũ bằng null ko nếu bằng thì thoát state cũ và thay state mới vào
    public void ChangeState(IState newState)
    {
       
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }


    protected override void OnDeath()
    {
        base.OnDeath();
        ChangeState(null);
    }

    public void Moving()
    {
        ChangeAnim("run");
        Erb.velocity = transform.right * moveSpeed;
        
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        Erb.velocity= Vector2.zero;
        
    }

    public void Attack()
    {
       
        ChangeAnim("attack");


        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);

    }


    //Xem mục tiêu có trong tầm đánh không
    public bool isTargetInRange()
    {
        if (Target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }    
        return false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemywall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool is_Right)
    {
        this.isRight= is_Right;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    public void SetTarget(Characters character)
    {
        this.target = character;
        if(isTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if(character != null)
        {
            ChangeState(new PatrolState());
        }
        else if(character == null)
        {
            ChangeState(new IdleState());
        }
    }


    //=== Attack Active =========
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
