using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUI_Effects : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    [SerializeField] bool selectOnAwake;
    [SerializeField, Range(0, 1)] float Blend3D;
    [SerializeField] AudioClip mouseDownSFX;
    [SerializeField] AudioClip mouseEnterSFX;

    private void Start()
    {
        if(selectOnAwake) GetComponent<Button>().Select();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioSource sfx = AudioPlayer.PlaySFX(mouseDownSFX, transform.position, 0.1f);
        sfx.spatialBlend = Blend3D;
        sfx.reverbZoneMix = Blend3D;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioSource sfx = AudioPlayer.PlaySFX(mouseEnterSFX, transform.position, 0.1f);
        sfx.spatialBlend = Blend3D;
        sfx.reverbZoneMix = Blend3D;

    }


}
