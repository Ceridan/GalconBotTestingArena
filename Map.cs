using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GalconBotTestingArena
{
    public class Map
    {
        public Map(int mapId, FileInfo mapFileInfo)
        {
            this.mapId = mapId;
            this.mapFileInfo = mapFileInfo;
        }

        public int MapId()
        {
            return mapId;
        }

        public FileInfo MapFileInfo()
        {
            return mapFileInfo;
        }

        public void MapFileInfo(FileInfo newMapFileInfo)
        {
            this.mapFileInfo = newMapFileInfo;
        }

        private int mapId;
        private FileInfo mapFileInfo;

    }
}
