using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using C = ClientPackets;

namespace Server.MirDatabase
{
    public class GroupFinderInfo
    {
        public Guid Id = Guid.Empty;
        public int MinimumLevel = 0;
        public string PlayerName = string.Empty;
        public string Title = string.Empty;
        public DateTime Created = new DateTime(1970,1,1);
        public string Description = string.Empty;
        public int PlayerLimit = 0;
        public GroupFinderInfo()
        {

        }
        public GroupFinderInfo(C.AddGroupFinder p)
        {
            Id = p.Id;
            MinimumLevel = p.MinimumLevel;
            PlayerName = p.PlayerName;
            Title = p.Title;
            Created = p.Created;
            Description = p.Description;
            PlayerLimit = p.PlayerLimit;
        }
        public GroupFinderInfo(BinaryReader reader)
        {
            Id = new Guid(reader.ReadString());
            MinimumLevel = reader.ReadInt32();
            PlayerName = reader.ReadString();
            Title = reader.ReadString();
            Created = DateTime.FromBinary(reader.ReadInt64());
            Description = reader.ReadString();
            PlayerLimit = reader.ReadInt32();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(Id.ToString());
            writer.Write(MinimumLevel);
            writer.Write(PlayerName);
            writer.Write(Title);
            writer.Write(Created.ToBinary());
            writer.Write(Description);
            writer.Write(PlayerLimit);
        }
    }
}
