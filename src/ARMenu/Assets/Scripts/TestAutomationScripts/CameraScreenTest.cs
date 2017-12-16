using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class CameraScreenTest : UITest {

	[UnityTest]
	public IEnumerator CanOpenCamera() {
		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));
	}

	[UnityTest]
	public IEnumerator CanSeeBaseModel() {
		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//check for Base model
		yield return WaitFor(new ObjectAppeared("fries(Clone)"));
	}

	[UnityTest]
	public IEnumerator CanSelectVariants() {
		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//get current base model
		ObjectAppeared baseModel = new ObjectAppeared("fries(Clone)");
		yield return WaitFor(baseModel);

		//press Customize button
		yield return MouseUpAsButton("CustomizeSphere");

		//check if Selection circle appear
		yield return WaitFor(new ObjectAppeared("SelectCircle"));

		//click another model
		yield return MouseUpAsButton("fries_small(Clone)");
	}

	[UnityTest]
	public IEnumerator MakeOrder() {
		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//press Order button
		yield return MouseUpAsButton("OrderSphere");

		//check if OrderCanvas is appeared
		yield return WaitFor(new ObjectAppeared("OrderCanvas"));

		//set input in quantity input
		yield return SetInput("QuantityInput", "10");
		
		//set input in requirements input
		yield return SetInput("RequirementsInput", "No sauce");

		//click on Order
		yield return Press("OrderButton");
	}
}
