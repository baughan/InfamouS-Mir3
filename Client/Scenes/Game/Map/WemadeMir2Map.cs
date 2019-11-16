using Client.Envir;
using Client.Models;
using Client.Scenes.Game.Map.Layers;
using Client.Scenes.Views;
using Library;
using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Drawing;
using System.IO;

namespace Client.Scenes.Game.Map
{
    class WemadeMir2Map : MirMap
    {
        public WemadeMir2Map(byte[] Bytes)
        {
            try
            {
                int offSet = 21;

                int w = BitConverter.ToInt16(Bytes, offSet);
                offSet += 2;
                int xor = BitConverter.ToInt16(Bytes, offSet);
                offSet += 2;
                int h = BitConverter.ToInt16(Bytes, offSet);
                Width = w ^ xor;
                Height = h ^ xor;
                Cells = new WemadeMir2MapCell[Width, Height];

                offSet = 54;

                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        WemadeMir2MapCell newCell = new WemadeMir2MapCell
                        {
                            BackFile = 0,
                            BackImage = (int)(BitConverter.ToInt32(Bytes, offSet) ^ 0xAA38AA38),
                            MiddleFile = 1,
                            MiddleImage = (short)(BitConverter.ToInt16(Bytes, offSet += 4) ^ xor),
                            FrontImage = (short)(BitConverter.ToInt16(Bytes, offSet += 2) ^ xor),
                            DoorIndex = (byte)(Bytes[offSet += 2] & 0x7F),
                            DoorOffset = Bytes[++offSet],
                            FrontAnimationFrame = Bytes[++offSet],
                            FrontAnimationTick = Bytes[++offSet],
                            FrontFile = (short)(Bytes[++offSet] + 2),
                            Light = Bytes[++offSet],
                            Unknown = Bytes[++offSet],
                        };
                        offSet++;

                        if (newCell.Light >= 100 && newCell.Light <= 119)
                            newCell.FishingCell = true;

                        if ((newCell.BackImage & 0x20000000) != 0 || (newCell.FrontImage & 0x8000) != 0)
                            newCell.Flag = true;

                        Cells[x, y] = newCell;
                    }
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
        }

        #region LightDrawingStrategy
        public override DrawingStrategy GetLightDrawingStrategy()
        {
            return new LightDrawingStrategy();
        }

        private class LightDrawingStrategy : DrawingStrategy
        {
            public override void Apply()
            {
                const float lightScale = 0.02F; //Players/Monsters
                const float baseSize = 0.1F;

                int minX = Math.Max(0, User.CurrentLocation.X - OffSetX - 15), maxX = Math.Min(GameScene.Game.MapControl.Width - 1, User.CurrentLocation.X + OffSetX + 15);
                int minY = Math.Max(0, User.CurrentLocation.Y - OffSetY - 15), maxY = Math.Min(GameScene.Game.MapControl.Height - 1, User.CurrentLocation.Y + OffSetY + 15);

                float fX;
                float fY;
                for (int y = minY; y <= maxY; y++)
                {
                    if (y < 0) continue;
                    if (y >= GameScene.Game.MapControl.Height) break;

                    int drawY = (y - User.CurrentLocation.Y + OffSetY) * CellHeight - User.MovingOffSet.Y;

                    for (int x = minX; x <= maxX; x++)
                    {
                        if (x < 0) continue;
                        if (x >= GameScene.Game.MapControl.Width) break;

                        int drawX = (x - User.CurrentLocation.X + OffSetX) * CellWidth - User.MovingOffSet.X;

                        WemadeMir2MapCell tile = (WemadeMir2MapCell)GameScene.Game.MapControl.Cells[x, y];

                        if (tile.Light == 0) continue;

                        float scale = baseSize + tile.Light * 30 * lightScale;

                        fX = drawX + CellWidth / 2;
                        fY = drawY + CellHeight / 2;

                        fX -= DXManager.LightWidth * scale / 2;
                        fY -= DXManager.LightHeight * scale / 2;

                        fX /= scale;
                        fY /= scale;

                        DXManager.Sprite.Transform = Matrix.Scaling(scale, scale, 1);
                        DXManager.Sprite.Draw(DXManager.LightTexture, Vector3.Zero, new Vector3(fX, fY, 0), Color.White);
                        DXManager.Sprite.Transform = Matrix.Identity;
                    }
                }
            }
        }
        #endregion

        #region FloorDrawingStrategy
        public override DrawingStrategy GetFloorDrawingStrategy()
        {
            return new FloorDrawingStrategy();
        }

        private class FloorDrawingStrategy : DrawingStrategy
        {
            public override void Apply()
            {
                int minX = Math.Max(0, User.CurrentLocation.X - OffSetX - 4), maxX = Math.Min(GameScene.Game.MapControl.Width - 1, User.CurrentLocation.X + OffSetX + 4);
                int minY = Math.Max(0, User.CurrentLocation.Y - OffSetY - 4), maxY = Math.Min(GameScene.Game.MapControl.Height - 1, User.CurrentLocation.Y + OffSetY + 4);

                #region BackImage
                for (int y = minY; y <= maxY; y++)
                {
                    if (y < 0 || y % 2 == 1) continue; //TODO: MAP-1 Apply to WemadeMir3Map?
                    if (y >= GameScene.Game.MapControl.Height) break;

                    int drawY = (y - User.CurrentLocation.Y + OffSetY) * CellHeight - User.MovingOffSet.Y;
                    for (int x = minX; x <= maxX; x++)
                    {
                        if (x < 0 || x % 2 == 1) continue; //TODO: MAP-1 Apply to WemadeMir3Map?
                        if (x >= GameScene.Game.MapControl.Width) break;

                        int drawX = (x - User.CurrentLocation.X + OffSetX) * CellWidth - User.MovingOffSet.X;
                        WemadeMir2MapCell tile = (WemadeMir2MapCell)GameScene.Game.MapControl.Cells[x, y];

                        if (!Libraries.WemadeMir2Order.TryGetValue(tile.BackFile, out LibraryFile file)) continue;
                        if (!CEnvir.LibraryList.TryGetValue(file, out MirLibrary library)) continue;
                        library.Draw((tile.BackImage & 0x1FFFF) - 1, drawX, drawY, Color.White, false, 1F, ImageType.Image);
                    }
                }
                #endregion

                #region Draw Middle & Front Image
                for (int y = minY; y <= maxY; y++)
                {
                    int drawY = (y - User.CurrentLocation.Y + OffSetY + 1) * CellHeight - User.MovingOffSet.Y;
                    for (int x = minX; x <= maxX; x++)
                    {
                        int drawX = (x - User.CurrentLocation.X + OffSetX) * CellWidth - User.MovingOffSet.X;
                        WemadeMir2MapCell cell = (WemadeMir2MapCell)GameScene.Game.MapControl.Cells[x, y];

                        if (Libraries.WemadeMir2Order.TryGetValue(cell.MiddleFile, out LibraryFile file) && CEnvir.LibraryList.TryGetValue(file, out MirLibrary library))
                        {
                            int index = cell.MiddleImage - 1;
                            if (index < 0 || cell.MiddleFile == -1) continue;

                            Size s = library.GetSize(index);
                            if ((s.Width == CellWidth && s.Height == CellHeight) || (s.Width == CellWidth * 2 && s.Height == CellHeight * 2))
                                library.Draw(index, drawX, drawY - CellHeight, Color.White, false, 1F, ImageType.Image);
                        }
                    }
                }

                for (int y = minY; y <= maxY; y++)
                {
                    int drawY = (y - User.CurrentLocation.Y + OffSetY + 1) * CellHeight - User.MovingOffSet.Y;
                    for (int x = minX; x <= maxX; x++)
                    {
                        int drawX = (x - User.CurrentLocation.X + OffSetX) * CellWidth - User.MovingOffSet.X;
                        WemadeMir2MapCell cell = (WemadeMir2MapCell)GameScene.Game.MapControl.Cells[x, y];

                        if (Libraries.WemadeMir2Order.TryGetValue(cell.FrontFile, out LibraryFile file) && CEnvir.LibraryList.TryGetValue(file, out MirLibrary library))
                        {
                            int index = (cell.FrontImage & 0x7FFF) - 1;
                            if (index < 0 || index == 200) continue;

                            Size s = library.GetSize(index);
                            if ((s.Width == CellWidth && s.Height == CellHeight) || (s.Width == CellWidth * 2 && s.Height == CellHeight * 2))
                                library.Draw(index, drawX, drawY - CellHeight, Color.White, false, 1F, ImageType.Image);
                        }
                    }
                }
                #endregion
            }
        }
        #endregion


        #region ObjectDrawingStrategy
        public override void DrawObjects()
        {
            new ObjectDrawingStrategy(Cells).Apply();
        }

        private class ObjectDrawingStrategy : DrawingStrategy
        {
            MapCell[,] Cells;

            public ObjectDrawingStrategy(MapCell[,] cells)
            {
                Cells = cells;
            }

            public override void Apply()
            {
                int minX = Math.Max(0, User.CurrentLocation.X - OffSetX - 4), maxX = Math.Min(GameScene.Game.MapControl.Width - 1, User.CurrentLocation.X + OffSetX + 4);
                int minY = Math.Max(0, User.CurrentLocation.Y - OffSetY - 4), maxY = Math.Min(GameScene.Game.MapControl.Height - 1, User.CurrentLocation.Y + OffSetY + 25);

                for (int y = minY; y <= maxY; y++)
                {
                    int drawY = (y - User.CurrentLocation.Y + OffSetY + 1) * CellHeight - User.MovingOffSet.Y;
                    for (int x = minX; x <= maxX; x++)
                    {
                        int drawX = (x - User.CurrentLocation.X + OffSetX) * CellWidth - User.MovingOffSet.X;
                        WemadeMir2MapCell cell = (WemadeMir2MapCell)Cells[x, y];

                        MirLibrary library; LibraryFile file;
                        if (Libraries.WemadeMir2Order.TryGetValue(cell.MiddleFile, out file) && CEnvir.LibraryList.TryGetValue(file, out library))
                        {
                            int index = cell.MiddleImage - 1;

                            bool blend = false;
                            if (cell.MiddleAnimationFrame > 1 && cell.MiddleAnimationFrame < 255)
                            {
                                index += GameScene.Game.MapControl.Animation % (cell.MiddleAnimationFrame & 0x4F);
                                blend = (cell.MiddleAnimationFrame & 0x50) > 0;
                            }

                            Size s = library.GetSize(index);

                            if ((s.Width != CellWidth || s.Height != CellHeight) && (s.Width != CellWidth * 2 || s.Height != CellHeight * 2))
                            {
                                if (!blend)
                                    library.Draw(index, drawX, drawY - s.Height, Color.White, false, 1F, ImageType.Image);
                                else
                                    library.DrawBlend(index, drawX, drawY - s.Height, Color.White, false, 0.5F, ImageType.Image);
                            }
                        }

                        if (Libraries.WemadeMir2Order.TryGetValue(cell.FrontFile, out file) && CEnvir.LibraryList.TryGetValue(file, out library))
                        {
                            int index = (cell.FrontImage & 0x7FFF) - 1;
                            if (index == -1) continue;

                            int animation = cell.FrontAnimationFrame;

                            bool blend = false;
                            if ((animation & 0x80) > 0)
                            {
                                blend = true;
                                animation &= 0x7F;
                            }

                            if (animation > 0)
                            {
                                int animationTick = cell.FrontAnimationTick;
                                index += (GameScene.Game.MapControl.Animation % (animation + (animation * animationTick))) / (1 + animationTick);
                            }

                            Size s = library.GetSize(index);
                            if ((s.Width != CellWidth || s.Height != CellHeight || animation != 0) && (s.Width != CellWidth * 2 || s.Height != CellHeight * 2 || animation != 0))
                            {
                                if (!blend)
                                    library.Draw(index, drawX, drawY - s.Height, Color.White, false, 1F, ImageType.Image);
                                else //TODO: MAP-1 Different for ShandaMir2
                                    library.DrawBlend(index, drawX, drawY - s.Height, Color.White, (index >= 2723 && index <= 2732), 0.5F, ImageType.Image);
                            }
                        }
                    }

                    foreach (MapObject ob in GameScene.Game.MapControl.Objects)
                    {
                        if (ob.RenderY == y)
                            ob.Draw();
                    }

                    if (Config.DrawEffects)
                    {
                        foreach (MirEffect ob in GameScene.Game.MapControl.Effects)
                        {
                            if (ob.DrawType != DrawType.Object) continue;

                            if (ob.MapTarget.IsEmpty && ob.Target != null)
                            {
                                if (ob.Target.RenderY == y && ob.Target != User)
                                    ob.Draw();
                            }
                            else if (ob.MapTarget.Y == y)
                                ob.Draw();
                        }
                    }

                }

                if (User.Opacity != 1f) return;
                float oldOpacity = MapObject.User.Opacity;
                MapObject.User.Opacity = 0.65F;

                MapObject.User.Draw();

                MapObject.User.Opacity = oldOpacity;

                if (Config.DrawEffects)
                {
                    foreach (MirEffect ob in GameScene.Game.MapControl.Effects)
                    {
                        if (ob.DrawType != DrawType.Object || !ob.MapTarget.IsEmpty || ob.Target != User) continue;
                        ob.Draw();
                    }
                }
            }
        }
        #endregion

    }
}