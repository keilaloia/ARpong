using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class gameController : MonoBehaviour {

    private static gameController _instance;
    public static gameController instance { get { return _instance; } }

    public Action clicked = delegate { };
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Update () {

        Vector3 test = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);


        Debug.DrawLine(transform.position, transform.forward * 1f, Color.red);

        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetMouseButtonUp(0))
        {
            if(clicked != null)
            {
                clicked();
            }
        }
	}
}
