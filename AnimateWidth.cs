using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimateWidth : MonoBehaviour
{
    private const int ShowingDirection = 1;
    private const int HidingDirection = -1;

    public float startValue;
    public float endValue;
    public float duration;

    [HideInInspector]
    public float animatedFactor;

    private RectTransform mTransform;
    private bool isPlaying;
    private float startTime;
    private int direction;
    private float startFactor;
    private float widthDifference;

    private void Start()
    {
        this.mTransform = this.GetComponent<RectTransform>();
        this.isPlaying = false;
        this.animatedFactor = 0f;
        this.direction = ShowingDirection;
    }

    public void PlayForward()
    {
        this.startTime = Time.time;
        this.direction = ShowingDirection;
        this.startFactor = 0f;
        this.widthDifference = Mathf.Abs(this.startValue - this.endValue);
        this.isPlaying = true;
    }

    public void PlayReverse()
    {
        this.startTime = Time.time;
        this.direction = HidingDirection;
        this.startFactor = 1f;
        this.widthDifference = Mathf.Abs(this.startValue - this.endValue);
        this.isPlaying = true;
    }

    public void Toggle()
    {
        if (this.animatedFactor <= 0)
        {
            PlayForward();
        }
        else if (this.animatedFactor >= 1f)
        {
            PlayReverse();
        }
    }

    private void Update()
    {
        if (this.isPlaying)
        {
            float factor = (Time.time - this.startTime) / this.duration;
            this.animatedFactor = this.startFactor + (factor * this.direction);
            if (this.animatedFactor > 1f)
            {
                UpdateObject(1);
                this.isPlaying = false;
                return;
            }
            else if (this.animatedFactor < 0f)
            {
                UpdateObject(0);
                this.isPlaying = false;
                return;
            }

            UpdateObject(this.animatedFactor);
        }
    }

    private void UpdateObject(float fFraction)
    {
        float newWidth = this.startValue;
        newWidth += this.widthDifference * fFraction + Mathf.Sin(fFraction * Mathf.PI);
        this.mTransform.sizeDelta = new Vector2(newWidth, this.mTransform.sizeDelta.y);
    }
}
