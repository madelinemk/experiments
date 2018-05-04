using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackController : MonoBehaviour {

  public Image PlayerCard1;
  public Image PlayerCard2;
  public Image PlayerCard3;
  public Image PlayerCard4;
  public Image PlayerCard5;

  public Image ComputerCard1;
  public Image ComputerCard2;
  public Image ComputerCard3;
  public Image ComputerCard4;
  public Image ComputerCard5;

  const string BACK_OF_CARD_IMAGE = "b1fv";

  Button StayButton;
  Button HitButton;

  Text TextResult;

  public int[] deckAsInts = new int[52];
  public int[] deckValues = { 11,11,11,11, 10,10,10,10, 10,10,10,10, 10,10,10,10 ,10,10,10,10,
                       9,9,9,9, 8,8,8,8, 7,7,7,7 ,6,6,6,6, 5,5,5,5, 4,4,4,4, 3,3,3,3, 2,2,2,2};

  public int nextCardInDeck = 0;
  public int playerCardCount = 0;
  public int computerCardCount = 0;
  public int playerHandValue = 0;
  public int computerHandValue = 0;
  public int computerHiddenCard;

  // Use this for initialization
  void Start () {
    Debug.Log("inside Start()");
    PlayerCard1 = GameObject.Find("Canvas/PlayerCard1").GetComponent<Image>();
    //PlayerCard1.gameObject.SetActive(false);
    PlayerCard2 = GameObject.Find("Canvas/PlayerCard2").GetComponent<Image>();
    //    PlayerCard2.gameObject.SetActive(false);
    PlayerCard3 = GameObject.Find("Canvas/PlayerCard3").GetComponent<Image>();
    //PlayerCard3.gameObject.SetActive(false);
    PlayerCard4 = GameObject.Find("Canvas/PlayerCard4").GetComponent<Image>();
    //PlayerCard4.gameObject.SetActive(false);
    PlayerCard5 = GameObject.Find("Canvas/PlayerCard5").GetComponent<Image>();
    //PlayerCard5.gameObject.SetActive(false);

    ComputerCard1 = GameObject.Find("Canvas/ComputerCard1").GetComponent<Image>();
    //    ComputerCard1.gameObject.SetActive(false);
    ComputerCard2 = GameObject.Find("Canvas/ComputerCard2").GetComponent<Image>();
    //ComputerCard2.gameObject.SetActive(false);
    ComputerCard3 = GameObject.Find("Canvas/ComputerCard3").GetComponent<Image>();
    //ComputerCard3.gameObject.SetActive(false);
    ComputerCard4 = GameObject.Find("Canvas/ComputerCard4").GetComponent<Image>();
    //ComputerCard4.gameObject.SetActive(false);
    ComputerCard5 = GameObject.Find("Canvas/ComputerCard5").GetComponent<Image>();
    //ComputerCard5.gameObject.SetActive(false);

    PlayerCard1.enabled = false;
    PlayerCard2.enabled = false;
    PlayerCard3.enabled = false;
    PlayerCard4.enabled = false;
    PlayerCard5.enabled = false;
    ComputerCard1.enabled = false;
    ComputerCard2.enabled = false;
    ComputerCard3.enabled = false;
    ComputerCard4.enabled = false;
    ComputerCard5.enabled = false;

    StayButton = GameObject.Find("Canvas/StayButton").GetComponent<Button>();
    StayButton.interactable = false;

    HitButton = GameObject.Find("Canvas/HitButton").GetComponent<Button>();
    HitButton.interactable = false;

    for (int i = 0; i < 52; i++) {
      deckAsInts[i] = i;
    }
    Shuffle(deckAsInts);

    TextResult = GameObject.Find("Canvas/TextResult").GetComponent<Text>();
 }

  // Update is called once per frame
  void Update () {
  }

  public void handleDealButtonClicked() {
    Debug.Log("in handleDealButtonClicked()");

    playerCardCount = 0;
    computerCardCount = 0;
    playerHandValue = 0;
    computerHandValue = 0;

    if (nextCardInDeck > 45) {
      nextCardInDeck = 0;
      Shuffle(deckAsInts);
    }

    // player card 1
    int currentCard = deckAsInts[nextCardInDeck];
    PlayerCard1.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
    playerHandValue += deckValues[currentCard];
    playerCardCount++; // increment the number of cards the player has
    nextCardInDeck++; // move to next card index and ready for next card to be dealt

    // computer card 1
    currentCard = deckAsInts[nextCardInDeck];
    ComputerCard1.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
    computerHandValue += deckValues[currentCard];
    computerCardCount++;
    nextCardInDeck++;

    // player card 2
    currentCard = deckAsInts[nextCardInDeck];
    PlayerCard2.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
    playerHandValue += deckValues[currentCard];
    playerCardCount++;
    nextCardInDeck++;

    // Computer card 2
    currentCard = deckAsInts[nextCardInDeck];
    computerHiddenCard = currentCard;
    ComputerCard2.sprite = Resources.Load<Sprite>(BACK_OF_CARD_IMAGE);
    computerHandValue += deckValues[computerHiddenCard];
    computerCardCount++;
    nextCardInDeck++;

    //Activate all the images
    // Debug.Log("activating cards 1 and 2 for player and computer");
    //ComputerCard1.gameObject.SetActive(true);
    //ComputerCard2.gameObject.SetActive(true);
    //PlayerCard1.gameObject.SetActive(true);
    //PlayerCard2.gameObject.SetActive(true);


    PlayerCard1.enabled = true;
    PlayerCard2.enabled = true;
    PlayerCard3.enabled = false;
    PlayerCard4.enabled = false;
    PlayerCard5.enabled = false;
    ComputerCard1.enabled = true;
    ComputerCard2.enabled = true;
    ComputerCard3.enabled = false;
    ComputerCard4.enabled = false;
    ComputerCard5.enabled = false;

    //Debug.Log("making sure cards 3,4,5 are invisible");
    /*    PlayerCard3.gameObject.SetActive(false);
    PlayerCard4.gameObject.SetActive(false);
    PlayerCard5.gameObject.SetActive(false);
    ComputerCard3.gameObject.SetActive(false);
    ComputerCard4.gameObject.SetActive(false);
    ComputerCard5.gameObject.SetActive(false);
    */
    //PlayerCard3.enabled = false;
    // Make hit and stay button interactable
    StayButton.interactable = true;
    HitButton.interactable = true;

    // check if player got 21
    if (playerHandValue == 21) {
      playerWins();
    }
  }

  public void handleStayButtonClicked() {
    Debug.Log("in handleStayButtonClicked()");

    if (nextCardInDeck > 45) {
      nextCardInDeck = 0;
      Shuffle(deckAsInts);
    }

    ComputerCard2.sprite = Resources.Load<Sprite>(computerHiddenCard.ToString());

    int currentCard;
    while (computerHandValue < 17 && computerHandValue <= 21 && computerCardCount < 5) {
      currentCard = deckAsInts[nextCardInDeck];
      computerHandValue += deckValues[currentCard];
      computerCardCount++;
      nextCardInDeck++;
      switch(computerCardCount) {
      case 3:
        ComputerCard3.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
        ComputerCard3.gameObject.SetActive(true);
        break;
      case 4:
        ComputerCard4.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
        ComputerCard4.gameObject.SetActive(true);
        break;
      case 5:
        ComputerCard5.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
        ComputerCard5.gameObject.SetActive(true);
        break;
      default:
        Debug.Log("Dealer already has 5 cards");
        break;
      }
    }

    if (playerHandValue == computerHandValue) {
      Debug.Log("tie");
      tie();
    } else if (computerHandValue < 22 && computerHandValue > playerHandValue) {
      dealerWins();
    } else {
      playerWins();
    }
  }

  public void handleHitButtonClicked() {
    Debug.Log("in handleHitButtonClicked()");

    if (nextCardInDeck > 45) {
      nextCardInDeck = 0;
      Shuffle(deckAsInts);
    }

    // Give player another card
    int currentCard;
    switch(playerCardCount) {
    case 2:
      currentCard = deckAsInts[nextCardInDeck];
      PlayerCard3.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
      playerHandValue += deckValues[currentCard];
      playerCardCount++;
      nextCardInDeck++;
      //PlayerCard3.gameObject.SetActive(true);
      PlayerCard3.enabled = true;
      break;
    case 3:
      currentCard = deckAsInts[nextCardInDeck];
      PlayerCard4.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
      playerHandValue += deckValues[currentCard];
      playerCardCount++;
      nextCardInDeck++;
      //PlayerCard4.gameObject.SetActive(true);
      PlayerCard4.enabled = true;
      break;
    case 4:
      currentCard = deckAsInts[nextCardInDeck];
      PlayerCard5.sprite = Resources.Load<Sprite>((currentCard+1).ToString());
      playerHandValue += deckValues[currentCard];
      playerCardCount++;
      nextCardInDeck++;
      //PlayerCard5.gameObject.SetActive(true);
      PlayerCard5.enabled = true;
      break;
    default:
      Debug.Log("tried to Hit before deal, or already has 5 cards");
      break;
    }

    if (playerHandValue > 21) {
      Debug.Log("bust");
      dealerWins();
    }
    else if (playerHandValue == 21) {
      playerWins();
    }
  }

  public void dealerWins() {
    Debug.Log("dealer Wins");
    TextResult.text = "Dealer Wins! Click Deal to play again.";
    StayButton.interactable = false;
    HitButton.interactable = false;
  }

  public void playerWins() {
    Debug.Log("player Wins");
    TextResult.text = "You Win! Click Deal to play again.";
    StayButton.interactable = false;
    HitButton.interactable = false;
  }

  public void tie() {
    Debug.Log("It's a tie");
    TextResult.text = "It's a tie. Click Deal to play again.";
    //resetGame();
    StayButton.interactable = false;
    HitButton.interactable = false;
  }

  /*
  public void resetGame() {
    Debug.Log("in resetGame()");
    TextResult.text = "";

    playerCardCount = 0;
    computerCardCount = 0;
    playerHandValue = 0;
    computerHandValue = 0;

    Debug.Log("making all the cards invisible again");
    PlayerCard1.gameObject.SetActive(false);
    PlayerCard2.gameObject.SetActive(false);
    PlayerCard3.gameObject.SetActive(false);
    PlayerCard4.gameObject.SetActive(false);
    PlayerCard5.gameObject.SetActive(false);

    ComputerCard1.gameObject.SetActive(false);
    ComputerCard2.gameObject.SetActive(false);
    ComputerCard3.gameObject.SetActive(false);
    ComputerCard4.gameObject.SetActive(false);
    ComputerCard5.gameObject.SetActive(false);

    if (nextCardInDeck > 45) {
      nextCardInDeck = 0;
      Shuffle(deckAsInts);
    }
  }*/

  /// Shuffle the array.
  public static void Shuffle<T>(T[] array) {
    for (int i = array.Length; i > 1; i--) {
      // Pick random element to swap.
      int j = Random.Range(0,i); // 0 <= j <= i-1
      // Swap.
      T tmp = array[j];
      array[j] = array[i - 1];
      array[i - 1] = tmp;
    }
  }
}
