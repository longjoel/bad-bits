using System;
using System.Collections.Generic;
using System.Text;

namespace BadBits.Engine.Models.Host
{
    public enum AssetType {
        Texture = 0,
        Sprite = 1,
        Sound = 2
    }
    public class Asset
    {
        public AssetType AssetType { get; set; }
        public string AssetName { get; set; }
        public string AssetPath { get; set; }
    }
}
