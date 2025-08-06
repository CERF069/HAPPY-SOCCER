using UnityEngine;
public class CurrencyService : ICurrencyService
{
    private const string CurrencyKey = "Coins";

    public bool TrySpend(int amount)
    {
        int coins = PlayerPrefs.GetInt(CurrencyKey, 0);
        if (coins < amount) return false;

        coins -= amount;
        PlayerPrefs.SetInt(CurrencyKey, coins);
        PlayerPrefs.Save();
        Debug.Log($"[CurrencyService] Spent {amount} coins. Remaining: {coins}");
        return true;
    }
}