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
namespace LemmingsMetroid.Screens
{
    public abstract partial class GameScreen : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        
        protected FlatRedBall.TileGraphics.LayeredTileMap Map;
        protected FlatRedBall.TileCollisions.TileShapeCollection SolidCollision;
        protected FlatRedBall.TileCollisions.TileShapeCollection CloudCollision;
        protected FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.MainCharacter> MainCharacterList;
        private LemmingsMetroid.Entities.MainCharacter MainCharacter1;
        private FlatRedBall.Math.Collision.DelegateListVsSingleRelationship<Entities.MainCharacter, FlatRedBall.TileCollisions.TileShapeCollection> MainCharacterListVsSolidCollision;
        private FlatRedBall.Math.Collision.DelegateListVsSingleRelationship<Entities.MainCharacter, FlatRedBall.TileCollisions.TileShapeCollection> MainCharacterListVsCloudCollision;
        protected FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.Bullet> BulletList;
        protected FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.Gun> GunList;
        private FlatRedBall.Math.Collision.CollidableListVsTileShapeCollectionRelationship<Entities.Bullet> BulletListVsSolidCollision;
        public event System.Action<Entities.Bullet, FlatRedBall.TileCollisions.TileShapeCollection> BulletListVsSolidCollisionCollisionOccurred;
        public GameScreen () 
        	: base ("GameScreen")
        {
            // Not instantiating for FlatRedBall.TileGraphics.LayeredTileMap Map in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection SolidCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection CloudCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            MainCharacterList = new FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.MainCharacter>();
            MainCharacterList.Name = "MainCharacterList";
            BulletList = new FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.Bullet>();
            BulletList.Name = "BulletList";
            GunList = new FlatRedBall.Math.PositionedObjectList<LemmingsMetroid.Entities.Gun>();
            GunList.Name = "GunList";
        }
        public override void Initialize (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            // Not instantiating for FlatRedBall.TileGraphics.LayeredTileMap Map in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection SolidCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            // Not instantiating for FlatRedBall.TileCollisions.TileShapeCollection CloudCollision in Screens\GameScreen (Screen) because properties on the object prevent it
            MainCharacterList.Clear();
            MainCharacter1 = new LemmingsMetroid.Entities.MainCharacter(ContentManagerName, false);
            MainCharacter1.Name = "MainCharacter1";
            MainCharacter1.CreationSource = "Glue";
            BulletList.Clear();
            GunList.Clear();
            FillCollisionForSolidCollision();
            FillCollisionForCloudCollision();
                {
        var temp = new FlatRedBall.Math.Collision.DelegateListVsSingleRelationship<Entities.MainCharacter, FlatRedBall.TileCollisions.TileShapeCollection>(MainCharacterList, SolidCollision);
        var isCloud = false;
        temp.CollisionFunction = (first, second) =>
        {
            return first.CollideAgainst(second, isCloud);
        }
        ;
        FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Add(temp);
        MainCharacterListVsSolidCollision = temp;
    }
    MainCharacterListVsSolidCollision.Name = "MainCharacterListVsSolidCollision";

                {
        var temp = new FlatRedBall.Math.Collision.DelegateListVsSingleRelationship<Entities.MainCharacter, FlatRedBall.TileCollisions.TileShapeCollection>(MainCharacterList, CloudCollision);
        var isCloud = true;
        temp.CollisionFunction = (first, second) =>
        {
            return first.CollideAgainst(second, isCloud);
        }
        ;
        FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Add(temp);
        MainCharacterListVsCloudCollision = temp;
    }
    MainCharacterListVsCloudCollision.Name = "MainCharacterListVsCloudCollision";

                if (SolidCollision != null)
    {
        BulletListVsSolidCollision = FlatRedBall.Math.Collision.CollisionManagerTileShapeCollectionExtensions.CreateTileRelationship(FlatRedBall.Math.Collision.CollisionManager.Self, BulletList, SolidCollision);
        BulletListVsSolidCollision.Name = "BulletListVsSolidCollision";
        BulletListVsSolidCollision.SetBounceCollision(0f, 1f, 1f);
    }

            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers () 
        {
            Factories.MainCharacterFactory.Initialize(ContentManagerName);
            Factories.BulletFactory.Initialize(ContentManagerName);
            Factories.GunFactory.Initialize(ContentManagerName);
            Factories.MainCharacterFactory.AddList(MainCharacterList);
            Factories.BulletFactory.AddList(BulletList);
            Factories.GunFactory.AddList(GunList);
            MainCharacter1.AddToManagers(mLayer);
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
                
                for (int i = MainCharacterList.Count - 1; i > -1; i--)
                {
                    if (i < MainCharacterList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        MainCharacterList[i].Activity();
                    }
                }
                for (int i = BulletList.Count - 1; i > -1; i--)
                {
                    if (i < BulletList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        BulletList[i].Activity();
                    }
                }
                for (int i = GunList.Count - 1; i > -1; i--)
                {
                    if (i < GunList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        GunList[i].Activity();
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
        public override void ActivityEditMode () 
        {
            if (FlatRedBall.Screens.ScreenManager.IsInEditMode)
            {
                foreach (var item in MainCharacterList)
                {
                    item.ActivityEditMode();
                }
                foreach (var item in BulletList)
                {
                    item.ActivityEditMode();
                }
                foreach (var item in GunList)
                {
                    item.ActivityEditMode();
                }
                CustomActivityEditMode();
                base.ActivityEditMode();
            }
        }
        public override void Destroy () 
        {
            base.Destroy();
            Factories.MainCharacterFactory.Destroy();
            Factories.BulletFactory.Destroy();
            Factories.GunFactory.Destroy();
            
            MainCharacterList.MakeOneWay();
            BulletList.MakeOneWay();
            GunList.MakeOneWay();
            for (int i = MainCharacterList.Count - 1; i > -1; i--)
            {
                MainCharacterList[i].Destroy();
            }
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            for (int i = GunList.Count - 1; i > -1; i--)
            {
                GunList[i].Destroy();
            }
            MainCharacterList.MakeTwoWay();
            BulletList.MakeTwoWay();
            GunList.MakeTwoWay();
            FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Clear();
            CustomDestroy();
        }
        public virtual void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            BulletListVsSolidCollision.CollisionOccurred += OnBulletListVsSolidCollisionCollisionOccurred;
            BulletListVsSolidCollision.CollisionOccurred += OnBulletListVsSolidCollisionCollisionOccurredTunnel;
            if (Map!= null)
            {
            }
            if (SolidCollision!= null)
            {
            }
            if (CloudCollision!= null)
            {
            }
            MainCharacterList.Add(MainCharacter1);
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp () 
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers () 
        {
            for (int i = MainCharacterList.Count - 1; i > -1; i--)
            {
                MainCharacterList[i].Destroy();
            }
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            for (int i = GunList.Count - 1; i > -1; i--)
            {
                GunList[i].Destroy();
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements) 
        {
            if (callOnContainedElements)
            {
                MainCharacter1.AssignCustomVariables(true);
            }
            if (Map != null)
            {
            }
            if (SolidCollision != null)
            {
            }
            if (CloudCollision != null)
            {
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
            if (CloudCollision != null)
            {
            }
            for (int i = 0; i < MainCharacterList.Count; i++)
            {
                MainCharacterList[i].ConvertToManuallyUpdated();
            }
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].ConvertToManuallyUpdated();
            }
            for (int i = 0; i < GunList.Count; i++)
            {
                GunList[i].ConvertToManuallyUpdated();
            }
        }
        public static void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
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
            return null;
        }
        public static object GetFile (string memberName) 
        {
            return null;
        }
        object GetMember (string memberName) 
        {
            return null;
        }
        public static void Reload (object whatToReload) 
        {
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
        protected virtual void FillCollisionForCloudCollision () 
        {
            if (CloudCollision != null)
            {
                // normally we wait to set variables until after the object is created, but in this case if the
                // TileShapeCollection doesn't have its Visible set before creating the tiles, it can result in
                // really bad performance issues, as shapes will be made visible, then invisible. Really bad perf!
                CloudCollision.Visible = false;
                FlatRedBall.TileCollisions.TileShapeCollectionLayeredTileMapExtensions.AddCollisionFromTilesWithType(CloudCollision, Map, "CloudCollision", false);
            }
        }
        partial void CustomActivityEditMode();
    }
}
