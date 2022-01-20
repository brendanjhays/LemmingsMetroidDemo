using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using LemmingsMetroid.Entities;
using LemmingsMetroid.Screens;
namespace LemmingsMetroid.Screens
{
    public partial class GameScreen
    {
        void OnBulletListVsSolidCollisionCollisionOccurredTunnel (Entities.Bullet bullet, FlatRedBall.TileCollisions.TileShapeCollection tileShapeCollection) 
        {
            if (this.BulletListVsSolidCollisionCollisionOccurred != null)
            {
                BulletListVsSolidCollisionCollisionOccurred(bullet, tileShapeCollection);
            }
        }
    }
}
