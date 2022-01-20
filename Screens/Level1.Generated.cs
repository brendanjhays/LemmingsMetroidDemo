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
    public partial class Level1 : GameScreen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        protected static FlatRedBall.TileGraphics.LayeredTileMap Level1Map;
        
        public Level1 () 
        	: base ()
        {
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<MainCharacter> MainCharacterList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<Bullet> BulletList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<Gun> GunList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
        }
        public override void Initialize (bool addToManagers) 
        {
            LoadStaticContent(ContentManagerName);
            Map = Level1Map;
            SolidCollision = new FlatRedBall.TileCollisions.TileShapeCollection(); SolidCollision.Name = "SolidCollision";
            CloudCollision = new FlatRedBall.TileCollisions.TileShapeCollection(); CloudCollision.Name = "CloudCollision";
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<MainCharacter> MainCharacterList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<Bullet> BulletList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
            // skipping instantiation of FlatRedBall.Math.PositionedObjectList<Gun> GunList in Screens\Level1 (Screen) because it has its InstantiatedByBase set to true
            
            
            base.Initialize(addToManagers);
        }
        public override void AddToManagers () 
        {
            Level1Map.AddToManagers(mLayer);
            base.AddToManagers();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled) 
        {
            if (!IsPaused)
            {
                
                Level1Map?.AnimateSelf();;
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
                CustomActivityEditMode();
                base.ActivityEditMode();
            }
        }
        public override void Destroy () 
        {
            base.Destroy();
            Level1Map.Destroy();
            Level1Map = null;
            
            MainCharacterList.MakeOneWay();
            BulletList.MakeOneWay();
            GunList.MakeOneWay();
            if (Map != null)
            {
                Map.Destroy();
            }
            if (SolidCollision != null)
            {
                SolidCollision.Visible = false;
            }
            if (CloudCollision != null)
            {
                CloudCollision.Visible = false;
            }
            MainCharacterList.MakeTwoWay();
            BulletList.MakeTwoWay();
            GunList.MakeTwoWay();
            FlatRedBall.Math.Collision.CollisionManager.Self.Relationships.Clear();
            CustomDestroy();
        }
        public override void PostInitialize () 
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            base.PostInitialize();
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public override void AddToManagersBottomUp () 
        {
            base.AddToManagersBottomUp();
        }
        public override void RemoveFromManagers () 
        {
            base.RemoveFromManagers();
            Level1Map.Destroy();
            if (Map != null)
            {
                Map.Destroy();
            }
            if (SolidCollision != null)
            {
                SolidCollision.Visible = false;
            }
            if (CloudCollision != null)
            {
                CloudCollision.Visible = false;
            }
        }
        public override void AssignCustomVariables (bool callOnContainedElements) 
        {
            base.AssignCustomVariables(callOnContainedElements);
            if (callOnContainedElements)
            {
            }
        }
        public override void ConvertToManuallyUpdated () 
        {
            base.ConvertToManuallyUpdated();
        }
        public static new void LoadStaticContent (string contentManagerName) 
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            LemmingsMetroid.Screens.GameScreen.LoadStaticContent(contentManagerName);
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
            Level1Map = FlatRedBall.TileGraphics.LayeredTileMap.FromTiledMapSave("content/screens/level1/level1map.tmx", contentManagerName);
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
        public static new object GetStaticMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Level1Map":
                    return Level1Map;
            }
            return null;
        }
        public static new object GetFile (string memberName) 
        {
            switch(memberName)
            {
                case  "Level1Map":
                    return Level1Map;
            }
            return null;
        }
        object GetMember (string memberName) 
        {
            switch(memberName)
            {
                case  "Level1Map":
                    return Level1Map;
            }
            return null;
        }
        public static void Reload (object whatToReload) 
        {
        }
        partial void CustomActivityEditMode();
    }
}
