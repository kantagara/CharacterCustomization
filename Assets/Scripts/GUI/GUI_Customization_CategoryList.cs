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

        private void Start()
        {
            _categoryFetcher = GetComponentInParent<ICategoryFetcher>();

            var categories = _categoryFetcher.FetchCategories();
            
            EventSystem<OnCategoriesFetched>.Invoke(new OnCategoriesFetched(){Categories = categories});


            for (var index = 0; index < categories.Count; index++)
            {
                var category = categories[index];
                var categoryButtonInstance = Instantiate(categoryToggle, customizationTabsParent);
                categoryButtonInstance.Configure(toggleGroup, category, index == 0);
            }
        }
    }
}