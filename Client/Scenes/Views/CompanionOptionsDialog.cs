using System;
using System.Drawing;
using Client.Controls;
using Client.Envir;
using Client.UserModels;
using Library;
using C = Library.Network.ClientPackets;

//Cleaned
namespace Client.Scenes.Views
{
    public sealed class CompanionOptionsDialog : DXWindow
    {
        #region Properties
        public override WindowType Type => WindowType.CompanionOptions;
        public override bool CustomSize => false;
        public override bool AutomaticVisiblity => true;

        public DXLabel TypeFilterLabel, GradeFilterLabel, BookLabel;
        public Point CheckBoxRightPoint;
        public DXCheckBox GoldCheckBox, WeaponCheckBox, ArmourCheckBox, HelmetCheckBox, ShieldCheckBox, NecklaceCheckBox, BraceletCheckBox, RingCheckBox, ShoesCheckBox, PotionCheckBox, OreCheckBox, CommonCheckBox, EliteCheckBox, SuperiorCheckBox, BookWarriorCheckBox, BookWizardCheckBox, BookTaoistCheckBox, BookAssassinCheckBox, EmblemsCheckBox, WingsCheckBox;

        #endregion

        public CompanionOptionsDialog()
        {
            TitleLabel.Text = "Companion Options";
            SetClientSize(new Size(182, 341));
            Movable = false;

            TypeFilterLabel = new DXLabel
            {
                Parent = this,
                Outline = true,
                Font = new Font(Config.FontName, CEnvir.FontSize(10F), FontStyle.Bold),
                ForeColour = Color.FromArgb(198, 166, 99),
                OutlineColour = Color.Black,
                IsControl = false,
                Text = "Item Type Filter:",
            };
            TypeFilterLabel.Location = new Point(ClientArea.Left + 3, ClientArea.Y + 1);
            CheckBoxRightPoint = new Point(TypeFilterLabel.Size.Width, 0);
            int i = 22;
            int gap = 16;

            GoldCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Gold:" },
                Visible = true
            };
            GoldCheckBox.Location = new Point(CheckBoxRightPoint.X - GoldCheckBox.Size.Width, ClientArea.Y + i);
            GoldCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Gold });
            };
            i += gap;

            WeaponCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Weapon:" },
                Visible = true
            };
            WeaponCheckBox.Location = new Point(CheckBoxRightPoint.X - WeaponCheckBox.Size.Width, ClientArea.Y + i);
            WeaponCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Weapon });
            };
            i += gap;

            ArmourCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Armour:" },
                Visible = true
            };
            ArmourCheckBox.Location = new Point(CheckBoxRightPoint.X - ArmourCheckBox.Size.Width, ClientArea.Y + i);
            ArmourCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Armour });
            };
            i += gap;

            HelmetCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Helmet:" },
                Visible = true
            };
            HelmetCheckBox.Location = new Point(CheckBoxRightPoint.X - HelmetCheckBox.Size.Width, ClientArea.Y + i);
            HelmetCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Helmet });
            };
            i += gap;

            ShieldCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Shield:" },
                Visible = true
            };
            ShieldCheckBox.Location = new Point(CheckBoxRightPoint.X - ShieldCheckBox.Size.Width, ClientArea.Y + i);
            ShieldCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Shield });
            };
            i += gap;

            NecklaceCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Necklace:" },
                Visible = true
            };
            NecklaceCheckBox.Location = new Point(CheckBoxRightPoint.X - NecklaceCheckBox.Size.Width, ClientArea.Y + i);
            NecklaceCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Necklace });
            };
            i += gap;

            BraceletCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Bracelet:" },
                Visible = true
            };
            BraceletCheckBox.Location = new Point(CheckBoxRightPoint.X - BraceletCheckBox.Size.Width, ClientArea.Y + i);
            BraceletCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Bracelet });
            };
            i += gap;

            RingCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Ring:" },
                Visible = true
            };
            RingCheckBox.Location = new Point(CheckBoxRightPoint.X - RingCheckBox.Size.Width, ClientArea.Y + i);
            RingCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Ring });
            };
            i += gap;

            ShoesCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Shoes:" },
                Visible = true
            };
            ShoesCheckBox.Location = new Point(CheckBoxRightPoint.X - ShoesCheckBox.Size.Width, ClientArea.Y + i);
            ShoesCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Shoes });
            };
            i += gap;

            EmblemsCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Emblems:" },
                Visible = true
            };
            EmblemsCheckBox.Location = new Point(CheckBoxRightPoint.X - EmblemsCheckBox.Size.Width, ClientArea.Y + i);
            EmblemsCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Emblem });
            };
            i += gap;

            WingsCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Wings:" },
                Visible = true
            };
            WingsCheckBox.Location = new Point(CheckBoxRightPoint.X - WingsCheckBox.Size.Width, ClientArea.Y + i);
            WingsCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Wings });
            };
            i += gap;

            BookLabel = new DXLabel
            {
                Parent = this,
                ForeColour = Color.White,
                IsControl = false,
                Text = "Book:",
            };
            BookLabel.Location = new Point(CheckBoxRightPoint.X - BookLabel.Size.Width - 16, ClientArea.Y + i);

            BookWarriorCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "" },
                Hint = "Warrior",
                Visible = true
            };
            BookWarriorCheckBox.Location = new Point(CheckBoxRightPoint.X - 16, ClientArea.Y + i);
            BookWarriorCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Book, Class = RequiredClass.Warrior });
            };

            BookWizardCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "" },
                Hint = "Wizard",
                Visible = true
            };
            BookWizardCheckBox.Location = new Point(CheckBoxRightPoint.X + BookWarriorCheckBox.Size.Width - 16, ClientArea.Y + i);
            BookWizardCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Book, Class = RequiredClass.Wizard });
            };

            BookTaoistCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "" },
                Hint = "Taoist",
                Visible = true
            };
            BookTaoistCheckBox.Location = new Point(CheckBoxRightPoint.X + BookWarriorCheckBox.Size.Width + BookWizardCheckBox.Size.Width - 16, ClientArea.Y + i);
            BookTaoistCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Book, Class = RequiredClass.Taoist });
            };

            BookAssassinCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "" },
                Hint = "Assassin",
                Visible = true
            };
            BookAssassinCheckBox.Location = new Point(CheckBoxRightPoint.X + BookWarriorCheckBox.Size.Width + BookWizardCheckBox.Size.Width + BookTaoistCheckBox.Size.Width - 16, ClientArea.Y + i);
            BookAssassinCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Book, Class = RequiredClass.Assassin });
            };
            i += gap;

            PotionCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Potion:" },
                Visible = true
            };
            PotionCheckBox.Location = new Point(CheckBoxRightPoint.X - PotionCheckBox.Size.Width, ClientArea.Y + i);
            PotionCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Consumable });
            };
            i += gap;

            OreCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Ore:" },
                Visible = true
            };
            OreCheckBox.Location = new Point(CheckBoxRightPoint.X - OreCheckBox.Size.Width, ClientArea.Y + i);
            OreCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupToggle { Type = ItemType.Ore });
            };
            i += gap;


            i += gap;
            GradeFilterLabel = new DXLabel
            {
                Parent = this,
                Outline = true,
                Font = new Font(Config.FontName, CEnvir.FontSize(10F), FontStyle.Bold),
                ForeColour = Color.FromArgb(198, 166, 99),
                OutlineColour = Color.Black,
                IsControl = false,
                Text = "Item Grade Filter:",
            };
            GradeFilterLabel.Location = new Point(ClientArea.Left + 3, ClientArea.Y + i);
            CheckBoxRightPoint = new Point(GradeFilterLabel.Size.Width, 0);
            i += 22;

            CommonCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Common:" },
                Visible = true
            };
            CommonCheckBox.Location = new Point(CheckBoxRightPoint.X - CommonCheckBox.Size.Width, ClientArea.Y + i);
            CommonCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupGradeToggle { Grade = Rarity.Common });
            };
            i += gap;

            SuperiorCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Superior:" },
                Visible = true
            };
            SuperiorCheckBox.Location = new Point(CheckBoxRightPoint.X - SuperiorCheckBox.Size.Width, ClientArea.Y + i);
            SuperiorCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupGradeToggle { Grade = Rarity.Superior });
            };
            i += gap;

            EliteCheckBox = new DXCheckBox
            {
                Parent = this,
                ForeColour = Color.White,
                Label = { Text = "Elite:" },
                Visible = true
            };
            EliteCheckBox.Location = new Point(CheckBoxRightPoint.X - EliteCheckBox.Size.Width, ClientArea.Y + i);
            EliteCheckBox.MouseClick += (o, e) =>
            {
                CEnvir.Enqueue(new C.CompanionPickupGradeToggle { Grade = Rarity.Elite });
            };
            i += gap;
        }

        #region Methods
        public void Draw(DXItemCell cell, int index)
        {
            if (InterfaceLibrary == null) return;

            if (cell.Item != null) return;

            Size s = InterfaceLibrary.GetSize(index);
            int x = (cell.Size.Width - s.Width) / 2 + cell.DisplayArea.X;
            int y = (cell.Size.Height - s.Height) / 2 + cell.DisplayArea.Y;

            InterfaceLibrary.Draw(index, x, y, Color.White, false, 0.2F, ImageType.Image);
        }

        public override void Process()
        {
            base.Process();
        }

        public void Refresh()
        {
            GoldCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Gold, RequiredClass.None));
            WeaponCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Weapon, RequiredClass.None));
            ArmourCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Armour, RequiredClass.None));
            HelmetCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Helmet, RequiredClass.None));
            ShieldCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Shield, RequiredClass.None));
            NecklaceCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Necklace, RequiredClass.None));
            BraceletCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Bracelet, RequiredClass.None));
            RingCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Ring, RequiredClass.None));
            ShoesCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Shoes, RequiredClass.None));
            EmblemsCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Emblem, RequiredClass.None));
            WingsCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Wings, RequiredClass.None));
            BookWarriorCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Book, RequiredClass.Warrior));
            BookWizardCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Book, RequiredClass.Wizard));
            BookTaoistCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Book, RequiredClass.Taoist));
            BookAssassinCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Book, RequiredClass.Assassin));
            PotionCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Consumable, RequiredClass.None));
            OreCheckBox.Checked = !GameScene.Game.CompanionForbiddenItems.Contains(Tuple.Create(ItemType.Meat, RequiredClass.None));

            CommonCheckBox.Checked = !GameScene.Game.CompanionForbiddenGrades.Contains(Rarity.Common);
            EliteCheckBox.Checked = !GameScene.Game.CompanionForbiddenGrades.Contains(Rarity.Elite);
            SuperiorCheckBox.Checked = !GameScene.Game.CompanionForbiddenGrades.Contains(Rarity.Superior);
        }

        protected override void OnBeforeDraw()
        {
            base.OnBeforeDraw();

            Location = new Point(GameScene.Game.CompanionBox.Location.X + GameScene.Game.CompanionBox.Size.Width + 1, GameScene.Game.CompanionBox.Location.Y);
        }
        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                CheckBoxRightPoint = Point.Empty;

                if (TypeFilterLabel != null)
                {
                    if (!TypeFilterLabel.IsDisposed)
                        TypeFilterLabel.Dispose();

                    TypeFilterLabel = null;
                }

                if (GradeFilterLabel != null)
                {
                    if (!GradeFilterLabel.IsDisposed)
                        GradeFilterLabel.Dispose();

                    GradeFilterLabel = null;
                }

                if (BookLabel != null)
                {
                    if (!BookLabel.IsDisposed)
                        BookLabel.Dispose();

                    BookLabel = null;
                }

                if (WeaponCheckBox != null)
                {
                    if (!WeaponCheckBox.IsDisposed)
                        WeaponCheckBox.Dispose();

                    WeaponCheckBox = null;
                }

                if (ArmourCheckBox != null)
                {
                    if (!ArmourCheckBox.IsDisposed)
                        ArmourCheckBox.Dispose();

                    ArmourCheckBox = null;
                }

                if (HelmetCheckBox != null)
                {
                    if (!HelmetCheckBox.IsDisposed)
                        HelmetCheckBox.Dispose();

                    HelmetCheckBox = null;
                }

                if (ShieldCheckBox != null)
                {
                    if (!ShieldCheckBox.IsDisposed)
                        ShieldCheckBox.Dispose();

                    ShieldCheckBox = null;
                }

                if (NecklaceCheckBox != null)
                {
                    if (!NecklaceCheckBox.IsDisposed)
                        NecklaceCheckBox.Dispose();

                    NecklaceCheckBox = null;
                }

                if (BraceletCheckBox != null)
                {
                    if (!BraceletCheckBox.IsDisposed)
                        BraceletCheckBox.Dispose();

                    BraceletCheckBox = null;
                }

                if (RingCheckBox != null)
                {
                    if (!RingCheckBox.IsDisposed)
                        RingCheckBox.Dispose();

                    RingCheckBox = null;
                }

                if (ShoesCheckBox != null)
                {
                    if (!ShoesCheckBox.IsDisposed)
                        ShoesCheckBox.Dispose();

                    ShoesCheckBox = null;
                }

                if (BookWarriorCheckBox != null)
                {
                    if (!BookWarriorCheckBox.IsDisposed)
                        BookWarriorCheckBox.Dispose();

                    BookWarriorCheckBox = null;
                }

                if (BookWizardCheckBox != null)
                {
                    if (!BookWizardCheckBox.IsDisposed)
                        BookWizardCheckBox.Dispose();

                    BookWizardCheckBox = null;
                }

                if (BookTaoistCheckBox != null)
                {
                    if (!BookTaoistCheckBox.IsDisposed)
                        BookTaoistCheckBox.Dispose();

                    BookTaoistCheckBox = null;
                }

                if (BookAssassinCheckBox != null)
                {
                    if (!BookAssassinCheckBox.IsDisposed)
                        BookAssassinCheckBox.Dispose();

                    BookAssassinCheckBox = null;
                }

                if (PotionCheckBox != null)
                {
                    if (!PotionCheckBox.IsDisposed)
                        PotionCheckBox.Dispose();

                    PotionCheckBox = null;
                }

                if (OreCheckBox != null)
                {
                    if (!OreCheckBox.IsDisposed)
                        OreCheckBox.Dispose();

                    OreCheckBox = null;
                }

                if (GoldCheckBox != null)
                {
                    if (!GoldCheckBox.IsDisposed)
                        GoldCheckBox.Dispose();

                    GoldCheckBox = null;
                }

                if (CommonCheckBox != null)
                {
                    if (!CommonCheckBox.IsDisposed)
                        CommonCheckBox.Dispose();

                    CommonCheckBox = null;
                }

                if (EliteCheckBox != null)
                {
                    if (!EliteCheckBox.IsDisposed)
                        EliteCheckBox.Dispose();

                    EliteCheckBox = null;
                }

                if (SuperiorCheckBox != null)
                {
                    if (!SuperiorCheckBox.IsDisposed)
                        SuperiorCheckBox.Dispose();

                    SuperiorCheckBox = null;
                }
            }

        }

        #endregion
    }
}
