using System;
using System.Collections.Generic;
using Scripts.Customization;
using Scripts.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI
{
    public class GUI_Customization_CategoryList : MonoBehaviour
    {
        [SerializeField] private Transform customizationTabsParent;
        [SerializeField] private GUI_Customization_CategoryToggle categoryToggle;
        [SerializeField] private ToggleGroup toggleGroup;

        private ICategoryFetcher _categoryFetcher;

        private List<IItemCategory> _categories;

        private void Start()
        {
            _categoryFetcher = GetComponentInParent<ICategoryFetcher>();

            _categories = _categoryFetcher.FetchCategories();
            EventSystem<OnCategoriesFetched>.Invoke(new OnCategoriesFetched(){Categories = _categories});

            
            for (var index = 0; index < _categories.Count; index++)
            {
                var category = _categories[index];
                var categoryButtonInstance = Instantiate(categoryToggle, customizationTabsParent);
                categoryButtonInstance.Configure(toggleGroup, category, index == 0);
            }
            
            EventSystem<OnUserJoined>.Subscribe(OnUserLogin);
        }

        private void OnUserLogin(OnUserJoined obj)
        {
            if(_categories == null) return;
            EventSystem<OnUserLoginCategoriesReady>.Invoke(new OnUserLoginCategoriesReady(){Categories = _categories});
        }

        private void OnDestroy()
        {
            EventSystem<OnUserJoined>.Unsubscribe(OnUserLogin);
        }
    }
}