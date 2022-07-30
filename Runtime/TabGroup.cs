using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepFreeze.Packages.UIExtensions.Runtime
{
    public class TabGroup : MonoBehaviour
    {
        public List<Tab> tabs = new List<Tab>();
        public Tab currentSelectedTab;

        [Header("Overrides")] 
        public bool overrideIdleState;
        public Color overrideIdleColor;
        public Sprite overrideIdleSprite;

        public bool overrideHighlightState;
        public Color overrideHighlightColor;
        public Sprite overrideHighlightSprite;

        public bool overrideActiveState;
        public Color overrideActiveColor;
        public Sprite overrideActiveSprite;
        
        //Events
        public Action<Tab> onTabEnter;
        public Action<Tab> onTabExit;
        public Action<Tab> onTabSelect;

        public void AddTab(Tab tab)
        {
            if (tabs.Contains(tab))
            {
                return;
            }
            
            tabs.Add(tab);
        }

        public void RemoveTab(Tab tab)
        {
            if (tabs.Contains(tab))
            {
                tabs.Remove(tab);
            }
        }

        public void TabEnter(Tab tab)
        {
            onTabEnter?.Invoke(tab);

            if (overrideHighlightState)
            {
                
            }
        }

        public void TabExit(Tab tab)
        {
            onTabExit?.Invoke(tab);
        }

        public void TabSelected(Tab tab)
        {
            onTabSelect?.Invoke(tab);
        }
    }
}
