/// <summary>
/// Provides an abstraction for selecting and retrieving skins based on their type.
/// Allows runtime logic to interact with skin selection independently of specific implementation details.
/// </summary>
public interface ISkinService
{
    /// <summary>
    /// Selects a skin of the given type by its unique identifier.
    /// If the skin is not owned or does not exist, the selection is ignored or handled gracefully.
    /// </summary>
    /// <param name="type">The type of skin to select (e.g., Character, Background).</param>
    /// <param name="skinId">The unique identifier of the skin to select.</param>
    void SelectSkin(SkinType type, string skinId);

    /// <summary>
    /// Retrieves the currently selected skin of the specified type.
    /// If no skin has been selected yet, a default skin may be returned.
    /// </summary>
    /// <param name="type">The type of skin to retrieve.</param>
    /// <returns>The currently selected <see cref="SkinItem"/> of the given type.</returns>
    SkinItem GetSelectedSkin(SkinType type);
}