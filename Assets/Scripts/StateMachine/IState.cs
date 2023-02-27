using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnEnter(Enemy enemy); //Bắt đầu vào state

    void OnExecute(Enemy enemy); //Trong state

    void OnExit(Enemy enemy); //Thoát state
}
