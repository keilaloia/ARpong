using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IPopulate
{
    [SerializeField]
    private Image[] _ballImage = new Image[2];
    private Stack<Image> ballImage = new Stack<Image>();

    [SerializeField]
    private Image clickImg;

    private Image LastIMG;

    private int curTop = 1;

    [HideInInspector]
    public float force = 0;
    private float val = 0;
    private Vector3 min = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 max = new Vector3(1f, 1f, 1f);
    private Color32 OGcolor;

    private static UIController _instance;
    public static UIController instance { get { return _instance; } }



    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Warning: multiple UIManagers");
        }
        OGcolor = clickImg.color;
        populate(2);

        Ball.ThrowBall += disableIMG;
        Ball.inCup += enableIMG;
        Ball.BallDisable += resetClick;

    }

    public void tick()//update
    {
        if(clickImg.isActiveAndEnabled)
        {
            val += .0001f;
            force = Mathf.Clamp(force + val, .3f, 1f);
            ScaleUpButton(clickImg.transform, val);

        }

    }


    public void populate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ballImage.Push(_ballImage[i]);
            LastIMG = _ballImage[i];
        }
    }
    private void ScaleUpButton(Transform model, float val)
    {
        Vector3 newScale;
        newScale.x = Mathf.Clamp(model.localScale.x + val, min.x, max.x);
        newScale.y = Mathf.Clamp(model.localScale.y + val, min.y, max.y);
        newScale.z = Mathf.Clamp(model.localScale.z + val, min.z, max.z);
        model.localScale = newScale;
    }
    private void ScaleToMin(Transform model)
    {
        Vector3 newScale;
        newScale.x = min.x;
        newScale.y = min.y;
        newScale.z = min.z;
        model.localScale = newScale;
    }

    private void setAlpha(Image image, float val)
    {
        Color temp = image.GetComponent<Image>().color;
        temp.a = val;
        image.color = temp;

    }

    private void disableIMG()
    {
        clickImg.enabled = false;
        LastIMG = ballImage.Peek();
        setAlpha(ballImage.Pop(), .1f);
    }

    private void enableIMG()
    {
        ballImage.Push(LastIMG);
        setAlpha(ballImage.Peek(), 1f);
    }
    private void resetClick()
    {
        ScaleToMin(clickImg.transform);
        clickImg.color = OGcolor;
        clickImg.enabled = true;
    }






}
