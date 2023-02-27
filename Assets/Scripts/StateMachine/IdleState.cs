using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    [SerializeField] public float timeIdle;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timeIdle = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Enemy enemy)
    { 
        
         timeIdle += Time.deltaTime;

        if (timeIdle > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }             

    }

    public void OnExit(Enemy enemy)
    {
       
    }
}
