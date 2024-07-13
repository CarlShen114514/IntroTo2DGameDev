using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{

    [CreateAssetMenu(menuName = "MenuStrip UI/Create New Theme")]
    public class MenuStripTheme : ScriptableObject
    {
        public enum enumMenuAnimation
        {
            None,
            Fade,
            Scroll,
            FadeAndScroll,
        }

        public enum enumMenuAnimationDirection
        {
            TopToBottom,
            LeftToRight,
        }

        [Header("Menu Bar")]
        public Color menuBarBackground = Color.white;
        public Color menuBarTextColor = Color.black;
        public Color menuBarSelectedTextColor = Color.black;

        [Header("Animation")]
        public enumMenuAnimation menuAnimation = enumMenuAnimation.None;
        public enumMenuAnimationDirection topMenuAnimation = enumMenuAnimationDirection.TopToBottom;
        public enumMenuAnimationDirection childMenuAnimation = enumMenuAnimationDirection.LeftToRight;

        public float animationSpeed = 6f;

        [Header("Normal")]
        public Color menuBackground = Color.white;
        public Color textColor = Color.black;
        public float menuWidth = 180f;

        [Header("Icon")]
        public bool isShowIcon = true;
        public float iconSize = 20;
        public float iconBarWidth = 25;
        public Color iconBarColor = Color.gray;

        [Header("Shadow")]
        public bool isShadow = true;
        public Color shadowColor = new Color(0.23f, 0.23f, 0.23f, 0.24f);

        [Header("Border")]
        public bool isShowBorder = true;
        public Color borderColor = Color.gray;

        public MenuStripItemTheme itemTheme;
    }

    [Serializable]
    public class MenuStripItemTheme
    {
        public float itemHeight = 30;
        public float textLeftSpace = 10;

        [Header("Normal")]
        public Color normalBackground = new Color(0, 0, 0, 0);
        public Color normalTextColor = Color.black;
        public Sprite arrowIcon;

        [Header("Enter")]
        public Color enterBackground = Color.gray;
        public Color enterTextColor = Color.black;

        [Header("Disable")]
        public Color disableTextColor = Color.gray;

        [Header("Border")]
        public bool isShowItemBorder = true;
        public Color itemBorderColor = Color.gray;

        [Header("Split")]
        public Color splitColor1 = Color.gray;
        public Color splitColor2 = Color.white;
    }
}