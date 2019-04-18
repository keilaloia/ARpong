using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour , ITurnControl, ITurnBase
{
    [SerializeField]
    private Image[] _ballImage = new Image[2];
    private Stack<Image> ballImage = new Stack<Image>();

    [SerializeField]
    private Image clickImg;
    [HideInInspector]
    public float force = 0;
    private float val = 0;
    private Vector3 min = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 max = new Vector3(1f, 1f, 1f);
    private Color32 OGcolor;
    private int BallinCup = 0;
    private int thrownBalls = 0;


    private static UIController _instance;
    public static UIController instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("multiple statemanager error");
        }
        OGcolor = clickImg.color;
        for (int i = 0; i < _ballImage.Length; i++)
        {
            ballImage.Push(_ballImage[i]);
        }
        onAwake();
    }
    public void Tick()//update
    {
        val += .0001f;
        force = Mathf.Clamp(force + val, .3f, 1f);
        ScaleUpButton(clickImg.transform, val);
    }

    public void onAwake()
    {
        Ball.ThrowBall += isThrown;
        Ball.inCup += inCup;
        Ball.BallDisable += endThrow;
    }
    public void onSleep()
    {
        Ball.ThrowBall -= isThrown;
        Ball.inCup -= inCup;
        Ball.BallDisable -= endThrow;
    }
    public void isThrown()
    {
        thrownBalls++;
        clickImg.enabled = false;
        SetAlpha(ballImage.Pop(), .1f);
    }

    public void inCup()
    {
        BallinCup += 1;
    }

    public  void endThrow()
    {
        ScaleToMin(clickImg.transform);
        clickImg.color = OGcolor;
        clickImg.enabled = true;
        TurnEnd(_ballImage.Length);


    }
    public void TurnEnd(int Count)
    {
        if (thrownBalls == Count)
        {
            if(BallinCup != 0)
                BallsBack(BallinCup);
            else
                BallsBack(2);

            thrownBalls = 0;
            BallinCup = 0;
            val = 0;
            force = 0;
        }
    }

    public void BallsBack(int Count)
    {
        ballImage.Clear();
        for (int i = 0; i < Count; i++)
        {
            SetAlpha(_ballImage[i], 1f);
            ballImage.Push(_ballImage[i]);
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

    private void SetAlpha(Image image, float val)
    {
        Color temp = image.GetComponent<Image>().color;
        temp.a = val;
        image.color = temp;

    }

}
