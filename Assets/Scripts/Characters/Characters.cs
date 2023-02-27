using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] private float hp;

    [SerializeField] private Animator anim;

    [SerializeField] protected HealthBar healthBar;
    
    
    [SerializeField] protected CombatText combatTextPrefab;


    private string currentAnimName;

    public bool isDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    //Có chức năng như hàm khởi tạo nhưng có thể gọi bất cứ lúc nào
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(hp,transform);
    }


    //Gần giống hàm hủy
    public virtual void OnDespawn()
    {

    }

    public void ChangeAnim(string animName)
    {

        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn),1.5f);
    }

    public virtual void OnHit(float damage)
    {
        if(!isDead)
        {
            hp -= damage;
            if (isDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.SetHp(hp);

            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }

        
    }

   
}
