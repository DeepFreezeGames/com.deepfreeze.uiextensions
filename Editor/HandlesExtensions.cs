using System;
using UnityEditor;

namespace UIExtensions.Editor
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
