using System.Collections;
using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody _RB;
    public float force = 0f;

    private Transform parent;
    private Vector3 ogPosition;
    private Quaternion ogRotation;
    public static Action inCup = delegate { };
    public static Action ThrowBall = delegate { };
    public static Action BallDisable = delegate { };

    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        parent = this.transform.parent;
        ogPosition = this.transform.localPosition;
        ogRotation = this.transform.localRotation;
        if (ThrowBall != null)
        {
            ThrowBall();
        }
        _RB.AddForce(parent.transform.forward * force, ForceMode.Impulse);
        transform.parent = null;
        _RB.useGravity = true;
        StartCoroutine(disable(5f));
    }

    private void OnTriggerEnter(Collider other)
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
        gameObject.SetActive(false);
        yield return 0;


    }

    private void OnDisable()
    {
        ResetVariables();
        if (BallDisable != null)
        {
            BallDisable();
        }

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
