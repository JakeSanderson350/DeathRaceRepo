
using EditorAttributes;
using ImprovedTimers;
using PrimeTween;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popularity : Resource
{
    [Header("Countdown Tweaks")]
    [SerializeField] int countdown = 5;
    [SerializeField, Range(0.1f, 4)] float countdownCheat = 1f;

    [Header("UI Links")]
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Image popularityBar;

    float countdownFuse;
    TickTimer timer;
    Vector3 countTextPos;

    public event Action<int> onCountdownTick;
    public event Action onCountdownExplode;
    public event Action onCountdownRecover;


    [Header("Debug")]
    [SerializeField] float debugValue;
    [Button] void SetPopularity() => Value = debugValue;
    float TickLength => countdown / (countdown * countdownCheat);

    private void Start()
    {
        onEmpty += StartCountdown;
        onChanged += OnChanged;
        countTextPos = countText.transform.localPosition;
        countdownFuse = countdown;
        timer = new TickTimer(countdownFuse / ((float)countdown * countdownCheat));
        timer.OnTick += CountDownTick;
        AudienceManager.inst.racers.Add(this);
    }

    private void OnDestroy()
    {
        timer?.Dispose();
        AudienceManager.inst.racers.Remove(this);
    }

    void OnChanged(float value, float change, float percent)
    {
        if(percent != popularityBar.transform.localScale.x) 
            Tween.ScaleX(popularityBar.transform, percent, 0.5f, Ease.OutBounce);
    }

    void StartCountdown()
    {
        if(timer.IsRunning) return;
        
        //Debug.Log("Start death Clock");

        countdownFuse = countdown;
        timer.Start();
    }

    void StopCountdown()
    {
        if (!timer.IsRunning) return;

        //Debug.Log("Stop death Clock");
        timer.Stop();
    }

    void CountDownTick()
    {
        int count = Mathf.RoundToInt(countdownFuse);
        //Debug.Log("Death Clock: " + count);

        onCountdownTick?.Invoke(count);

        countText.text = count.ToString();
        Tween.LocalPositionY(countText.transform, countTextPos.y, countTextPos.y + 200f, TickLength * 0.9f, Ease.OutExpo);
        Tween.Color(countText, Color.white, Color.clear, TickLength * 0.9f, Ease.InExpo);


        if (Value > 0)
        {
            StopCountdown();
            onCountdownRecover?.Invoke();
        }

        if(count <= 0 && Value <= 0)
            Explode();

        countdownFuse--;
    }

    void Explode()
    {
        StopCountdown();
        Debug.Log("BOOM!");
        onCountdownExplode?.Invoke();
    }
}
