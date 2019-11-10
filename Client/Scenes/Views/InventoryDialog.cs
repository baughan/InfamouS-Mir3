using System.Drawing;
using System.Windows.Forms;
using Client.Controls;
using Client.Envir;
using Client.Models;
using Client.UserModels;
using Library;

//Cleaned
namespace Client.Scenes.Views
{
    public sealed class InventoryDialog : DXWindow
    {
        #region Properties

        public DXItemGrid Grid;

        public DXLabel GoldLabel, WeightLabel;
        public DXButton FragmentButton;
        public override void OnIsVisibleChanged(bool oValue, bool nValue)
        {
            if (!IsVisible)
                Grid.ClearLinks();

            base.OnIsVisibleChanged(oValue, nValue);
        }

        public override WindowType Type => WindowType.InventoryBox;
        public override bool CustomSize => false;
        public override bool AutomaticVisiblity => true;

        #endregion

        public InventoryDialog()
        {
            TitleLabel.Text = "Inventory";
            
            Grid = new DXItemGrid
            {
                GridSize = new Size(14, 14),
                Parent = this,
                ItemGrid = GameScene.Game.Inventory,
                GridType = GridType.Inventory
            };

            SetClientSize(new Size(Grid.Size.Width, Grid.Size.Height+ 45));
            Grid.Location = ClientArea.Location;


            GoldLabel = new DXLabel
            {
                AutoSize = false,
                Border = true,
                BorderColour = Color.FromArgb(99, 83, 50),
                ForeColour = Color.White,
                DrawFormat = TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(ClientArea.Left + 80, ClientArea.Bottom - 41),
                Text = "0",
                Size = new Size(ClientArea.Width - 81, 20),
                Sound = SoundIndex.GoldPickUp
            };
            GoldLabel.MouseClick += GoldLabel_MouseClick;

            new DXLabel
            {
                AutoSize = false,
                Border = true,
                BorderColour = Color.FromArgb(99, 83, 50),
                ForeColour = Color.White,
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(ClientArea.Left + 1, ClientArea.Bottom - 41),
                Text = "Gold",
                Size = new Size(78, 20),
                IsControl = false,
            };


            WeightLabel = new DXLabel
            {
                AutoSize = false,
                Border = true,
                BorderColour = Color.FromArgb(99, 83, 50),
                ForeColour = Color.White,
                DrawFormat = TextFormatFlags.VerticalCenter,
                Parent = this,
                Location = new Point(ClientArea.Left + 80, ClientArea.Bottom - 20),
                Text = "0",
                Size = new Size(ClientArea.Width - 81, 20),
                Sound = SoundIndex.GoldPickUp
            };

            new DXLabel
            {
                AutoSize = false,
                Border = true,
                BorderColour = Color.FromArgb(99, 83, 50),
                ForeColour = Color.White,
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                Location = new Point(ClientArea.Left + 1, ClientArea.Bottom - 20),
                Text = "Weight",
                Size = new Size(78, 20),
                IsControl = false,
            };

            FragmentButton = new DXButton
            {
                Location = new Point(ClientArea.Right - 100, Size.Height - 41),
                Size = new Size(60, DefaultHeight),
                Parent = this,
                Label = { Text = "Fragment" }
            };
            FragmentButton.MouseClick += (o, e) =>
            {
                if (!GameScene.Game.NPCItemFragmentBox.Visible)
                {
                    GameScene.Game.NPCItemFragmentBox.Visible = true;
                    if (DisplayArea.Left - GameScene.Game.NPCItemFragmentBox.Size.Width < 0)
                        GameScene.Game.NPCItemFragmentBox.Location = new Point(DisplayArea.Right + 1, DisplayArea.Top);
                    else
                        GameScene.Game.NPCItemFragmentBox.Location = new Point(DisplayArea.Left - GameScene.Game.NPCItemFragmentBox.Size.Width, DisplayArea.Top);
                }
            };
        }

        #region Methods
        private void GoldLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (GameScene.Game.SelectedCell == null)
                GameScene.Game.GoldPickedUp = !GameScene.Game.GoldPickedUp && MapObject.User.Gold > 0;
        }
        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (Grid != null)
                {
                    if (!Grid.IsDisposed)
                        Grid.Dispose();

                    Grid = null;
                }

                if (GoldLabel != null)
                {
                    if (!GoldLabel.IsDisposed)
                        GoldLabel.Dispose();

                    GoldLabel = null;
                }

                if (WeightLabel != null)
                {
                    if (!WeightLabel.IsDisposed)
                        WeightLabel.Dispose();

                    WeightLabel = null;
                }

                if (FragmentButton != null)
                {
                    if (!FragmentButton.IsDisposed)
                        FragmentButton.Dispose();

                    FragmentButton = null;
                }
            }

        }

        #endregion
    }

}