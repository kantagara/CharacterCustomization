using System.Collections.Generic;

namespace Scripts.Customization
{
    public interface ICategoryFetcher
    {
        public List<IItemCategory> FetchCategories();
    }
}