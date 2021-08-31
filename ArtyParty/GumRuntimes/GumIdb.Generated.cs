    namespace FlatRedBall.Gum
    {
        public  class GumIdbExtensions
        {
            public static void RegisterTypes () 
            {
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Circle", typeof(ArtyParty.GumRuntimes.CircleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("ColoredRectangle", typeof(ArtyParty.GumRuntimes.ColoredRectangleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Container", typeof(ArtyParty.GumRuntimes.ContainerRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Container<T>", typeof(ArtyParty.GumRuntimes.ContainerRuntime<>));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("NineSlice", typeof(ArtyParty.GumRuntimes.NineSliceRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Polygon", typeof(ArtyParty.GumRuntimes.PolygonRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Rectangle", typeof(ArtyParty.GumRuntimes.RectangleRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Sprite", typeof(ArtyParty.GumRuntimes.SpriteRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Text", typeof(ArtyParty.GumRuntimes.TextRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/Button", typeof(ArtyParty.GumRuntimes.DefaultForms.ButtonRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/CheckBox", typeof(ArtyParty.GumRuntimes.DefaultForms.CheckBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ColoredFrame", typeof(ArtyParty.GumRuntimes.DefaultForms.ColoredFrameRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ComboBox", typeof(ArtyParty.GumRuntimes.DefaultForms.ComboBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/DialogBox", typeof(ArtyParty.GumRuntimes.DefaultForms.DialogBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/Keyboard", typeof(ArtyParty.GumRuntimes.DefaultForms.KeyboardRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/KeyboardKey", typeof(ArtyParty.GumRuntimes.DefaultForms.KeyboardKeyRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/Label", typeof(ArtyParty.GumRuntimes.DefaultForms.LabelRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ListBox", typeof(ArtyParty.GumRuntimes.DefaultForms.ListBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ListBoxItem", typeof(ArtyParty.GumRuntimes.DefaultForms.ListBoxItemRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/PasswordBox", typeof(ArtyParty.GumRuntimes.DefaultForms.PasswordBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/RadioButton", typeof(ArtyParty.GumRuntimes.DefaultForms.RadioButtonRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ScrollBar", typeof(ArtyParty.GumRuntimes.DefaultForms.ScrollBarRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ScrollBarThumb", typeof(ArtyParty.GumRuntimes.DefaultForms.ScrollBarThumbRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ScrollViewer", typeof(ArtyParty.GumRuntimes.DefaultForms.ScrollViewerRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/Slider", typeof(ArtyParty.GumRuntimes.DefaultForms.SliderRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/TextBox", typeof(ArtyParty.GumRuntimes.DefaultForms.TextBoxRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/Toast", typeof(ArtyParty.GumRuntimes.DefaultForms.ToastRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/ToggleButton", typeof(ArtyParty.GumRuntimes.DefaultForms.ToggleButtonRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/TreeView", typeof(ArtyParty.GumRuntimes.DefaultForms.TreeViewRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/TreeViewItem", typeof(ArtyParty.GumRuntimes.DefaultForms.TreeViewItemRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/TreeViewToggleButton", typeof(ArtyParty.GumRuntimes.DefaultForms.TreeViewToggleButtonRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DefaultForms/UserControl", typeof(ArtyParty.GumRuntimes.DefaultForms.UserControlRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("GameScreenGum", typeof(ArtyParty.GumRuntimes.GameScreenGumRuntime));
                GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Level1Gum", typeof(ArtyParty.GumRuntimes.Level1GumRuntime));
                
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Button)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ButtonRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.CheckBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.CheckBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ComboBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ComboBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Games.DialogBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.DialogBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Games.OnScreenKeyboard)] = typeof(ArtyParty.GumRuntimes.DefaultForms.KeyboardRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Label)] = typeof(ArtyParty.GumRuntimes.DefaultForms.LabelRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ListBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ListBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ListBoxItem)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ListBoxItemRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.PasswordBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.PasswordBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.RadioButton)] = typeof(ArtyParty.GumRuntimes.DefaultForms.RadioButtonRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ScrollBar)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ScrollBarRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ScrollViewer)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ScrollViewerRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Slider)] = typeof(ArtyParty.GumRuntimes.DefaultForms.SliderRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.TextBox)] = typeof(ArtyParty.GumRuntimes.DefaultForms.TextBoxRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.Popups.Toast)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ToastRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.ToggleButton)] = typeof(ArtyParty.GumRuntimes.DefaultForms.ToggleButtonRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.TreeView)] = typeof(ArtyParty.GumRuntimes.DefaultForms.TreeViewRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.TreeViewItem)] = typeof(ArtyParty.GumRuntimes.DefaultForms.TreeViewItemRuntime);
                FlatRedBall.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(FlatRedBall.Forms.Controls.UserControl)] = typeof(ArtyParty.GumRuntimes.DefaultForms.UserControlRuntime);
            }
        }
    }
