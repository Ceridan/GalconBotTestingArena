using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GalconBotTestingArena
{
    public class Bot
    {
        public Bot(int botId, FileInfo botFileInfo)
        {
            this.botId = botId;
            this.botFileInfo = botFileInfo;
            this.isMyBot = false;
        }

        public Bot(int botId, FileInfo botFileInfo, bool isMyBot)
        {
            this.botId = botId;
            this.botFileInfo = botFileInfo;
            this.isMyBot = isMyBot;
        }

        public int BotId()
        {
            return botId;
        }

        public FileInfo BotFileInfo()
        {
            return botFileInfo;
        }

        public bool IsMyBot()
        {
            return isMyBot;
        }

        public bool isExe()
        {
            if (botFileInfo.Extension == ".exe")
                return true;
            else
                return false;
        }

        public bool isJar()
        {
            if (botFileInfo.Extension == ".jar")
                return true;
            else
                return false;
        }


        public void BotFileInfo(FileInfo newBotFileInfo)
        {
            this.botFileInfo = newBotFileInfo;
        }

        public void IsMyBot(bool newIsMyBot)
        {
            this.isMyBot = newIsMyBot;
        }

        private int botId;
        private FileInfo botFileInfo;
        private bool isMyBot;
    }
}
