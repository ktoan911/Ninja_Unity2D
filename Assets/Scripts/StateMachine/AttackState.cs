using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    [SerializeField] private float timeAttack;
    public void OnEnter(Enemy enemy)
    {
       if(enemy.Target != null)
        {
            //Doi huong toi huong cua player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            enemy.StopMoving();
            enemy.Attack();

            
        }

       timeAttack= 0;

    }

    public void OnExecute(Enemy enemy)
    {
        
        timeAttack += Time.deltaTime;
        if(timeAttack >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
