using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.MirScenes.Dialogs
{
    public sealed class GroupFinderFormDialog : MirImageControl
    {
        MirButton CloseButton;
        MirTextBox TitleTextBox, MinimumLevelTextBox, GroupSizeTextBox, DescriptionTextBox;
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
                Location = new Point(180, 5),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };

            CloseButton.Click += (o, e) => Hide();

            TitleTextBox = new MirTextBox
            {
                Location = new Point(38, 78),
                Parent = this,
                Size = new Size(140, 15),
                MaxLength = 20,
                CanLoseFocus = true
            };

            MinimumLevelTextBox = new MirTextBox
            {
                Location = new Point(58, 128),
                Parent = this,
                Size = new Size(35, 15),
                MaxLength = 20,
                CanLoseFocus = true
            };

            GroupSizeTextBox = new MirTextBox
            {
                Location = new Point(134, 128),
                Parent = this,
                Size = new Size(35, 15),
                MaxLength = 20,
                CanLoseFocus = true
            };

            DescriptionTextBox = new MirTextBox
            {
                Location = new Point(40, 178),
                Parent = this,
                Size = new Size(160, 100),
                MaxLength = 20,
                CanLoseFocus = true
            };

            TitleTextBox.TextBox.KeyPress += TitleTextBox_KeyPress;
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
        private void TitleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
