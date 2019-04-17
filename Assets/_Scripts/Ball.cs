using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody _RB;
    public float force;

    private Transform parent;
    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        parent = this.transform.parent;
    }
    private void OnEnable()
    {
        transform.parent = null;
        _RB.AddForce(transform.forward * force, ForceMode.Impulse);
        _RB.useGravity = true;
        StartCoroutine(disable(5f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cup")
        {
            Debug.Log("fdasfadsfasf");
        }
    }

    private IEnumerator disable(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.transform.parent = parent;
        _RB.useGravity = false;

    }

}
