using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class EX_A_PlayerPanel : MonoSingleton<EX_A_PlayerPanel>
    {
        [Header("player panel")]
        public RectTransform panel;

        public void Start()
        {
            panel = GetComponent<RectTransform>();
        }
    }
}