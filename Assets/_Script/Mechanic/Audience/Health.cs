using EditorAttributes;
using PrimeTween;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : Resource
{
    [Header("VFX")]
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem explode;

    [Header("UI Links")]
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Image healthBar;

    [Header("Debug")]
    [SerializeField] float debugValue;
    [Button] void SetHealth() => Value = debugValue;


    //retainer variables
    private bool isDestroyed;

    private void Start()
    {
        onChanged += OnChanged;
        onEmpty += Explode;
    }

    void OnChanged(float value, float change, float percent)
    {
        if(isDestroyed) return;

        if(percent != healthBar.transform.localScale.x) 
            Tween.ScaleX(healthBar.transform, percent, 0.5f, Ease.OutBounce);

        if(fire != null)
        if(percent < 0.5f) fire.Play(); else fire.Stop();
    }

    void Explode()
    {
        if(isDestroyed) return;

        isDestroyed = true;
        explode.Play();
    }
}
