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

		//click back
		yield return Press("Back");
	}

	[UnityTest]
	public IEnumerator CanCookedOutside() {
		//open Chef scene
		yield return LoadScene("ChefScreen");

		//check if any order appears on screen
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click cooked
		//the above testObj disappear -> pass the test
		yield return Press("/Orderlist/Background/ScrollView_1/ScrollRect/Content/OrderItem(Clone)/CookDone");
		ObjectDisappeared _testObj = new ObjectDisappeared("OrderItem(Clone)");
		if (testObj.o = _testObj.o) 
			yield return WaitFor(_testObj);
	}

	[UnityTest]
	public IEnumerator CanCookedInside() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");

		//check if any order appears on screen
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click that order
		yield return Press(testObj.getPath());

		//click cooked
		//the above testObj disapear -> pass the test
		yield return Press("/Orderlist/OrderDetail/ScrollView_5/ScrollRect/Content/CookDone");
		ObjectDisappeared _testObj = new ObjectDisappeared("OrderItem(Clone)");
		if (testObj.o = _testObj.o) 
			yield return WaitFor(_testObj);
	}

}
