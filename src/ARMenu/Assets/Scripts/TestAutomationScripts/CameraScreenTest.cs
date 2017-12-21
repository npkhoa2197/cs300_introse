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
		
		string baseModelPath = "/FrenchFriesTarget/BaseFoodObject/fries(Clone)";

		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//check for Base model
		yield return WaitFor(new ObjectAppeared(baseModelPath));
	}

	[UnityTest]
	public IEnumerator CanSelectVariants() {
		string baseModelPath = "/FrenchFriesTarget/BaseFoodObject/fries(Clone)";
		string selectedModelPath = "/FrenchFriesTarget/SelectCircle/fries_small(Clone)";

		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//get current base model
		yield return WaitFor(new ObjectAppeared(baseModelPath));

		//press Customize button
		yield return MouseUpAsButton("CustomizeSphere");

		//check if Selection circle appear
		yield return WaitFor(new ObjectAppeared("SelectCircle"));

		//click another model
		yield return MouseUpAsButton(selectedModelPath);

		//close SelectCircle
		yield return MouseUpAsButton("CustomizeSphere");

		//check if SelectCircle is closed
		yield return WaitFor(new ObjectDisappeared("SelectCircle"));

		//check if previous base model disappear
		yield return WaitFor(new ObjectDisappeared(baseModelPath));
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

	[UnityTest]
	public IEnumerator SelectCircleDisappearsWhenOrder() {
		//open GlobalContentProvider
		yield return LoadSceneIntermediate("ContentProviderScene");

		//check if CameraScreen appear
		yield return WaitFor(new SceneLoaded("CameraScreen"));

		//click customize
		yield return MouseUpAsButton("CustomizeSphere");

		//check if Selection circle appear
		yield return WaitFor(new ObjectAppeared("SelectCircle"));

		//press Order button
		yield return MouseUpAsButton("OrderSphere");

		//check if OrderCanvas is appeared
		yield return WaitFor(new ObjectAppeared("OrderCanvas"));

		//check if SelectCircle disappear
		yield return WaitFor(new ObjectDisappeared("SelectCircle"));
	}
}
