using UnityEngine;
using Zenject;

/// <summary>
/// Handles skin selection and persistence.
/// Implements ISkinService and uses PlayerPrefs for saving the selected skin.
/// </summary>
public class SkinSelector : ISkinService
{
    private const string SkinKeyPrefix = "SelectedSkin_";

    private readonly SkinData _skinData;
    private readonly SignalBus _signalBus;
    private readonly ICurrencyService _currencyService;

    [Inject]
    public SkinSelector(SkinData skinData, SignalBus signalBus, ICurrencyService currencyService)
    {
        _skinData = skinData;
        _signalBus = signalBus;
        _currencyService = currencyService;
    }

    public void SelectSkin(SkinType type, string skinId)
    {
        if (_skinData == null)
        {
            Debug.LogError("[SkinSelector] _skinData is null in SelectSkin");
            return;
        }

        var skin = _skinData.GetSkinById(type, skinId);
        if (skin == null)
        {
            Debug.LogError($"[SkinSelector] Skin not found: type={type}, id={skinId}");
            return;
        }

        if (skin.State == SkinState.NotPurchased)
        {
            Debug.Log($"[SkinSelector] Attempting to buy skin {skinId} for {skin.Price} coins");

            if (_currencyService.TrySpend(skin.Price))
            {
                Debug.Log($"[SkinSelector] Successfully purchased skin {skinId}");
                skin.State = SkinState.Purchased;
            }
            else
            {
                Debug.LogWarning($"[SkinSelector] Not enough currency to purchase skin {skinId}");
                return;
            }
        }

        // Deselect previously selected skins of the same type
        foreach (var item in _skinData.GetSkinsByType(type))
        {
            if (item.State == SkinState.Selected)
                item.State = SkinState.Purchased;
        }

        skin.State = SkinState.Selected;
        PlayerPrefs.SetString(SkinKeyPrefix + type.ToString(), skinId);
        PlayerPrefs.Save();

        _signalBus?.Fire(new SkinSelectedSignal(type));
    }

    public SkinItem GetSelectedSkin(SkinType type)
    {
        Debug.Log($"[SkinSelector] GetSelectedSkin called for type: {type}");

        if (_skinData == null)
        {
            Debug.LogError("[SkinSelector] _skinData is null!");
            return null;
        }

        string key = SkinKeyPrefix + type.ToString();
        string selectedId = PlayerPrefs.GetString(key, "");

        Debug.Log($"[SkinSelector] Loaded skin id from PlayerPrefs: '{selectedId}' for type: {type}");

        if (string.IsNullOrEmpty(selectedId))
        {
            Debug.LogWarning($"[SkinSelector] No saved skin id for {type}, falling back to default skin.");
        }

        var skin = _skinData.GetSkinById(type, selectedId);

        if (skin == null)
        {
            Debug.LogWarning($"[SkinSelector] No skin found with id '{selectedId}', trying default skin...");

            var defaultSkin = _skinData.GetDefaultSkin(type);

            if (defaultSkin == null)
            {
                Debug.LogError($"[SkinSelector] Default skin is null for type {type}!");
                return null;
            }

            defaultSkin.State = SkinState.Selected;
            return defaultSkin;
        }

        return skin;
    }
}
