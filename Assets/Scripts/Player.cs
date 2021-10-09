using UnityEngine;

public class Player : MonoBehaviour
{
	// Abilities
	public bool canFly = true;
	public bool lifeRegeneration = true;
	public bool skill1 = true;
	public bool skill2 = true;
	public bool skill3 = true;

	public void Skill1()
	{
		if (!skill1) return;
		
		Debug.Log("Skill 1");
	}
	
	public void Skill2()
	{
		if (!skill2) return;
		
		Debug.Log("Skill 2");
	}
	
	public void Skill3()
	{
		if (!skill3) return;
		
		Debug.Log("Skill 3");
	}
}