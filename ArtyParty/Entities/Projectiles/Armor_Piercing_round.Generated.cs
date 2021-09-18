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
        
        private FlatRedBall.Sprite SpriteInstance;
        private FlatRedBall.Math.Geometry.Polygon mPolygonInstance;
        public FlatRedBall.Math.Geometry.Polygon PolygonInstance
        {
            get
            {
                return mPolygonInstance;
            }
            private set
            {
                mPolygonInstance = value;
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
            mPolygonInstance = new FlatRedBall.Math.Geometry.Polygon();
            mPolygonInstance.Name = "PolygonInstance";
            mPolygonInstance.CreationSource = "Glue";
            
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
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mPolygonInstance, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo) 
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mPolygonInstance, LayerProvidedByContainer);
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
            if (PolygonInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(PolygonInstance);
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
            SpriteInstance.TextureScale = 1f;
            if (mPolygonInstance.Parent == null)
            {
                mPolygonInstance.CopyAbsoluteToRelative();
                mPolygonInstance.AttachTo(this, false);
            }
            if (PolygonInstance.Parent == null)
            {
                PolygonInstance.X = 0f;
            }
            else
            {
                PolygonInstance.RelativeX = 0f;
            }
            if (PolygonInstance.Parent == null)
            {
                PolygonInstance.Y = 0f;
            }
            else
            {
                PolygonInstance.RelativeY = 0f;
            }
            FlatRedBall.Math.Geometry.Point[] PolygonInstancePoints = new FlatRedBall.Math.Geometry.Point[] {new FlatRedBall.Math.Geometry.Point(0, 0), new FlatRedBall.Math.Geometry.Point(2, 0), new FlatRedBall.Math.Geometry.Point(2, 3), new FlatRedBall.Math.Geometry.Point(1, 5), new FlatRedBall.Math.Geometry.Point(0, 3), new FlatRedBall.Math.Geometry.Point(0, 0) };
            PolygonInstance.Points = PolygonInstancePoints;
            mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
            Collision.Polygons.AddOneWay(mPolygonInstance);
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
            if (PolygonInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(PolygonInstance);
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
            }
            SpriteInstance.TextureScale = 1f;
            if (PolygonInstance.Parent == null)
            {
                PolygonInstance.X = 0f;
            }
            else
            {
                PolygonInstance.RelativeX = 0f;
            }
            if (PolygonInstance.Parent == null)
            {
                PolygonInstance.Y = 0f;
            }
            else
            {
                PolygonInstance.RelativeY = 0f;
            }
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
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "PlatformerValuesStatic":
                    return PlatformerValuesStatic;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "PlatformerValuesStatic":
                    return PlatformerValuesStatic;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            return null;
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
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(PolygonInstance);
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
                layerToRemoveFrom.Remove(PolygonInstance);
            }
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(PolygonInstance, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
