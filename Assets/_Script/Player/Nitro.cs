using ImprovedTimers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    [SerializeField] private CarProperties carProfile;

    public int NitroCount { get => nitroCount; }

    private int nitroCount;
    private int nitroMax;
    private float nitroCooldown;
    private bool isGrounded;

    CountdownTimer rechargeTimer;

    // Start is called before the first frame update
    void Start()
    {
        nitroCount = carProfile.startNitro;
        nitroMax = carProfile.maxNitro;
        nitroCooldown = carProfile.nitroRechargeRate;

        rechargeTimer = new CountdownTimer(nitroCooldown);
    }

    public void UpdateNitro(bool _isGrounded)
    {
        isGrounded = _isGrounded;

        if (!rechargeTimer.IsRunning && nitroCount < nitroMax)
        {
            StartRechargeTimer();
        }
    }

    // Returns false if nitro not available, true if nitro gets used
    public bool CanUseNitro()
    {
        if (isGrounded && nitroCount > 0)
        {
            return true;
        }

        return false;
    }

    // Assumes CanUseNitro is true
    public void UseNitro()
    {
        nitroCount--;


    }

    private void RechargeNitro()
    {
        if (nitroCount < nitroMax)
        {
            nitroCount++;

            StartRechargeTimer();
        }

        else
        {
            rechargeTimer.Dispose();
        }
    }

    private void StartRechargeTimer()
    {
        rechargeTimer = new CountdownTimer(nitroCooldown);
        rechargeTimer.OnTimerStop += RechargeNitro;
        rechargeTimer.Start();
    }
}
