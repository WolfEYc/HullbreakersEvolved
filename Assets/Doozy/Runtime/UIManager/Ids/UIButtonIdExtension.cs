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
namespace Doozy.Runtime.UIManager.Components
{
    public partial class UIButton
    {
        public static IEnumerable<UIButton> GetButtons(UIButtonId.Difficulty id) => GetButtons(nameof(UIButtonId.Difficulty), id.ToString());
        public static bool SelectButton(UIButtonId.Difficulty id) => SelectButton(nameof(UIButtonId.Difficulty), id.ToString());

        public static IEnumerable<UIButton> GetButtons(UIButtonId.UINav id) => GetButtons(nameof(UIButtonId.UINav), id.ToString());
        public static bool SelectButton(UIButtonId.UINav id) => SelectButton(nameof(UIButtonId.UINav), id.ToString());
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIButtonId
    {
        public enum Difficulty
        {
            Easy,
            Hard,
            Legendary,
            Normal
        }

        public enum UINav
        {
            Exit,
            Next,
            SaveHighScore,
            Settings
        }    
    }
}