using UnityEngine;

namespace Scripts.Customization
{
    [CreateAssetMenu(menuName = "Customization/Item")]
    public class LocalItem : ScriptableObject, IItem
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }

        //Is still not being used, but something to be thought about. Like parsing json to picture and storing it here.
        [field: SerializeField] public Sprite Preview { get; private set; }

        public string Name => name;
        public IItemCategory Category { get; set; }
    }
}