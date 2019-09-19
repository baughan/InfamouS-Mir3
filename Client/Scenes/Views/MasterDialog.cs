using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Client.Controls;
using Client.Envir;
using Client.UserModels;
using Library;
using C = Library.Network.ClientPackets;

//Cleaned
namespace Client.Scenes.Views
{
    public sealed  class MasterDialog : DXWindow
    {
        #region Properties

        private DXTabControl MasterTabs;
        public ClientPlayerInfo Master, Student;
        public TimeSpan Duration;
        public decimal Experience;
        public int StudentCount;

        #region MasterTab
        public DXButton AddButton, MasterRemoveButton;
        public DXLabel MasterNameLabel, MasterNameInfoLabel, MasterLengthLabel, MasterLengthInfoLabel, MasterExperienceLabel, MasterExperienceAmountLabel;

        #endregion

        #region StudentTab
        public DXButton AllowMasterButton, StudentRemoveButton;
        public DXLabel StudentNameLabel, StudentNameInfoLabel, StudentLengthLabel, StudentLengthInfoLabel, StudentExperienceLabel, StudentExperienceAmountLabel, StudentCountLabel, StudentCountAmountLabel;

        #endregion

        #region AllowMaster

        public bool AllowMaster
        {
            get => _AllowMaster;
            set
            {
                if (_AllowMaster == value) return;

                bool oldValue = _AllowMaster;
                _AllowMaster = value;

                OnAllowMasterChanged(oldValue, value);
            }
        }
        private bool _AllowMaster;
        public event EventHandler<EventArgs> AllowMasterChanged;
        public void OnAllowMasterChanged(bool oValue, bool nValue)
        {
            AllowMasterChanged?.Invoke(this, EventArgs.Empty);

            if (AllowMaster)
            {
                AllowMasterButton.Index = 122;
                AllowMasterButton.Hint = "Master Status: Allowing";
            }
            else
            {
                AllowMasterButton.Index = 142;
                AllowMasterButton.Hint = "Master Status: Not Allowing";
            }
        }

        #endregion

        public DXTab MasterTab;
        public DXTab StudentTab;      

        public override WindowType Type => WindowType.MasterBox;
        public override bool CustomSize => false;
        public override bool AutomaticVisiblity => false;

        #endregion

        public MasterDialog()
        {
            TitleLabel.Text = "Master/Student";

            SetClientSize(new Size(200, 200));

            MasterTabs = new DXTabControl
            {
                Parent = this,
                Size = ClientArea.Size,
                Location = ClientArea.Location,
            };

            MasterTab = new DXTab
            {
                TabButton =
                {
                    Label =
                    {
                        Text = "Master"
                    },
                },
                Parent = MasterTabs,
                Border = true,
            };

            StudentTab = new DXTab
            {
                TabButton =
                {
                    Label =
                    {
                        Text = "Student"
                    },
                },
                Parent = MasterTabs,
                Border = true,
            };

            AllowMasterButton = new DXButton
            {
                LibraryFile = LibraryFile.GameInter2,
                Index = 142,
                Parent = StudentTab,
                Hint = "Master Status: Not Allowing",
                Location = new Point(ClientArea.X, Size.Height - 100)
            };
            AllowMasterButton.MouseClick += (o, e) =>
            {
                if (GameScene.Game.Observer) return;

                CEnvir.Enqueue(new C.MasterSwitch { Allow = !AllowMaster });
            };

            AddButton = new DXButton
            {
                Size = new Size(60, SmallButtonHeight),
                ButtonType = ButtonType.SmallButton,
                Label = {Text = "Invite"},
                Location = new Point(ClientArea.Right - 150, Size.Height - 90),
                Parent = MasterTab,
            };
            AddButton.MouseClick += (o, e) =>
            {
                if (GameScene.Game.Observer) return;

                DXInputWindow window = new DXInputWindow("Please enter the name of the person you wish to become Master to.", "Invite Student")
                {
                    ConfirmButton = { Enabled = false },
                    Modal = true
                };
                window.ValueTextBox.TextBox.TextChanged += (o1, e1) =>
                {
                    window.ConfirmButton.Enabled = Globals.CharacterReg.IsMatch(window.ValueTextBox.TextBox.Text);
                };
                window.ConfirmButton.MouseClick += (o1, e1) =>
                {
                    CEnvir.Enqueue(new C.MasterInvite { Name = window.Value });
                };
            };

            MasterRemoveButton = new DXButton
            {
                Size = new Size(60, SmallButtonHeight),
                ButtonType = ButtonType.SmallButton,
                Label = { Text = "End" },
                Location = new Point(ClientArea.Right - 80, Size.Height - 90),
                Parent = MasterTab,
                Enabled = false,
            };
            MasterRemoveButton.MouseClick += (o, e) =>
            {
                if (GameScene.Game.Observer) return;

                DXMessageBox box = new DXMessageBox("Are you sure you want to end the Master relationship?", "Master Confirmation", DXMessageBoxButtons.YesNo);

                box.YesButton.MouseClick += (o1, e1) =>
                {
                    CEnvir.Enqueue(new C.MasterEnd { });
                };
            };

            int y = 6;
            StudentNameLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Student Name: ",
            };
            StudentNameInfoLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(10 + StudentNameLabel.DisplayArea.Width, y),
            };

            y += 15;
            StudentLengthLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Student Duration: ",
            };
            StudentLengthInfoLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(10 + StudentLengthLabel.DisplayArea.Width, y),
            };

            y += 19;
            StudentExperienceLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Bonus Experience Recieved:",
            };
            y += 15;
            StudentExperienceAmountLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(15, y),
                Text = "0",
            };

            y += 15;
            StudentCountLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Students Successfully Trained:",
            };

            y += 15;
            StudentCountAmountLabel = new DXLabel
            {
                Parent = MasterTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(15, y),
                Text = "0",
            };

            StudentRemoveButton = new DXButton
            {
                Size = new Size(60, SmallButtonHeight),
                ButtonType = ButtonType.SmallButton,
                Label = { Text = "End" },
                Location = new Point(ClientArea.Right - 80, Size.Height - 90),
                Parent = StudentTab,
                Enabled = false,
            };
            StudentRemoveButton.MouseClick += (o, e) =>
            {
                if (GameScene.Game.Observer) return;

                DXMessageBox box = new DXMessageBox("Are you sure you want to end the Master relationship?", "Master Confirmation", DXMessageBoxButtons.YesNo);

                box.YesButton.MouseClick += (o1, e1) =>
                {
                    CEnvir.Enqueue(new C.MasterEnd { });
                };
            };

            y = 6;
            MasterNameLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Master Name: ",
            };

            MasterNameInfoLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(10 + MasterNameLabel.DisplayArea.Width, y),
            };

            y += 15;
            MasterLengthLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Master Duration: ",
            };

            MasterLengthInfoLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(10 + MasterLengthLabel.DisplayArea.Width, y),
            };

            y += 19;
            MasterExperienceLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.FromArgb(198, 166, 99),
                IsControl = false,
                Location = new Point(10, y),
                Text = "Bonus Experience Recieved:",
            };

            y += 15;
            MasterExperienceAmountLabel = new DXLabel
            {
                Parent = StudentTab,
                Outline = true,
                ForeColour = Color.White,
                IsControl = false,
                Location = new Point(15, y),
                Text = "0",
            };
        }

        public override void OnVisibleChanged(bool oValue, bool nValue)
        {
            base.OnVisibleChanged(oValue, nValue);

            if (IsVisible)
            {
                if (GameScene.Game.User.Stats[Stat.Rebirth] < 4)
                {
                    MasterTab.Enabled = false;
                    StudentTab.Enabled = true;
                    MasterTabs.SelectedTab = StudentTab;
                }
                else
                {
                    MasterTab.Enabled = true;                    
                    StudentTab.Enabled = false;
                    MasterTabs.SelectedTab = MasterTab;
                }
                UpdateInterface();
            }
        }

        #region Methods
        public void UpdateInterface()
        {
            MasterNameInfoLabel.Text = Master?.Name;
            MasterLengthInfoLabel.Text = Master != null ? Duration.Days.ToString() + " days" : string.Empty;
            MasterExperienceAmountLabel.Text = Master != null ? Math.Floor(Experience).ToString() : "0";

            StudentNameInfoLabel.Text = Student?.Name;
            StudentLengthInfoLabel.Text = Student != null ? Duration.Days.ToString() + " days" : string.Empty;
            StudentExperienceAmountLabel.Text = Student != null ? Math.Floor(Experience).ToString() : "0";
            StudentCountAmountLabel.Text = StudentCount.ToString();

            AddButton.Enabled = AddButton.Parent.Enabled && Student == null;
            MasterRemoveButton.Enabled = MasterRemoveButton.Parent.Enabled && Student != null;
            StudentRemoveButton.Enabled = StudentRemoveButton.Parent.Enabled && Master != null;
        }
        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _AllowMaster = false;
                AllowMasterChanged = null;

                if (AllowMasterButton != null)
                {
                    if (!AllowMasterButton.IsDisposed)
                        AllowMasterButton.Dispose();

                    AllowMasterButton = null;
                }
                
                if (AddButton != null)
                {
                    if (!AddButton.IsDisposed)
                        AddButton.Dispose();

                    AddButton = null;
                }

                if (MasterRemoveButton != null)
                {
                    if (!MasterRemoveButton.IsDisposed)
                        MasterRemoveButton.Dispose();

                    MasterRemoveButton = null;
                }

                if (StudentRemoveButton != null)
                {
                    if (!StudentRemoveButton.IsDisposed)
                        StudentRemoveButton.Dispose();

                    StudentRemoveButton = null;
                }

                if (MasterTab != null)
                {
                    if (!MasterTab.IsDisposed)
                        MasterTab.Dispose();

                    MasterTab = null;
                }

                if (StudentTab != null)
                {
                    if (!StudentTab.IsDisposed)
                        StudentTab.Dispose();

                    StudentTab = null;
                }

                Master = null;
                Student = null;
            }

        }

        #endregion
    }


}
