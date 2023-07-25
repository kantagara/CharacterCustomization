using System;
using Scripts.Customization;
using Scripts.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI
{
    /// <summary>
    /// Toggle that is responsible for changing the category of items in the customization menu
    /// </summary>
    public class GUI_Customization_CategoryToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Text categoryLabel;

        private IItemCategory _category;

        public void Configure(ToggleGroup toggleGroup, IItemCategory category, bool isOn)
        {
            toggle.group = toggleGroup;
            categoryLabel.text = category.Name;
            _category = category;
            toggle.onValueChanged.AddListener(ToggleValueChanged);
            toggle.isOn = isOn;
        }

        private void ToggleValueChanged(bool isOn)
        {
            if(isOn)
                EventSystem<OnCategoryChanged>.Invoke(new OnCategoryChanged() {CurrentCategory = _category});
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}