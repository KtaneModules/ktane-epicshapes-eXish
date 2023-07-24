using System;
using System.Collections;
using KModkit;
using UnityEngine;

public class EpicShapesScript : MonoBehaviour
{
	private void Awake()
	{
		moduleId = moduleIDCounter++;
		KMSelectable[] array = buttons;
		for (int i = 0; i < array.Length; i++)
		{
			KMSelectable kmselectable = array[i];
			KMSelectable pressedButton = kmselectable;
			KMSelectable kmselectable2 = kmselectable;
			kmselectable2.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(kmselectable2.OnInteract, new KMSelectable.OnInteractHandler(delegate()
			{
				ButtonPress(pressedButton);
				return false;
			}));
		}
	}

	private void Start()
	{
		cbEnabled = cbMode.ColorblindModeActive;
		Reset();
	}

	private void Reset()
	{
		pickSmallFigure();
		pickLargeFigure();
		pickBG();
		DetermineList();
		pickButtons();
		ButtonSolve();
		firstButtonClicked = 0;
		secondButtonClicked = 0;
		thirdButtonClicked = 0;
		smallFigureIndex = 0;
		largeFigureIndex = 0;
		bgIndex = 0;
		firstButtonIndex = 0;
		secondButtonIndex = 0;
		thirdButtonIndex = 0;
		moduleSolved = false;
	}

	private void DetermineList()
	{
		Debug.LogFormat("[Epic Shapes #{0}] The number of batteries on the bomb is {1}.", moduleId, bomb.GetBatteryCount());
	}

	private void pickButtons()
	{
		firstButtonIndex = UnityEngine.Random.Range(0, 14);
		firstButton.GetComponentInChildren<TextMesh>().text = buttonOptions[firstButtonIndex];
		Debug.LogFormat("[Epic Shapes #{0}] The first button number is {1}.", new object[]
		{
			moduleId,
			buttonOptions[firstButtonIndex]
		});
		secondButtonIndex = UnityEngine.Random.Range(0, 14);
		secondButton.GetComponentInChildren<TextMesh>().text = buttonOptions[secondButtonIndex];
		Debug.LogFormat("[Epic Shapes #{0}] The second button number is {1}.", new object[]
		{
			moduleId,
			buttonOptions[secondButtonIndex]
		});
		thirdButtonIndex = UnityEngine.Random.Range(0, 14);
		thirdButton.GetComponentInChildren<TextMesh>().text = buttonOptions[thirdButtonIndex];
		Debug.LogFormat("[Epic Shapes #{0}] The third button number is {1}.", new object[]
		{
			moduleId,
			buttonOptions[thirdButtonIndex]
		});
	}

	private void pickSmallFigure()
	{
		smallFigureIndex = UnityEngine.Random.Range(0, 12);
		smallFigure.material = smallFigureOptions[smallFigureIndex];
		Debug.LogFormat("[Epic Shapes #{0}] The smaller shape is {1}.", new object[]
		{
			moduleId,
			smallFigureOptions[smallFigureIndex].name
		});
	}

	private void pickLargeFigure()
	{
		largeFigureIndex = UnityEngine.Random.Range(0, 5);
		largeFigure.material = largeFigureOptions[largeFigureIndex];
		Debug.LogFormat("[Epic Shapes #{0}] The larger shape is {1}.", new object[]
		{
			moduleId,
			largeFigureOptions[largeFigureIndex].name
		});
	}

	private void pickBG()
	{
		bgIndex = UnityEngine.Random.Range(0, 5);
		bg.material = bgOptions[bgIndex];
		if (cbEnabled)
			cbText.text = bgOptions[bgIndex].name;
		Debug.LogFormat("[Epic Shapes #{0}] The background color is {1}.", new object[]
		{
			moduleId,
			bgOptions[bgIndex].name
		});
	}

	private void ButtonPress(KMSelectable button)
	{
		button.AddInteractionPunch(1f);
		audio.HandlePlayGameSoundAtTransform.Invoke(0, button.transform);
		if (!moduleSolved)
		{
			if (string.Equals(button.name, "ButtonFirst"))
			{
				firstButtonClicked++;
				Debug.LogFormat("[Epic Shapes #{0}] The first button has been pressed {1} times.", moduleId, firstButtonClicked);
			}
			else if (string.Equals(button.name, "ButtonSecond"))
			{
				secondButtonClicked++;
				Debug.LogFormat("[Epic Shapes #{0}] The second button has been pressed {1} times.", moduleId, secondButtonClicked);
			}
			else if (string.Equals(button.name, "ButtonThird"))
			{
				thirdButtonClicked++;
				Debug.LogFormat("[Epic Shapes #{0}] The third button has been pressed {1} times.", moduleId, thirdButtonClicked);
			}
			else if (firstButtonClicked == firstButtonClickNum)
			{
				if (secondButtonClicked == secondButtonClickNum)
				{
					if (thirdButtonClicked == thirdButtonClickNum)
					{
						Debug.LogFormat("[Epic Shapes #{0}] Submit pressed, module solved.", moduleId);
						GetComponent<KMBombModule>().HandlePass();
						moduleSolved = true;
					}
					else
					{
						Debug.LogFormat("[Epic Shapes #{0}] Submit pressed, strike and resetting.", moduleId);
						GetComponent<KMBombModule>().HandleStrike();
						Reset();
					}
				}
				else
				{
					Debug.LogFormat("[Epic Shapes #{0}] Submit pressed, strike and resetting.", moduleId);
					GetComponent<KMBombModule>().HandleStrike();
					Reset();
				}
			}
			else
			{
				Debug.LogFormat("[Epic Shapes #{0}] Submit pressed, strike and resetting.", moduleId);
				GetComponent<KMBombModule>().HandleStrike();
				Reset();
			}
		}
	}

	private void ButtonSolve()
	{
		if (bgOptions[bgIndex].name == "red")
		{
			if (largeFigureOptions[largeFigureIndex].name == "Star")
			{
				x = 3;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Square")
			{
				x = 1;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Pentagon")
			{
				x = 6;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "PentagonUpside")
			{
				x = 6;
			}
			else
			{
				x = 2;
			}
		}
		else if (bgOptions[bgIndex].name == "green")
		{
			if (largeFigureOptions[largeFigureIndex].name == "Star")
			{
				x = 1;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Square")
			{
				x = 3;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Pentagon")
			{
				x = 2;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "PentagonUpside")
			{
				x = 2;
			}
			else
			{
				x = 6;
			}
		}
		else if (bgOptions[bgIndex].name == "blue")
		{
			if (largeFigureOptions[largeFigureIndex].name == "Star")
			{
				x = 2;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Square")
			{
				x = 5;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Pentagon")
			{
				x = 4;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "PentagonUpside")
			{
				x = 4;
			}
			else
			{
				x = 1;
			}
		}
		else
		{
			if (largeFigureOptions[largeFigureIndex].name == "Star")
			{
				x = 4;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Square")
			{
				x = 2;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "Pentagon")
			{
				x = 1;
			}
			else if (largeFigureOptions[largeFigureIndex].name == "PentagonUpside")
			{
				x = 1;
			}
			else
			{
				x = 3;
			}
		}
		if (smallFigureOptions[smallFigureIndex].name == "Square")
		{
			y = 2;
		}
		else if (smallFigureOptions[smallFigureIndex].name == "PentagonFilled")
		{
			y = 5;
		}
		else if (smallFigureOptions[smallFigureIndex].name == "StarFilled")
		{
			y = 1;
		}
		else if (smallFigureOptions[smallFigureIndex].name == "SquareFilled")
		{
			y = 4;
		}
		else
		{
			y = 6;
		}
		int.TryParse(buttonOptions[firstButtonIndex], out a);
		int.TryParse(buttonOptions[secondButtonIndex], out b);
		int.TryParse(buttonOptions[thirdButtonIndex], out c);
		if (a >= b)
		{
			d = a - b;
		}
		else
		{
			d = b - a;
		}
		z = d + c;
		w = x + y + z;
		Debug.LogFormat("[Epic Shapes #{0}] Value W is {1}.", moduleId, w);
		if (w <= 8)
		{
			if (w >= 0)
			{
				firstButtonClickNum = a;
				secondButtonClickNum = b;
				thirdButtonClickNum = c;
			}
		}
		else if (w <= 16)
		{
			if (w >= 9)
			{
				firstButtonClickNum = 6;
				secondButtonClickNum = 3;
				thirdButtonClickNum = a;
			}
		}
		else if (w <= 25)
		{
			if (w >= 17)
			{
				if (bomb.GetBatteryCount() >= 2)
				{
					firstButtonClickNum = c;
					secondButtonClickNum = b;
					thirdButtonClickNum = c;
				}
				else
				{
					firstButtonClickNum = c;
					secondButtonClickNum = 1;
					thirdButtonClickNum = a;
				}
			}
		}
		else
		{
			firstButtonClickNum = c;
			secondButtonClickNum = 1;
			thirdButtonClickNum = a;
		}
		Debug.LogFormat("[Epic Shapes #{0}] The first button must be pressed {1} times, the second button {2} times, and the third button {3} times.", moduleId, firstButtonClickNum, secondButtonClickNum, thirdButtonClickNum);
	}

	//twitch plays
	#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press <btn> <#> [Presses the specified button '#' times] | !{0} submit [Presses the “SUBMIT” button] | !{0} colorblind [Toggles colorblind mode] | Valid buttons are 1-3 from left to right";
	#pragma warning restore 414
	IEnumerator ProcessTwitchCommand(string command)
    {
		if (command.EqualsIgnoreCase("colorblind"))
		{
			yield return null;
			cbEnabled = !cbEnabled;
			if (cbEnabled)
				cbText.text = bg.material.name.Replace(" (Instance)", "");
			else
				cbText.text = "";
			yield break;
		}
		if (command.EqualsIgnoreCase("submit"))
        {
			yield return null;
			buttons[3].OnInteract();
			yield break;
        }
		string[] parameters = command.Split(' ');
		if (parameters[0].ToLowerInvariant().StartsWith("press"))
        {
			if (parameters.Length == 1)
            {
				yield return "sendtochaterror Please specify a button and an amount of times to press the button!";
				yield break;
            }
			if (parameters.Length == 2 && parameters[1].EqualsAny("1", "2", "3"))
			{
				yield return "sendtochaterror Please specify an amount of times to press the button!";
				yield break;
			}
			if (parameters.Length == 2 && !parameters[1].EqualsAny("1", "2", "3"))
			{
				yield return "sendtochaterror!f The specified button '" + parameters[1] + "' is invalid!";
				yield break;
			}
			if (parameters.Length > 3)
			{
				yield return "sendtochaterror Too many parameters!";
				yield break;
			}
			if (!parameters[1].EqualsAny("1", "2", "3"))
            {
				yield return "sendtochaterror!f The specified button '" + parameters[1] + "' is invalid!";
				yield break;
			}
			int times = -1;
			if (!int.TryParse(parameters[2], out times))
			{
				yield return "sendtochaterror!f The specified amount '" + parameters[2] + "' is invalid!";
				yield break;
			}
			if (times <= 0)
            {
				yield return "sendtochaterror A button cannot be pressed '" + times + "' times!";
				yield break;
			}
			yield return null;
			for (int i = 0; i < times; i++)
            {
				buttons[int.Parse(parameters[1]) - 1].OnInteract();
				yield return new WaitForSeconds(.1f);
			}
		}
    }

	IEnumerator TwitchHandleForcedSolve()
    {
		if (firstButtonClicked > firstButtonClickNum || secondButtonClicked > secondButtonClickNum || thirdButtonClicked > thirdButtonClickNum)
        {
			GetComponent<KMBombModule>().HandlePass();
			moduleSolved = true;
		}
		while (firstButtonClicked < firstButtonClickNum)
        {
			buttons[0].OnInteract();
			yield return new WaitForSeconds(.1f);
		}
		while (secondButtonClicked < secondButtonClickNum)
		{
			buttons[1].OnInteract();
			yield return new WaitForSeconds(.1f);
		}
		while (thirdButtonClicked < thirdButtonClickNum)
		{
			buttons[2].OnInteract();
			yield return new WaitForSeconds(.1f);
		}
		buttons[3].OnInteract();
	}

	public KMAudio audio;

	public KMBombInfo bomb;

	public KMColorblindMode cbMode;

	public KMSelectable[] buttons;

	public Material[] smallFigureOptions;

	public Material[] largeFigureOptions;

	public Material[] bgOptions;

	public string[] buttonOptions;

	public Renderer smallFigure;

	public Renderer largeFigure;

	public Renderer bg;

	public Renderer firstButton;

	public Renderer secondButton;

	public Renderer thirdButton;

	public TextMesh cbText;

	private int smallFigureIndex;

	private int largeFigureIndex;

	private int bgIndex;

	private int firstButtonIndex;

	private int secondButtonIndex;

	private int thirdButtonIndex;

	private int firstButtonClickNum;

	private int secondButtonClickNum;

	private int thirdButtonClickNum;

	private int firstButtonClicked;

	private int secondButtonClicked;

	private int thirdButtonClicked;

	private int x;

	private int y;

	private int z;

	private int a;

	private int b;

	private int c;

	private int d;

	private int w;

	private bool cbEnabled;

	private static int moduleIDCounter = 1;

	private int moduleId;

	private bool moduleSolved;
}
