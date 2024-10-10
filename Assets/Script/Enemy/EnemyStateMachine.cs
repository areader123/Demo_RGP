using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
  public EnemyState _currentState { get; private set; } 

  public EnemyState lastState{ get; private set; }

  public void Intialize(EnemyState _starstate) 
    {
        _currentState = _starstate;
        _currentState.Enter();
    }
    public void ChangeState(EnemyState _newstate) 
    {
        _currentState.Exit();
        _currentState = _newstate;
        _currentState.Enter();
    }
}
