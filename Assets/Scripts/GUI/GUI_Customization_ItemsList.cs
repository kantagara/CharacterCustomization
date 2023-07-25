using System.Collections.Generic;
using Scripts.EventSystem;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI
{
    /// <summary>
    ///     View Class Responsible for displaying the items of the current category
    /// </summary>
    public class GUI_Customization_ItemsList : MonoBehaviour
    {
        private const int INITIAL_ITEMS_AMOUNT = 10;
        [SerializeField] private GUI_Customization_ItemToggle itemTogglePrefab;
        [SerializeField] private Transform contentParent;
        [SerializeField] private ToggleGroup toggleGroup;

        private readonly List<GUI_Customization_ItemToggle> _itemToggles = new();

        private void Awake()
        {
            EventSystem<OnCategoryChanged>.Subscribe(CategoryChanged);
            InstantiateCategoryPrefabs(INITIAL_ITEMS_AMOUNT);
        }

        private void OnDestroy()
        {
            EventSystem<OnCategoryChanged>.Unsubscribe(CategoryChanged);
        }

        private void InstantiateCategoryPrefabs(int amount)
        {
            for (var i = 0; i < amount; i++) _itemToggles.Add(Instantiate(itemTogglePrefab, contentParent));
        }


        private void CategoryChanged(OnCategoryChanged categoryChanged)
        {
            var newCategory = categoryChanged.CurrentCategory;

            contentParent.DeactivateChildren();

            //If current category has more elements than what we currently have
            //Instantiate more.
            if (newCategory.Items.Count > _itemToggles.Count)
                InstantiateCategoryPrefabs(newCategory.Items.Count - _itemToggles.Count);

            for (var index = 0; index < newCategory.Items.Count; index++)
            {
                var item = newCategory.Items[index];
                var itemToggleInstance = _itemToggles[index];
                itemToggleInstance.Configure(item, newCategory, toggleGroup);
                itemToggleInstance.gameObject.SetActive(true);
            }
        }
    }
}