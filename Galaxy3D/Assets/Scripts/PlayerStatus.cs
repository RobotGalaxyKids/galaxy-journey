using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public enum StatusEffect
	{
		Trap,
		DoubleDice,
		BackBack,
		None
	}	

	public StatusEffect effct = StatusEffect.None;


}
