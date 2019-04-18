
using UnityEngine;

public class Cup : MonoBehaviour {

    private Material Mat;
    private Material ogMat;

    [SerializeField]
    private Material newMat;
    private bool ballhit = false;
    private void Awake()
    {
        ogMat = GetComponent<MeshRenderer>().material;
        Player.endTurn += endTurn;
    }

    private void OnTriggerEnter(Collider other)
    {

        GetComponent<MeshRenderer>().material = newMat;
        ballhit = true;
    }

    private void endTurn()
    {
        if(ballhit)
        {
            this.gameObject.SetActive(false);
            GetComponent<MeshRenderer>().material = ogMat;
            ballhit = false;

        }

    }

}
