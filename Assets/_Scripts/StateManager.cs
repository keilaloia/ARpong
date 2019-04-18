using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public Player[] players = new Player[2];
    private enum State { p1,p2 }
    [SerializeField]
    private State currentState;

    private static StateManager _instance;
    public static StateManager instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("multiple statemanager error");
        }
        currentState = State.p1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.p1:
                players[1].gameObject.SetActive(false);
                players[0].gameObject.SetActive(true);
                players[0].Tick();
                break;
            case State.p2:
                players[0].gameObject.SetActive(false);
                players[1].gameObject.SetActive(true);
                players[1].Tick();

                break;
        }
    }

    public void Playerswap()
    {
        Debug.Log("swapping");
        if (currentState == State.p1)
            currentState = State.p2;
        else
            currentState = State.p1;
    }

}
