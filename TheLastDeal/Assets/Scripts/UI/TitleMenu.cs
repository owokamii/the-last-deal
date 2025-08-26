using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleMenuCanvasGroup;
    [SerializeField] private Button playButton;

    private void OnEnable() => playButton.onClick.AddListener(OnPlayPressed);

    private void OnDisable() => playButton.onClick.RemoveListener(OnPlayPressed);

    private void OnPlayPressed()
    {
        SetAlpha(false);
    }

    private void SetAlpha(bool state)
    { 
        titleMenuCanvasGroup.alpha = state ? 1 : 0;
        titleMenuCanvasGroup.interactable = state;
        titleMenuCanvasGroup.blocksRaycasts = state;
    }
}
