using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public interface ISystem
    {
        void Start();
        void Update();
        void LogicUpdate();
    }
}