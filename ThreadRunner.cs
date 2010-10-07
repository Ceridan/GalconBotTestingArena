using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GalconBotTestingArena
{
    public class ThreadRunner
    {
        public ThreadRunner(BotFight botFight, ManualResetEvent doneEvent)
        {
            this.botFight = botFight;
            this.doneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            bool fightResult = Run();
            botFight.Status(Convert.ToInt32(fightResult));
            this.doneEvent.Set();
        }

        public bool Run()
        {
            return FightSimulation.SingleFight(this.botFight);
        }

        public void Abort()
        {
            Thread.CurrentThread.Abort();
        }

        public void Interrupt()
        {
            Thread.CurrentThread.Interrupt();
        }

        public BotFight GetBotFight()
        {
            return this.botFight;
        }
        private BotFight botFight;

        private ManualResetEvent doneEvent;
    }
}
