using UnityEngine;
public class ExitButton : MonoBehaviour
{
    public void OnExitButtonPressed()
    {
        Debug.Log("Выход из приложения");
        Application.Quit();
    }
}