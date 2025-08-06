using UnityEngine;
using Zenject;

/// <summary>
/// Automatically updates the attached SpriteRenderer based on the currently selected skin
/// of a specific <see cref="SkinType"/>. Subscribes to <see cref="SkinSelectedSignal"/> via <see cref="SignalBus"/>.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SkinRenderer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("SpriteRenderer component that will be updated with the selected skin sprite.")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Skin Settings")]
    [Tooltip("The type of skin this renderer listens for (e.g., Character, Background).")]
    [SerializeField] private SkinType _skinType;

    [Inject] private ISkinService _skinService;
    [Inject] private SignalBus _signalBus;

    /// <summary>
    /// Subscribes to the skin selection signal on object awake.
    /// </summary>
    private void Awake()
    {
        _signalBus.Subscribe<SkinSelectedSignal>(OnSkinSelected);
    }

    /// <summary>
    /// Called on the first frame. Applies the current selected skin immediately.
    /// </summary>
    private void Start()
    {
        UpdateSprite();
    }

    /// <summary>
    /// Ensures signal unsubscription to prevent memory leaks or dangling references.
    /// </summary>
    private void OnDisable()
    {
        _signalBus.Unsubscribe<SkinSelectedSignal>(OnSkinSelected);
    }

    /// <summary>
    /// Called when a new skin is selected via <see cref="SignalBus"/>.
    /// Updates the sprite if the signal matches the current skin type.
    /// </summary>
    /// <param name="signal">The signal carrying the updated skin type.</param>
    private void OnSkinSelected(SkinSelectedSignal signal)
    {
        if (signal.SkinType == _skinType)
            UpdateSprite();
    }

    /// <summary>
    /// Retrieves the currently selected skin and updates the SpriteRenderer if applicable.
    /// </summary>
    private void UpdateSprite()
    {
        var selectedSkin = _skinService.GetSelectedSkin(_skinType);

        if (selectedSkin != null && selectedSkin.Sprite != null && _spriteRenderer != null)
        {
            _spriteRenderer.sprite = selectedSkin.Sprite;
#if UNITY_EDITOR
            Debug.Log($"[SkinRenderer] Updated sprite to '{selectedSkin.Sprite.name}' for type '{_skinType}'.");
#endif
        }
        else
        {
            Debug.LogWarning($"[SkinRenderer] Failed to update sprite. Missing sprite or renderer for skin type '{_skinType}'.");
        }
    }
}
