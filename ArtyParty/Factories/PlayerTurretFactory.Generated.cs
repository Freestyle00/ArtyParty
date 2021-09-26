using ArtyParty.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using ArtyParty.Performance;

namespace ArtyParty.Factories
{
    public class PlayerTurretFactory : IEntityFactory
    {
        public static FlatRedBall.Math.Axis? SortAxis { get; set;}
        public static PlayerTurret CreateNew (float x = 0, float y = 0, float z = 0) 
        {
            return CreateNew(null, x, y, z);
        }
        public static PlayerTurret CreateNew (Microsoft.Xna.Framework.Vector3 position) 
        {
            return CreateNew(null, position.X, position.Y, position.Z);
        }
        public static PlayerTurret CreateNew (Layer layer, float x = 0, float y = 0, float z = 0) 
        {
            PlayerTurret instance = null;
            instance = new PlayerTurret(mContentManagerName ?? FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, false);
            instance.AddToManagers(layer);
            instance.X = x;
            instance.Y = y;
            instance.Z = z;
            foreach (var list in ListsToAddTo)
            {
                if (SortAxis == FlatRedBall.Math.Axis.X && list is PositionedObjectList<PlayerTurret>)
                {
                    var index = (list as PositionedObjectList<PlayerTurret>).GetFirstAfter(x, Axis.X, 0, list.Count);
                    list.Insert(index, instance);
                }
                else if (SortAxis == FlatRedBall.Math.Axis.Y && list is PositionedObjectList<PlayerTurret>)
                {
                    var index = (list as PositionedObjectList<PlayerTurret>).GetFirstAfter(y, Axis.Y, 0, list.Count);
                    list.Insert(index, instance);
                }
                else
                {
                    // Sort Z not supported
                    list.Add(instance);
                }
            }
            if (EntitySpawned != null)
            {
                EntitySpawned(instance);
            }
            return instance;
        }
        
        public static void Initialize (string contentManager) 
        {
            mContentManagerName = contentManager;
        }
        
        public static void Destroy () 
        {
            mContentManagerName = null;
            ListsToAddTo.Clear();
            SortAxis = null;
            mPool.Clear();
            EntitySpawned = null;
        }
        
        private static void FactoryInitialize () 
        {
            const int numberToPreAllocate = 20;
            for (int i = 0; i < numberToPreAllocate; i++)
            {
                PlayerTurret instance = new PlayerTurret(mContentManagerName, false);
                mPool.AddToPool(instance);
            }
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (PlayerTurret objectToMakeUnused) 
        {
            MakeUnused(objectToMakeUnused, true);
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (PlayerTurret objectToMakeUnused, bool callDestroy) 
        {
            if (callDestroy)
            {
                objectToMakeUnused.Destroy();
            }
        }
        
        public static void AddList<T> (System.Collections.Generic.IList<T> newList) where T : PlayerTurret
        {
            ListsToAddTo.Add(newList as System.Collections.IList);
        }
        public static void RemoveList<T> (System.Collections.Generic.IList<T> listToRemove) where T : PlayerTurret
        {
            ListsToAddTo.Remove(listToRemove as System.Collections.IList);
        }
        public static void ClearListsToAddTo () 
        {
            ListsToAddTo.Clear();
        }
        
        
            static string mContentManagerName;
            static System.Collections.Generic.List<System.Collections.IList> ListsToAddTo = new System.Collections.Generic.List<System.Collections.IList>();
            static PoolList<PlayerTurret> mPool = new PoolList<PlayerTurret>();
            public static Action<PlayerTurret> EntitySpawned;
            object IEntityFactory.CreateNew (float x = 0, float y = 0) 
            {
                return PlayerTurretFactory.CreateNew(x, y);
            }
            object IEntityFactory.CreateNew (Microsoft.Xna.Framework.Vector3 position) 
            {
                return PlayerTurretFactory.CreateNew(position);
            }
            object IEntityFactory.CreateNew (Layer layer) 
            {
                return PlayerTurretFactory.CreateNew(layer);
            }
            void IEntityFactory.Initialize (string contentManagerName) 
            {
                PlayerTurretFactory.Initialize(contentManagerName);
            }
            void IEntityFactory.ClearListsToAddTo () 
            {
                PlayerTurretFactory.ClearListsToAddTo();
            }
            System.Collections.Generic.List<System.Collections.IList> IEntityFactory.ListsToAddTo => PlayerTurretFactory.ListsToAddTo;
            static PlayerTurretFactory mSelf;
            public static PlayerTurretFactory Self
            {
                get
                {
                    if (mSelf == null)
                    {
                        mSelf = new PlayerTurretFactory();
                    }
                    return mSelf;
                }
            }
    }
}
