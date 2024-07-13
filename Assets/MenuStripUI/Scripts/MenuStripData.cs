using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{

    [Serializable]
    public class MenuStripData
    {
        public string key;
        public RectTransform dockTransform;

        [Multiline(20)]
        public string itemText;

        [NonSerialized]
        [HideInInspector]
        public List<MenuStripItemData> itemData = new List<MenuStripItemData>();

        [HideInInspector]
        public MenuStripUI menuUI;
    }
}