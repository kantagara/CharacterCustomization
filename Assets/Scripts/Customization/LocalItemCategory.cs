using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Customization
{
   
    [CreateAssetMenu(menuName = "Customization/Item Category")]
    public class LocalItemCategory : ScriptableObject, IItemCategory
    {
        [field:SerializeField] public List<LocalItem> LocalItems { get; set; }

        public List<IItem> Items => LocalItems.Select(x => (IItem)x).ToList();
        public string Name => name;
    }
}