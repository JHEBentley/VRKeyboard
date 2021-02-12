using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRKeyboard
{
    //VRKeyboardManager v0.1. 2021/02/12. Contact james@jheb.xyz with any questions.

    public class VRKeyboardManager : MonoBehaviour
    {
        public static VRKeyboardManager instance;

        //Is the string currently shifted (in caps)?
        public bool shifted { get; set; }

        [Header("String")]
        [Tooltip("If true, a flashing cursor will appear at the end of the string.")]
        public string _string;
        public int maxLength;

        [Header("Keys")]
        [SerializeField]
        private List<VRKey> keys;
        public Color pressedColour;
        public Color unPressedColour;
        public float colourLerpSpeed = 15;

        [Header("Batons")]
        [SerializeField]
        [Tooltip("If true, objects with tag \"Baton\" will be allowed to hit keys.")]
        public bool onlyAllowBatons;

        [Space(10)]
        public UnityEvent onEnter;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            //Collect all active keys in to a list.
            //Not currently used but may be useful later on?
            GatherKeys(transform);
        }

        private void GatherKeys(Transform currTransform)
        {
            //Recursively search all children for active keys
            foreach (Transform child in currTransform)
            {
                if (child.GetComponent<VRKey>())
                {
                    keys.Add(child.GetComponent<VRKey>());                    
                }

                else
                {
                    GatherKeys(child);
                }
            }
        }

        public void addToString(string addition)
        {
            if (_string.Length < maxLength)
            {
                _string += addition;
            }
        }

        public void Backspace()
        {
            _string = _string.Remove(_string.Length - 1);
        }

        public void OnEnter()
        {
            onEnter.Invoke();
        }

        public void ToggleShift()
        {
            shifted = !shifted;
        }
    }
}
