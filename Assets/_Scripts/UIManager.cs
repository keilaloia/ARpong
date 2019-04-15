using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image[] _ballImage = new Image[2];

    private int curTop = 1;

    private event Action disableImage;
    private void Start()
    {
        gameController.instance.clicked += disable;
    }

    public void turnSwap()
    {
        ballActive();
        curTop = 1;
    }

    public void disable()
    {
        setAlpha(_ballImage[curTop], .01f);
        curTop = 0;
    }

    public void activate()
    {
        curTop = 1;
    }

    private void ballActive()
    {
        foreach(Image ball in _ballImage)
        {
            setAlpha(ball, 255);
        }
    }

    private void setAlpha(Image image, float val)
    {
        Color temp = image.GetComponent<Image>().color;
        temp.a = val;
        image.color = temp;

    }



}
