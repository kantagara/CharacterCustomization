using System.Collections.Generic;
using Scripts.EventSystem;
using UnityEngine;

namespace Scripts.Customization.Character
{
    public abstract class CharacterCustomizablePart<T, K> : MonoBehaviour where T : IItem where K : IItemCategory
    {
        [SerializeField] private K category;

        private T _item;

        protected virtual void Awake()
        {
            EventSystem<OnItemAdded>.Subscribe(ApplyCustomization);
            EventSystem<OnItemRemoved>.Subscribe(RemoveCustomization);
            EventSystem<OnUserLoginCategoriesReady>.Subscribe(OnUserLoginCategoriesReady);
            EventSystem<OnUserLeft>.Subscribe(OnUserLogout);
            EventSystem<OnCategoriesFetched>.Subscribe(OnCategoriesFetched);

        }

        protected virtual void OnDestroy()
        {
            EventSystem<OnItemAdded>.Unsubscribe(ApplyCustomization);
            EventSystem<OnItemRemoved>.Unsubscribe(RemoveCustomization);
            EventSystem<OnUserLoginCategoriesReady>.Unsubscribe(OnUserLoginCategoriesReady);
            EventSystem<OnCategoriesFetched>.Unsubscribe(OnCategoriesFetched);
            EventSystem<OnUserLeft>.Unsubscribe(OnUserLogout);
        }

        private void OnCategoriesFetched(OnCategoriesFetched obj)
        {
            UpdateClothing(obj.Categories);
        }

        private void OnUserLogout(OnUserLeft obj)
        {
            RemoveCustomization(_item);
        }

        private void OnUserLoginCategoriesReady(OnUserLoginCategoriesReady obj)
        {
            UpdateClothing(obj.Categories);
        }

        private void UpdateClothing(List<IItemCategory> categories)
        {
            var cat = categories.Find(x => x.Name == category.Name);
            if (cat == null) return;
            if (!UserManager.LocalUser.CurrentlySelectedItems.ContainsKey(cat.Name)) return;
            var item = cat.Items.Find(x => x.Name == UserManager.LocalUser.CurrentlySelectedItems[cat.Name]);
            if (item == null) return;
            _item = (T)item;
            ApplyCustomization(_item);
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