using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{

    [Serializable]
    public class MenuStripItemData //: ISerializationCallbackReceiver
    {
        public enum enumShortcutKey
        {
            NONE,
            Alt,
            Ctrl,
            Shift,
            Ctrl_Alt,
            Ctrl_Shift,
        }

        public string name;
        public Sprite icon;
        public int level;
        public bool isSplit = false;
        public bool isBold = false;
        public bool isDisable = false;

        public enumShortcutKey shortcutKey = enumShortcutKey.NONE;
        public string shortcutLetter = "";
        //public KeyCode shortcutKey = KeyCode.None;
        //public KeyCode shortcutLetter = KeyCode.None;

        //[SerializeReference] 
        [HideInInspector]
        [NonSerialized]
        public List<MenuStripItemData> childItems = new List<MenuStripItemData>();

        //void ISerializationCallbackReceiver.OnAfterDeserialize()
        //{

        //}

        //void ISerializationCallbackReceiver.OnBeforeSerialize()
        //{
        //    if (childItems.Count > 0)
        //    {
        //        Serialize(childItems);
        //    }
        //}

        public override string ToString()
        {
            return name + "[" + childItems.Count.ToString() + "]";
        }
    }

}

