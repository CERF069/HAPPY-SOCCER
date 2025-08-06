using UnityEngine;

    /// <summary>
    /// Represents a single skin in the game.
    /// Contains information about its type, visual appearance, unique ID, price, availability, and current state.
    /// Used within the SkinData ScriptableObject.
    /// </summary>
    [System.Serializable]
    public class SkinItem
    {
        [Header("Core Properties")]

        [Tooltip("Unique identifier for the skin.")]
        [SerializeField] private string _skinId;

        [Tooltip("Skin type (e.g., Character, Background).")]
        [SerializeField] private SkinType _type;

        [Tooltip("Visual representation of the skin.")]
        [SerializeField] private Sprite _sprite;

        [Header("Purchase & State")]

        [Tooltip("Price of the skin in in-game currency.")]
        [SerializeField] private int _price;

        [Tooltip("Indicates whether the skin is available for purchase.")]
        [SerializeField] private bool _isPurchasable;

        [Tooltip("Current state of the skin (NotPurchased, Purchased, Selected).")]
        [SerializeField] private SkinState _state;

        /// <summary>
        /// Unique ID of the skin (read-only).
        /// </summary>
        public string SkinId => _skinId;

        /// <summary>
        /// Type of the skin (Character, Background, etc.).
        /// </summary>
        public SkinType Type => _type;

        /// <summary>
        /// The sprite associated with this skin.
        /// </summary>
        public Sprite Sprite => _sprite;

        /// <summary>
        /// Cost of the skin in in-game currency.
        /// Only considered if <see cref="IsPurchasable"/> is true.
        /// </summary>
        public int Price => _price;

        /// <summary>
        /// Determines whether the skin can be purchased.
        /// If false, price is ignored.
        /// </summary>
        public bool IsPurchasable => _isPurchasable;

        /// <summary>
        /// Current runtime state of the skin (e.g., selected, purchased).
        /// This property can be modified during gameplay.
        /// </summary>
        public SkinState State
        {
            get => _state;
            set => _state = value;
        }
    }
