using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
    public class InputManagerExt : InputManager
    {
        public PlayerInput InputActions;
        protected bool _inputActionsEnabled = true;
        protected bool _initialized = false;
        public MMInput.IMButton NorthButton { get; protected set; }
        public MMInput.IMButton LeftButton { get; protected set; }
        public MMInput.IMButton RightButton { get; protected set; }
        public MMInput.IMButton SouthButton { get; protected set; }


        public static InputManagerExt Instance { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        protected override void Start()
        {
            if (!_initialized)
            {
                Initialization();
            }
        }

        //public bool NorthInteractionPressed = false;
        protected override void Initialization()
        {
            base.Initialization();

            _inputActionsEnabled = true;

            InputActions = new PlayerInput();

            ButtonList.Add(NorthButton = new MMInput.IMButton(PlayerID, "NorthB", NorthButtonDown, NorthButtonPressed, NorthButtonUp));
            ButtonList.Add(LeftButton = new MMInput.IMButton(PlayerID, "LeftB", LeftButtonDown, LeftButtonPressed, LeftButtonUp));
            ButtonList.Add(RightButton = new MMInput.IMButton(PlayerID, "RightB", RightButtonDown, RightButtonPressed, RightButtonUp));
            ButtonList.Add(SouthButton = new MMInput.IMButton(PlayerID, "SouthB", SouthButtonDown, SouthButtonPressed, SouthButtonUp));

            InputActions.PlayerControls.Movement.performed += context => _primaryMovement = context.ReadValue<Vector2>();
            InputActions.PlayerControls.Jump.performed += context => { BindButton(context, JumpButton); };
            InputActions.PlayerControls.NorthB.performed += context => { BindButton(context, NorthButton); };
            InputActions.PlayerControls.LeftB.performed += context => { BindButton(context, LeftButton); };
            InputActions.PlayerControls.RightB.performed += context => { BindButton(context, RightButton); };
            InputActions.PlayerControls.SouthB.performed += context => { BindButton(context, SouthButton); };




            //InputActions.PlayerControls.SecondaryMovement.performed += context => _secondaryMovement = context.ReadValue<Vector2>();


            /*
            InputActions.PlayerControls.Run.performed += context => { BindButton(context, RunButton); };
            InputActions.PlayerControls.Dash.performed += context => { BindButton(context, DashButton); };
            InputActions.PlayerControls.Shoot.performed += context => { BindButton(context, ShootButton); };
            InputActions.PlayerControls.SecondaryShoot.performed += context => { BindButton(context, SecondaryShootButton); };
            InputActions.PlayerControls.Interact.performed += context => { BindButton(context, InteractButton); };
            InputActions.PlayerControls.Reload.performed += context => { BindButton(context, ReloadButton); };
            InputActions.PlayerControls.Pause.performed += context => { BindButton(context, PauseButton); };
            InputActions.PlayerControls.SwitchWeapon.performed += context => { BindButton(context, SwitchWeaponButton); };
            InputActions.PlayerControls.SwitchCharacter.performed += context => { BindButton(context, SwitchCharacterButton); };
            InputActions.PlayerControls.TimeControl.performed += context => { BindButton(context, TimeControlButton); };
            InputActions.PlayerControls.Roll.performed += context => { BindButton(context, RollButton); };

            InputActions.PlayerControls.Swim.performed += context => { BindButton(context, SwimButton); };
            InputActions.PlayerControls.Glide.performed += context => { BindButton(context, GlideButton); };
            InputActions.PlayerControls.Jetpack.performed += context => { BindButton(context, JetpackButton); };
            InputActions.PlayerControls.Fly.performed += context => { BindButton(context, FlyButton); };
            InputActions.PlayerControls.Grab.performed += context => { BindButton(context, GrabButton); };
            InputActions.PlayerControls.Throw.performed += context => { BindButton(context, ThrowButton); };
            InputActions.PlayerControls.Push.performed += context => { BindButton(context, PushButton); };
            */
            _initialized = true;









        }

        protected virtual void BindButton(InputAction.CallbackContext context, MMInput.IMButton imButton)
        {
            var control = context.control;

            if (control is ButtonControl button)
            {
                if (button.wasPressedThisFrame)
                {
                    imButton.TriggerButtonDown();
                }
                if (button.wasReleasedThisFrame)
                {
                    imButton.TriggerButtonUp();
                }
            }
        }

        protected override void Update()
        {
            if (IsMobile && _inputActionsEnabled)
            {
                _inputActionsEnabled = false;
                InputActions.Disable();
            }

            if (!IsMobile && (InputDetectionActive != _inputActionsEnabled))
            {
                if (InputDetectionActive)
                {
                    _inputActionsEnabled = true;
                    InputActions.Enable();
                    ForceRefresh();
                }
                else
                {
                    _inputActionsEnabled = false;
                    InputActions.Disable();
                }
            }
        }

        protected virtual void ForceRefresh()
        {
            _primaryMovement = InputActions.PlayerControls.Movement.ReadValue<Vector2>();
            //_secondaryMovement = InputActions.PlayerControls.SecondaryMovement.ReadValue<Vector2>();
        }

        /// <summary>
        /// On enable we enable our input actions
        /// </summary>
        protected virtual void OnEnable()
        {
            if (!_initialized)
            {
                Initialization();
            }
            InputActions.Enable();
        }

        /// <summary>
        /// On disable we disable our input actions
        /// </summary>
        protected virtual void OnDisable()
        {
            InputActions.Disable();
        }

        

        public virtual void NorthButtonDown() { NorthButton.State.ChangeState(MMInput.ButtonStates.ButtonDown); }
        public virtual void NorthButtonPressed() { NorthButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed); }
        public virtual void NorthButtonUp() { NorthButton.State.ChangeState(MMInput.ButtonStates.ButtonUp); }


        public virtual void LeftButtonDown() { LeftButton.State.ChangeState(MMInput.ButtonStates.ButtonDown); }
        public virtual void LeftButtonPressed() { LeftButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed); }
        public virtual void LeftButtonUp() { LeftButton.State.ChangeState(MMInput.ButtonStates.ButtonUp); }


        public virtual void RightButtonDown() { RightButton.State.ChangeState(MMInput.ButtonStates.ButtonDown); }
        public virtual void RightButtonPressed() { RightButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed); }
        public virtual void RightButtonUp() { RightButton.State.ChangeState(MMInput.ButtonStates.ButtonUp); }


        public virtual void SouthButtonDown() { SouthButton.State.ChangeState(MMInput.ButtonStates.ButtonDown); }
        public virtual void SouthButtonPressed() { SouthButton.State.ChangeState(MMInput.ButtonStates.ButtonPressed); }
        public virtual void SouthButtonUp() { SouthButton.State.ChangeState(MMInput.ButtonStates.ButtonUp); }


    }
}
