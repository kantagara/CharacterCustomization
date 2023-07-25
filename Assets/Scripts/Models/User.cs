using System;
using System.Collections.Generic;

namespace Scripts.Model
{
    [Serializable]
    public record User(string Username, string Password, Dictionary<string, string> CurrentlySelectedItems,
        HashSet<string> OwnedItems);
}