    namespace ArtyParty.FormsControls.Screens
    {
        public partial class GameScreenGumForms
        {
            private Gum.Wireframe.GraphicalUiElement Visual;
            public GameScreenGumForms () 
            {
                CustomInitialize();
            }
            public GameScreenGumForms (Gum.Wireframe.GraphicalUiElement visual) 
            {
                Visual = visual;
                ReactToVisualChanged();
                CustomInitialize();
            }
            private void ReactToVisualChanged () 
            {
            }
            partial void CustomInitialize();
        }
    }
