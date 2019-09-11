using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KRD
{
	public class RobinSpe : Character
	{
		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();
			AttackDamage = 372;
			StunAbility = false;
		}


		// Update is called once per frame
		protected override void Update()
		{
			base.Update();
		}
	}
}