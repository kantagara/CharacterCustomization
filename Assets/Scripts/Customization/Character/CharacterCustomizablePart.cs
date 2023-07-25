using Scripts.EventSystem;
using UnityEngine;

namespace Scripts.Customization.Character
{
    public abstract class CharacterCustomizablePart<T, K> : MonoBehaviour where T : IItem where K : IItemCategory
    {
        [SerializeField] private K category;

        protected virtual void Awake()
        {
            EventSystem<OnItemAdded>.Subscribe(ApplyCustomization);
            EventSystem<OnItemRemoved>.Subscribe(RemoveCustomization);
            EventSystem<OnCategoriesFetched>.Subscribe(OnCategoriesFetched);
        }

        protected virtual void OnDestroy()
        {
            EventSystem<OnItemAdded>.Unsubscribe(ApplyCustomization);
            EventSystem<OnItemRemoved>.Unsubscribe(RemoveCustomization);
            EventSystem<OnCategoriesFetched>.Unsubscribe(OnCategoriesFetched);
        }

        private void OnCategoriesFetched(OnCategoriesFetched obj)
        {
            var cat = obj.Categories.Find(x => x.Name == category.Name);
            if (cat == null) return;
            if (!UserManager.LocalUser.CurrentlySelectedItems.ContainsKey(cat.Name)) return;
            var item = cat.Items.Find(x => x.Name == UserManager.LocalUser.CurrentlySelectedItems[cat.Name]);
            if (item == null) return;
            ApplyCustomization((T)item);
        }

        private void RemoveCustomization(OnItemRemoved obj)
        {
            if (obj.Item.Category.Name == category.Name)
                RemoveCustomization((T)obj.Item);
        }

        private void ApplyCustomization(OnItemAdded obj)
        {
            if (obj.Item.Category.Name == category.Name)
                ApplyCustomization((T)obj.Item);
        }


        protected abstract void ApplyCustomization(T item);
        protected abstract void RemoveCustomization(T item);
    }
}