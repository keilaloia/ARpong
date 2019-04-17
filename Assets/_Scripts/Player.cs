using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image clickImg;

    public GameObject[] balls = new GameObject[2];
    private Stack<GameObject> ball = new Stack<GameObject>();

    private float val = 0;
    private Vector3 min = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 max = new Vector3(1f, 1f, 1f);


    private void OnEnable()
    {
        repopulate(2);
    }

    private void repopulate(int count)
    {
        for(int i = 0; i < count; i++)
        {
            ball.Push(balls[i]);
        }
    }

    public void ScaleUpButton(Transform model, float val)
    {
        Vector3 newScale;
        newScale.x = Mathf.Clamp(model.localScale.x + val , min.x, max.x);
        newScale.y = Mathf.Clamp(model.localScale.y + val, min.y, max.y);
        newScale.z = Mathf.Clamp(model.localScale.z + val, min.z, max.z);
        model.localScale = newScale;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetMouseButton(0))
        {

            val += .0001f;
            ScaleUpButton(clickImg.transform, val);
            clickImg.color = new Color(clickImg.color.r + val * 10f, clickImg.color.b - val, clickImg.color.g - val, 1);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetMouseButtonUp(0))
        {
            if(ball.Count != 0)
                initball();
        }
    }

    private void initball()
    {
        //scale is clampped between 0-1 change to actual value in future
        ball.Peek().GetComponent<Ball>().force = clickImg.transform.localScale.x * 70f;
        ball.Peek().SetActive(true);
        ball.Pop();
    }

}
