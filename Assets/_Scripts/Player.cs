using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPopulate
{

    public GameObject[] balls = new GameObject[2];
    private Stack<GameObject> ball = new Stack<GameObject>();
    private GameObject LastBall;
    private bool canThrow = true;
    
    private void Awake()
    {
        Ball.ThrowBall += ThrowCheck;
        Ball.inCup += enableBall;
        Ball.BallDisable += ThrowCheck;
    }

    private void OnEnable()
    {
        populate(2);
    }
    private void ThrowCheck()
    {
        if (canThrow)
        {
            canThrow = false;
        }
        else
            canThrow = true;
    }
    public void populate(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ball.Push(balls[i]);
            LastBall = balls[i];
        }
    }
    private void enableBall()
    {
        ball.Push(LastBall);
    }

    private void Update()
    {
        //charge ball throw
        if(canThrow)
        {
            if (Input.GetKey(KeyCode.Mouse1) || Input.GetMouseButton(0))
            {
                //mocks the data happening in the tick function;
                UIController.instance.tick();

            }
            if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetMouseButtonUp(0))//throwball
            {

                if (ball.Count != 0)
                    initball();
            }
        }

    }

    private void initball()
    {
        LastBall = ball.Peek();
        ball.Peek().GetComponent<Ball>().force = UIController.instance.force * 70f;
        ball.Peek().SetActive(true);
        ball.Pop();
    }

}
