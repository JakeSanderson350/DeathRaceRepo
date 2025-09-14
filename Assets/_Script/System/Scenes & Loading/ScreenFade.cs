using PrimeTween;
using UnityEngine.UI;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] Image screenFade;
    [SerializeField] TweenSettings<float> settings;

    private void OnEnable()
    {
        EventManager.onSceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        EventManager.onSceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(LevelData data)
    {
        screenFade.gameObject.SetActive(true);
        Tween.Alpha(screenFade, settings).OnComplete(() => screenFade.gameObject.SetActive(false));
    }
}
