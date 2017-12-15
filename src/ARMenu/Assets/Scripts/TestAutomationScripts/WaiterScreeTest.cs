using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class WaiterScreenTest : UITest {

	[UnityTest]
	public IEnumerator CanReceiveCookedOrders() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");
		
		//check if any order appears on screen
		yield return WaitFor(new ObjectAppeared("OrderItem(Clone)"));
	}
	
	[UnityTest]
	public IEnumerator CanNavigateDetails() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");

		//check if any order appears on screen
		yield return WaitFor(new ObjectAppeared("OrderItem(Clone)"));

		//click that order
		yield return Press("OrderItem(Clone)");

		//click back
		yield return Press("Back");
	}

	[UnityTest]
	public IEnumerator CanPaidOutside() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");

		//check if any order appears on screen
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click paid
		//the above testObj disapear -> pass the test
		yield return Press("/Orderlist/Background/ScrollView_1/ScrollRect/Content/OrderItem(Clone)/Paid");
		ObjectDisappeared _testObj = new ObjectDisappeared("OrderItem(Clone)");
		if (testObj.o = _testObj.o) 
			yield return WaitFor(_testObj);
	}

	[UnityTest]
	public IEnumerator CanPaidInside() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");

		//check if any order appears on screen
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click that order
		yield return Press(testObj.getPath());

		//click paid
		//the above testObj disapear -> pass the test
		yield return Press("/Orderlist/OrderDetail/ScrollView_5/ScrollRect/Content/Paid");
		ObjectDisappeared _testObj = new ObjectDisappeared("OrderItem(Clone)");
		if (testObj.o = _testObj.o) 
			yield return WaitFor(_testObj);
	}
}
