using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	private Player _player;
	
	private Transform _transform;
	
	private Rigidbody _rigidbody;
	private PlayerInput _playerInput;
	
	private InputActionMap _defaultActionMap, _flyingActionMap;
	
	private bool _canFly = true;
	private Vector2 _movement;

	private void Awake()
	{
		_player = GetComponent<Player>();
		
		_transform = transform;

		_rigidbody = GetComponent<Rigidbody>();
		_playerInput = GetComponent<PlayerInput>();
		
		_defaultActionMap = _playerInput.actions.FindActionMap("Default");
		_flyingActionMap = _playerInput.actions.FindActionMap("Flying");
	}

	private void Update()
	{
		if (_player.canFly != _canFly)
		{
			_canFly = _player.canFly;
			UpdateActionMap();
		}
		
				
		if (_canFly)
		{
			_transform.position += (Vector3)_movement;
		}
		else
		{
			_rigidbody.AddForce(_movement);
		}
	}

	private void UpdateActionMap()
	{
		if (_canFly)
		{
			_defaultActionMap.Disable();
			_flyingActionMap.Enable();
			_rigidbody.useGravity = false;
			_rigidbody.velocity = Vector3.zero;
		}
		else
		{
			_defaultActionMap.Enable();
			_flyingActionMap.Disable();
			_rigidbody.useGravity = true;
		}
	}

	public void Move(InputAction.CallbackContext callbackContext)
	{
		_movement = callbackContext.ReadValue<Vector2>();
	}

	public void Skill1(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !_player.skill1) return;
		
		_player.Skill1();
	}
	
	public void Skill2(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !_player.skill2) return;
		
		_player.Skill2();
	}
	
	public void Skill3(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !_player.skill3) return;
		
		_player.Skill3();
	}
}