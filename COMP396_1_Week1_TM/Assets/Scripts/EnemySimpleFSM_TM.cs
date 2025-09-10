using System;
using UnityEngine;

public class EnemySimpleFSM_TM : MonoBehaviour
{
    public enum EnemyState
    {
        Evade,
        Patrol,
        Attack
    }
    public EnemyState currentState;
    public GameObject target;

    public bool isSafe = false;
    public bool isStrongerThanTarget = false;

    //Header("Read Only")
    public Color colorAttack = Color.red;
    public Color colorPatrol = Color.green;
    public Color colorEvade = Color.yellow;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentState = EnemyState.Patrol;
        ChangeState(EnemyState.Evade);
    }

    // Update is called once per frame
    void Update()
    {
        print($"deltaTime={Time.deltaTime}, FPS={1/Time.deltaTime}");
        UpdateState();
    }

    private void FixedUpdate()
    {
        //print($"fixedDeltaTime={Time.fixedDeltaTime}, FPS={1/Time.fixedDeltaTime}");
        //UpdateState();
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Evade:
                HandleEveade();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
            default:
                break;
        }
    }

    private void HandleAttack()
    {
        //print($"HandleAttack");
        if(!StrongerThanTarget())
        {
            ChangeState(EnemyState.Evade);
        }
    }

    private void HandlePatrol()
    {
        //print($"HandlePatrol");
        if (Threatened())
        {
            if (StrongerThanTarget())
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
                ChangeState(EnemyState.Evade);
            }
        }
    }

    private bool StrongerThanTarget()
    {
        //throw new NotImplementedException();
        return isStrongerThanTarget;
    }

    private bool Threatened()
    {
        //throw new NotImplementedException();
        return !Safe();
    }

    private void HandleEveade()
    {
        print($"HandleEvade");
        if (Safe())
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    

    private void ChangeState(EnemyState state)
    {
        currentState = state;
        switch (currentState)
        {
            case EnemyState.Evade:
                this.GetComponent<Renderer>().sharedMaterial.color = colorEvade;
                break;
            case EnemyState.Patrol:
                this.GetComponent<Renderer>().sharedMaterial.color = colorPatrol;
                break;
            case EnemyState.Attack:
                this.GetComponent<Renderer>().sharedMaterial.color = colorAttack;
                break;
            default:
                break;
        }
        print($"In ChangeState:currentState={currentState}");
    }
    private bool Safe()
    {
        float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        isSafe = (distanceToTarget > 10);
        return isSafe;
        //return Vector3.Distance(this.transform.position, target.transform.position)>10;
    }

}

