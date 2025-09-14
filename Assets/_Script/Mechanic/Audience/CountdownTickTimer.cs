using System;
using UnityEngine;

namespace ImprovedTimers
{
    public class CountdownTickTimer : Timer
    {
        public CountdownTickTimer(float value, float tickDelay) : base(value) { TickDelay = tickDelay; }

        public float TickDelay { get; private set; }

        public Action OnTick = delegate { };

        float timeThreshold;

        public override void Tick()
        {
            if (IsRunning && CurrentTime <= timeThreshold)
            {
                OnTick.Invoke();
                CalculateTimeThreshold();
            }

            if (IsRunning && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0)
            {
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime <= 0;

        void CalculateTimeThreshold()
        {
            timeThreshold = CurrentTime - TickDelay;
        }

        override public void Reset()
        {
            Reset();
            CalculateTimeThreshold();
        }
    }
}
