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
using ArtyParty.DataTypes;
using FlatRedBall.IO.Csv;
namespace ArtyParty.Entities.Projectiles
{
    public partial class Armor_Piercing_round : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable, FlatRedBall.Math.Geometry.ICollidable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        public static System.Collections.Generic.Dictionary<System.String, ArtyParty.DataTypes.PlatformerValues> PlatformerValuesStatic;
        protected static Microsoft.Xna.Framework.Graphics.Texture2D Proyectile_2_pixelart;
        
        private FlatRedBall.Sprite SpriteInstance;
        private FlatRedBall.Math.Geometry.Circle mCircleInstance;
        public FlatRedBall.Math.Geometry.Circle CircleInstance
        {
            get
            {
                return mCircleInstance;
            }
            private set
            {
                mCircleInstance = value;
            }
        }
        public event Action<ArtyParty.DataTypes.PlatformerValues> BeforeGroundMovementSet;
        public event System.EventHandler AfterGroundMovementSet;
        private ArtyParty.DataTypes.PlatformerValues mGroundMovement;
        public virtual ArtyParty.DataTypes.PlatformerValues GroundMovement
        {
            set
            {
                if (BeforeGroundMovementSet != null)
                {
                    BeforeGroundMovementSet(value);
                }
                mGroundMovement = value;
                if (AfterGroundMovementSet != null)
                {
                    AfterGroundMovementSet(this, null);
                }
            }
            get
            {
                return mGroundMovement;
            }
        }
        public event Action<ArtyParty.DataTypes.PlatformerValues> BeforeAirMovementSet;
        public event System.EventHandler AfterAirMovementSet;
        private ArtyParty.DataTypes.PlatformerValues mAirMovement;
        public virtual ArtyParty.DataTypes.PlatformerValues AirMovement
        {
            set
            {
                if (BeforeAirMovementSet != null)
                {
                    BeforeAirMovementSet(value);
                }
                mAirMovement = value;
                if (AfterAirMovementSet != null)
                {
                    AfterAirMovementSet(this, null);
                }
            }
            get
            {
                return mAirMovement;
            }
        }
        public event Action<ArtyParty.DataTypes.PlatformerValues> BeforeAfterDoubleJumpSet;
        public event System.EventHandler AfterAfterDoubleJumpSet;
        private ArtyParty.DataTypes.PlatformerValues mAfterDoubleJump;
        public virtual ArtyParty.DataTypes.PlatformerValues AfterDoubleJump
        {
            set
            {
                if (BeforeAfterDoubleJumpSet != null)
                {
                    BeforeAfterDoubleJumpSet(value);
                }
                mAfterDoubleJump = value;
                if (AfterAfterDoubleJumpSet != null)
                {
                    AfterAfterDoubleJumpSet(this, null);
                }
            }
            get
            {
                return mAfterDoubleJump;
            }
        }
        public int Index { get; set; }
        public bool Used { get; set; }
        private FlatRedBall.Math.Geometry.ShapeCollection mGeneratedCollision;
        public FlatRedBall.Math.Geometry.ShapeCollection Collision
        {
            get
            {
                return mGeneratedCollision;
            }
        }
        public string EditModeType { get; set; } = "ArtyParty.Entities.Projectiles.Armor_Piercing_round";
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public Armor_Piercing_round () 
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Armor_Piercing_round (string contentManagerName) 
        	: this(contentManagerName, true)
        {
        }
        public Armor_Piercing_round (string contentManagerName, bool addToManagers) 
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            SpriteInstance = new FlatRedBall.Sprite();
            SpriteInstance.Name = "SpriteInstance";
            SpriteInstance.CreationSource = "Glue";
            mCircleInstance = new FlatRedBall.Math.Geometry.Circle();
            mCircleInstance.Name = "CircleInstance";
            mCircleInstance.CreationSource = "Glue";
            
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
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
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
                Factories.Armor_Piercing_roundFactory.MakeUnused(this, false);
            }
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (CircleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(CircleInstance);
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.CopyAbsoluteToRelative();
                SpriteInstance.AttachTo(this, false);
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.X = 0f;
            }
            else
            {
                SpriteInstance.RelativeX = 0f;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Y = 0f;
            }
            else
            {
                SpriteInstance.RelativeY = 0f;
            }
            SpriteInstance.Texture = Proyectile_2_pixelart;
            SpriteInstance.TextureScale = 0.30936873f;
            SpriteInstance.Width = 16f;
            SpriteInstance.Height = 16f;
            if (mCircleInstance.Parent == null)
            {
                mCircleInstance.CopyAbsoluteToRelative();
                mCircleInstance.AttachTo(this, false);
            }
            CircleInstance.Radius = 4.000001f;
            mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
            Collision.Circles.AddOneWay(mCircleInstance);
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (CircleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(CircleInstance);
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.X = 0f;
            }
            else
            {
                SpriteInstance.RelativeX = 0f;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Y = 0f;
            }
            else
            {
                SpriteInstance.RelativeY = 0f;
            }
            SpriteInstance.Texture = Proyectile_2_pixelart;
            SpriteInstance.TextureScale = 0.30936873f;
            SpriteInstance.Width = 16f;
            SpriteInstance.Height = 16f;
            CircleInstance.Radius = 4.000001f;
            GroundMovement = Entities.Projectiles.Armor_Piercing_round.PlatformerValuesStatic["Ground"];
            AirMovement = Entities.Projectiles.Armor_Piercing_round.PlatformerValuesStatic["Air"];
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
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
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("Armor_Piercing_roundStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (PlatformerValuesStatic == null)
                {
                    {
                        // We put the { and } to limit the scope of oldDelimiter
                        char oldDelimiter = FlatRedBall.IO.Csv.CsvFileManager.Delimiter;
                        FlatRedBall.IO.Csv.CsvFileManager.Delimiter = ',';
                        System.Collections.Generic.Dictionary<System.String, ArtyParty.DataTypes.PlatformerValues> temporaryCsvObject = new System.Collections.Generic.Dictionary<System.String, ArtyParty.DataTypes.PlatformerValues>();
                        FlatRedBall.IO.Csv.CsvFileManager.CsvDeserializeDictionary<System.String, ArtyParty.DataTypes.PlatformerValues>("content/entities/projectiles/armor_piercing_round/platformervaluesstatic.csv", temporaryCsvObject, FlatRedBall.IO.Csv.DuplicateDictionaryEntryBehavior.Replace);
                        FlatRedBall.IO.Csv.CsvFileManager.Delimiter = oldDelimiter;
                        PlatformerValuesStatic = temporaryCsvObject;
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/projectiles/armor_piercing_round/proyectile_2_pixelart.png", ContentManagerName))
                {
                    registerUnload = true;
                }
                Proyectile_2_pixelart = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/projectiles/armor_piercing_round/proyectile_2_pixelart.png", ContentManagerName);
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("Armor_Piercing_roundStaticUnload", UnloadStaticContent);
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
                if (PlatformerValuesStatic != null)
                {
                    PlatformerValuesStatic= null;
                }
                if (Proyectile_2_pixelart != null)
                {
                    Proyectile_2_pixelart= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "PlatformerValuesStatic":
                    return PlatformerValuesStatic;
                case  "Proyectile_2_pixelart":
                    return Proyectile_2_pixelart;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "PlatformerValuesStatic":
                    return PlatformerValuesStatic;
                case  "Proyectile_2_pixelart":
                    return Proyectile_2_pixelart;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Proyectile_2_pixelart":
                    return Proyectile_2_pixelart;
            }
            return null;
        }
        public static void Reload (object whatToReload) 
        {
            if (whatToReload == PlatformerValuesStatic)
            {
                FlatRedBall.IO.Csv.CsvFileManager.UpdateDictionaryValuesFromCsv(PlatformerValuesStatic, "content/entities/projectiles/armor_piercing_round/platformervaluesstatic.csv");
            }
            if (whatToReload == Proyectile_2_pixelart)
            {
                var oldTexture = Proyectile_2_pixelart;
                {
                    var cm = FlatRedBall.FlatRedBallServices.GetContentManagerByName("Global");
                    cm.UnloadAsset(Proyectile_2_pixelart);
                    Proyectile_2_pixelart = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("content/entities/projectiles/armor_piercing_round/proyectile_2_pixelart.png");
                }
                FlatRedBall.SpriteManager.ReplaceTexture(oldTexture, Proyectile_2_pixelart);
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
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(CircleInstance);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo) 
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(SpriteInstance);
            }
            if (layerToMoveTo != null || !SpriteManager.AutomaticallyUpdatedSprites.Contains(SpriteInstance))
            {
                FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
            }
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(CircleInstance);
            }
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(CircleInstance, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
        partial void CustomActivityEditMode();
    }
}
