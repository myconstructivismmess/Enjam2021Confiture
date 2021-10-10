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

	public void Attack()
	{
		if (!attack) return;
		
		Debug.Log("Attack");
	}
	
	public void LongRangeAttack()
	{
		if (!longRangeAttack) return;
		
		Debug.Log("Long Range Attack");
	}
}