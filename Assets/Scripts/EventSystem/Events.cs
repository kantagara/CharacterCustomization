using System;
using System.Collections.Generic;
using Scripts.Customization;
using Scripts.Model;

namespace Scripts.EventSystem
{

    public class OnUserJoined : EventArgs
    {
        public User User { get; set; }
    }
    public class OnUserLeft : EventArgs
    {
    }

   

    public class OnCategoryChanged : EventArgs
    {
        public IItemCategory CurrentCategory { get; set; }
    }

    public class OnItemAdded : EventArgs
    {
        public IItem Item { get; set; }
    }

    public class OnCategoriesFetched : EventArgs
    {
        public List<IItemCategory> Categories { get; set; }
    }



    public class OnUserLoginCategoriesReady : EventArgs
    {
        public List<IItemCategory> Categories { get; set; }

    }

    public class OnItemRemoved : EventArgs
    {
        public IItem Item { get; set; }
    }
    
    /// <summary>
    /// Since we need to have additional checks to make sure that we are actually removing an item
    /// from the list, we need to have a separate event for that, since this can also get triggered when toggle goes off.
    /// </summary>
    public class OnGUIItemRemoved : EventArgs
    {
        public IItem Item { get; set; }
    }
}