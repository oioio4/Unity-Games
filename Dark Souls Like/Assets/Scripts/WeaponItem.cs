using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    [CreateAssetMenu(menuName = "Items/Weapons")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
    }
}
