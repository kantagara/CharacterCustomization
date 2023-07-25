using UnityEngine;

namespace Scripts.Utils
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                Object.Destroy(child.gameObject);
            }
        }
        
        public static void DeactivateChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }
    }
}