using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private void Start()
    {
        UpdateCoinsDisplay();
    }

    /// <summary>
    /// Обновляет текст с количеством монет из PlayerPrefs.
    /// </summary>
    public void UpdateCoinsDisplay()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }
}