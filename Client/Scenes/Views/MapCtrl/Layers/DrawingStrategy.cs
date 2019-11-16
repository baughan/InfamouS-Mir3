using Client.Models;

namespace Client.Scenes.Game.Map.Layers
{
    public abstract class DrawingStrategy
    {
        public static UserObject User => GameScene.Game.User;

        public int OffSetX => GameScene.Game.MapControl.getOffSetX();
        public int OffSetY => GameScene.Game.MapControl.getOffSetY();
        public int CellWidth => GameScene.Game.MapControl.getCellWidth();
        public int CellHeight => GameScene.Game.MapControl.getCellHeight();

        public abstract void Apply();
    }
}