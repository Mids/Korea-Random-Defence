using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KRD
{
	public class JumpChopper : Character
	{
		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();
			AttackDamage = 502;
			StunAbility = false;
		}


		// Update is called once per frame
		protected override void Update()
		{
			base.Update();
		}
	}
}