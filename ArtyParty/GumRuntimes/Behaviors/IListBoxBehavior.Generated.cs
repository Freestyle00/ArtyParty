    namespace ArtyParty.GumRuntimes
    {
        #region State Enums
        public enum ListBoxBehaviorListBoxCategory
        {
            Enabled,
            Disabled,
            Focused
        }
        #endregion
        public interface IListBoxBehavior
        {
            ListBoxBehaviorListBoxCategory CurrentListBoxBehaviorListBoxCategoryState {set;}
        }
    }
