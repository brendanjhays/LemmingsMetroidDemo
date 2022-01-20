using LemmingsMetroid.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using LemmingsMetroid.Performance;

namespace LemmingsMetroid.Factories
{
    public class MainCharacterFactory : IEntityFactory
    {
        public static FlatRedBall.Math.Axis? SortAxis { get; set;}
        public static MainCharacter CreateNew (float x = 0, float y = 0, float z = 0) 
        {
            return CreateNew(null, x, y, z);
        }
        public static MainCharacter CreateNew (Microsoft.Xna.Framework.Vector3 position) 
        {
            return CreateNew(null, position.X, position.Y, position.Z);
        }
        public static MainCharacter CreateNew (Layer layer, float x = 0, float y = 0, float z = 0) 
        {
            MainCharacter instance = null;
            instance = new MainCharacter(mContentManagerName ?? FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, false);
            instance.AddToManagers(layer);
            instance.X = x;
            instance.Y = y;
            instance.Z = z;
            foreach (var list in ListsToAddTo)
            {
                if (SortAxis == FlatRedBall.Math.Axis.X && list is PositionedObjectList<MainCharacter>)
                {
                    var index = (list as PositionedObjectList<MainCharacter>).GetFirstAfter(x, Axis.X, 0, list.Count);
                    list.Insert(index, instance);
                }
                else if (SortAxis == FlatRedBall.Math.Axis.Y && list is PositionedObjectList<MainCharacter>)
                {
                    var index = (list as PositionedObjectList<MainCharacter>).GetFirstAfter(y, Axis.Y, 0, list.Count);
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
                MainCharacter instance = new MainCharacter(mContentManagerName, false);
                mPool.AddToPool(instance);
            }
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (MainCharacter objectToMakeUnused) 
        {
            MakeUnused(objectToMakeUnused, true);
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (MainCharacter objectToMakeUnused, bool callDestroy) 
        {
            if (callDestroy)
            {
                objectToMakeUnused.Destroy();
            }
        }
        
        public static void AddList<T> (System.Collections.Generic.IList<T> newList) where T : MainCharacter
        {
            ListsToAddTo.Add(newList as System.Collections.IList);
        }
        public static void RemoveList<T> (System.Collections.Generic.IList<T> listToRemove) where T : MainCharacter
        {
            ListsToAddTo.Remove(listToRemove as System.Collections.IList);
        }
        public static void ClearListsToAddTo () 
        {
            ListsToAddTo.Clear();
        }
        
        
            static string mContentManagerName;
            static System.Collections.Generic.List<System.Collections.IList> ListsToAddTo = new System.Collections.Generic.List<System.Collections.IList>();
            static PoolList<MainCharacter> mPool = new PoolList<MainCharacter>();
            /// <summary> Event raised whenever an instance is created through this factory.
            /// These events are cleared out whenever a Screen is destroyed, so there is 
            /// no reason to explicitly remove added events. </summary>
            public static event Action<MainCharacter> EntitySpawned;
            object IEntityFactory.CreateNew (float x = 0, float y = 0) 
            {
                return MainCharacterFactory.CreateNew(x, y);
            }
            object IEntityFactory.CreateNew (Microsoft.Xna.Framework.Vector3 position) 
            {
                return MainCharacterFactory.CreateNew(position);
            }
            object IEntityFactory.CreateNew (Layer layer) 
            {
                return MainCharacterFactory.CreateNew(layer);
            }
            void IEntityFactory.Initialize (string contentManagerName) 
            {
                MainCharacterFactory.Initialize(contentManagerName);
            }
            void IEntityFactory.ClearListsToAddTo () 
            {
                MainCharacterFactory.ClearListsToAddTo();
            }
            System.Collections.Generic.List<System.Collections.IList> IEntityFactory.ListsToAddTo => MainCharacterFactory.ListsToAddTo;
            static MainCharacterFactory mSelf;
            public static MainCharacterFactory Self
            {
                get
                {
                    if (mSelf == null)
                    {
                        mSelf = new MainCharacterFactory();
                    }
                    return mSelf;
                }
            }
    }
}
