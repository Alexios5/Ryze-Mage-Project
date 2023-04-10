using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace MoreMountains.CorgiEngine
{
    public class CharacterHandleSpells : CharacterAbility
    {

        [SerializeField] private float ComboTimeMax = 1.0f;
        [SerializeField] private List<string> InputComboSequence;
        public SpellSOScript CurrentSpell;
        private float Timer;
        private bool InitiateSequence = false;
        private int CounterCombo = 0;


        protected override void Initialization()
        {
            base.Initialization();
            InputComboSequence = new List<string>();

        }

        public override void ProcessAbility()
        {
            base.ProcessAbility();



            if (InitiateSequence)
            {
                Timer += Time.deltaTime;
                Debug.Log("Timer : " + Timer);
                if (Timer>= ComboTimeMax)
                {
                    Timer = 0;
                    InitiateSequence = false;
                    CheckComboMatch();
                }                
            }

           

        }
		protected override void HandleInput()
		{

            if (CheckComboButtonsPressed())
            {
                Timer = 0;
                InitiateSequence = true;

            }





        }
        public bool CheckComboButtonsPressed()
        {


            /*
             * 
             * if (CurrentSpell.ComboSpell[0].currentActionReference.action.name == InputManagerExt.Instance.InputActions.PlayerControls.LeftB.name)
                {
                    Debug.Log("Same First INput");
                }
             */
            if (InputManagerExt.Instance.NorthButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.NorthB.name);               
                
                return true;
            }
            else if (InputManagerExt.Instance.LeftButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.LeftB.name);
                
                return true;
            }
            else if (InputManagerExt.Instance.RightButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.RightB.name);

                return true;
            }
            else if (InputManagerExt.Instance.SouthButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
            {
                InputComboSequence.Add(InputManagerExt.Instance.InputActions.PlayerControls.SouthB.name);

                return true;
            }

            return false;
        }

        public void CheckComboMatch()
        {

            CounterCombo = 0;
            for(int i=0;i<InputComboSequence.Count;i++)
            {
                if (i< CurrentSpell.ComboSpell.Count)
                {
                    if (CurrentSpell.ComboSpell[i].currentActionReference.action.name == InputComboSequence[i])
                    {
                        CounterCombo++;
                    }
                }
                
            }


            if (CounterCombo >= InputComboSequence.Count)
            {
                Debug.Log("It is a match!!!!!!!!!!!");
            }
            else
            {
                Debug.Log("No Combo");
            }
            
            InputComboSequence.Clear();


        }
	}
}
