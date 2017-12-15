using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class ChefScreenTest : UITest {

	[UnityTest]
	public IEnumerator CanReceiveOrders() {
		//open Chef scene
		yield return LoadScene("ChefScreen");
		
		//check if any order appears on screen
		yield return WaitFor(new ObjectAppeared("OrderItem(Clone)"));
	}
	
	[UnityTest]
	public IEnumerator CanNavigateDetails() {
		//open Chef scene
		yield return LoadScene("ChefScreen");

		//check if any order appears on screen
		yield return WaitFor(new ObjectAppeared("OrderItem(Clone)"));

		//click that order
		yield return Press("OrderItem(Clone)");
	}

}
