using Client.Controls;
using Client.Envir;
using Client.Models;
using Client.Scenes.Views;
using Library;
using System;
using System.Drawing;

namespace Client.Scenes.Game.Map.Layers
{
    public sealed class FloorLayer : DXControl
    {
        public static UserObject User => GameScene.Game.User;



        public FloorLayer()
        {
            IsControl = false;
        }

        protected override void OnClearTexture()
        {
            base.OnClearTexture();
            DrawingStrategy.Apply();
        }

        private DrawingStrategy DrawingStrategy;
        public void Draw(DrawingStrategy Strategy)
        {
            DrawingStrategy = Strategy;
            Draw();
        }

        private DrawingStrategy GetDrawingStrategy()
        {
            return DrawingStrategy;
        }

        public override void Draw()
        {
            if (!TextureValid)
                CreateTexture();
        }

        protected override void DrawControl() { }
    }

}