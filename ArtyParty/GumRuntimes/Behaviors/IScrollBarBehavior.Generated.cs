    namespace ArtyParty.GumRuntimes
    {
        #region State Enums
        public enum ScrollBarBehaviorScrollBarCategory
        {
        }
        #endregion
        public interface IScrollBarBehavior
        {
            ScrollBarBehaviorScrollBarCategory CurrentScrollBarBehaviorScrollBarCategoryState {set;}
        }
    }
