using System;
using UnityEditor;

namespace DeepFreeze.Packages.UIExtensions.Editor
{
    public class HandlesExtensions
    {
        public class HandlesBlock : IDisposable
        {
            public HandlesBlock()
            {
                Handles.BeginGUI();
            }
        
            public void Dispose()
            {
                Handles.EndGUI();
            }
        }
    }
}
