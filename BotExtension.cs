using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GalconBotTestingArena
{
    public class BotExtension
    {
        public BotExtension(int botExtensionId, string botCommandLine, string botExt)
        {
            this.botExtensionId = botExtensionId;
            this.botCommandLine = botCommandLine;
            this.botExt = botExt;
        }

        public int BotExtensionId()
        {
            return botExtensionId;
        }

        public string BotCommandLine()
        {
            return botCommandLine;
        }

        public string BotExt()
        {
            return botExt;
        }

        public void BotCommandLine(string newCommandLine)
        {
            this.botCommandLine = newCommandLine;
        }

        public void BotExt(string newExt)
        {
            this.botExt = newExt;
        }

        public string fullCommandByName(FileInfo botFileInfo)
        {
            return "\"" + botCommandLine + " \\\"" + botFileInfo.FullName + "\\\"\"";
        }

        private int botExtensionId;
        private string botCommandLine;
        private string botExt;
    }
}
