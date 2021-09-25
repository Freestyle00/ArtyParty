#if ANDROID || IOS || DESKTOP_GL
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
#define SUPPORTS_GLUEVIEW_2
using Color = Microsoft.Xna.Framework.Color;
using System.Linq;
using FlatRedBall;
using System;
using System.Collections.Generic;
using System.Text;
namespace ArtyParty.Screens
{
    public partial class GameScreen : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        protected static ArtyParty.GumRuntimes.GameScreenGumRuntime GameScreenGum;
        
        protected FlatRedBall.TileGraphics.LayeredTileMap Map;
        protected FlatRedBall.TileCollisions.TileShapeCollection SolidCollision;
        private FlatRedBall.Entities.CameraControllingEntity CameraControllingEntityInstance;
        private ArtyParty.Entities.Player PlayerInstance;
        private FlatRedBall.Math.Collision.DelegateCollisionRelationship<ArtyParty.Entities.Player, FlatRedBall.TileCollisions.TileShapeCollection> PlayerInstanceVsSolidCollision;
        protected FlatRedBall.Math.PositionedObjectList<ArtyParty.Entities.Projectiles.Armor_Piercing_round> Armor_Piercing_roundList;
        private FlatRedBall.Math.Collision.PositionedObjectVsListRelationship<ArtyParty.Entities.Player, Entities.Projectiles.Armor_Piercing_round> PlayerInstanceVsArmor_Piercing_roundList;
        public event System.Action<ArtyParty.Entities.Player, Entities.Projectiles.Armor_Piercing_round> PlayerInstanceVsArmor_Piercing_roundListCollisionOccurred;
        ArtyParty.FormsControls.Screens.GameScreenGumForms Forms;
        ArtyParty.GumRuntimes.GameScreenGumRuntime GumScreen;
        public GameScreen () 
        	: base ("GameScreen")
        {
            // Not instantiating for FlatRedBall.TileGraphics.LayeredTileMap Map in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection SolidCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            Armor_Piercing_roundList = new FlatRedBall.Math.PositionedObjectList<ArtyParty.Entities.Projectiles.Armor_Piercing_round>();
            Armor_Piercing_roundList.Name = "Armor_Piercing_roundList";
        }
        public override void Initialize (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            // Not instantiating for FlatRedBall.TileGraphics.LayeredTileMap Map in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection SolidCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            CameraControllingEntityInstance = new FlatRedBall.Entities.CameraControllingEntity();
            CameraControllingEntityInstance.Name = "CameraControllingEntityInstance";
            CameraControllingEntityInstance.CreationSource = "Glue";
            PlayerInstance = new ArtyParty.Entities.Player(ContentManagerName, false);
            PlayerInstance.Name = "PlayerInstance";
            PlayerInstance.CreationSource = "Glue";
            Armor_Piercing_roundList.Clear();
                {
        var temp = new FlatRedBall.Math.Collision.DelegateCollisionRelationship<ArtyParty.Entities.Player, FlatRedBall.TileCollisions.TileShapeCollection>(PlayerInstance, SolidCollision);
        var isCloud = false;
        temp.CollisionFunction = (first, second) =>
        {
            return first.CollideAgainst(second, isCloud);
        }
        ;
        FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Add(temp);
        PlayerInstanceVsSolidCollision = temp;
    }
    PlayerInstanceVsSolidCollision.Name = "PlayerInstanceVsSolidCollision";

                PlayerInstanceVsArmor_Piercing_roundList = FlatRedBall.Math.Collision.CollisionManager.Self.CreateRelationship(PlayerInstance, Armor_Piercing_roundList);
    PlayerInstanceVsArmor_Piercing_roundList.Name = "PlayerInstanceVsArmor_Piercing_roundList";

            Forms = new ArtyParty.FormsControls.Screens.GameScreenGumForms(GameScreenGum);
            GumScreen = GameScreenGum;
            FillCollisionForSolidCollision();
            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers () 
        {
            GameScreenGum.AddToManagers();FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged += RefreshLayoutInternal;
            Factories.Armor_Piercing_roundFactory.Initialize(ContentManagerName);
            Factories.Armor_Piercing_roundFactory.AddList(Armor_Piercing_roundList);
            FlatRedBall.SpriteManager.AddPositionedObject(CameraControllingEntityInstance); CameraControllingEntityInstance.Activity();
            PlayerInstance.AddToManagers(mLayer);
            FlatRedBall.TileEntities.TileEntityInstantiator.CreateEntitiesFrom(Map);
            base.AddToManagers();
            AddToManagersBottomUp();
            BeforeCustomInitialize?.Invoke();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled) 
        {
            if (!IsPaused)
            {
                
                CameraControllingEntityInstance.Activity();
                PlayerInstance.Activity();
                for (int i = Armor_Piercing_roundList.Count - 1; i > -1; i--)
                {
                    if (i < Armor_Piercing_roundList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        Armor_Piercing_roundList[i].Activity();
                    }
                }
            }
            else
            {
            }
            base.Activity(firstTimeCalled);
            if (!IsActivityFinished)
            {
                CustomActivity(firstTimeCalled);
            }
        }
        public override void Destroy () 
        {
            base.Destroy();
            Factories.Armor_Piercing_roundFactory.Destroy();
            GameScreenGum.RemoveFromManagers();FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged -= RefreshLayoutInternal;
            GameScreenGum = null;
            
            Armor_Piercing_roundList.MakeOneWay();
            if (CameraControllingEntityInstance != null)
            {
                FlatRedBall.SpriteManager.RemovePositionedObject(CameraControllingEntityInstance);;
            }
            if (PlayerInstance != null)
            {
                PlayerInstance.Destroy();
                PlayerInstance.Detach();
            }
            for (int i = Armor_Piercing_roundList.Count - 1; i > -1; i--)
            {
                Armor_Piercing_roundList[i].Destroy();
            }
            Armor_Piercing_roundList.MakeTwoWay();
            FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Clear();
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            PlayerInstanceVsArmor_Piercing_roundList.CollisionOccurred += OnPlayerInstanceVsArmor_Piercing_roundListCollisionOccurred;
            PlayerInstanceVsArmor_Piercing_roundList.CollisionOccurred += OnPlayerInstanceVsArmor_Piercing_roundListCollisionOccurredTunnel;
            if (Map!= null)
            {
            }
            if (SolidCollision!= null)
            {
            }
            CameraControllingEntityInstance.Map = Map;
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.X = 56f;
            }
            else
            {
                PlayerInstance.RelativeX = 56f;
            }
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.Y = -486f;
            }
            else
            {
                PlayerInstance.RelativeY = -486f;
            }
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp () 
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            GameScreenGum.RemoveFromManagers();FlatRedBall.FlatRedBallServices.GraphicsOptions.SizeOrOrientationChanged -= RefreshLayoutInternal;
            if (CameraControllingEntityInstance != null)
            {
                FlatRedBall.SpriteManager.RemovePositionedObject(CameraControllingEntityInstance);;
            }
            PlayerInstance.RemoveFromManagers();
            for (int i = Armor_Piercing_roundList.Count - 1; i > -1; i--)
            {
                Armor_Piercing_roundList[i].Destroy();
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                PlayerInstance.AssignCustomVariables(true);
            }
            if (Map != null)
            {
            }
            if (SolidCollision != null)
            {
            }
            CameraControllingEntityInstance.Map = Map;
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.X = 56f;
            }
            else
            {
                PlayerInstance.RelativeX = 56f;
            }
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.Y = -486f;
            }
            else
            {
                PlayerInstance.RelativeY = -486f;
            }
        }
        public virtual void ConvertToManuallyUpdated () 
        {
            if (Map != null)
            {
            }
            if (SolidCollision != null)
            {
            }
            PlayerInstance.ConvertToManuallyUpdated();
            for (int i = 0; i < Armor_Piercing_roundList.Count; i++)
            {
                Armor_Piercing_roundList[i].ConvertToManuallyUpdated();
            }
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
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
            if(GameScreenGum == null) GameScreenGum = (ArtyParty.GumRuntimes.GameScreenGumRuntime)GumRuntime.ElementSaveExtensions.CreateGueForElement(Gum.Managers.ObjectFinder.Self.GetScreen("GameScreenGum"), true);
            ArtyParty.Entities.Player.LoadStaticContent(contentManagerName);
            CustomLoadStaticContent(contentManagerName);
        }
        public override void PauseThisScreen () 
        {
            StateInterpolationPlugin.TweenerManager.Self.Pause();
            base.PauseThisScreen();
        }
        public override void UnpauseThisScreen () 
        {
            StateInterpolationPlugin.TweenerManager.Self.Unpause();
            base.UnpauseThisScreen();
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
        public static object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "GameScreenGum":
                    return GameScreenGum;
            }
            return null;
        }
        public static void Reload (object whatToReload) 
        {
        }
        private void RefreshLayoutInternal (object sender, EventArgs e) 
        {
            GameScreenGum.UpdateLayout();
        }
        protected virtual void FillCollisionForSolidCollision () 
        {
            if (SolidCollision != null)
            {
                // normally we wait to set variables until after the object is created, but in this case if the
                // TileShapeCollection doesn't have its Visible set before creating the tiles, it can result in
                // really bad performance issues, as shapes will be made visible, then invisible. Really bad perf!
                SolidCollision.Visible = false;
                FlatRedBall.TileCollisions.TileShapeCollectionLayeredTileMapExtensions.AddCollisionFromTilesWithType(SolidCollision, Map, "SolidCollision", false);
            }
        }
        partial void CustomActivityEditMode();
    }
}
