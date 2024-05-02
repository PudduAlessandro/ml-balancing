//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Selection"",
            ""id"": ""3fe1aaac-8d16-4d52-a142-a72e95ea7fd7"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""1893d100-5171-4e63-b5a5-a454ee7a41d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""17296216-577b-486b-acb6-cf76080cef10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""c823d076-a2d3-4738-b1f2-eb297b5626e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""6a1cb7e9-5a03-4813-903d-f73da1d2312a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65874d64-b01a-4d5a-82d2-8f3a0b21081c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7391d2df-ab0d-4f7b-ac0e-11686f448878"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afd9edce-ed53-4340-bb96-ea46978afef8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8ee56e7-6987-4fbc-b6dd-d5e234599f78"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf3ac94b-80de-4c2a-9b35-400cb0123f99"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed0cda1d-42bb-4a99-a61c-3ccb5d5c9729"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a99bf723-5f94-4c7b-8f29-e0af4ce16484"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70c5b516-d61a-49e0-88bd-f9e7586b33f0"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa52e4bf-9f53-460b-9b2d-9e31e92c80de"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""059ab8aa-a14f-4fc6-93e3-0647fd058b9f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cff5cfd2-6c2f-4f32-8c51-358f282c725a"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e4be1e7-a699-43f1-8977-1e69205bcd08"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Editor"",
            ""id"": ""739f26dc-91c8-4b6c-ab96-6826ced8a610"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0f0cad5c-0e6a-4354-8213-f05b6731d58f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectGrass"",
                    ""type"": ""Button"",
                    ""id"": ""4b0cb4c1-51e9-4307-a305-ed9fed9fa9e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectWater"",
                    ""type"": ""Button"",
                    ""id"": ""297fad67-a0ee-41df-a610-a7f093ff339c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectWall"",
                    ""type"": ""Button"",
                    ""id"": ""567d1e51-ff76-4f73-bb0a-247dedaafd00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectFood"",
                    ""type"": ""Button"",
                    ""id"": ""47d68cc5-b0fb-4d59-892b-73516e06edce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectPlayer"",
                    ""type"": ""Button"",
                    ""id"": ""867ff57b-ac87-48a3-aa0b-2fc6fe7268ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectOpponent"",
                    ""type"": ""Button"",
                    ""id"": ""42a106db-822b-43f2-a740-20043772e020"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Value"",
                    ""id"": ""cf434aaa-90a1-4986-89dc-f35360e6e456"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2821bcb-5ec2-4b73-8c78-0febd1dc32a6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24734448-5a95-49f5-b557-38d829adb2c3"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectGrass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77587801-709f-447a-9af9-376cfc901e7f"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectGrass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee83a21e-ae57-4293-9c1d-6697d0165f9b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectWater"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f61f0e86-9b48-485c-9bd2-b2ddcb678590"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectWater"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bdafb9e-31dc-4955-a3d4-dda89caaca0f"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectWall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53210bf0-9f75-4210-92f3-40ca0a9967e2"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectWall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63374d16-3a53-4bff-8a9b-8b3447e4a005"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectFood"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de822bf4-2a32-4aba-bc14-10b87a96025b"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectFood"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66b510b6-e005-4e2b-b2b7-4c29d1ea9fd0"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""667d7af9-8006-4773-939f-3743902ca506"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a091c50-f6d7-40da-bf58-f5c38b79f430"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectOpponent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2618045-019f-43e6-bb9b-f1d700adbfee"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""SelectOpponent"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c4ca2f5-9bd4-48a2-8e33-5cc9a4bc27a4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player1"",
            ""bindingGroup"": ""Player1"",
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
        },
        {
            ""name"": ""Player2"",
            ""bindingGroup"": ""Player2"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Selection
        m_Selection = asset.FindActionMap("Selection", throwIfNotFound: true);
        m_Selection_Up = m_Selection.FindAction("Up", throwIfNotFound: true);
        m_Selection_Down = m_Selection.FindAction("Down", throwIfNotFound: true);
        m_Selection_Left = m_Selection.FindAction("Left", throwIfNotFound: true);
        m_Selection_Right = m_Selection.FindAction("Right", throwIfNotFound: true);
        // Editor
        m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
        m_Editor_Click = m_Editor.FindAction("Click", throwIfNotFound: true);
        m_Editor_SelectGrass = m_Editor.FindAction("SelectGrass", throwIfNotFound: true);
        m_Editor_SelectWater = m_Editor.FindAction("SelectWater", throwIfNotFound: true);
        m_Editor_SelectWall = m_Editor.FindAction("SelectWall", throwIfNotFound: true);
        m_Editor_SelectFood = m_Editor.FindAction("SelectFood", throwIfNotFound: true);
        m_Editor_SelectPlayer = m_Editor.FindAction("SelectPlayer", throwIfNotFound: true);
        m_Editor_SelectOpponent = m_Editor.FindAction("SelectOpponent", throwIfNotFound: true);
        m_Editor_Point = m_Editor.FindAction("Point", throwIfNotFound: true);
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

    // Selection
    private readonly InputActionMap m_Selection;
    private List<ISelectionActions> m_SelectionActionsCallbackInterfaces = new List<ISelectionActions>();
    private readonly InputAction m_Selection_Up;
    private readonly InputAction m_Selection_Down;
    private readonly InputAction m_Selection_Left;
    private readonly InputAction m_Selection_Right;
    public struct SelectionActions
    {
        private @PlayerControls m_Wrapper;
        public SelectionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Selection_Up;
        public InputAction @Down => m_Wrapper.m_Selection_Down;
        public InputAction @Left => m_Wrapper.m_Selection_Left;
        public InputAction @Right => m_Wrapper.m_Selection_Right;
        public InputActionMap Get() { return m_Wrapper.m_Selection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectionActions set) { return set.Get(); }
        public void AddCallbacks(ISelectionActions instance)
        {
            if (instance == null || m_Wrapper.m_SelectionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SelectionActionsCallbackInterfaces.Add(instance);
            @Up.started += instance.OnUp;
            @Up.performed += instance.OnUp;
            @Up.canceled += instance.OnUp;
            @Down.started += instance.OnDown;
            @Down.performed += instance.OnDown;
            @Down.canceled += instance.OnDown;
            @Left.started += instance.OnLeft;
            @Left.performed += instance.OnLeft;
            @Left.canceled += instance.OnLeft;
            @Right.started += instance.OnRight;
            @Right.performed += instance.OnRight;
            @Right.canceled += instance.OnRight;
        }

        private void UnregisterCallbacks(ISelectionActions instance)
        {
            @Up.started -= instance.OnUp;
            @Up.performed -= instance.OnUp;
            @Up.canceled -= instance.OnUp;
            @Down.started -= instance.OnDown;
            @Down.performed -= instance.OnDown;
            @Down.canceled -= instance.OnDown;
            @Left.started -= instance.OnLeft;
            @Left.performed -= instance.OnLeft;
            @Left.canceled -= instance.OnLeft;
            @Right.started -= instance.OnRight;
            @Right.performed -= instance.OnRight;
            @Right.canceled -= instance.OnRight;
        }

        public void RemoveCallbacks(ISelectionActions instance)
        {
            if (m_Wrapper.m_SelectionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISelectionActions instance)
        {
            foreach (var item in m_Wrapper.m_SelectionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SelectionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SelectionActions @Selection => new SelectionActions(this);

    // Editor
    private readonly InputActionMap m_Editor;
    private List<IEditorActions> m_EditorActionsCallbackInterfaces = new List<IEditorActions>();
    private readonly InputAction m_Editor_Click;
    private readonly InputAction m_Editor_SelectGrass;
    private readonly InputAction m_Editor_SelectWater;
    private readonly InputAction m_Editor_SelectWall;
    private readonly InputAction m_Editor_SelectFood;
    private readonly InputAction m_Editor_SelectPlayer;
    private readonly InputAction m_Editor_SelectOpponent;
    private readonly InputAction m_Editor_Point;
    public struct EditorActions
    {
        private @PlayerControls m_Wrapper;
        public EditorActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Editor_Click;
        public InputAction @SelectGrass => m_Wrapper.m_Editor_SelectGrass;
        public InputAction @SelectWater => m_Wrapper.m_Editor_SelectWater;
        public InputAction @SelectWall => m_Wrapper.m_Editor_SelectWall;
        public InputAction @SelectFood => m_Wrapper.m_Editor_SelectFood;
        public InputAction @SelectPlayer => m_Wrapper.m_Editor_SelectPlayer;
        public InputAction @SelectOpponent => m_Wrapper.m_Editor_SelectOpponent;
        public InputAction @Point => m_Wrapper.m_Editor_Point;
        public InputActionMap Get() { return m_Wrapper.m_Editor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
        public void AddCallbacks(IEditorActions instance)
        {
            if (instance == null || m_Wrapper.m_EditorActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_EditorActionsCallbackInterfaces.Add(instance);
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
            @SelectGrass.started += instance.OnSelectGrass;
            @SelectGrass.performed += instance.OnSelectGrass;
            @SelectGrass.canceled += instance.OnSelectGrass;
            @SelectWater.started += instance.OnSelectWater;
            @SelectWater.performed += instance.OnSelectWater;
            @SelectWater.canceled += instance.OnSelectWater;
            @SelectWall.started += instance.OnSelectWall;
            @SelectWall.performed += instance.OnSelectWall;
            @SelectWall.canceled += instance.OnSelectWall;
            @SelectFood.started += instance.OnSelectFood;
            @SelectFood.performed += instance.OnSelectFood;
            @SelectFood.canceled += instance.OnSelectFood;
            @SelectPlayer.started += instance.OnSelectPlayer;
            @SelectPlayer.performed += instance.OnSelectPlayer;
            @SelectPlayer.canceled += instance.OnSelectPlayer;
            @SelectOpponent.started += instance.OnSelectOpponent;
            @SelectOpponent.performed += instance.OnSelectOpponent;
            @SelectOpponent.canceled += instance.OnSelectOpponent;
            @Point.started += instance.OnPoint;
            @Point.performed += instance.OnPoint;
            @Point.canceled += instance.OnPoint;
        }

        private void UnregisterCallbacks(IEditorActions instance)
        {
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
            @SelectGrass.started -= instance.OnSelectGrass;
            @SelectGrass.performed -= instance.OnSelectGrass;
            @SelectGrass.canceled -= instance.OnSelectGrass;
            @SelectWater.started -= instance.OnSelectWater;
            @SelectWater.performed -= instance.OnSelectWater;
            @SelectWater.canceled -= instance.OnSelectWater;
            @SelectWall.started -= instance.OnSelectWall;
            @SelectWall.performed -= instance.OnSelectWall;
            @SelectWall.canceled -= instance.OnSelectWall;
            @SelectFood.started -= instance.OnSelectFood;
            @SelectFood.performed -= instance.OnSelectFood;
            @SelectFood.canceled -= instance.OnSelectFood;
            @SelectPlayer.started -= instance.OnSelectPlayer;
            @SelectPlayer.performed -= instance.OnSelectPlayer;
            @SelectPlayer.canceled -= instance.OnSelectPlayer;
            @SelectOpponent.started -= instance.OnSelectOpponent;
            @SelectOpponent.performed -= instance.OnSelectOpponent;
            @SelectOpponent.canceled -= instance.OnSelectOpponent;
            @Point.started -= instance.OnPoint;
            @Point.performed -= instance.OnPoint;
            @Point.canceled -= instance.OnPoint;
        }

        public void RemoveCallbacks(IEditorActions instance)
        {
            if (m_Wrapper.m_EditorActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IEditorActions instance)
        {
            foreach (var item in m_Wrapper.m_EditorActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_EditorActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public EditorActions @Editor => new EditorActions(this);
    private int m_Player1SchemeIndex = -1;
    public InputControlScheme Player1Scheme
    {
        get
        {
            if (m_Player1SchemeIndex == -1) m_Player1SchemeIndex = asset.FindControlSchemeIndex("Player1");
            return asset.controlSchemes[m_Player1SchemeIndex];
        }
    }
    private int m_Player2SchemeIndex = -1;
    public InputControlScheme Player2Scheme
    {
        get
        {
            if (m_Player2SchemeIndex == -1) m_Player2SchemeIndex = asset.FindControlSchemeIndex("Player2");
            return asset.controlSchemes[m_Player2SchemeIndex];
        }
    }
    public interface ISelectionActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
    public interface IEditorActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnSelectGrass(InputAction.CallbackContext context);
        void OnSelectWater(InputAction.CallbackContext context);
        void OnSelectWall(InputAction.CallbackContext context);
        void OnSelectFood(InputAction.CallbackContext context);
        void OnSelectPlayer(InputAction.CallbackContext context);
        void OnSelectOpponent(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
    }
}