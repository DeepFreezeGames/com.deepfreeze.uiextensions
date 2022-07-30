using DeepFreeze.Packages.UIExtensions.Runtime;
using UnityEngine.UI;

namespace DeepFreeze.Packages.UIExtensions.Runtime
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public AnchorPosition startingPosition;
        public AlignmentMode horizontalAlignment;
        public AlignmentMode verticalAlignment;
        
        public override void CalculateLayoutInputVertical()
        {
            throw new System.NotImplementedException();
        }

        public override void SetLayoutHorizontal()
        {
            throw new System.NotImplementedException();
        }

        public override void SetLayoutVertical()
        {
            throw new System.NotImplementedException();
        }
    }
}