using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRKeyboard
{
    public class VRKey : MonoBehaviour
    {
        [Header("Functionality")]
        private VRKeyboardManager manager;
        [SerializeField]
        private string nonShiftChar;
        [SerializeField]
        private string shiftChar;
        private TextMeshProUGUI shiftDisp;
        private TextMeshProUGUI mainDisp;

        private bool pressed = false;

        [Header("Colours")]
        private Material mat;
        private Color currentColour;
        private float colourLerpSpeed = 15f;

        void Start()
        {
            Collider col = GetComponent<Collider>();

            if (!col)
            {
                col = gameObject.AddComponent<BoxCollider>();
            }

            //Collider has to be a trigger collider
            if (col.isTrigger == false)
            {
                col.isTrigger = true;
            }

            manager = VRKeyboardManager.instance;
            mat = GetComponent<MeshRenderer>().material;

            //Find the displays on the key
            shiftDisp = transform.GetChild(0).Find("ShiftDisp").GetComponent<TextMeshProUGUI>();
            mainDisp = transform.GetChild(0).Find("MainDisp").GetComponent<TextMeshProUGUI>();

            if (!shiftDisp || !mainDisp)
            {
                Debug.LogError(gameObject.name + " could not find the displays for it's key. Please ensure the displays are present and are called ShiftDisp and MainDisp");
            }

            currentColour = manager.unPressedColour;
            colourLerpSpeed = manager.colourLerpSpeed;
        }

        private void Update()
        {
            mat.color = Color.Lerp(mat.color, currentColour, Time.deltaTime * colourLerpSpeed);

            if (!manager.shifted)
            {
                shiftDisp.text = shiftChar;
                mainDisp.text = nonShiftChar;
            }

            else
            {
                shiftDisp.text = nonShiftChar;
                mainDisp.text = shiftChar;
            }
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (manager.onlyAllowBatons && !other.CompareTag("Baton"))
            {
                return;
            }

            Debug.Log("Triggered!");
            //A bool check stops any accidental double calls
            if (!pressed)
            {
                if (manager.shifted)
                {
                    manager.addToString(shiftChar);
                }

                else
                {
                    manager.addToString(nonShiftChar);
                }

                currentColour = manager.pressedColour;

                pressed = true;
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            if (manager.onlyAllowBatons && !other.CompareTag("Baton"))
            {
                return;
            }

            pressed = false;
            currentColour = manager.unPressedColour;
        }
    }
}
