// GENERATED AUTOMATICALLY FROM 'Assets/Config/CustomInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CustomInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CustomInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomInput"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""56b013c2-6b85-4c24-ae80-6e80396f15e3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2ed60dcb-6478-46a2-9e92-180d7d0c68af"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ship-Skills"",
                    ""type"": ""Button"",
                    ""id"": ""a4d5cf0d-c70e-4759-ad78-b6bc38814862"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ship-Warp"",
                    ""type"": ""Button"",
                    ""id"": ""6ccc6a82-2dda-4a08-986d-4a44b21ff655"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4b9e2f9f-eec8-43e4-a556-0f9ebe1285ac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""78f7bf3e-0bc9-4670-a46d-698dbbfc0145"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7fe2d315-9aa4-4128-9ab5-41d505e87e5d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f2e14f58-6c8f-4552-a6c3-b180f68a409d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0a182096-0697-4f77-a6a1-3c20cd936011"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3a201e0f-dc76-4e57-8b8d-43e2f9dbf94b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Ship-Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a5b2b19-7cc1-4f3c-863c-a573a03a2c49"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""basic_control_scheme"",
                    ""action"": ""Ship-Warp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""basic_control_scheme"",
            ""bindingGroup"": ""basic_control_scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Movement = m_Keyboard.FindAction("Movement", throwIfNotFound: true);
        m_Keyboard_ShipSkills = m_Keyboard.FindAction("Ship-Skills", throwIfNotFound: true);
        m_Keyboard_ShipWarp = m_Keyboard.FindAction("Ship-Warp", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Movement;
    private readonly InputAction m_Keyboard_ShipSkills;
    private readonly InputAction m_Keyboard_ShipWarp;
    public struct KeyboardActions
    {
        private @CustomInput m_Wrapper;
        public KeyboardActions(@CustomInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Keyboard_Movement;
        public InputAction @ShipSkills => m_Wrapper.m_Keyboard_ShipSkills;
        public InputAction @ShipWarp => m_Wrapper.m_Keyboard_ShipWarp;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMovement;
                @ShipSkills.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipSkills;
                @ShipSkills.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipSkills;
                @ShipSkills.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipSkills;
                @ShipWarp.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipWarp;
                @ShipWarp.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipWarp;
                @ShipWarp.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShipWarp;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @ShipSkills.started += instance.OnShipSkills;
                @ShipSkills.performed += instance.OnShipSkills;
                @ShipSkills.canceled += instance.OnShipSkills;
                @ShipWarp.started += instance.OnShipWarp;
                @ShipWarp.performed += instance.OnShipWarp;
                @ShipWarp.canceled += instance.OnShipWarp;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_basic_control_schemeSchemeIndex = -1;
    public InputControlScheme basic_control_schemeScheme
    {
        get
        {
            if (m_basic_control_schemeSchemeIndex == -1) m_basic_control_schemeSchemeIndex = asset.FindControlSchemeIndex("basic_control_scheme");
            return asset.controlSchemes[m_basic_control_schemeSchemeIndex];
        }
    }
    public interface IKeyboardActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShipSkills(InputAction.CallbackContext context);
        void OnShipWarp(InputAction.CallbackContext context);
    }
}
