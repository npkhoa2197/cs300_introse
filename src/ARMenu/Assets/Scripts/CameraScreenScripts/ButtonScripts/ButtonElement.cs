using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

abstract public class ButtonElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private bool isActivated = false;
	private Animator btnAnim;
	private Button btn;
	private ButtonManager btnManager;

	abstract public void InternalActivate();
	abstract public void InternalDeactivate();
	abstract public void InternalStart();

	void Start() {
		btnManager = transform.parent.GetComponent<ButtonManager>();
		btnAnim = GetComponent<Animator>();
		btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
		InternalStart();
	}

	public void OnClick() {
		btnManager.OnButtonClick(this);
	}

	public void Activate() {
		isActivated = true;
		btnAnim.Play("ButtonSelected");
		InternalActivate();
	}

	public void Deactivate(bool isMouseOver) {
		isActivated = false;
		if (isMouseOver) {
			btnAnim.Play("ButtonDeselected");
		}
		else {
			btnAnim.Play("ButtonDeselectedNotOver");
		}
		InternalDeactivate();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (!isActivated) {
			btnAnim.Play("ButtonEnter");
		}
		else {
			btnAnim.Play("ButtonSelectedEnter");
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (!isActivated) {
			btnAnim.Play("ButtonExit");
		}
		else {
			btnAnim.Play("ButtonSelectedExit");
		}
	}
}