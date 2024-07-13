using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{

    [Serializable]
    public class MenuBarData
    {
        public string name;
        public string key;

        [Multiline(20)]
        public string itemText;

        //public List<MenuStripItemData> itemData = new List<MenuStripItemData>();
        public List<MenuStripItemData> itemData = new List<MenuStripItemData>();

        [HideInInspector]
        public MenuStripUI menuStripUI;

        [HideInInspector]
        public GameObject menuBarUI;

    }
}