using Client.Controls;
using Client.Models;
using Client.Scenes.Game.Map.Layers;
using Client.Scenes.Views;
using System.Drawing;

namespace Client.Scenes.Game.Map
{
    public abstract class MirMap
    {
        public static UserObject User => GameScene.Game.User;

        public MapCell[,] Cells;
        public int Width, Height;

        public abstract void DrawObjects();

        public abstract DrawingStrategy GetFloorDrawingStrategy();
        public abstract DrawingStrategy GetLightDrawingStrategy();

        #region Dispose
        public void Dispose()
        {
            Width = 0;
            Height = 0;
            Cells = null;
        }
        #endregion

    }

}