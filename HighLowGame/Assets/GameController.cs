using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int bank;
	private int bet;
	private bool firstRoll = true;
	Text TextDisplayGamePlay;
	Text TextName;
	Text TextBet;
	Text TextComputerRoll;
	Text TextUserRoll;
	Text TextResult;
	InputField InputFieldBank;
	Button ButtonYes;
	Button ButtonNo;
	CanvasGroup CanvasYesNo;

	// Use this for initialization
	void Start () {
		TextDisplayGamePlay = GameObject.Find("Canvas/TextDisplayGamePlay").GetComponent<Text>();
		TextName = GameObject.Find("Canvas/InputFieldName/Text").GetComponent<Text>();
		TextBet = GameObject.Find("Canvas/InputFieldBet/Text").GetComponent<Text>();
		TextComputerRoll = GameObject.Find("Canvas/TextComputerRoll").GetComponent<Text>();
		TextUserRoll = GameObject.Find("Canvas/TextUserRoll").GetComponent<Text>();
		TextResult = GameObject.Find("Canvas/TextResult").GetComponent<Text>();
		InputFieldBank = GameObject.Find("Canvas/InputFieldBank").GetComponent<InputField>(); 
		ButtonYes = GameObject.Find("CanvasYesNo/ButtonYes").GetComponent<Button>();
		ButtonNo = GameObject.Find("CanvasYesNo/ButtonNo").GetComponent<Button>();
		CanvasYesNo = GameObject.Find("CanvasYesNo").GetComponent<CanvasGroup>();
		
		CanvasYesNo.alpha = 0f;
		CanvasYesNo.blocksRaycasts = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void handleButtonRollClicked() {
		Debug.Log("handleButtonRollClicked() started");

		if (firstRoll) {
			firstRoll = false;
			InputFieldBank.readOnly = true;
			bank = System.Convert.ToInt32(InputFieldBank.text);
		}

		bet = System.Convert.ToInt32(TextBet.text);
		if (bet > bank) {
			TextDisplayGamePlay.text = "You cannot bet more than you have in your bank! Please enter a smaller bet and try again.";
			TextResult.text = "";
			TextComputerRoll.text = "";
			TextUserRoll.text = "";
			return;
		}

		TextDisplayGamePlay.text = "Hello, " + TextName.text + "!\n"
														 + "You bet $" + bet + "\n";

		// get computer roll
		int computerRoll = Random.Range(1, 7);
		TextComputerRoll.text = "I rolled a " + computerRoll + ".\n";

		// get user roll
		int userRoll = Random.Range(1, 7);
		TextUserRoll.text = "You rolled a " + userRoll + ".\n";

		// determine win/loss and update bank accordingly
		string result = "lose";
		if (userRoll > computerRoll) {
			 result = "win";
			 bank += bet;
		} else {
			 bank -= bet;
		}

		TextResult.text = "You " + result + ".\n" +
			"Your  bank is now $" + bank + ".";

		InputFieldBank.text = bank.ToString();

		if (bank <= 0) {
			TextResult.text = TextResult.text + "You are out of money. Game Over! \n"
				+ "Would you like to play again?";
			CanvasYesNo.alpha = 1f;
			CanvasYesNo.blocksRaycasts = true;
		}
	}

	public void handleButtonYesClicked() {
		firstRoll = true;
		CanvasYesNo.alpha = 0f;
		CanvasYesNo.blocksRaycasts = false;
		InputFieldBank.readOnly = false;
		InputFieldBank.text = "Enter your initial bank balance...";
		TextResult.text = "";
		TextDisplayGamePlay.text = "";
		TextComputerRoll.text = "";
		TextUserRoll.text = "";
		TextResult.text = "";
	}

	public void handleButtonNoClicked() {
		Debug.Log("handleButtonNoClicked()");
		Application.Quit();
	}
}
