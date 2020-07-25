using Client.MirControls;
using Client.MirGraphics;
using Client.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirScenes.Dialogs
{
    public sealed class GroupFinderDialog : MirImageControl
    {
        public MirImageControl TitleLabel;
        public MirButton CloseButton, CreateButton;
        public List<GroupFinderDetail> GroupFinderDetails = new List<GroupFinderDetail>();

        public GroupFinderDialog()
        {
            Index = 782;
            Library = Libraries.Prguse3;
            Sort = true;
            Movable = true;

            TitleLabel = new MirImageControl
            {
                Index = 24,
                Library = Libraries.Title,
                Location = new Point(18, 9),
                Parent = this
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(662, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };

            CloseButton.Click += (o, e) => Hide();

            CreateButton = new MirButton
            {
                Index = 199,
                HoverIndex = 200,
                PressedIndex = 201,
                Location = new Point(18, 444),
                Library = Libraries.Prguse3,
                Parent = this,
                Sound = SoundList.ButtonA,
            };

            CreateButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupFinderFormDialog.Visible == true)
                {
                    GameScene.Scene.GroupFinderFormDialog.Hide();
                }
                else
                {
                    GameScene.Scene.GroupFinderFormDialog.Show();
                }
            };
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
    public class GroupFinderDetail
    {
        public Guid Id { get; set; }
        public int MinimumLevel { get; set; }
        public string PlayerName { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
        public int PlayerLimit { get; set; }
        public string Message { get; set; }
    }
    public class GroupFinderForm
    {
        public int MinimumLevel { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public int PlayerLimit { get; set; }
    }
}
