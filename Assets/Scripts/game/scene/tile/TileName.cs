using System.Collections.Generic;
using System.Linq;

namespace game.scene.tile
{
    public enum TileName
    {
        Grass,
        GrassTopLeft,
        GrassTop,
        GrassTop2,
        GrassTopRight,
        GrassLeft,
        GrassRight,
        GrassBottomLeft,
        GrassBottom,
        GrassBottomRight,
        LakeTopLeft,
        LakeTop,
        LakeTop2,
        LakeTopRight,
        LakeLeft,
        Lake,
        Lake2,
        LakeRight,
        LakeBottomLeft,
        LakeBottom,
        LakeBottom2,
        LakeBottomRight
    }

    public static class TileNameMapper
    {
        private static Dictionary<TileName, string> _map = new()
        {
            { TileName.Grass, "grounds2_10" },
            { TileName.GrassTopLeft, "grounds2_3" },
            { TileName.GrassTop, "grounds2_4" },
            { TileName.GrassTop2, "grounds2_13" },
            { TileName.GrassTopRight, "grounds2_5" },
            { TileName.GrassLeft, "grounds2_9" },
            { TileName.GrassRight, "grounds2_11" },
            { TileName.GrassBottomLeft, "grounds2_15" },
            { TileName.GrassBottom, "grounds2_16" },
            { TileName.GrassBottomRight, "grounds2_17" },
            { TileName.LakeTopLeft, "lake_0" },
            { TileName.LakeTop, "lake_1" },
            { TileName.LakeTop2, "lake_2" },
            { TileName.LakeTopRight, "lake_3" },
            { TileName.LakeLeft, "lake_4" },
            { TileName.Lake, "lake_5" },
            { TileName.Lake2, "lake_6" },
            { TileName.LakeRight, "lake_7" },
            { TileName.LakeBottomLeft, "lake_8" },
            { TileName.LakeBottom, "lake_9" },
            { TileName.LakeBottom2, "lake_10" },
            { TileName.LakeBottomRight, "lake_11" },
        };

        private static HashSet<TileName> _nonWalkableTiles = new()
        {
            TileName.LakeTopLeft,
            TileName.LakeTop,
            TileName.LakeTop2,
            TileName.LakeTopRight,
            TileName.LakeLeft,
            TileName.Lake,
            TileName.Lake2,
            TileName.LakeRight,
            TileName.LakeBottomLeft,
            TileName.LakeBottom,
            TileName.LakeBottom2,
            TileName.LakeBottomRight
        };

        private static Dictionary<string, TileName> _reverseMap = new();

        static TileNameMapper()
        {
            foreach (var (key, value) in _map)
            {
                _reverseMap.Add(value, key);
            }
        }

        public static bool IsWalkableTile(string tileName)
        {
            if (tileName == null)
            {
                return true;
            }

            if (!_reverseMap.ContainsKey(tileName))
            {
                return true;
            }
            
            var tile = _reverseMap[tileName];
            return !_nonWalkableTiles.Contains(tile);
        }
    }
}