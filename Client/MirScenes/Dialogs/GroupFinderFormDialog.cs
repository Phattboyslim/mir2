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

        private MirButton CloseButton, CreateButton;
        private MirTextBox TitleTextBox, MinimumLevelTextBox, GroupSizeTextBox, DescriptionTextBox;
        
        private GameScene GameScene;

        public GroupFinderFormDialog()
        {
            Index = 783;
            Library = Libraries.Prguse3;
            Sort = true;
            Location = new Point(300, 300);
            Movable = true;
            GameScene = (GameScene)Parent;

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

            TitleTextBox = new MirTextBox
            {
                Location = new Point(38, 74),
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
                Size = new Size(160, 15),
                MaxLength = 20,
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
                Created = DateTime.UtcNow,
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
