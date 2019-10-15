using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Client.Controls;
using Client.Envir;
using Client.Models;
using Client.UserModels;
using Library;
using Library.SystemModels;
using C = Library.Network.ClientPackets;

//Cleaned
namespace Client.Scenes.Views
{
    public sealed  class BigMapDialog : DXWindow
    {
        #region Properties

        #region SelectedInfo

        public MapInfo SelectedInfo
        {
            get { return _SelectedInfo; }
            set
            {
                if (_SelectedInfo == value) return;

                MapInfo oldValue = _SelectedInfo;
                _SelectedInfo = value;

                OnSelectedInfoChanged(oldValue, value);
            }
        }
        private MapInfo _SelectedInfo;
        public event EventHandler<EventArgs> SelectedInfoChanged;
        public void OnSelectedInfoChanged(MapInfo oValue, MapInfo nValue)
        {
            SelectedInfoChanged?.Invoke(this, EventArgs.Empty);

            foreach (DXControl control in MapInfoObjects.Values)
                control.Dispose();

            MapInfoObjects.Clear();

            if (SelectedInfo == null) return;

            TitleLabel.Text = SelectedInfo.Description;
            Image.Index = SelectedInfo.MiniMap;

            SetClientSize(Image.Size);
            Location = new Point((GameScene.Game.Size.Width - Size.Width) / 2, (GameScene.Game.Size.Height - Size.Height) / 2);

            Size size = GetMapSize(SelectedInfo.FileName);
            ScaleX = Image.Size.Width / (float)size.Width;
            ScaleY = Image.Size.Height / (float)size.Height;

            foreach (NPCInfo ob in Globals.NPCInfoList.Binding)
                Update(ob);

            foreach (MovementInfo ob in Globals.MovementInfoList.Binding)
                Update(ob);

            foreach (ClientObjectData ob in GameScene.Game.DataDictionary.Values)
                Update(ob);


        }
        private Size GetMapSize(string fileName)
        {
            if (!File.Exists(Config.MapPath + fileName + ".map")) return Size.Empty;            


            byte[] Bytes = File.ReadAllBytes(Config.MapPath + fileName + ".map");
            //c# custom map format
            if ((Bytes[2] == 0x43) && (Bytes[3] == 0x23))
                return LoadMapType100(Bytes);
            //wemade mir3 maps have no title they just start with blank bytes
            else if (Bytes[0] == 0)
                return LoadMapType5(Bytes);
            //shanda mir3 maps start with title: (C) SNDA, MIR3.
            else if ((Bytes[0] == 0x0F) && (Bytes[5] == 0x53) && (Bytes[14] == 0x33))
                return LoadMapType6(Bytes);
            //wemades antihack map (laby maps) title start with: Mir2 AntiHack
            else if ((Bytes[0] == 0x15) && (Bytes[4] == 0x32) && (Bytes[6] == 0x41) && (Bytes[19] == 0x31))
                return LoadMapType4(Bytes);
            //wemades 2010 map format i guess title starts with: Map 2010 Ver 1.0
            else if ((Bytes[0] == 0x10) && (Bytes[2] == 0x61) && (Bytes[7] == 0x31) && (Bytes[14] == 0x31))
                return LoadMapType1(Bytes);
            //shanda's 2012 format and one of shandas(wemades) older formats share same header info, only difference is the filesize
            else if ((Bytes[4] == 0x0F) || (Bytes[4] == 0x03) && (Bytes[18] == 0x0D) && (Bytes[19] == 0x0A))
            {
                int W = Bytes[0] + (Bytes[1] << 8);
                int H = Bytes[2] + (Bytes[3] << 8);
                if (Bytes.Length > (52 + (W * H * 14)))
                    return LoadMapType3(Bytes);
                else
                    return LoadMapType2(Bytes);
            }
            //3/4 heroes map format (myth/lifcos i guess)
            else if ((Bytes[0] == 0x0D) && (Bytes[1] == 0x4C) && (Bytes[7] == 0x20) && (Bytes[11] == 0x6D))
                return LoadMapType7(Bytes);
            else
                //if it's none of the above load the default old school format
                return LoadMapType0(Bytes);


            /*
             * using (FileStream stream = File.OpenRead(Config.MapPath + fileName + ".map"))
             * using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Seek(22, SeekOrigin.Begin);

                return new Size(reader.ReadInt16(), reader.ReadInt16());
            }*/
        }

        private Size LoadMapType0(byte[] Bytes)
        {
            try
            {
                int offset = 0;
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType1(byte[] Bytes)
        {
            try
            {
                int offSet = 21;

                int w = BitConverter.ToInt16(Bytes, offSet);
                offSet += 2;
                int xor = BitConverter.ToInt16(Bytes, offSet);
                offSet += 2;
                int h = BitConverter.ToInt16(Bytes, offSet);
                int Width = w ^ xor;
                int Height = h ^ xor;
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType2(byte[] Bytes)
        {
            try
            {
                int offset = 0;
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType3(byte[] Bytes)
        {
            try
            {
                int offset = 0;
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType4(byte[] Bytes)
        {
            try
            {
                int offset = 31;
                int w = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int xor = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int h = BitConverter.ToInt16(Bytes, offset);
                int Width = w ^ xor;
                int Height = h ^ xor;
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType5(byte[] Bytes)
        {
            try
            {
                int offset = 20;
                short Attribute = (short)(BitConverter.ToInt16(Bytes, offset));
                int Width = (int)(BitConverter.ToInt16(Bytes, offset += 2));
                int Height = (int)(BitConverter.ToInt16(Bytes, offset += 2));
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType6(byte[] Bytes)
        {
            try
            {
                byte flag = 0;
                int offset = 16;
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType7(byte[] Bytes)
        {
            try
            {
                int offset = 21;
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 4;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        private Size LoadMapType100(byte[] Bytes)
        {
            try
            {
                int offset = 4;
                if ((Bytes[0] != 1) || (Bytes[1] != 0)) return Size.Empty;//only support version 1 atm
                int Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                int Height = BitConverter.ToInt16(Bytes, offset);
                return new Size(Width, Height);
            }
            catch (Exception ex)
            {
                CEnvir.SaveError(ex.ToString());
            }
            return Size.Empty;
        }

        #endregion

        public Rectangle Area;
        public DXImageControl Image;
        public DXControl Panel;
        
        public static float ScaleX, ScaleY;

        public Dictionary<object, DXControl> MapInfoObjects = new Dictionary<object, DXControl>();

        public override void OnClientAreaChanged(Rectangle oValue, Rectangle nValue)
        {
            base.OnClientAreaChanged(oValue, nValue);

            if (Panel == null) return;

            Panel.Location = ClientArea.Location;
            Panel.Size = ClientArea.Size;
        }
        public override void OnIsVisibleChanged(bool oValue, bool nValue)
        {
            base.OnIsVisibleChanged(oValue, nValue);

            SelectedInfo = IsVisible ? GameScene.Game.MapControl.MapInfo : null;

        }
        public override void OnOpacityChanged(float oValue, float nValue)
        {
            base.OnOpacityChanged(oValue, nValue);

            foreach (DXControl control in Controls)
                control.Opacity = Opacity;

            foreach (DXControl control in MapInfoObjects.Values)
                control.Opacity = Opacity;

            if (Image != null)
            {
                Image.Opacity = Opacity;
                Image.ImageOpacity = Opacity;
            }
        }


        public override WindowType Type => WindowType.None;
        public override bool CustomSize => false;
        public override bool AutomaticVisiblity => false;

        #endregion

        public BigMapDialog()
        {
            BackColour = Color.Black;
            HasFooter = false;

            AllowResize = true;

            Panel = new DXControl
            {
                Parent = this,
                Location = Area.Location,
                Size = Area.Size
            };

            Image = new DXImageControl
            {
                Parent = Panel,
                LibraryFile = LibraryFile.MiniMap,
            };
            Image.MouseClick += Image_MouseClick;
        }

        private void Image_MouseClick(object sender, MouseEventArgs e)
        {

            //if (SelectedInfo != GameScene.Game.MapControl.MapInfo) return;

            //if (MapObject.User.Buffs.All(z => z.Type != BuffType.Developer))
            //if (!SelectedInfo.AllowRT || !SelectedInfo.AllowTT || !GameScene.Game.MapControl.MapInfo.AllowRT || !GameScene.Game.MapControl.MapInfo.AllowTT) return;


            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                //TODO Teleport Ring
                uint x = (uint)((e.Location.X - Image.DisplayArea.X) / ScaleX);
                uint y = (uint)((e.Location.Y - Image.DisplayArea.Y) / ScaleY);
               
                CEnvir.Enqueue(new C.TeleportRing { LocationX = x, LocationY = y, Index = SelectedInfo.Index });
            }
        }

        #region Methods
        public override void Draw()
        {
            if (!IsVisible || Size.Width == 0 || Size.Height == 0) return;

            OnBeforeDraw();
            DrawControl();
            OnBeforeChildrenDraw();
            DrawChildControls();
            DrawWindow();
            TitleLabel.Draw();
            DrawBorder();
            OnAfterDraw();
        }
        public void Update(NPCInfo ob)
        {
            if (SelectedInfo == null) return;

            DXControl control;

            if (!MapInfoObjects.TryGetValue(ob, out control))
            {
                if (ob.Region?.Map != SelectedInfo) return;

                control = GameScene.Game.GetNPCControl(ob);
                control.Parent = Image;
                control.Opacity = Opacity;
                MapInfoObjects[ob] = control;
            }
            else if ((QuestIcon)control.Tag == ob.CurrentIcon) return;

                control.Dispose();
                MapInfoObjects.Remove(ob);
            if (ob.Region?.Map != SelectedInfo)  return;

            control = GameScene.Game.GetNPCControl(ob);
            control.Parent = Image;
            control.Opacity = Opacity;
            MapInfoObjects[ob] = control;

            Size size = GetMapSize(SelectedInfo.FileName);

            if (ob.Region.PointList == null)
                ob.Region.CreatePoints(size.Width);

            int minX = size.Width, maxX = 0, minY = size.Height, maxY = 0;

            foreach (Point point in ob.Region.PointList)
            {
                if (point.X < minX)
                    minX = point.X;
                if (point.X > maxX)
                    maxX = point.X;

                if (point.Y < minY)
                    minY = point.Y;
                if (point.Y > maxY)
                    maxY = point.Y;
            }

            int x = (minX + maxX) / 2;
            int y = (minY + maxY) / 2;


            control.Location = new Point((int)(ScaleX * x) - control.Size.Width / 2, (int)(ScaleY * y) - control.Size.Height / 2);
        }
        public void Update(MovementInfo ob)
        {
            if (ob.SourceRegion == null || ob.SourceRegion.Map != SelectedInfo) return;
            if (ob.DestinationRegion?.Map == null || ob.Icon == MapIcon.None) return;

            Size size = GetMapSize(SelectedInfo.FileName);

            if (ob.SourceRegion.PointList == null)
                ob.SourceRegion.CreatePoints(size.Width);

            int minX = size.Width, maxX = 0, minY = size.Height, maxY = 0;

            foreach (Point point in ob.SourceRegion.PointList)
            {
                if (point.X < minX)
                    minX = point.X;
                if (point.X > maxX)
                    maxX = point.X;

                if (point.Y < minY)
                    minY = point.Y;
                if (point.Y > maxY)
                    maxY = point.Y;
            }

            int x = (minX + maxX)/2;
            int y = (minY + maxY)/2;


            DXImageControl control;
            MapInfoObjects[ob] = control = new DXImageControl
            {
                LibraryFile = LibraryFile.Interface,
                Parent = Image,
                Opacity =  Opacity,
                ImageOpacity =  Opacity,
                Hint = ob.DestinationRegion.Map.Description,
            };
            control.OpacityChanged += (o, e) => control.ImageOpacity = control.Opacity;

            switch (ob.Icon)
            {
                case MapIcon.Cave:
                    control.Index = 70;
                    control.ForeColour = Color.Red;
                    break;
                case MapIcon.Exit:
                    control.Index = 70;
                    control.ForeColour = Color.Green;
                    break;
                case MapIcon.Down:
                    control.Index = 70;
                    control.ForeColour = Color.MediumVioletRed;
                    break;
                case MapIcon.Up:
                    control.Index = 70;
                    control.ForeColour = Color.DeepSkyBlue;
                    break;
                case MapIcon.Province:
                    control.Index = 6125;
                    control.LibraryFile = LibraryFile.GameInter;
                    break;
                case MapIcon.Building:
                    control.Index = 6124;
                    control.LibraryFile = LibraryFile.GameInter;
                    break;
            }
            control.MouseClick += (o, e) => SelectedInfo = ob.DestinationRegion.Map;
            control.Location = new Point((int) (ScaleX*x) - control.Size.Width/2, (int) (ScaleY*y) - control.Size.Height/2);
        }
        public void Update(ClientObjectData ob)
        {
            if (SelectedInfo == null) return;


            DXControl control;

            if (!MapInfoObjects.TryGetValue(ob, out control))
            {
                if (ob.MapIndex != SelectedInfo.Index) return;
                if (ob.ItemInfo != null && ob.ItemInfo.Rarity == Rarity.Common) return;
                if (ob.MonsterInfo != null && ob.Dead) return;


                MapInfoObjects[ob] = control = new DXControl
                {
                    DrawTexture = true,
                    Parent = Image,
                    Opacity =  Opacity,
                };


            }
            else if (ob.MapIndex != SelectedInfo.Index || (ob.MonsterInfo != null && ob.Dead) || (ob.ItemInfo != null && ob.ItemInfo.Rarity == Rarity.Common))
            {
                control.Dispose();
                MapInfoObjects.Remove(ob);
                return;
            }
            
            Size size = new Size(3, 3);
            Color colour = Color.White;
            string name = ob.Name;

            if (ob.MonsterInfo != null)
            {

                name = $"{ob.MonsterInfo.MonsterName}";
                if (ob.MonsterInfo.AI < 0)
                {
                    colour = Color.LightBlue;
                }
                else
                {
                    colour = Color.Red;

                    if (GameScene.Game.HasQuest(ob.MonsterInfo, GameScene.Game.MapControl.MapInfo))
                        colour = Color.Orange;
                }

                if (ob.MonsterInfo.IsBoss)
                {
                    size = new Size(5, 5);

                    if (control.Controls.Count == 0) // This is disgusting but its cheap
                    {
                        new DXControl
                        {
                            Parent = control,
                            Location = new Point(1, 1),
                            BackColour = colour,
                            DrawTexture = true,
                            Size = new Size(3, 3)
                        };
                    }
                    else
                        control.Controls[0].BackColour = colour;

                    colour = Color.White;

                }

                if (!string.IsNullOrEmpty(ob.PetOwner))
                {
                    name += $" ({ob.PetOwner})";
                    control.DrawTexture = false;
                }
            }
            else if (ob.ItemInfo != null)
            {
                colour = Color.DarkBlue;
            }
            else
            {
                if (MapObject.User.ObjectID == ob.ObjectID)
                {
                    colour = Color.Cyan;
                }
                else if (GameScene.Game.Observer)
                {
                    control.Visible = false;
                }
                else if (GameScene.Game.GroupBox.Members.Any(x => x.ObjectID == ob.ObjectID))
                {
                    colour = Color.Blue;
                }
                else if (GameScene.Game.Partner != null && GameScene.Game.Partner.ObjectID == ob.ObjectID) 
                {
                    colour = Color.DeepPink;
                }
                else if (GameScene.Game.GuildBox.GuildInfo != null && GameScene.Game.GuildBox.GuildInfo.Members.Any(x => x.ObjectID == ob.ObjectID))
                {
                    colour = Color.DeepSkyBlue;
                }
            }

            control.Hint = name;
            control.BackColour = colour;
            control.Size = size;
            control.Location = new Point((int) (ScaleX*ob.Location.X) - size.Width/2, (int) (ScaleY*ob.Location.Y) - size.Height/2);
        }

        public void Remove(object ob)
        {
            DXControl control;

            if (!MapInfoObjects.TryGetValue(ob, out control)) return;

            control.Dispose();
            MapInfoObjects.Remove(ob);
        }
        
        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _SelectedInfo = null;
                SelectedInfoChanged = null;

                Area = Rectangle.Empty;
                ScaleX = 0;
                ScaleY = 0;

                foreach (KeyValuePair<object, DXControl> pair in MapInfoObjects)
                {
                    if (pair.Value == null) continue;
                    if (pair.Value.IsDisposed) continue;

                    pair.Value.Dispose();
                }

                MapInfoObjects.Clear();
                MapInfoObjects = null;


                if (Image != null)
                {
                    if (!Image.IsDisposed)
                        Image.Dispose();

                    Image = null;
                }
                
                if (Panel != null)
                {
                    if (!Panel.IsDisposed)
                        Panel.Dispose();

                    Panel = null;
                }
            }
        }

        #endregion
    }
}
