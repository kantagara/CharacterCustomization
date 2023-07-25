using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Customization
{
    public class LocalCategoryFetcher : MonoBehaviour, ICategoryFetcher
    {
        [SerializeField] private List<LocalItemCategory> _itemCategories;
        
        public List<IItemCategory> FetchCategories()
        {
            return _itemCategories.Select(x => (IItemCategory)x).ToList();
        }
    }
}