using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	private Player _player;
	
	private Transform _transform;
	
	private Rigidbody2D _rigidbody;
	private PlayerInput _playerInput;
	private Animator _animator;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer _demonMaskSpriteRenderer;
	private SpriteRenderer _demonCapeSpriteRenderer;
	private SpriteRenderer _flyFlamesSpriteRenderer;
	private SpriteRenderer _flyFlamesSpriteRenderer2;
	
	private InputActionMap _defaultActionMap, _flyingActionMap;

	private static int _isMovingAnimatorHash = Animator.StringToHash("IsMoving");
	private static int _dashAnimatorHash = Animator.StringToHash("Dash");
	
	private bool _canFly = true;
	private bool _demon = true;
	private bool _cape = true;
	private Vector2 _movement;

	private void Awake()
	{
		_player = GetComponent<Player>();
		
		_transform = transform;

		_rigidbody = GetComponent<Rigidbody2D>();
		_playerInput = GetComponent<PlayerInput>();
		_animator = GetComponent<Animator>();

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_demonMaskSpriteRenderer = _transform.GetComponentsInChildren<SpriteRenderer>()[1];
		_demonCapeSpriteRenderer = _transform.GetComponentsInChildren<SpriteRenderer>()[2];
		_flyFlamesSpriteRenderer = _transform.GetComponentsInChildren<SpriteRenderer>()[3];
		_flyFlamesSpriteRenderer2 = _transform.GetComponentsInChildren<SpriteRenderer>()[4];
		
		_defaultActionMap = _playerInput.actions.FindActionMap("Default");
		_flyingActionMap = _playerInput.actions.FindActionMap("Flying");

		UpdateActionMap();
	}

	private void Update()
	{
		if (_player.fly != _canFly)
		{
			_canFly = _player.fly;
			UpdateFlyFlamesVisibility();
			UpdateActionMap();
		}

		if (_player.demon != _demon)
		{
			_demon = _player.demon;
			UpdateDemonMaskVisibility();
		}
		
		if (_player.cape != _cape)
		{
			_cape = _player.cape;
			UpdateCapeVisibility();
		}

		if (_canFly)
			_rigidbody.AddForce(_movement * Time.deltaTime * _player.flySpeed);
		else
			_rigidbody.AddForce(new Vector2(_movement.x, Mathf.Max(_movement.y, 0)) * Time.deltaTime * _player.speed);
	}

	private void UpdateActionMap()
	{
		if (_canFly)
		{
			_defaultActionMap.Disable();
			_flyingActionMap.Enable();
			_rigidbody.gravityScale = 0;
			_rigidbody.velocity = Vector3.zero;
		}
		else
		{
			_defaultActionMap.Enable();
			_flyingActionMap.Disable();
			_rigidbody.gravityScale = 1;
		}
	}

	private void UpdateDemonMaskVisibility()
	{
		_demonMaskSpriteRenderer.enabled = _demon;
	}

	private void UpdateCapeVisibility()
	{
		_demonCapeSpriteRenderer.enabled = _cape;
	}

	private void UpdateFlyFlamesVisibility()
	{
		_flyFlamesSpriteRenderer.enabled = _canFly;
		_flyFlamesSpriteRenderer2.enabled = _canFly;
	}

	public void Move(InputAction.CallbackContext callbackContext)
	{
		Vector2 movement = callbackContext.ReadValue<Vector2>();
		
		if (movement.magnitude > 0.3f)
		{
			_movement = movement.normalized;
			_animator.SetBool(_isMovingAnimatorHash, true);
			
			_player.flipX = movement.x < 0;
			_spriteRenderer.flipX = movement.x < 0;
			_demonMaskSpriteRenderer.flipX = _player.flipX;
			_demonCapeSpriteRenderer.flipX = _player.flipX;
			_flyFlamesSpriteRenderer.flipX = _player.flipX;
			_flyFlamesSpriteRenderer2.flipX = _player.flipX;
		}
		else
		{
			_movement = Vector2.zero;
			_animator.SetBool(_isMovingAnimatorHash, false);
		}
	}

	public void Move1D(InputAction.CallbackContext callbackContext)
	{
		Vector2 movement = new Vector2(callbackContext.ReadValue<Vector2>().x, 0);
		
		if (movement.magnitude > 0.3f)
		{
			_movement = movement;
			_animator.SetBool(_isMovingAnimatorHash, true);
			
			_player.flipX = movement.x < 0;
			_spriteRenderer.flipX = movement.x < 0;
			_demonMaskSpriteRenderer.flipX = _player.flipX;
			_demonCapeSpriteRenderer.flipX = _player.flipX;
			_flyFlamesSpriteRenderer.flipX = _player.flipX;
			_flyFlamesSpriteRenderer2.flipX = _player.flipX;
		}
		else
		{
			_movement = Vector2.zero;
			_animator.SetBool(_isMovingAnimatorHash, false);
		}
	}

	public void Jump(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !callbackContext.performed) return;
		
		_rigidbody.AddForce(new Vector2(0, _player.jumpSpeed));
	}

	public void Attack(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !callbackContext.performed) return;

		_player.Attack();
	}

	public void LongRangeAttack(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !callbackContext.performed) return;

		_player.LongRangeAttack();
	}
	
	public void Dash(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.ReadValueAsButton() || !callbackContext.performed) return;

		if (!_player.dash) return;

		StartCoroutine(DashCoroutine());
	}

	private IEnumerator DashCoroutine()
	{
		yield return new WaitForSeconds(1f);
	}
}