using UnityEngine;

public class FpsUnlocker
{
    public FpsUnlocker()
    {
        // Отключаем вертикальную синхронизацию
        QualitySettings.vSyncCount = 0;

        // Снимаем ограничение по FPS
        Application.targetFrameRate = -1;

        Debug.Log("[FpsUnlocker] Ограничение по FPS снято.");
    }
}