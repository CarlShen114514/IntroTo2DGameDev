using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{
    public class MenuHelper 
    {
        public static List<MenuStripItemData> ItemTextToItemData(string text)
        {
            List<MenuStripItemData> root = new List<MenuStripItemData>();
            List<MenuStripItemData> allDatas = new List<MenuStripItemData>();

            text = text.Replace("\r", "");

            string[] lines = text.Split("\n");

            foreach (string line in lines)
            {
                string[] spt = line.Split(',');
                MenuStripItemData data = new MenuStripItemData();
                data.name = spt[0].Trim();

                data.level = FindLevel(spt[0]);

                if (data.name.Trim() == "") continue;
                if (spt.Length >= 2)
                {
                    for (int i = 1; i < spt.Length; i++)
                    {
                        switch (spt[i].ToLower())
                        {
                            case "split":
                                data.isSplit = true;
                                break;
                            case "bold":
                                data.isBold = true;
                                break;
                            case "disable":
                                data.isDisable = true;
                                break;
                            default:
                                //可能是图标，也可能是快捷键
                                int check = spt[i].IndexOf('+');
                                if (check>-1)
                                {
                                    //快捷键！
                                    string[] cmd = spt[i].Split('+');
                                    switch (cmd[0].ToLower())
                                    {
                                        case "alt":
                                            data.shortcutKey = MenuStripItemData.enumShortcutKey.Alt;
                                            data.shortcutLetter = cmd[1];
                                            break;
                                        case "shift":
                                            data.shortcutKey = MenuStripItemData.enumShortcutKey.Ctrl;
                                            data.shortcutLetter = cmd[1];
                                            break;
                                        case "ctrl":
                                            if (cmd[1].ToLower() == "alt")
                                            {
                                                data.shortcutKey = MenuStripItemData.enumShortcutKey.Ctrl_Alt;
                                                data.shortcutLetter = cmd[2];
                                            }
                                            else if (cmd[1].ToLower() == "shift")
                                            {
                                                data.shortcutKey = MenuStripItemData.enumShortcutKey.Ctrl_Shift;
                                                data.shortcutLetter = cmd[2];
                                            }
                                            else
                                            {
                                                data.shortcutKey = MenuStripItemData.enumShortcutKey.Ctrl;
                                                data.shortcutLetter = cmd[1];
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    //图标！
                                    data.icon = Resources.Load<Sprite>(spt[i]);
                                }
                                break;
                        }
                    }
                }

                if (data.level==0)
                    root.Add(data);
                else
                {
                    //查找上一级level
                    MenuStripItemData parentData = null;
                    for (int i=allDatas.Count-1; i>=0; i--)
                    {
                        if (allDatas[i].level==data.level-1)
                        {
                            parentData= allDatas[i];
                            break;
                        }
                    }
                    if (parentData!=null)
                    {
                        parentData.childItems.Add(data);
                    }
                }

                allDatas.Add(data);
            }

            return root;
        }

        static int FindLevel(string text)
        {
            int iSpace = 0;
            for(int i=0;i<text.Length; i++)
            {
                if (text.Substring(i, 1) == "\t")
                    iSpace++;
                else
                    break;
            }

            return iSpace;
        }
    }
}