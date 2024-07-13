using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maything.UI.MenuStripUI
{

    [RequireComponent(typeof(RectTransform))]
    public class MenuStripUI : MonoBehaviour
    {
        public RectTransform canvasTransform;

        public MenuStripTheme theme;
        public GameObject menuTemplate;
        public GameObject menuItemTemplate;


        public Image imageBackground;
        public Shadow backgroundShadow;
        public RectTransform border;
        public Image imageIconBar;
        public RectTransform content;

        [Multiline(20)]
        public string itemText;

        [HideInInspector]
        [NonSerialized]
        public List<MenuStripItemData> items = new List<MenuStripItemData>();

        List<MenuStripItemUI> uiItems = new List<MenuStripItemUI>();

        RectTransform ownerRectTransform;
        CanvasGroup canvasGroup;

        bool isFading = false;
        float fadeTick = 0;
        bool isFadeIn = false;

        bool isRegisterUI = false;

        [HideInInspector]
        public MenuStripUI parentMenuStripUI = null;

        [HideInInspector]
        public bool isClose = true;

        MenuStripTheme.enumMenuAnimationDirection menuAnimationDirection = MenuStripTheme.enumMenuAnimationDirection.TopToBottom;

        // Start is called before the first frame update
        void Start()
        {
            if (parentMenuStripUI == null)
            {
                List<MenuStripItemData> dataList = MenuHelper.ItemTextToItemData(itemText);
                if (dataList.Count > 0)
                    items = dataList;
            }

            ownerRectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            ownerRectTransform.sizeDelta = new Vector2(theme.menuWidth, 0);

            UpdateBackground();
            UpdateBorder();
            AddItemMenus();

            if (parentMenuStripUI != null)
            {
                menuAnimationDirection = MenuStripTheme.enumMenuAnimationDirection.LeftToRight;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isRegisterUI == false)
            {
                if (MenuStripEventSystem.instance != null)
                {
                    MenuStripEventSystem.instance.menuUIs.Add(this);
                    isRegisterUI = true;
                }
            }

            UpdateFadeAnimation();
        }

        void UpdateBackground()
        {
            if (imageBackground != null)
            {
                imageBackground.color = theme.menuBackground;
            }
            if (backgroundShadow != null)
            {
                backgroundShadow.enabled = theme.isShadow;
                backgroundShadow.effectColor = theme.shadowColor;
            }

            if (imageIconBar != null)
            {
                if (theme.isShowIcon == false)
                {
                    imageIconBar.gameObject.SetActive(false);
                }
                else
                {
                    imageIconBar.color = theme.iconBarColor;
                    imageIconBar.GetComponent<RectTransform>().sizeDelta = new Vector2(theme.iconBarWidth, 0);
                }
            }
        }

        void UpdateBorder()
        {
            if (theme.isShowBorder == false)
            {
                border.gameObject.SetActive(false);
            }
            else
            {
                for (int j = 0; j < border.childCount; j++)
                {
                    Image img = border.GetChild(j).GetComponent<Image>();
                    if (img != null)
                        img.color = theme.borderColor;

                }
            }
        }

        void ClearItems()
        {
            //移除已存在的数据
            if (content.childCount > 0)
            {
                for (int i = 0; i < content.childCount; i++)
                {
                    Destroy(content.GetChild(i).gameObject);
                }
                content.DetachChildren();
            }
            uiItems.Clear();
        }

        void AddItemMenus()
        {
            ClearItems();

            ownerRectTransform.sizeDelta = new Vector2(ownerRectTransform.rect.width, items.Count * theme.itemTheme.itemHeight);

            for (int i = 0; i < items.Count; i++)
            {
                GameObject go = Instantiate(menuItemTemplate, content);
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);
                rect.sizeDelta = new Vector2(ownerRectTransform.rect.width, theme.itemTheme.itemHeight);

                rect.localPosition = new Vector3(ownerRectTransform.rect.width / -2f, ownerRectTransform.rect.height / 2f - 1 * Convert.ToSingle(i) * theme.itemTheme.itemHeight, 0);
                MenuStripItemUI ui = go.GetComponent<MenuStripItemUI>();
                if (ui != null)
                {
                    ui.Initialization(this, items[i]);
                }
                uiItems.Add(ui);

            }
        }

        public void CloseChildMenu(bool isCloseParent)
        {
            if (isCloseParent)
            {
                if (parentMenuStripUI != null)
                    parentMenuStripUI.CloseChildMenu(isCloseParent);

            }

            foreach (MenuStripItemUI ui in uiItems)
            {
                ui.CloseChildMenu();
            }

            if (isCloseParent)
            {
                //关闭自己
                //ownerRectTransform.gameObject.SetActive(false);
                CloseMenu();
            }
        }

        public void ShowMenu()
        {
            //if (isFading) return;
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            if (theme.menuAnimation == MenuStripTheme.enumMenuAnimation.None)
            {
                isClose = false;
                this.gameObject.SetActive(true);
                canvasGroup.alpha = 1;
            }
            else
            {
                this.gameObject.SetActive(true);
                canvasGroup.alpha = 0;
                isFading = true;
                isFadeIn = true;
                //fadeTick = 0;
            }
        }

        public void CloseMenu()
        {
            //if (isFading) return;
            isClose = true;

            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            if (theme.menuAnimation == MenuStripTheme.enumMenuAnimation.None)
            {
                this.gameObject.SetActive(false);
                canvasGroup.alpha = 1;
            }
            else
            {
                isFading = true;
                isFadeIn = false;
                //fadeTick = 1;
            }

        }

        private void UpdateFadeAnimation()
        {
            if (isFading == false) return;

            if (isFadeIn)
            {
                fadeTick += Time.deltaTime * theme.animationSpeed;
                if (fadeTick >= 1)
                {
                    fadeTick = 1;
                    isFading = false;
                    isClose = false;
                }

                switch (theme.menuAnimation)
                {
                    case MenuStripTheme.enumMenuAnimation.Fade:
                        canvasGroup.alpha = fadeTick;
                        break;
                    case MenuStripTheme.enumMenuAnimation.Scroll:
                        canvasGroup.alpha = 1;

                        switch (menuAnimationDirection)
                        {
                            case MenuStripTheme.enumMenuAnimationDirection.TopToBottom:
                                ownerRectTransform.localScale = new Vector3(1, fadeTick, 1);
                                break;
                            case MenuStripTheme.enumMenuAnimationDirection.LeftToRight:
                                ownerRectTransform.localScale = new Vector3(fadeTick, 1, 1);
                                break;
                        }
                        break;
                    case MenuStripTheme.enumMenuAnimation.FadeAndScroll:
                        canvasGroup.alpha = fadeTick;
                        switch (menuAnimationDirection)
                        {
                            case MenuStripTheme.enumMenuAnimationDirection.TopToBottom:
                                ownerRectTransform.localScale = new Vector3(1, fadeTick, 1);
                                break;
                            case MenuStripTheme.enumMenuAnimationDirection.LeftToRight:
                                ownerRectTransform.localScale = new Vector3(fadeTick, 1, 1);
                                break;
                        }
                        break;
                }
            }
            else
            {
                fadeTick -= Time.deltaTime * theme.animationSpeed;
                if (fadeTick <= 0)
                {
                    isFading = false;
                    this.gameObject.SetActive(false);
                    fadeTick = 0;
                }
                canvasGroup.alpha = fadeTick;

            }
        }
    }
}