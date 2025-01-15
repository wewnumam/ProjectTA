﻿// Cristian Pop - https://boxophobic.com/

using System;
using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledColoringDrawer : MaterialPropertyDrawer
    {
        public StyledColoringDrawer()
        {

        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materialEditor)
        {
            GUI.color = prop.colorValue;
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}