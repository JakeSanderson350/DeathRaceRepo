using ImprovedTimers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nitro : MonoBehaviour
{
    [SerializeField] private CarProperties carProfile;

    public int NitroCount { get => nitroCount; }

    private int nitroCount;
    private int nitroMax;
    private float nitroCooldown;
    private bool isGrounded;

    [Header ("UI Nitro Settings")]
    public Image nitroBar;

    [Range(0f, 1f)]
    public float minFillAmount = 0.1f; //fill amount when nitroCount is at 0
    [Range(0f, 1f)]
    public float maxFillAmount = 0.65f; //fill amount when nitro is at max

    public Image needle;

    //needle rotation info
    private float maxAngle = -90;
    private float zeroAngle = 90;

    CountdownTimer rechargeTimer;

    // Start is called before the first frame update
    void Start()
    {
        nitroCount = carProfile.startNitro;
        nitroMax = carProfile.maxNitro;
        nitroCooldown = carProfile.nitroRechargeRate;

        rechargeTimer = new CountdownTimer(nitroCooldown);

        UpdateNitroBar(); //intialize
    }

    public void UpdateNitro(bool _isGrounded)
    {
        isGrounded = _isGrounded;

        if (!rechargeTimer.IsRunning && nitroCount < nitroMax)
        {
            StartRechargeTimer();
        }

        UpdateNitroBar();
    }

    // Returns false if nitro not available, true if nitro gets used
    private bool CanUseNitro()
    {
        if (isGrounded && nitroCount > 0)
        {
            return true;
        }

        return false;
    }

    // Returns true if nitro is used false if no nitro left
    public bool TryUseNitro()
    {
        if (CanUseNitro())
        {
            nitroCount--;
            UpdateNitroBar(); //updates when used
            return true;
        }

        return false;
    }

    private void RechargeNitro()
    {
        if (nitroCount < nitroMax)
        {
            nitroCount++;
            UpdateNitroBar(); //updates when recharged
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

    //start bar fill at 0.1 when at zero nitro
    //bar fill amount reaches 0.35 when nitroCount is at 1
    //bar fill amount is at 0.5 when nitroCount is at 2
    //bar fill amount is 0.65 when nitro count is 3 and 3 is the max 
    private void UpdateNitroBar()
    {
        if (nitroBar != null)
        {
            float normalizedNitro = nitroCount / nitroMax;
            float targetFillAmount = Mathf.Lerp(minFillAmount, maxFillAmount, normalizedNitro);

            nitroBar.fillAmount = Mathf.Lerp(nitroBar.fillAmount, targetFillAmount, Time.deltaTime * 5f);
        }
        else
        {
            Debug.LogWarning("Nitro: nitro bar isn't assigned in the inspector");
        }
    }
}
