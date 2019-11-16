using Client.Controls;
using Client.Envir;
using Client.Models;
using Client.Scenes.Views;
using Library;
using SlimDX;
using SlimDX.Direct3D9;
using System;

using System.Drawing;

namespace Client.Scenes.Game.Map.Layers
{
    public sealed class LightLayer : DXControl
    {
        public static UserObject User => GameScene.Game.User;

        public int OffSetX => GameScene.Game.MapControl.getOffSetX();
        public int OffSetY => GameScene.Game.MapControl.getOffSetY();
        public int CellWidth => GameScene.Game.MapControl.getCellWidth();
        public int CellHeight => GameScene.Game.MapControl.getCellHeight();

        public LightLayer()
        {
            IsControl = false;
            BackColour = Color.FromArgb(15, 15, 15);
        }

        #region Methods

        protected override void OnClearTexture()
        {
            base.OnClearTexture();

            if (MapObject.User.Dead)
            {
                DXManager.Device.Clear(ClearFlags.Target, Color.IndianRed, 0, 0);
                return;
            }

            DXManager.SetBlend(true);
            DXManager.Device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);

            const float lightScale = 0.02F; //Players/Monsters
            const float baseSize = 0.1F;

            float fX;
            float fY;

            if ((MapObject.User.Poison & PoisonType.Abyss) == PoisonType.Abyss)
            {
                DXManager.Device.Clear(ClearFlags.Target, Color.Black, 0, 0);

                float scale = baseSize + 4 * lightScale;

                fX = (OffSetX + MapObject.User.CurrentLocation.X - User.CurrentLocation.X) * CellWidth + CellWidth / 2;
                fY = (OffSetY + MapObject.User.CurrentLocation.Y - User.CurrentLocation.Y) * CellHeight;

                fX -= (DXManager.LightWidth * scale) / 2;
                fY -= (DXManager.LightHeight * scale) / 2;

                fX /= scale;
                fY /= scale;

                DXManager.Sprite.Transform = Matrix.Scaling(scale, scale, 1);

                DXManager.Sprite.Draw(DXManager.LightTexture, Vector3.Zero, new Vector3(fX, fY, 0), Color.White);

                DXManager.Sprite.Transform = Matrix.Identity;

                DXManager.SetBlend(false);

                MapObject.User.AbyssEffect.Draw();
                return;
            }

            foreach (MapObject ob in GameScene.Game.MapControl.Objects)
            {
                if (ob.Light > 0 && (!ob.Dead || ob == MapObject.User || ob.Race == ObjectType.Spell))
                {
                    float scale = baseSize + ob.Light * 2 * lightScale;

                    fX = (OffSetX + ob.CurrentLocation.X - User.CurrentLocation.X) * CellWidth + ob.MovingOffSet.X - User.MovingOffSet.X + CellWidth / 2;
                    fY = (OffSetY + ob.CurrentLocation.Y - User.CurrentLocation.Y) * CellHeight + ob.MovingOffSet.Y - User.MovingOffSet.Y;

                    fX -= (DXManager.LightWidth * scale) / 2;
                    fY -= (DXManager.LightHeight * scale) / 2;

                    fX /= scale;
                    fY /= scale;

                    DXManager.Sprite.Transform = Matrix.Scaling(scale, scale, 1);

                    DXManager.Sprite.Draw(DXManager.LightTexture, Vector3.Zero, new Vector3(fX, fY, 0), ob.LightColour);

                    DXManager.Sprite.Transform = Matrix.Identity;
                }
            }

            foreach (MirEffect ob in GameScene.Game.MapControl.Effects)
            {
                float frameLight = ob.FrameLight;

                if (frameLight > 0)
                {
                    float scale = baseSize + frameLight * 2 * lightScale / 5;

                    fX = ob.DrawX + CellWidth / 2;
                    fY = ob.DrawY + CellHeight / 2;

                    fX -= (DXManager.LightWidth * scale) / 2;
                    fY -= (DXManager.LightHeight * scale) / 2;

                    fX /= scale;
                    fY /= scale;

                    DXManager.Sprite.Transform = Matrix.Scaling(scale, scale, 1);

                    DXManager.Sprite.Draw(DXManager.LightTexture, Vector3.Zero, new Vector3(fX, fY, 0), ob.FrameLightColour);

                    DXManager.Sprite.Transform = Matrix.Identity;
                }
            }


            DrawingStrategy.Apply();
        }

        public void UpdateLights()
        {
            switch (GameScene.Game.MapControl.MapInfo.Light)
            {
                case LightSetting.Default:
                    byte shading = (byte)(255 * GameScene.Game.DayTime);
                    BackColour = Color.FromArgb(shading, shading, shading);
                    Visible = true;
                    break;
                case LightSetting.Night:
                    BackColour = Color.FromArgb(15, 15, 15);
                    Visible = true;
                    break;
                case LightSetting.Light:
                    Visible = MapObject.User != null && (MapObject.User.Poison & PoisonType.Abyss) != PoisonType.Abyss;
                    break;
            }
        }

        private DrawingStrategy DrawingStrategy;
        public void Draw(DrawingStrategy Strategy)
        {
            DrawingStrategy = Strategy;
            Draw();
        }
        public override void Draw()
        {
            CreateTexture();
        }
        protected override void DrawControl() { }
        #endregion
    }
}