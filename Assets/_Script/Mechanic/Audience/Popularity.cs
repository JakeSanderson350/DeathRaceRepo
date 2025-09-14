
using EditorAttributes;
using ImprovedTimers;
using System;
using UnityEngine;

public class Popularity : Resource
{
    [SerializeField] int countdown = 5;
    [SerializeField, Range(0.1f, 2)] float countdownCheat = 1f;

    float countdownFuse;
    CountdownTickTimer timer;

    public event Action<int> onCountdownTick;
    public event Action onCountdownExplode;
    public event Action onCountdownRecover;


    [Button] void SetPopularity() => Value = debugValue;
    [SerializeField] float debugValue;
        

    private void Start()
    {
        onEmpty += StartCountdown;
    }
    private void OnEnable()
    {
        AudienceManager.inst.racers.Add(this);
    }
    private void OnDisable()
    {
        timer?.Dispose();
        AudienceManager.inst.racers.Remove(this);
    }

    void StartCountdown()
    {
        countdownFuse = countdown * countdownCheat;
        timer = new CountdownTickTimer(countdownFuse, countdownFuse / countdown);
        timer.OnTick += CountDownTick;
        timer.OnTimerStop += Explode;
        timer.Start();
    }

    void StopCountdown()
    {
        timer.Pause();
        timer.Reset();
        countdownFuse = countdown;
    }

    void CountDownTick()
    {
        onCountdownTick?.Invoke(Mathf.RoundToInt(timer.CurrentTime / countdownFuse * countdown));
        if (Value > 0)
        {
            StopCountdown();
            onCountdownRecover?.Invoke();
        }
    }

    void Explode()
    {
        onCountdownExplode?.Invoke();
    }
}
