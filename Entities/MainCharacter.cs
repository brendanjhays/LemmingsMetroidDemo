using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;

namespace LemmingsMetroid.Entities
{
    public partial class MainCharacter
    {
        Gun gun;
        bool wasLastDirectionRight = false;
        private void CustomInitialize()
        {
            GunCooldown = 0;
            ShotCooldown = 0;
            WaveDashCooldown = 0;   
            WaveDashCooldown = 0;
            gun = Factories.GunFactory.CreateNew(this.X + 9, this.Y);
        }

        private void CustomActivity()
        {
            UpdateTimers();
            UpdateCamera();
            MouseInput();
            KeyInput();
            gun.X = this.X + 9;
            gun.Y = this.Y;
        }

        private void MouseInput()
        {
            float mx = InputManager.Mouse.WorldXAt(0);
            float my = InputManager.Mouse.WorldYAt(0);

            var gunDistance = new Vector2(mx - gun.X, my - gun.Y);
            //gun.RotationX *= ((float)Math.Atan2(gunDistance.Y, gunDistance.X));

            if(InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton) && this.GunCooldown == 0 && this.ShotCooldown == 0)
            {
                var newBullet = Factories.BulletFactory.CreateNew(this.X, this.Y);
                gunDistance.Normalize();
                newBullet.Velocity.X = gunDistance.X * Bullet.Speed;
                newBullet.Velocity.Y = gunDistance.Y * Bullet.Speed;
                this.SpriteInstance.Red = 100;
            }
        }

        void KeyInput()
        {
            //Run non movement keyboard input logic here
            if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                FacingRight = true;
            }
            if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                FacingRight = false;
            }
            this.SpriteInstance.FlipHorizontal = FacingRight;
            if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.S) && CanFastfall(this.Velocity.Y) && this.HasFastfall)
            {
                this.Velocity.Y -= 250;
                this.HasFastfall = false;
                this.AirMovement.MaxFallSpeed *= 1.3f;
            }
            
        }

        bool CanFastfall(float velocity)
        {
            return (velocity < 90);
        }


        private void UpdateCamera()
        {
            Camera.Main.X = 256;
            Camera.Main.Y = -256;
        }

        private void UpdateTimers()
        {

            if (this.GunCooldown > 0) this.GunCooldown--;
            if (this.ShotCooldown > 0) this.ShotCooldown--;
            if (this.IsOnGround)
            {
                this.GunCooldown = 0;
                this.ScreenShake = false;
                ScreenShakeTimer = 0;
                this.HasFastfall = true;
                this.AirMovement.MaxFallSpeed = 400;
            }
            if (this.WaveDashCooldown > 0) this.WaveDashCooldown--;
            if (this.ScreenShakeTimer > 1) this.ScreenShakeTimer--;
            if (this.ScreenShakeTimer == 1)
            {
                this.ScreenShakeTimer--;
                this.ScreenShake = false;
                Camera.Main.X = 256;
                Camera.Main.Y = -256;
            }

            if (this.GunCooldown > 0) this.GunCooldown -= 1;
            if (this.ShotCooldown > 0) this.ShotCooldown -= 1;
            if (this.IsOnGround) this.GunCooldown = 0;
            if (this.WaveDashCooldown > 0) this.WaveDashCooldown -= 1;

        }

        private void CustomDestroy()
        {


        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
    }
}
