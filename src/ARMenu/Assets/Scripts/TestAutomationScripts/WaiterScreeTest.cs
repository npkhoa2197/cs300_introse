using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.UI;

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
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click that order
		yield return Press(testObj.getPath());

		//check if the screen is navigated to the dish detail screen
		GameObject testGameObject = testObj.o;
		yield return AssertLabel("/Orderlist/OrderDetail/Title/Text", testGameObject.transform.Find("Dishname").GetComponent<Text>().text);
	}

	[UnityTest]
	public IEnumerator CanNavigateBackFromDetails() {
		//open Waiter scene
		yield return LoadScene("WaiterScreen");

		//check if any order appears on screen
		ObjectAppeared testObj = new ObjectAppeared("OrderItem(Clone)");
		yield return WaitFor(testObj);

		//click that order
		yield return Press(testObj.getPath());

		//check if the screen is navigated to the dish detail screen
		GameObject testGameObject = testObj.o;
		yield return AssertLabel("/Orderlist/OrderDetail/Title/Text", testGameObject.transform.Find("Dishname").GetComponent<Text>().text);
	
		//press back button
		//check if the screen is navigated back to the main waiter screen
		yield return Press("Back");
		yield return AssertLabel("/Orderlist/Title/Text", "Waiter name");
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
