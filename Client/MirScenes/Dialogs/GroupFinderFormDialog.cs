using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class GroupFinderFormDialog : MirImageControl
    {
        public static long SearchTime;

        public MirButton CloseButton, CreateButton;
        public MirTextBox TitleTextBox, MinimumLevelTextBox, GroupSizeTextBox, DescriptionTextBox;
        

        public GroupFinderFormDialog()
        {
            Index = 783;
            Library = Libraries.Prguse3;
            Sort = true;
            Location = new Point(300, 300);
            Movable = true;

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(200, 8),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };

            CloseButton.Click += (o, e) => Hide();

            MinimumLevelTextBox = new MirTextBox
            {
                Location = new Point(58, 128),
                Parent = this,
                Size = new Size(38, 15),
                MaxLength = 20,
                CanLoseFocus = true,
                Text = "150"
            };

            MinimumLevelTextBox.KeyPress += MinimumLevelTextBox_KeyPress;
            MinimumLevelTextBox.TextBox.TextChanged += MinimumLevelTextBox_TextChanged;
            MinimumLevelTextBox.TextBox.LostFocus += MinimumLevelTextBox_LostFocus;

            GroupSizeTextBox = new MirTextBox
            {
                Location = new Point(135, 128),
                Parent = this,
                Size = new Size(38, 15),
                MaxLength = 20,
                CanLoseFocus = true,
                Text = "2"
            };

            GroupSizeTextBox.KeyPress += GroupSizeTextBox_KeyPress;
            GroupSizeTextBox.TextBox.TextChanged += GroupSizeTextBox_TextChanged;
            GroupSizeTextBox.TextBox.LostFocus += GroupSizeTextBox_LostFocus;
            DescriptionTextBox = new MirTextBox
            {
                Location = new Point(38, 178),
                Parent = this,
                Size = new Size(160, 15),
                MaxLength = 24,
                CanLoseFocus = true
            };

            CreateButton = new MirButton
            {
                Index = 199,
                HoverIndex = 200,
                PressedIndex = 201,
                Location = new Point(40, 278),
                Library = Libraries.Prguse3,
                Parent = this,
                Sound = SoundList.ButtonA,
            };

            CreateButton.Click += CreateButton_Click;

            TitleTextBox = new MirTextBox
            {
                Location = new Point(38, 74),
                Parent = this,
                Size = new Size(160, 15),
                MaxLength = 16,
                CanLoseFocus = true
            };

        }

        private void MinimumLevelTextBox_LostFocus(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int minimumLevel;
            if (int.TryParse(textBox.Text, out minimumLevel))
            {
                if (minimumLevel < 150)
                {
                    minimumLevel = 150;
                }
            }
            textBox.Text = minimumLevel.ToString();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void GroupSizeTextBox_LostFocus(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int groupSize;
            if (int.TryParse(textBox.Text, out groupSize))
            {
                if (groupSize < 2)
                {
                    groupSize = 2;
                }
            }
            textBox.Text = groupSize.ToString();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void MinimumLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int minimumLevel;
            if (int.TryParse(textBox.Text, out minimumLevel))
            {
                if (minimumLevel > 330)
                {
                    minimumLevel = 330;
                }
            }
            textBox.Text = minimumLevel.ToString();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void GroupSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int groupSize;
            if (int.TryParse(textBox.Text, out groupSize))
            {
                if(groupSize > Globals.MaxGroup)
                {
                    groupSize = Globals.MaxGroup;
                }
            }
            textBox.Text = groupSize.ToString();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void GroupSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void MinimumLevelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text) ||
                string.IsNullOrEmpty(MinimumLevelTextBox.Text) ||
                string.IsNullOrEmpty(GroupSizeTextBox.Text) ||
                string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                return;
            }

            if (GameScene.Scene.GroupFinderDialog.Visible)
                GameScene.Scene.GroupFinderDialog.Hide();

            Hide();

            Network.Enqueue(new C.AddGroupFinder
            {
                Id = Guid.NewGuid(),
                Title = TitleTextBox.Text,
                MinimumLevel = int.Parse(MinimumLevelTextBox.Text),
                PlayerLimit = int.Parse(GroupSizeTextBox.Text),
                Description = DescriptionTextBox.Text,
                Created = DateTime.Now,
                PlayerName = MainDialog.User.Name
            });

            Network.Enqueue(new C.GroupFinderRefresh());
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }
    }
}
