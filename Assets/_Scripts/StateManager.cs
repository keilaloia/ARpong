using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public Player[] players = new Player[2];
    private enum State { p1,p2 }
    [SerializeField]
    private State currentState;

    private void Start()
    {
        currentState = State.p1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.p1:
                players[1].gameObject.SetActive(false);
                players[0].gameObject.SetActive(true);
                break;
            case State.p2:
                players[0].gameObject.SetActive(false);
                players[1].gameObject.SetActive(true);
                break;
        }
    }
}
