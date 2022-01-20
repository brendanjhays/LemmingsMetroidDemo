using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Gui;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;

using Microsoft.Xna.Framework;





namespace LemmingsMetroid.Screens
{
    public partial class GameScreen
    {
        Random rand = new Random();
        void CustomInitialize()
        {

            //Only method calls in this, no game logic


            AssignInputs();
        }

        void AssignInputs()
        {
            MainCharacter1.HorizontalInput = InputManager.Keyboard.Get1DInput(Microsoft.Xna.Framework.Input.Keys.A, Microsoft.Xna.Framework.Input.Keys.D);
            MainCharacter1.JumpInput = InputManager.Keyboard.GetKey(Microsoft.Xna.Framework.Input.Keys.Space);
        }

        void CustomActivity(bool firstTimeCalled)
        {

            //Only method calls in this, no game logic
            MouseInput();
            GamepadInput();
            UpdateSprite();
            UpdateCamera();
        }

        void UpdateCamera()
        {
            int delay = 3;
            int screenOffset = 5;
            Vector2 offset = new Vector2(0, 0);
            if (MainCharacter1.ScreenShake)
            {
                offset = new Vector2((float)rand.Next(-screenOffset, screenOffset), rand.Next(-screenOffset, screenOffset));
                if (MainCharacter1.ScreenShakeTimer % delay == 0)
                {
                    Camera.Main.X += offset.X;
                    Camera.Main.Y += offset.Y;
                }
            }
        }

        void UpdateSprite()
        {
        }

        void GamepadInput()
        {
            //Run non movement keyboard input logic here

            MouseInput();


        }

        void MouseInput()
        {
            if (InputManager.Mouse.ButtonPushed(FlatRedBall.Input.Mouse.MouseButtons.LeftButton) && MainCharacter1.GunCooldown == 0 && MainCharacter1.ShotCooldown == 0)
            {

                MainCharacter1.ScreenShakeTimer = 6;
                MainCharacter1.ScreenShake = true;


                float ex = MainCharacter1.X;
                float mx = InputManager.Mouse.WorldXAt(0);
                float dx = mx - ex;

                float ey = MainCharacter1.Y;
                float my = InputManager.Mouse.WorldYAt(0);
                float dy = my - ey;
                if (Math.Abs(dx) > 35)
                {
                    if (MainCharacter1.IsOnGround && (ey - my < 0))
                    {
                        if (MainCharacter1.WaveDashCooldown == 0)
                        {
                            MainCharacter1.Velocity.X = -(float)(Math.Log(Math.Pow(dx, 4) + 1, 3.5) * 24 * ReturnNegativity(dx)); //Wavedashing
                            MainCharacter1.WaveDashCooldown = 12;
                        }
                    }
                    else MainCharacter1.Velocity.X = -(float)(Math.Log(Math.Pow(dx, 4) + 1, 3) * 20 * ReturnNegativity(dx));
                }
                if (Math.Abs(dy) > 35)
                {
                    MainCharacter1.Velocity.Y = -(float)(Math.Log(Math.Pow(dy, 4) + 1, 3) * 20 * ReturnNegativity(dy));
                }
                MainCharacter1.GunCooldown = 120;
                MainCharacter1.ShotCooldown = 9;

                


            }

            if (InputManager.Mouse.ButtonPushed(FlatRedBall.Input.Mouse.MouseButtons.RightButton))
            {/*
                float ex = MainCharacter1.X;
                float mx = InputManager.Mouse.WorldXAt(0);
                float dx = mx - ex;

                float ey = MainCharacter1.Y;
                float my = InputManager.Mouse.WorldYAt(0);
                float dy = my - ey;

                MainCharacter1.Velocity.X -= (float)(Math.Log(Math.Pow((dx + (float) rand.Next(-250,250)), 4) + 1, 2.5) * 40 * ReturnNegativity(dx));

                MainCharacter1.Velocity.Y -= (float)(Math.Log(Math.Pow((dy + (float) rand.Next(-250, 250)), 4) + 1, 2.5) * 40 * ReturnNegativity(dy)); this is dumb */
            }
        }





        float ReturnNegativity(float v)
        {
            return v > 0 ? 1 : -1;
        }

        void CustomDestroy()
        {


        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

    }
}
