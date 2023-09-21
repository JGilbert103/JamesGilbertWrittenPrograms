/*
* Title:            GamblingManager.cs
* Author:           James (Jake) Gilbert
* Email:            jgilbert10345@gmail.com
* LinkedIn          https://www.linkedin.com/in/james-gilbert-2a79b1265/
* Description:      A simple higher/lower game I made for my game in Unity.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamblingManager : MonoBehaviour
{
    public BalanceController balanceController;
    public int currentBet;
    public int currentBalance;
    public int currentNumber;
    public int previousNumber;
    private int nextNumber;
    private int streak;
    public TMP_Text currentBetText;
    public TMP_Text currentNumberText;
    public TMP_Text previousNumberText;
    public TMP_Text streakText;
    public TMP_Text cashOutAmount;

    public GameObject cashOutButton;
    public GameObject depositButton;
    public GameObject higherButton;
    public GameObject sameButton;
    public GameObject lowerButton;
    public GameObject[] betsButtons;
    public int totalEarnedInDay;
    // Start is called before the first frame update
    void Start()
    {
        currentNumber = 50;
        currentBet = 10;
        cashOutButton.SetActive(false);
        higherButton.SetActive(false);
        sameButton.SetActive(false);
        lowerButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentBetText.text = "$" + currentBet.ToString();
        currentNumberText.text = currentNumber.ToString();
        previousNumberText.text = previousNumber.ToString();
        streakText.text = streak.ToString();
        cashOutAmount.text = "CASH OUT (+$" + currentBalance.ToString() + ")";
        if (currentBalance + totalEarnedInDay > 1000)
        {
            cashOut();
        }
    }

    public void newNumber()
    {
        nextNumber = Random.Range(1,101);
        previousNumber = currentNumber;
    }

    public void cashOut()
    {
        previousNumber = nextNumber;
        currentNumber = 50;
        totalEarnedInDay += currentBalance;
        balanceController.addBalance(currentBalance);
        cashOutButton.SetActive(false);
        higherButton.SetActive(false);
        sameButton.SetActive(false);
        lowerButton.SetActive(false);
        currentBalance = 0;
        foreach (GameObject obj in betsButtons)
        {
            obj.SetActive(true);
        }
        depositButton.SetActive(true);
    }
    public void Bet()
    {
        if (balanceController.GetBalance() >= currentBet)
        {
            totalEarnedInDay -= currentBet;
            currentNumber = 50;
            currentBalance = currentBet;
            balanceController.BuyItem(currentBet);
            foreach (GameObject obj in betsButtons)
            {
                obj.SetActive(false);
            }
            depositButton.SetActive(false);
            cashOutButton.SetActive(true);
            higherButton.SetActive(true);
            sameButton.SetActive(true);
            lowerButton.SetActive(true);
        }
        else
        {
            balanceController.BuyItem(-1);
        }
        
    }

    public void Higher()
    {
        if (currentBalance >= currentBet)
        {
            newNumber();
            if (nextNumber > currentNumber)
            {
                currentNumber = nextNumber;
                currentBalance = currentBalance*2;
                streak++;
            }
            else
            {
                streak = 0;
                currentBalance = 0;
                cashOut();
            }
            
        }
    }
    public void Same()
    {
        if (currentBalance >= currentBet)
        {
            newNumber();
            if (nextNumber == currentNumber)
            {
                currentNumber = nextNumber;
                currentBalance = currentBalance*20;
                streak++;
                
            }
            else
            {
                streak = 0;
                currentBalance = 0;
                cashOut();
            }
            
        }
    }
    public void Lower()
    {
        if (currentBalance >= currentBet)
        {
            newNumber();
            if (nextNumber < currentNumber)
            {
                currentNumber = nextNumber;
                currentBalance = (currentBalance*2);
                streak++;
            }
            else
            {
                streak = 0;
                currentBalance = 0;
                cashOut();
            }
            
        }
    }

    public void increaseBet()
    {
        if (currentBet < 100 && currentBet+10 < balanceController.GetBalance())
        {
            currentBet += 10;
        }
    }
    public void decreaseBet()
    {
        if (currentBet > 10)
        {
            currentBet -= 10;
        }
    }
    public void minBet()
    {
        currentBet = 10;
    }
    public void maxBet()
    {
        if (balanceController.GetBalance() > 100)
        {
            currentBet = 100;
        }
        else if (balanceController.GetBalance() > 90)
        {
            currentBet = 90;
        }
        else if (balanceController.GetBalance() > 80)
        {
            currentBet = 80;
        }
        else if (balanceController.GetBalance() > 70)
        {
            currentBet = 70;
        }
        else if (balanceController.GetBalance() > 60)
        {
            currentBet = 60;
        }
        else if (balanceController.GetBalance() > 50)
        {
            currentBet = 50;
        }
        else if (balanceController.GetBalance() > 40)
        {
            currentBet = 40;
        }
        else if (balanceController.GetBalance() > 30)
        {
            currentBet = 30;
        }
        else if (balanceController.GetBalance() > 20)
        {
            currentBet = 20;
        }
        else if (balanceController.GetBalance() > 10)
        {
            currentBet = 10;
        }
        else
        {
            currentBet = 10;
        }
    }

    public void endDay()
    {
        totalEarnedInDay = 0;
    }
}
