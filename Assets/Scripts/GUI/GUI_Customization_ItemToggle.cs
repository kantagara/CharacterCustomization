using System;
using Scripts.Customization;
using Scripts.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI
{
    /// <summary>
    /// Because all items are currently free, we can use toggle
    /// If we were to have some prices on the items, it would be more appropriate to have a button since that way
    /// We can process the click itself.
    /// </summary>
    public class GUI_Customization_ItemToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Text itemName;

        private IItem _item;
        public void Configure(IItem item, IItemCategory category, ToggleGroup toggleGroup)
        {
            var shouldBeOn = UserManager.LocalUser.CurrentlySelectedItems.TryGetValue(category.Name,
                out var selectedItem) && selectedItem == item.Name;
            _item = item;
            _item.Category = category;
            toggle.isOn = shouldBeOn;
            toggle.group = toggleGroup;
            toggleGroup.allowSwitchOff = true;
            itemName.text = item.Name;
            
            toggle.onValueChanged.AddListener(ToggleValueChanged);
        }

        private void ToggleValueChanged(bool isOn)
        {
            if (isOn)
            {
                //Here it would be nice to have some sort of a check for if we have enough money to buy this
                //But that's not necessary for this task.
                EventSystem<OnItemAdded>.Invoke(new OnItemAdded(){Item = _item});
            }
            else EventSystem<OnGUIItemRemoved>.Invoke(new OnGUIItemRemoved(){Item = _item});
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}