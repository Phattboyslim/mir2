using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirScenes;
using Client.MirSounds;
using ClientPackets;
using System.Drawing;
using System.Linq;

namespace Client.MirScenes.Dialogs
{
    public sealed class GroupFinderButtonDialog : MirImageControl
    {
        public MirButton GroupFinderButton;

        public GroupFinderButtonDialog()
        {
            Size = new Size(40, 19);
            Location = new Point((GameScene.Scene.MiniMapDialog.Location.X - 40), 60);

            GroupFinderButton = new MirButton()
            {
                Index = 2173,
                Library = Libraries.Prguse,
                Parent = this,
                Size = new Size(20, 19),
                Location = new Point(20, 0),
                HoverIndex = 2174,
                PressedIndex = 2175,
                Sound = SoundList.ButtonA,
                Hint = GameLanguage.GroupFinder
            };

            Network.Enqueue(new GroupFinderRefresh());

            GroupFinderButton.Click += (o, e) =>
            {
                if (GameScene.Scene.GroupFinderDialog.Visible == true)
                {
                    GameScene.Scene.GroupFinderDialog.Hide();
                }
                else
                {
                    var userHasGroupFinder = GameScene.Scene.GroupFinderDialog.GroupFinderDetails.Any(x => x.PlayerName == GameScene.User.Name);
                    var userIsInGroupFinder = GameScene.Scene.GroupFinderDialog.GroupFinderDetails.Any(x=> x.GroupMemberNames.Any(name => name == GameScene.User.Name));

                    GameScene.Scene.GroupFinderDialog.Show(!userHasGroupFinder && !userIsInGroupFinder);
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
}