#if ANDROID || IOS || DESKTOP_GL
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
#define SUPPORTS_GLUEVIEW_2
using Color = Microsoft.Xna.Framework.Color;
using System.Linq;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlatRedBall;
using System;
using System.Collections.Generic;
using System.Text;
namespace ArtyParty.Entities
{
    public partial class PlayerTurret : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        protected static Microsoft.Xna.Framework.Graphics.Texture2D Tank_Weapon_Pixelator;
        
        private FlatRedBall.Sprite SpriteInstance1;
        private FlatRedBall.Math.Geometry.Polygon mTurret;
        public FlatRedBall.Math.Geometry.Polygon Turret
        {
            get
            {
                return mTurret;
            }
            private set
            {
                mTurret = value;
            }
        }
        public int Index { get; set; }
        public bool Used { get; set; }
        public string EditModeType { get; set; } = "ArtyParty.Entities.PlayerTurret";
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public PlayerTurret () 
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public PlayerTurret (string contentManagerName) 
        	: this(contentManagerName, true)
        {
        }
        public PlayerTurret (string contentManagerName, bool addToManagers) 
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            SpriteInstance1 = new FlatRedBall.Sprite();
            SpriteInstance1.Name = "SpriteInstance1";
            SpriteInstance1.CreationSource = "Glue";
            mTurret = new FlatRedBall.Math.Geometry.Polygon();
            mTurret.Name = "Turret";
            mTurret.CreationSource = "Glue";
            
            PostInitialize();
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }
        public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance1, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mTurret, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance1, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mTurret, LayerProvidedByContainer);
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity () 
        {
            
            CustomActivity();
        }
        public virtual void Destroy () 
        {
            var wasUsed = this.Used;
            if (Used)
            {
                Factories.PlayerTurretFactory.MakeUnused(this, false);
            }
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (SpriteInstance1 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance1);
            }
            if (Turret != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Turret);
            }
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.CopyAbsoluteToRelative();
                SpriteInstance1.AttachTo(this, false);
            }
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.X = 19f;
            }
            else
            {
                SpriteInstance1.RelativeX = 19f;
            }
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.Y = 1f;
            }
            else
            {
                SpriteInstance1.RelativeY = 1f;
            }
            SpriteInstance1.Texture = Tank_Weapon_Pixelator;
            SpriteInstance1.TextureScale = 0.6532284f;
            SpriteInstance1.UseAnimationRelativePosition = false;
            SpriteInstance1.Red = 0f;
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.RotationZ = 0f;
            }
            else
            {
                SpriteInstance1.RelativeRotationZ = 0f;
            }
            SpriteInstance1.ParentRotationChangesRotation = true;
            if (mTurret.Parent == null)
            {
                mTurret.CopyAbsoluteToRelative();
                mTurret.AttachTo(this, false);
            }
            if (Turret.Parent == null)
            {
                Turret.X = 0f;
            }
            else
            {
                Turret.RelativeX = 0f;
            }
            if (Turret.Parent == null)
            {
                Turret.Y = 0f;
            }
            else
            {
                Turret.RelativeY = 0f;
            }
            if (Turret.Parent == null)
            {
                Turret.RotationZ = 0f;
            }
            else
            {
                Turret.RelativeRotationZ = 0f;
            }
            Turret.Color = Microsoft.Xna.Framework.Color.Turquoise;
            Turret.IgnoreParentPosition = false;
            Turret.ParentRotationChangesPosition = true;
            Turret.ParentRotationChangesRotation = true;
            FlatRedBall.Math.Geometry.Point[] TurretPoints = new FlatRedBall.Math.Geometry.Point[] {new FlatRedBall.Math.Geometry.Point(0, 0), new FlatRedBall.Math.Geometry.Point(40, 0), new FlatRedBall.Math.Geometry.Point(40, 1.5), new FlatRedBall.Math.Geometry.Point(40, 3), new FlatRedBall.Math.Geometry.Point(0, 3), new FlatRedBall.Math.Geometry.Point(0, 0) };
            Turret.Points = TurretPoints;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (SpriteInstance1 != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance1);
            }
            if (Turret != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Turret);
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
            }
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.X = 19f;
            }
            else
            {
                SpriteInstance1.RelativeX = 19f;
            }
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.Y = 1f;
            }
            else
            {
                SpriteInstance1.RelativeY = 1f;
            }
            SpriteInstance1.Texture = Tank_Weapon_Pixelator;
            SpriteInstance1.TextureScale = 0.6532284f;
            SpriteInstance1.UseAnimationRelativePosition = false;
            SpriteInstance1.Red = 0f;
            if (SpriteInstance1.Parent == null)
            {
                SpriteInstance1.RotationZ = 0f;
            }
            else
            {
                SpriteInstance1.RelativeRotationZ = 0f;
            }
            SpriteInstance1.ParentRotationChangesRotation = true;
            if (Turret.Parent == null)
            {
                Turret.X = 0f;
            }
            else
            {
                Turret.RelativeX = 0f;
            }
            if (Turret.Parent == null)
            {
                Turret.Y = 0f;
            }
            else
            {
                Turret.RelativeY = 0f;
            }
            if (Turret.Parent == null)
            {
                Turret.RotationZ = 0f;
            }
            else
            {
                Turret.RelativeRotationZ = 0f;
            }
            Turret.Color = Microsoft.Xna.Framework.Color.Turquoise;
            Turret.IgnoreParentPosition = false;
            Turret.ParentRotationChangesPosition = true;
            Turret.ParentRotationChangesRotation = true;
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(SpriteInstance1);
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            ContentManagerName = contentManagerName;
            // Set the content manager for Gum
            var contentManagerWrapper = new FlatRedBall.Gum.ContentManagerWrapper();
            contentManagerWrapper.ContentManagerName = contentManagerName;
            RenderingLibrary.Content.LoaderManager.Self.ContentLoader = contentManagerWrapper;
            // Access the GumProject just in case it's async loaded
            var throwaway = GlobalContent.GumProject;
            #if DEBUG
            if (contentManagerName == FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                HasBeenLoadedWithGlobalContentManager = true;
            }
            else if (HasBeenLoadedWithGlobalContentManager)
            {
                throw new System.Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
            }
            #endif
            bool registerUnload = false;
            if (LoadedContentManagers.Contains(contentManagerName) == false)
            {
                LoadedContentManagers.Add(contentManagerName);
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerTurretStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/player/tank_weapon_pixelator.png", ContentManagerName))
                {
                    registerUnload = true;
                }
                Tank_Weapon_Pixelator = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/player/tank_weapon_pixelator.png", ContentManagerName);
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerTurretStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
            }
            CustomLoadStaticContent(contentManagerName);
        }
        public static void UnloadStaticContent () 
        {
            if (LoadedContentManagers.Count != 0)
            {
                LoadedContentManagers.RemoveAt(0);
                mRegisteredUnloads.RemoveAt(0);
            }
            if (LoadedContentManagers.Count == 0)
            {
                if (Tank_Weapon_Pixelator != null)
                {
                    Tank_Weapon_Pixelator= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Tank_Weapon_Pixelator":
                    return Tank_Weapon_Pixelator;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "Tank_Weapon_Pixelator":
                    return Tank_Weapon_Pixelator;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Tank_Weapon_Pixelator":
                    return Tank_Weapon_Pixelator;
            }
            return null;
        }
        public static void Reload (object whatToReload) 
        {
            if (whatToReload == Tank_Weapon_Pixelator)
            {
                var oldTexture = Tank_Weapon_Pixelator;
                {
                    var cm = FlatRedBall.FlatRedBallServices.GetContentManagerByName("Global");
                    cm.UnloadAsset(Tank_Weapon_Pixelator);
                    Tank_Weapon_Pixelator = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("content/entities/player/tank_weapon_pixelator.png");
                }
                FlatRedBall.SpriteManager.ReplaceTexture(oldTexture, Tank_Weapon_Pixelator);
            }
        }
        protected bool mIsPaused;
        public override void Pause (FlatRedBall.Instructions.InstructionList instructions) 
        {
            base.Pause(instructions);
            mIsPaused = true;
        }
        public virtual void SetToIgnorePausing () 
        {
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance1);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Turret);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo) 
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(SpriteInstance1);
            }
            if (layerToMoveTo != null || !SpriteManager.AutomaticallyUpdatedSprites.Contains(SpriteInstance1))
            {
                FlatRedBall.SpriteManager.AddToLayer(SpriteInstance1, layerToMoveTo);
            }
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(Turret);
            }
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Turret, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
        partial void CustomActivityEditMode();
    }
}
