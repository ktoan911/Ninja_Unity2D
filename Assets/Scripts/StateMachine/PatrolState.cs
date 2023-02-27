using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{

    [SerializeField] private float timePatrol;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        randomTime = Random.Range(3f, 6f);
        timePatrol = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timePatrol += Time.deltaTime;
        if (enemy.Target !=null)
        {
            //Doi huong toi huong cua player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            if (enemy.isTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }

        }
        else
        {
            if (timePatrol > randomTime)
            {
                enemy.ChangeState(new IdleState());
            }
            else enemy.Moving();
        }

        



    }

    public void OnExit(Enemy enemy)
    {
       
    }
}
