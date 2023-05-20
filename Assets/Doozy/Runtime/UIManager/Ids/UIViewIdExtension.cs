// Copyright (c) 2015 - 2022 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using System.Collections.Generic;
// ReSharper disable All
namespace Doozy.Runtime.UIManager.Containers
{
    public partial class UIView
    {
        public static IEnumerable<UIView> GetViews(UIViewId.Death id) => GetViews(nameof(UIViewId.Death), id.ToString());
        public static void Show(UIViewId.Death id, bool instant = false) => Show(nameof(UIViewId.Death), id.ToString(), instant);
        public static void Hide(UIViewId.Death id, bool instant = false) => Hide(nameof(UIViewId.Death), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.Main id) => GetViews(nameof(UIViewId.Main), id.ToString());
        public static void Show(UIViewId.Main id, bool instant = false) => Show(nameof(UIViewId.Main), id.ToString(), instant);
        public static void Hide(UIViewId.Main id, bool instant = false) => Hide(nameof(UIViewId.Main), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.Shop id) => GetViews(nameof(UIViewId.Shop), id.ToString());
        public static void Show(UIViewId.Shop id, bool instant = false) => Show(nameof(UIViewId.Shop), id.ToString(), instant);
        public static void Hide(UIViewId.Shop id, bool instant = false) => Hide(nameof(UIViewId.Shop), id.ToString(), instant);
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIViewId
    {
        public enum Death
        {
            DeathDisplay,
            SaveHighScore
        }

        public enum Main
        {
            InGame,
            MainMenu,
            Paused,
            Settings,
            TitleScreen
        }

        public enum Shop
        {
            DroneShop,
            ShipShop
        }    
    }
}