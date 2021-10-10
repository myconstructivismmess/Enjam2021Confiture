using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Abilities")]
	public bool attack = true;
	public bool longRangeAttack = true;
	public bool fly = true;
	public bool dash = true;
	public bool lifeRegeneration = true;

	[Header("Properties")]
	public bool demon = true;
	public bool cape = true;
	public float flySpeed = 400;
	public float speed = 700;
	public float jumpSpeed = 1200;
	public float attackDistance = 4f;
	public int attackDammage = 5;
	public LayerMask attackLayerMask;

	public int health = 10;

	[NonSerialized] public bool flipX;

	private Transform _transform;

	public void Start()
	{
		_transform = transform;
	}

	public void Attack()
	{
		if (!attack) return;
		
		Debug.Log("Attack");

		RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(_transform.position, flipX ? Vector2.left : Vector2.right, attackDistance, attackLayerMask);

		foreach (RaycastHit2D raycastHit2D in raycastHit2Ds)
		{
			if (raycastHit2D.transform.CompareTag("Mob"))
			{
				raycastHit2D.transform.GetComponent<Mob>().Hit(attackDammage);
			}
		}
	}
	
	public void LongRangeAttack()
	{
		if (!longRangeAttack) return;
		
		Debug.Log("Long Range Attack");
	}

	public void Hit(int dmg)
    {
		health -= dmg;
		Debug.Log($"Ouch j'ai plus que {health}");
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(attackDistance, 0));
		Gizmos.DrawLine(transform.position, transform.position - new Vector3(attackDistance, 0));
	}
}