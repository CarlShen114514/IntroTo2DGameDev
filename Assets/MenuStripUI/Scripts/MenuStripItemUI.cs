using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maything.UI.MenuStripUI
{

    public class MenuStripItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public Image icon;
        public Text text;
        public Text textShortcut;

        public Image selector;
        public RectTransform border;

        public RectTransform split1;
        public RectTransform split2;

        public Image arrow;

        MenuStripUI menuStripUI;
        MenuStripItemData itemData;

        RectTransform ownerTransform;

        MenuStripUI childMenuUI = null;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void Initialization(MenuStripUI menuStripUI, MenuStripItemData itemData)
        {
            ownerTransform = GetComponent<RectTransform>();

            this.menuStripUI = menuStripUI;
            this.itemData = itemData;
            UpdateBorder();
            UpdateContent();
            UpdateMenuState();
            AddChildMenu();

        }

        void UpdateBorder()
        {
            Color c = menuStripUI.theme.itemTheme.itemBorderColor;


            if (menuStripUI.theme.itemTheme.isShowItemBorder == false)
            {
                border.gameObject.SetActive(false);
                c = new Color(0, 0, 0, 0);
            }

            for (int j = 0; j < border.childCount; j++)
            {
                Image img = border.GetChild(j).GetComponent<Image>();
                if (img != null)
                    img.color = c;
            }

        }

        void UpdateContent()
        {
            if (icon != null && itemData.icon != null && menuStripUI.theme.isShowIcon)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = itemData.icon;
            }
            else
            {
                //icon.sprite= null;
                icon.gameObject.SetActive(false);
            }

            if (arrow != null)
            {
                if (menuStripUI.theme.itemTheme.arrowIcon != null)
                {
                    if (itemData.childItems != null && itemData.childItems.Count > 0)
                    {
                        arrow.sprite = menuStripUI.theme.itemTheme.arrowIcon;
                        arrow.color = menuStripUI.theme.itemTheme.normalTextColor;
                    }
                    else
                        arrow.gameObject.SetActive(false);
                }
                else
                    arrow.gameObject.SetActive(false);
            }

            if (text != null)
            {
                text.text = itemData.name;
                text.color = menuStripUI.theme.itemTheme.normalTextColor;
                if (itemData.isBold)
                    text.fontStyle = FontStyle.Bold;

                RectTransform textRect = text.GetComponent<RectTransform>();
                if (menuStripUI.theme.isShowIcon)
                {
                    //有icon
                    textRect.localPosition = new Vector3(ownerTransform.rect.width / 2f + menuStripUI.theme.itemTheme.textLeftSpace + menuStripUI.theme.iconBarWidth, ownerTransform.rect.height / -2, 0);
                }
                else
                {
                    //没有icon
                    textRect.localPosition = new Vector3(ownerTransform.rect.width / 2f + menuStripUI.theme.itemTheme.textLeftSpace, ownerTransform.rect.height / -2, 0);
                }
            }

            if (textShortcut != null)
            {
                if (itemData.shortcutKey == MenuStripItemData.enumShortcutKey.NONE || itemData.shortcutLetter == "")
                    textShortcut.gameObject.SetActive(false);
                else
                {
                    string sText = itemData.shortcutKey.ToString().Replace("_", "+");
                    textShortcut.text = sText + "+" + itemData.shortcutLetter.ToUpper();
                }
            }

            if (split1 != null && split2 != null)
            {
                if (itemData.isSplit == false)
                {
                    split1.gameObject.SetActive(false);
                    split2.gameObject.SetActive(false);
                }
                else
                {
                    split1.gameObject.SetActive(true);
                    split2.gameObject.SetActive(true);
                    split1.GetComponent<Image>().color = menuStripUI.theme.itemTheme.splitColor1;
                    split2.GetComponent<Image>().color = menuStripUI.theme.itemTheme.splitColor2;
                    UpdateSplitLinePosition(split1, 0);
                    UpdateSplitLinePosition(split2, 1);
                }
            }

            if (selector != null)
            {
                selector.color = menuStripUI.theme.itemTheme.normalBackground;
            }
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }

        }

        void UpdateSplitLinePosition(RectTransform split, float offset)
        {
            if (menuStripUI.theme.isShowIcon)
            {
                split.sizeDelta = new Vector2(ownerTransform.rect.width - 4 - menuStripUI.theme.iconBarWidth, 1);
                //split.localPosition = new Vector3(ownerTransform.rect.width /2f + 2 + menuStripUI.theme.iconBarWidth,
                //                                  ownerTransform.rect.height / -2f - menuStripUI.theme.itemTheme.itemHeight/2f ,0);
                split.localPosition = new Vector3(ownerTransform.rect.width - 2,
                                                  ownerTransform.rect.height / -2f - menuStripUI.theme.itemTheme.itemHeight / 2f + offset, 0);
            }
            else
            {
                split.sizeDelta = new Vector2(ownerTransform.rect.width - 4, 1);
                split.localPosition = new Vector3(ownerTransform.rect.width - 2,
                                                  ownerTransform.rect.height / -2f - menuStripUI.theme.itemTheme.itemHeight / 2f + offset, 0);
            }

        }

        bool AddChildMenu()
        {
            if (itemData.childItems == null) return false;
            if (itemData.childItems.Count == 0) return false;

            GameObject childObject = Instantiate(menuStripUI.menuTemplate, menuStripUI.canvasTransform);
            childMenuUI = childObject.GetComponent<MenuStripUI>();
            if (childMenuUI == null) return false;

            childMenuUI.theme = menuStripUI.theme;
            childMenuUI.parentMenuStripUI = menuStripUI;

            childMenuUI.items = itemData.childItems;

            childMenuUI.gameObject.SetActive(false);

            return true;
        }

        // Update is called once per frame
        void Update()
        {
            checkShortcutKey();
        }

        void checkShortcutKey()
        {
            if (itemData == null) return;
            if (itemData.isDisable == true) return;

            if (itemData.shortcutKey == MenuStripItemData.enumShortcutKey.NONE)
                return;

            if (itemData.shortcutLetter == "") return;

            bool isPressShortcutKey = false;

            switch (itemData.shortcutKey)
            {
                case MenuStripItemData.enumShortcutKey.Alt:
                    if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                        isPressShortcutKey = true;

                    break;
                case MenuStripItemData.enumShortcutKey.Shift:
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        isPressShortcutKey = true;

                    break;
                case MenuStripItemData.enumShortcutKey.Ctrl:
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                        isPressShortcutKey = true;

                    break;
                case MenuStripItemData.enumShortcutKey.Ctrl_Shift:
                    if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                         (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                        isPressShortcutKey = true;


                    break;
                case MenuStripItemData.enumShortcutKey.Ctrl_Alt:
                    if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                         (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
                        isPressShortcutKey = true;

                    break;

            }

            if (isPressShortcutKey == false) return;

            char checkLetter = Convert.ToChar(itemData.shortcutLetter.ToLower().Substring(0, 1));
            KeyCode code = (KeyCode)((int)checkLetter);

            if (Input.GetKeyDown(code))
            {
                //Debug.Log("Shortcut Key!!");
                if (MenuStripEventSystem.instance != null)
                    MenuStripEventSystem.instance.onMenuClick.Invoke(itemData);

                menuStripUI.CloseChildMenu(true);
            }
        }

        void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (itemData == null) return;
            if (itemData.isDisable) return;

            menuStripUI.CloseChildMenu(false);

            if (selector != null)
            {
                selector.color = menuStripUI.theme.itemTheme.enterBackground;
            }
            if (text != null)
            {
                text.color = menuStripUI.theme.itemTheme.enterTextColor;
                textShortcut.color = menuStripUI.theme.itemTheme.enterTextColor;
            }
            if (arrow != null)
                arrow.color = menuStripUI.theme.itemTheme.enterTextColor;

            if (border != null)
            {
                border.gameObject.SetActive(true);
            }

            if (childMenuUI != null)
            {
                //计算位置
                childMenuUI.GetComponent<RectTransform>().position = ownerTransform.position + new Vector3(ownerTransform.rect.width, 0, 0);
                //childMenuUI.gameObject.SetActive(true);
                childMenuUI.ShowMenu();
            }
        }

        void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (itemData == null) return;
            if (itemData.isDisable) return;
            if (childMenuUI != null) return;

            RestoreToNormal();

            //if (childMenuUI != null)
            //{
            //    childMenuUI.gameObject.SetActive(false);
            //}
        }

        void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (itemData.isDisable) return;

            if (selector != null)
            {
                selector.color = menuStripUI.theme.itemTheme.normalBackground;
            }
            if (text != null)
            {
                text.color = menuStripUI.theme.itemTheme.normalTextColor;
                textShortcut.color = menuStripUI.theme.itemTheme.normalTextColor;
            }
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }

            if (MenuStripEventSystem.instance != null)
            {
                MenuStripEventSystem.instance.onMenuClick.Invoke(itemData);
                //MenuStripEventSystem.instance.isClickMenu = true;
            }


            menuStripUI.CloseChildMenu(true);
        }

        public void UpdateMenuState()
        {
            if (itemData.isDisable)
            {
                if (text != null) text.color = menuStripUI.theme.itemTheme.disableTextColor;
                if (textShortcut != null) textShortcut.color = menuStripUI.theme.itemTheme.disableTextColor;
                if (icon != null) icon.color = menuStripUI.theme.itemTheme.disableTextColor;
                if (arrow != null) arrow.color = menuStripUI.theme.itemTheme.disableTextColor;
            }
            else
            {
                if (text != null) text.color = menuStripUI.theme.itemTheme.normalTextColor;
                if (textShortcut != null) textShortcut.color = menuStripUI.theme.itemTheme.normalTextColor;
                if (arrow != null) arrow.color = menuStripUI.theme.itemTheme.normalTextColor;
                if (icon != null) icon.color = Color.white;
            }
        }

        void RestoreToNormal()
        {
            if (selector != null)
            {
                selector.color = menuStripUI.theme.itemTheme.normalBackground;
            }
            if (text != null)
            {
                text.color = menuStripUI.theme.itemTheme.normalTextColor;
                textShortcut.color = menuStripUI.theme.itemTheme.normalTextColor;
            }
            if (arrow != null)
                arrow.color = menuStripUI.theme.itemTheme.normalTextColor;
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }

        }

        public void CloseChildMenu()
        {
            if (childMenuUI != null)
            {
                RestoreToNormal();
                childMenuUI.CloseChildMenu(false);
                //childMenuUI.gameObject.SetActive(false);
                childMenuUI.CloseMenu();
            }
        }
    }
}