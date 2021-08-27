// GENERATED AUTOMATICALLY FROM 'Assets/Input Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""d218a5af-753c-4658-aea3-9195030adbae"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""524c7ac8-7580-454a-ac82-e6a74a55ce05"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3a92c61b-6933-455c-91c5-ffa7c63cda5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8ec3c80c-d2e1-49b1-b303-85dc28ea7f8c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""48f5f25c-ff1f-4562-8fbf-79346c5843f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e70f7be1-3f80-45e0-b808-c77f34f9a474"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""UserInterface"",
                    ""type"": ""Button"",
                    ""id"": ""496e2920-f625-46a1-826a-8024c59169a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""c01d5fcc-3558-4f9b-8e45-15f6dc7d23a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""f1863cee-3c5d-4f0b-903e-6c9960c52733"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""a4bd6522-a03e-43e6-89ce-d06ae9b226e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""020fc76b-82de-449f-b6ad-76156f444dd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""SwapMainWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""3cf9b331-b226-4ef5-9859-582e8062c5d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""SwapSecondaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""9d9787a8-5623-4d24-942f-8da62a14ab1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""DropWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""75f9a138-39b7-4915-8d28-34a350f83b21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""972a8cbc-35a9-4e81-9134-d314c0c99998"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""612efc42-d367-4766-92f3-41c5ff4d1d2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""TEST"",
                    ""type"": ""Button"",
                    ""id"": ""b27f5da7-dabd-4869-a09e-166ae8ddaa08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""05d37969-67ee-40ec-8e3f-deb2697d14ce"",
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
                    ""id"": ""1af466b2-75db-4e32-8b0d-c1edbecfe75e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""09186626-eabd-4fd3-8cfb-9d75e9f0c1b8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""99ec8ef2-26f5-40a2-a3bc-1a976b0a9e1a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ee24f5c9-886b-4653-91f8-f1cf6b8dba52"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""73fe03b5-586d-4d05-8366-63440aa69b45"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91cb0e38-2adb-4cc6-9a91-78d10cc32e8d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e127a6c-53b3-47f3-b0d1-e97c8fd8aea3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""601ff3d1-1327-4f08-ba58-c93e214b2380"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1bc6cbe-48eb-4e93-ac88-719f7cde6a03"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feeea074-f18c-4746-9809-af0ca0cc6b2b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc04f9ee-33b2-4083-a5f3-e6b6ea400e64"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4804a113-95fc-4c68-9f7f-d18d78531dd1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""117e454a-1f0d-47cf-915a-64af46aca7b8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ef35dd9-db77-4639-bf7a-b19ace747a30"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UserInterface"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""619bd9de-b709-4b23-8828-d30ff6bab1e2"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UserInterface"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db143d63-6c81-49c0-8159-5564a705db2d"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55c4e8ca-1f82-4b78-990c-d39d1aeb4620"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""907b8ea2-077d-4d44-923f-4ceec7e55e22"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1cde5d9-da22-4050-96bc-44aa6d3a2b6d"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fcfac3a-6700-4995-8d32-f21820ef2cfa"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96e12878-5bb9-4dae-bf2f-59a732eca0f7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2c95178-cd02-421d-bc04-5baac52d0ff1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb5ce388-4fac-4163-8c8b-fdacdb73313e"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""SwapMainWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca7b549a-b50a-4fd5-9c25-86feee9fc265"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwapMainWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb6263f7-eefb-4366-ab17-13e01fc51df7"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""SwapSecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75bca441-51bb-4796-b68f-98275b4a0857"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwapSecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1bcd134-865e-44dd-99ff-2c7a0a0cf764"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""DropWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cc81532-1c1f-4e2f-87e9-432235509ee7"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DropWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17857c55-7627-457d-b5d4-bd97a75e385c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""438dffcd-5121-46c1-89b9-65f69b94aaa5"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41ae3e86-d2e3-4887-9539-05ea71c3a5e2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f5eb0a9-5d9e-4815-90b3-f4ed74cd0e45"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26f405a4-4db8-4577-b147-5c97286629e5"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""TEST"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UserInterface"",
            ""id"": ""3a6266a4-b5f3-46e8-84c3-d0c9d2b53e43"",
            ""actions"": [
                {
                    ""name"": ""UserInterface"",
                    ""type"": ""Button"",
                    ""id"": ""03213dcf-a365-4aa8-9ddd-13c3d16b3990"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec33504b-ef19-43ec-bc41-77898b8202d1"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""UserInterface"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""M&K"",
            ""bindingGroup"": ""M&K"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_UserInterface = m_Player.FindAction("UserInterface", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
        m_Player_Throw = m_Player.FindAction("Throw", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_SwapMainWeapon = m_Player.FindAction("SwapMainWeapon", throwIfNotFound: true);
        m_Player_SwapSecondaryWeapon = m_Player.FindAction("SwapSecondaryWeapon", throwIfNotFound: true);
        m_Player_DropWeapon = m_Player.FindAction("DropWeapon", throwIfNotFound: true);
        m_Player_HeavyAttack = m_Player.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Player_LightAttack = m_Player.FindAction("LightAttack", throwIfNotFound: true);
        m_Player_TEST = m_Player.FindAction("TEST", throwIfNotFound: true);
        // UserInterface
        m_UserInterface = asset.FindActionMap("UserInterface", throwIfNotFound: true);
        m_UserInterface_UserInterface = m_UserInterface.FindAction("UserInterface", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_UserInterface;
    private readonly InputAction m_Player_Dodge;
    private readonly InputAction m_Player_Throw;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_SwapMainWeapon;
    private readonly InputAction m_Player_SwapSecondaryWeapon;
    private readonly InputAction m_Player_DropWeapon;
    private readonly InputAction m_Player_HeavyAttack;
    private readonly InputAction m_Player_LightAttack;
    private readonly InputAction m_Player_TEST;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @UserInterface => m_Wrapper.m_Player_UserInterface;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
        public InputAction @Throw => m_Wrapper.m_Player_Throw;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @SwapMainWeapon => m_Wrapper.m_Player_SwapMainWeapon;
        public InputAction @SwapSecondaryWeapon => m_Wrapper.m_Player_SwapSecondaryWeapon;
        public InputAction @DropWeapon => m_Wrapper.m_Player_DropWeapon;
        public InputAction @HeavyAttack => m_Wrapper.m_Player_HeavyAttack;
        public InputAction @LightAttack => m_Wrapper.m_Player_LightAttack;
        public InputAction @TEST => m_Wrapper.m_Player_TEST;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @UserInterface.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUserInterface;
                @UserInterface.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUserInterface;
                @UserInterface.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUserInterface;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Throw.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Block.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @SwapMainWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapMainWeapon;
                @SwapMainWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapMainWeapon;
                @SwapMainWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapMainWeapon;
                @SwapSecondaryWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapSecondaryWeapon;
                @SwapSecondaryWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapSecondaryWeapon;
                @SwapSecondaryWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapSecondaryWeapon;
                @DropWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @DropWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @DropWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @HeavyAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @LightAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @TEST.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTEST;
                @TEST.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTEST;
                @TEST.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTEST;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @UserInterface.started += instance.OnUserInterface;
                @UserInterface.performed += instance.OnUserInterface;
                @UserInterface.canceled += instance.OnUserInterface;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @SwapMainWeapon.started += instance.OnSwapMainWeapon;
                @SwapMainWeapon.performed += instance.OnSwapMainWeapon;
                @SwapMainWeapon.canceled += instance.OnSwapMainWeapon;
                @SwapSecondaryWeapon.started += instance.OnSwapSecondaryWeapon;
                @SwapSecondaryWeapon.performed += instance.OnSwapSecondaryWeapon;
                @SwapSecondaryWeapon.canceled += instance.OnSwapSecondaryWeapon;
                @DropWeapon.started += instance.OnDropWeapon;
                @DropWeapon.performed += instance.OnDropWeapon;
                @DropWeapon.canceled += instance.OnDropWeapon;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @TEST.started += instance.OnTEST;
                @TEST.performed += instance.OnTEST;
                @TEST.canceled += instance.OnTEST;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UserInterface
    private readonly InputActionMap m_UserInterface;
    private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
    private readonly InputAction m_UserInterface_UserInterface;
    public struct UserInterfaceActions
    {
        private @PlayerControls m_Wrapper;
        public UserInterfaceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @UserInterface => m_Wrapper.m_UserInterface_UserInterface;
        public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IUserInterfaceActions instance)
        {
            if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
            {
                @UserInterface.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnUserInterface;
                @UserInterface.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnUserInterface;
                @UserInterface.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnUserInterface;
            }
            m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UserInterface.started += instance.OnUserInterface;
                @UserInterface.performed += instance.OnUserInterface;
                @UserInterface.canceled += instance.OnUserInterface;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    private int m_MKSchemeIndex = -1;
    public InputControlScheme MKScheme
    {
        get
        {
            if (m_MKSchemeIndex == -1) m_MKSchemeIndex = asset.FindControlSchemeIndex("M&K");
            return asset.controlSchemes[m_MKSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnUserInterface(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnSwapMainWeapon(InputAction.CallbackContext context);
        void OnSwapSecondaryWeapon(InputAction.CallbackContext context);
        void OnDropWeapon(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnTEST(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnUserInterface(InputAction.CallbackContext context);
    }
}
