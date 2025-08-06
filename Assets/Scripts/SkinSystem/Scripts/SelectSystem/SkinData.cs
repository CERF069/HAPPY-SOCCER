using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// A ScriptableObject container that holds a list of all available skins in the game.
    /// Provides helper methods to retrieve skins by type or ID, and defines default skins for each type.
    /// </summary>
    [CreateAssetMenu(fileName = "SkinData", menuName = "Skins/Skin Data")]
    public class SkinData : ScriptableObject
    {
        [Header("All Skins")]

        [Tooltip("List of all available skins.")]
        [SerializeField] private List<SkinItem> _skins = new List<SkinItem>();

        [Header("Default Skins")]

        [Tooltip("The default skin ID for character type.")]
        [SerializeField] private string _defaultCharacterSkinId;

        [Tooltip("The default skin ID for background type.")]
        [SerializeField] private string _defaultBackgroundSkinId;

        /// <summary>
        /// Returns the default skin for the given skin type.
        /// Falls back to the defined default ID.
        /// </summary>
        /// <param name="type">The skin type (Character or Background).</param>
        /// <returns>The default <see cref="SkinItem"/> for the specified type.</returns>
        public SkinItem GetDefaultSkin(SkinType type)
        {
            string defaultId = type == SkinType.Character ? _defaultCharacterSkinId : _defaultBackgroundSkinId;
            return _skins.Find(s => s.Type == type && s.SkinId == defaultId);
        }

        /// <summary>
        /// Returns a specific skin by type and ID.
        /// </summary>
        /// <param name="type">The type of the skin.</param>
        /// <param name="id">The unique skin ID.</param>
        /// <returns>The matching <see cref="SkinItem"/> or null if not found.</returns>
        public SkinItem GetSkinById(SkinType type, string id)
        {
            return _skins.Find(s => s.Type == type && s.SkinId == id);
        }

        /// <summary>
        /// Returns all skins that match the given skin type.
        /// </summary>
        /// <param name="type">The skin type to filter by.</param>
        /// <returns>List of <see cref="SkinItem"/> for the specified type.</returns>
        public List<SkinItem> GetSkinsByType(SkinType type)
        {
            return _skins.FindAll(s => s.Type == type);
        }

        /// <summary>
        /// Exposes the raw list of skins (read-only).
        /// </summary>
        public IReadOnlyList<SkinItem> Skins => _skins;

        /// <summary>
        /// Gets the default skin ID for the given type.
        /// </summary>
        public string GetDefaultSkinId(SkinType type)
        {
            return type == SkinType.Character ? _defaultCharacterSkinId : _defaultBackgroundSkinId;
        }
    }
