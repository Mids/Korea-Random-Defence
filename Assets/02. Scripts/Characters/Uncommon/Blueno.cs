using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KRD
{
	public class Blueno : Character
	{
		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();
			AttackDamage = 162;
			StunAbility = false;
		}


		// Update is called once per frame
		protected override void Update()
		{
			base.Update();
		}
	}
}