using System;

using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
#pragma warning disable 0649
	[SerializeField]
	private Button btn;
#pragma warning restore 0649

	public event Action StartGameButtonClicked; 

	private void Start()
	{
		btn.onClick.AddListener(this.Hide);
		StartGameButtonClicked?.Invoke();
	}
}
