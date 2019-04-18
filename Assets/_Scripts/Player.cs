using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, ITurnControl,ITurnBase
{

    public GameObject[] balls = new GameObject[2];
    private Stack<GameObject> ball = new Stack<GameObject>();

    private GameObject LastBall;
    private bool canThrow = true;
    private int BallinCup = 0;
    private int thrownBalls = 0;

    public static Action endTurn = delegate { };

    private void OnEnable()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            ball.Push(balls[i]);
        }

        onAwake();
    }
    private void OnDisable()
    {
        onSleep();
    }
    public void Tick()
    {
        //charge ball throw
        if (canThrow)
        {
            if (Input.GetKey(KeyCode.Mouse1) || Input.GetMouseButton(0))
            {
                UIController.instance.Tick();
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetMouseButtonUp(0))//throwball
            {
                if (ball.Count != 0)
                {
                    initball();
                }
            }
        }

    }
    
    public void onAwake()
    {
        Ball.ThrowBall += isThrown;
        Ball.inCup += inCup;
        Ball.BallDisable += endThrow;
    }
    public void onSleep()
    {
        Ball.ThrowBall -= isThrown;
        Ball.inCup -= inCup;
        Ball.BallDisable -= endThrow;
    }
    public  void isThrown()
    {
        thrownBalls++;
        canThrow = false;

    }

    public void inCup()
    {
        BallinCup++;

    }

    public  void endThrow()
    {
        canThrow = true;
        TurnEnd(balls.Length);


    }

    public void BallsBack(int Count)//need work
    {
        ball.Clear();

        for (int i = 0; i < Count; i++)
        {
            ball.Push(balls[i]);
            Debug.Log(i);

        }

    }
    public void TurnEnd(int Count)
    {
        if (thrownBalls == Count)
        {
            if(BallinCup != 0)
            {
                BallsBack(BallinCup);
            }
            else
            {
                StateManager.instance.Playerswap();
            }
            thrownBalls = 0;
            BallinCup = 0;
            if (endTurn != null)
            {
                endTurn();
            }
        }
    }


    private void initball()
    {
        ball.Peek().GetComponent<Ball>().force = UIController.instance.force * 70f;
        ball.Peek().SetActive(true);
        ball.Pop();
    }



}
