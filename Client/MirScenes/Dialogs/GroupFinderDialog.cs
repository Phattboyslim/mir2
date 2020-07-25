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
        public MirButton CloseButton;

        public GroupFinderDialog()
        {
            Index = 782;
            Library = Libraries.Prguse3;
            Sort = true;

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
                Location = new Point(462, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();
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
