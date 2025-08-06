using UnityEngine;
using UnityEngine.UI;
using Zenject;

    /// <summary>
    /// UI Button component that represents and selects a specific skin when clicked.
    /// Automatically injects ISkinService to handle selection.
    /// </summary>
    [RequireComponent(typeof(Button))]
    sealed class SkinButton : MonoBehaviour
    {
        [Header("Skin Data")]
        [Tooltip("The type of skin this button represents.")]
        [SerializeField] private SkinType _skinType;

        [Tooltip("The unique ID of the skin.")]
        [SerializeField] private string _skinId;

        [Header("UI References")]
        [Tooltip("The image component that displays the skin icon.")]
        [SerializeField] private Image _skinIcon;

        [Tooltip("Optional: Visual highlight for selected state.")]
        [SerializeField] private GameObject _selectedFrame;

        private ISkinService _skinService;
        [SerializeField] private Button _button;

        [Inject]
        public void Construct(ISkinService skinService)
        {
            _skinService = skinService;
        }

        private void Awake() => _button.onClick.AddListener(OnButtonClicked);

    /// <summary>
    /// This is a temporary solution because there is no centralized UI controller, 
    /// and the object will update itself when it starts.
    /// </summary>
    private void Start() => UpdateSelectionState();


    /// <summary>
    /// Sets up the skin button UI based on the given SkinItem.
    /// This should be called from an external UI controller.
    /// </summary>
    public void Setup(SkinItem skin)
        {
            _skinType = skin.Type;
            _skinId = skin.SkinId;

            if (_skinIcon != null)
                _skinIcon.sprite = skin.Sprite;

            UpdateSelectionState();
        }

        /// <summary>
        /// Called when the button is clicked. Attempts to select the associated skin.
        /// </summary>
        private void OnButtonClicked()
        {
            _skinService.SelectSkin(_skinType, _skinId);
            UpdateSelectionStateForAll(); // update visuals globally
        }

        /// <summary>
        /// Updates this button's visual highlight if it's selected.
        /// </summary>
        public void UpdateSelectionState()
        {
            if (_skinService == null)
            {
                Debug.LogWarning($"[SkinButton] _skinService is null on UpdateSelectionState for skinId {_skinId}");
                return;
            }

            bool isSelected = _skinService.GetSelectedSkin(_skinType)?.SkinId == _skinId;
            if (_selectedFrame != null)
                _selectedFrame.SetActive(isSelected);
        }



        /// <summary>
        /// Updates all SkinButton instances in the scene (e.g. to refresh selection state).
        /// </summary>
        private void UpdateSelectionStateForAll()
        {
            foreach (var button in FindObjectsOfType<SkinButton>())
            {
                button.UpdateSelectionState();
            }
        }
    }
