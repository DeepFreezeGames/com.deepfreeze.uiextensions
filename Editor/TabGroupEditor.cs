using DeepFreeze.Packages.UIExtensions.Runtime;
using UnityEditor;

namespace DeepFreeze.Packages.UIExtensions.Editor
{
    [CustomEditor(typeof(TabGroup))]
    public class TabGroupEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}