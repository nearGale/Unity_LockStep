using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public static partial class GameHelper_Common
    {
        public static void UILog(string str)
        {
            EX_A_TextLog.instance.AppendLog(str);
        }
    }
}