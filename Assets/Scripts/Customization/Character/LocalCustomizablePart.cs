using Scripts.Utils;
using UnityEngine;

namespace Scripts.Customization.Character
{
    public class LocalCustomizablePart : CharacterCustomizablePart<LocalItem, LocalItemCategory>
    {
        [SerializeField] private Transform overridenParent;

        private Transform parent => overridenParent != null ? overridenParent : transform;
        
        protected override void ApplyCustomization(LocalItem item)
        {
            if(item.Prefab == null) return;
            var instance = Instantiate(item.Prefab, parent);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            instance.transform.localRotation = Quaternion.identity;
        }

        protected override void RemoveCustomization(LocalItem item)
        {
            parent.DestroyChildren();
        }
    }
}