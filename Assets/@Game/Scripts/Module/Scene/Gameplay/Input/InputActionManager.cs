//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/@Game/Scripts/Module/Scene/Gameplay/Input/InputActionManager.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ProjectTA.Module.Input
{
    public partial class @InputActionManager: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActionManager()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionManager"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""7d9c80cc-1c09-4ae4-8144-f02b5a0be64c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""06533a2d-f2fb-43a0-983f-4e8992c83a9f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""32768ae3-b500-408f-a373-bcb29d2ffc84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""b353573e-222b-482b-8bcc-99158e2d7536"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2c84b8ab-b990-4486-9cd1-c9929f23c802"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""93e0b822-a9bb-480c-a7a9-5e64235bcd04"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f2b779cd-9856-419b-8ac0-8b67c9e4f4f2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""31d4e1c4-7e43-48b9-949b-e4202a8c7a9f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ae1d7931-6c19-49ec-b1b7-4b2d153a3061"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c5813429-a330-4c12-964b-b5277a39edbc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f69d273b-8fcd-45fd-8b63-4003ccc8678a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""912ee433-af12-4476-9373-865686d64ccf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a6376a6-be14-4f8c-be23-e3757493a317"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44436e21-e204-4b04-a89e-12374ef237e0"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c6befeb-c258-46ca-9935-77bfe3283c72"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""128566ae-27b0-4f7d-a9e0-0fcdea8fc870"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""00290217-b1f8-450b-acc9-5a32a2164bfa"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""28eb8f84-0c5d-4b5a-b735-bd148490cb13"",
            ""actions"": [
                {
                    ""name"": ""TapStart"",
                    ""type"": ""Button"",
                    ""id"": ""ad57def6-8b77-40aa-8963-052941a898b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ba79cfdb-691f-443e-ab25-e355ec9f9d2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f46061af-796f-4fa1-9fb7-e0b1b0c6895f"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""571065a2-ab51-46f6-a3b3-905e4fa1c0a5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a97d1001-4291-4573-b645-aed2779ced63"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9ed812b-dca8-401a-9d6e-8e1fb3a62daf"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""04354088-6330-43cc-85ac-c8df1fa46cfe"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""42cfde8a-8b64-4c78-bc23-aec49a231476"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a53a4d3c-ab1b-4489-af92-55d99feca815"",
                    ""path"": ""<Touchscreen>/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""429b91bd-3a07-40a3-911f-60d03cede6cd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Character
            m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
            m_Character_Move = m_Character.FindAction("Move", throwIfNotFound: true);
            m_Character_Shoot = m_Character.FindAction("Shoot", throwIfNotFound: true);
            m_Character_Aim = m_Character.FindAction("Aim", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_TapStart = m_UI.FindAction("TapStart", throwIfNotFound: true);
            m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
            // MainMenu
            m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
            m_MainMenu_Quit = m_MainMenu.FindAction("Quit", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Character
        private readonly InputActionMap m_Character;
        private List<ICharacterActions> m_CharacterActionsCallbackInterfaces = new List<ICharacterActions>();
        private readonly InputAction m_Character_Move;
        private readonly InputAction m_Character_Shoot;
        private readonly InputAction m_Character_Aim;
        public struct CharacterActions
        {
            private @InputActionManager m_Wrapper;
            public CharacterActions(@InputActionManager wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Character_Move;
            public InputAction @Shoot => m_Wrapper.m_Character_Shoot;
            public InputAction @Aim => m_Wrapper.m_Character_Aim;
            public InputActionMap Get() { return m_Wrapper.m_Character; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
            public void AddCallbacks(ICharacterActions instance)
            {
                if (instance == null || m_Wrapper.m_CharacterActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_CharacterActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }

            private void UnregisterCallbacks(ICharacterActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Shoot.started -= instance.OnShoot;
                @Shoot.performed -= instance.OnShoot;
                @Shoot.canceled -= instance.OnShoot;
                @Aim.started -= instance.OnAim;
                @Aim.performed -= instance.OnAim;
                @Aim.canceled -= instance.OnAim;
            }

            public void RemoveCallbacks(ICharacterActions instance)
            {
                if (m_Wrapper.m_CharacterActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ICharacterActions instance)
            {
                foreach (var item in m_Wrapper.m_CharacterActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_CharacterActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public CharacterActions @Character => new CharacterActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
        private readonly InputAction m_UI_TapStart;
        private readonly InputAction m_UI_Pause;
        public struct UIActions
        {
            private @InputActionManager m_Wrapper;
            public UIActions(@InputActionManager wrapper) { m_Wrapper = wrapper; }
            public InputAction @TapStart => m_Wrapper.m_UI_TapStart;
            public InputAction @Pause => m_Wrapper.m_UI_Pause;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void AddCallbacks(IUIActions instance)
            {
                if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
                @TapStart.started += instance.OnTapStart;
                @TapStart.performed += instance.OnTapStart;
                @TapStart.canceled += instance.OnTapStart;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }

            private void UnregisterCallbacks(IUIActions instance)
            {
                @TapStart.started -= instance.OnTapStart;
                @TapStart.performed -= instance.OnTapStart;
                @TapStart.canceled -= instance.OnTapStart;
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
            }

            public void RemoveCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIActions instance)
            {
                foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIActions @UI => new UIActions(this);

        // MainMenu
        private readonly InputActionMap m_MainMenu;
        private List<IMainMenuActions> m_MainMenuActionsCallbackInterfaces = new List<IMainMenuActions>();
        private readonly InputAction m_MainMenu_Quit;
        public struct MainMenuActions
        {
            private @InputActionManager m_Wrapper;
            public MainMenuActions(@InputActionManager wrapper) { m_Wrapper = wrapper; }
            public InputAction @Quit => m_Wrapper.m_MainMenu_Quit;
            public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
            public void AddCallbacks(IMainMenuActions instance)
            {
                if (instance == null || m_Wrapper.m_MainMenuActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MainMenuActionsCallbackInterfaces.Add(instance);
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }

            private void UnregisterCallbacks(IMainMenuActions instance)
            {
                @Quit.started -= instance.OnQuit;
                @Quit.performed -= instance.OnQuit;
                @Quit.canceled -= instance.OnQuit;
            }

            public void RemoveCallbacks(IMainMenuActions instance)
            {
                if (m_Wrapper.m_MainMenuActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMainMenuActions instance)
            {
                foreach (var item in m_Wrapper.m_MainMenuActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MainMenuActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MainMenuActions @MainMenu => new MainMenuActions(this);
        public interface ICharacterActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnAim(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnTapStart(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
        public interface IMainMenuActions
        {
            void OnQuit(InputAction.CallbackContext context);
        }
    }
}
