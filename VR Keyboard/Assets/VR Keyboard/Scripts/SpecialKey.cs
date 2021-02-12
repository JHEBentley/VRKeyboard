using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRKeyboard
{
    public class SpecialKey : VRKey
    {
        public UnityEvent OnPressed;
        public UnityEvent OnUnPressed;

        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            OnPressed.Invoke();
        }

        public override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            OnUnPressed.Invoke();
        }
    }
}
