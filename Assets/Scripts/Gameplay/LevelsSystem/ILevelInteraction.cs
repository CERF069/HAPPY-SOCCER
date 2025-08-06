
public interface ILevelInteraction
{
    void UpdateRender();
    void CompleteLevel(int levelIndex);
    void SetButtons(LevelButton[] buttons);
    void OnLevelSelected(int levelIndex);
}