using ADOFAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternPlus
{
    public static class Pattern
    {
        public enum PatternType
        {
            Circle, PseudoCircle, Test
        }
        public static void Create()
        {
            LevelEvent levelEvent = EditorTabLib.CustomTabManager.GetEvent((LevelEventType)902);

            if (levelEvent == null)
            {
                Main.Logger?.Log("levelEvent is null!!!");
                return;
            }

            PatternType patternType = (PatternType)levelEvent["patternType"];

            Main.Logger?.Log(patternType.ToString());

            switch (patternType)
            {
                case PatternType.Circle:
                    Main.Logger?.Log("Circle");
                    break;
                case PatternType.PseudoCircle:
                    Main.Logger?.Log("PseudoCircle");
                    break;
            }
        }
    }
}
