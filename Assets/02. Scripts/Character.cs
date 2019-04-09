using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	private RTSManager _rtsManager;
	private Projector _selectionCircle;
	public bool IsSelected;

	// Start is called before the first frame update
	void Start()
	{
		_rtsManager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
		_selectionCircle = GetComponentInChildren<Projector>();
		Deselect();
	}

	// Update is called once per frame
	void Update()
	{
		if (_rtsManager.IsWithinSelectionBounds(gameObject))
		{
			Select();
		}
		else
		{
			Deselect();
		}
	}

	public void Select()
	{
		IsSelected = true;
		_selectionCircle.enabled = true;
	}

	public void Deselect()
	{
		IsSelected = false;
		_selectionCircle.enabled = false;
	}
}