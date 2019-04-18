using System.Collections;
using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody _RB;
    [HideInInspector]
    public float force;

    private Transform parent;
    private Vector3 ogPosition;
    private Quaternion ogRotation;
    public static Action inCup = delegate { };
    public static Action ThrowBall = delegate { };
    public static Action BallDisable = delegate { };

    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        parent = this.transform.parent;
        ogPosition = this.transform.localPosition;
        ogRotation = this.transform.localRotation;
    }
    private void OnEnable()
    {
        if (ThrowBall != null)
        {
            ThrowBall();
        }
        transform.parent = null;
        _RB.AddForce(transform.forward * force, ForceMode.Impulse);
        _RB.useGravity = true;
        StartCoroutine(disable(5f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cup")
        {
            if(inCup != null)
            {
                inCup();
            }
            Debug.Log("fdasfadsfasf");
        }
    }

    private IEnumerator disable(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (BallDisable != null)
        {
            BallDisable();
        }
        gameObject.SetActive(false);
        ResetVariables();

    }

    private void ResetVariables()
    {
        _RB.useGravity = false;
        this.transform.parent = parent;
        transform.localPosition = ogPosition;
        transform.localRotation = ogRotation;
        force = 0;
    }
}
