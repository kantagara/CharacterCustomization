using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Customization
{
    public interface IItemCategory
    { 
        public List<IItem> Items { get; }
        public string Name { get;  }
    }

    public interface IItem
    {
        public string Name { get; }
        public IItemCategory Category { get; set; }
    }
    
}