using System;
using UnityEngine;
using Component = App.ECS.Component;

namespace App.Code.Components
{
    [Serializable]
    public class InertiaComponent : Component
    {
        public AnimationCurve InertiaCurve;
        public float CurrentTime;
    }
}