/*
* Title:            GameSaveLoadController.cs
* Author:           James (Jake) Gilbert
* Email:            jgilbert10345@gmail.com
* LinkedIn          https://www.linkedin.com/in/james-gilbert-2a79b1265/
* Description:      A save/load controller I made for my game in Unity.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaveLoadController : MonoBehaviour
{
    private int worldNumber;
    private double balance;
    private int paintCans;
    private int paintCansComing;
    private int redTintCans;
    private int redTintCansComing;
    private int redTintCansTotal;
    private int greenTintCans;
    private int greenTintCansComing;
    private int greenTintTotal;
    private int blueTintCans;
    private int blueTintCansComing;
    private int blueTintTotal;
    private int stirSticks;
    private int stirSticksComing;
    private int maxRedTintCans;
    private int maxGreenTintCans;
    private int maxBlueTintCans;
    private int paintCansTotal;
    private int[] tintAmount = new int[3];
    private int dayNumbers;
    private int paintOptions;
    private int timeOfDay;

    private int autoTinter;
    private int paintMixer;
    private int fan;
    private int counterTop;
    private int posters;
    private int workBench;
    private int tipJarLevel;
    private int tintShelves;
    private int woodShelves;
    private int metalShelves;
    private int paintBrushShelves;
    private int paintBrushes;
    private int paintRollerCovers;
    private int paintRollers;
    private int paintTrays;
    private int paintersTape;
    private bool hasDrill;
    private int paintChipDisplayCount;

    private int customerChance;
    private float customerPossibility;
    private float firstCustomerPatience;

    private double loanBalanceOwed; 

    public BalanceController balanceController;
    public InventoryManager inventoryManager;
    public ButtonUI buttonUI;
    public DayEndController dayEndController;
    public PaintOrder paintOrder;
    public DecorToolController decorToolController;
    public NpcChanceController npcChanceController;
    public loanController LoanController;

    public GameObject saveIcon;

    private float timer;
    public float updateDelayTime;

    void Start()
    {
        Load();
        saveIcon.SetActive(false);
    }
    void Update()
    {
        if (updateDelayTime <= 0)
        {
            updateDelayTime = 99999999;
        }
        if (timer > updateDelayTime)
        {
            Save();
            timer = 0;
        }
        timer += Time.deltaTime;
        // if (Input.GetKeyDown(KeyCode.Minus))
        // {
        //     wipeData();
        //     Debug.Log("Successfully wiped data");
        // }
    }

    public void setUpdateTime(float time)
    {
        updateDelayTime = time;
    }

    IEnumerator saveIconRun()
    {
        saveIcon.SetActive(true);
        yield return new WaitForSeconds(2);
        saveIcon.SetActive(false);
    }
    public void Save()
    {
        StartCoroutine(saveIconRun());
        worldNumber = buttonUI.getWorldNumber();
        balance = balanceController.GetBalance();

        paintCans = inventoryManager.getPaintCans();
        paintCansComing = dayEndController.getPaintCansAdded();
        paintCansTotal = inventoryManager.getPaintCanTotal();

        redTintCans = inventoryManager.getRedStock();
        redTintCansComing = dayEndController.getRedTintAdded();
        redTintCansTotal = inventoryManager.GetRedTintCanTotal();

        greenTintCans = inventoryManager.getGreenStock();
        greenTintCansComing = dayEndController.getGreenTintAdded();
        greenTintTotal = inventoryManager.GetGreenTintCanTotal();

        blueTintCans = inventoryManager.getBlueStock();
        blueTintCansComing = dayEndController.getBlueTintAdded();
        blueTintTotal = inventoryManager.GetBlueTintCanTotal();

        stirSticks = inventoryManager.getStirSticks();
        stirSticksComing = dayEndController.getStirSticksAdded();

        tintAmount[0] = inventoryManager.getRedTint();
        tintAmount[1] = inventoryManager.getGreenTint();
        tintAmount[2] = inventoryManager.getBlueTint();

        dayNumbers = dayEndController.getDayNumber();
        timeOfDay = dayEndController.getTime();

        paintOptions = paintOrder.GetPaintRange();

        autoTinter = decorToolController.getAutoTinter();
        paintMixer = decorToolController.getPaintMixer();
        fan = decorToolController.getFan();
        counterTop = decorToolController.getCounterTop();
        posters = decorToolController.getPosters();
        workBench = decorToolController.getWorkbench();
        tipJarLevel = decorToolController.getTipJarLevel();
        tintShelves = decorToolController.getTintShelves();
        woodShelves = decorToolController.getWoodShelves();
        metalShelves = decorToolController.getMetalShelves(); 
        paintBrushShelves = decorToolController.getPaintBrushShelves();
        paintBrushes = decorToolController.getPaintBrushes();
        paintRollerCovers = decorToolController.getPaintRollerCovers();
        paintRollers = decorToolController.getPaintRollers();
        paintTrays = decorToolController.getPaintTrays();
        paintersTape = decorToolController.getPainterTape();
        hasDrill = decorToolController.getHasDrill();
        paintChipDisplayCount = decorToolController.getPaintChipDisplay();

        customerChance = npcChanceController.getCustomerChance();
        customerPossibility = npcChanceController.getCustomerPossibility();
        firstCustomerPatience = npcChanceController.getFirstCustomerPatience();

        loanBalanceOwed = LoanController.getLoanAmounts();

        Debug.Log("Game Successfully Saved");

        string destination = Application.persistentDataPath + "/save.dat";
        using (StreamWriter writer = new StreamWriter(destination))
        {
            writer.WriteLine(worldNumber);
            writer.WriteLine(balance);
            writer.WriteLine(paintCans);
            writer.WriteLine(paintCansComing);
            writer.WriteLine(paintCansTotal);
            writer.WriteLine(redTintCans);
            writer.WriteLine(redTintCansComing);
            writer.WriteLine(redTintCansTotal);
            writer.WriteLine(greenTintCans);
            writer.WriteLine(greenTintCansComing);
            writer.WriteLine(greenTintTotal);
            writer.WriteLine(blueTintCans);
            writer.WriteLine(blueTintCansComing);
            writer.WriteLine(blueTintTotal);
            writer.WriteLine(stirSticks);
            writer.WriteLine(stirSticksComing);
            writer.WriteLine(tintAmount[0]);
            writer.WriteLine(tintAmount[1]);
            writer.WriteLine(tintAmount[2]);
            writer.WriteLine(dayNumbers);
            writer.WriteLine(timeOfDay);
            writer.WriteLine(paintOptions);
            writer.WriteLine(autoTinter);
            writer.WriteLine(paintMixer);
            writer.WriteLine(fan);
            writer.WriteLine(counterTop);
            writer.WriteLine(posters);
            writer.WriteLine(workBench);
            writer.WriteLine(tipJarLevel);
            writer.WriteLine(tintShelves);
            writer.WriteLine(woodShelves);
            writer.WriteLine(metalShelves);
            writer.WriteLine(paintBrushShelves);
            writer.WriteLine(paintBrushes);
            writer.WriteLine(paintRollerCovers);
            writer.WriteLine(paintRollers);
            writer.WriteLine(paintTrays);
            writer.WriteLine(paintersTape);
            writer.WriteLine(hasDrill);
            writer.WriteLine(paintChipDisplayCount);
            writer.WriteLine(customerChance);
            writer.WriteLine(customerPossibility);
            writer.WriteLine(firstCustomerPatience);
            writer.WriteLine(loanBalanceOwed);
        }
    }
    public void Load()
    {
        string destination = Application.persistentDataPath + "/save.dat";

        if (File.Exists(destination))
        {
            using (StreamReader reader = new StreamReader(destination))
            {
                worldNumber = int.Parse(reader.ReadLine());
                balance = double.Parse(reader.ReadLine());
                paintCans = int.Parse(reader.ReadLine());
                paintCansComing = int.Parse(reader.ReadLine());
                paintCansTotal = int.Parse(reader.ReadLine());
                redTintCans = int.Parse(reader.ReadLine());
                redTintCansComing = int.Parse(reader.ReadLine());
                redTintCansTotal = int.Parse(reader.ReadLine());
                greenTintCans = int.Parse(reader.ReadLine());
                greenTintCansComing = int.Parse(reader.ReadLine());
                greenTintTotal = int.Parse(reader.ReadLine());
                blueTintCans = int.Parse(reader.ReadLine());
                blueTintCansComing = int.Parse(reader.ReadLine());
                blueTintTotal = int.Parse(reader.ReadLine());
                stirSticks = int.Parse(reader.ReadLine());
                stirSticksComing = int.Parse(reader.ReadLine());
                tintAmount[0] = int.Parse(reader.ReadLine());
                tintAmount[1] = int.Parse(reader.ReadLine());
                tintAmount[2] = int.Parse(reader.ReadLine());
                dayNumbers = int.Parse(reader.ReadLine());
                timeOfDay = int.Parse(reader.ReadLine());
                paintOptions = int.Parse(reader.ReadLine());
                autoTinter = int.Parse(reader.ReadLine());
                paintMixer = int.Parse(reader.ReadLine());
                fan = int.Parse(reader.ReadLine());
                counterTop = int.Parse(reader.ReadLine());
                posters = int.Parse(reader.ReadLine());
                workBench = int.Parse(reader.ReadLine());
                tipJarLevel = int.Parse(reader.ReadLine());
                tintShelves = int.Parse(reader.ReadLine());
                woodShelves = int.Parse(reader.ReadLine());
                metalShelves = int.Parse(reader.ReadLine());
                paintBrushShelves = int.Parse(reader.ReadLine());
                paintBrushes = int.Parse(reader.ReadLine());
                paintRollerCovers = int.Parse(reader.ReadLine());
                paintRollers = int.Parse(reader.ReadLine());
                paintTrays = int.Parse(reader.ReadLine());
                paintersTape = int.Parse(reader.ReadLine());
                hasDrill = bool.Parse(reader.ReadLine());
                paintChipDisplayCount = int.Parse(reader.ReadLine());
                customerChance = int.Parse(reader.ReadLine());
                customerPossibility = float.Parse(reader.ReadLine());
                firstCustomerPatience = float.Parse(reader.ReadLine());
                loanBalanceOwed = double.Parse(reader.ReadLine());
            }

            
            balanceController.setBalance(balance);
            dayEndController.LoadData(paintCansComing, redTintCansComing, greenTintCansComing, blueTintCansComing, stirSticksComing, dayNumbers, timeOfDay);
            paintOrder.setPaintRange(paintOptions);
            decorToolController.LoadData(autoTinter, paintMixer, fan, counterTop, posters, workBench, tipJarLevel, tintShelves, woodShelves, metalShelves, paintBrushShelves, paintBrushes, paintRollerCovers, paintRollers, paintTrays, paintersTape, hasDrill, paintChipDisplayCount);
            inventoryManager.LoadData(worldNumber, paintCans, paintCansTotal, redTintCans, redTintCansTotal, greenTintCans, greenTintTotal, blueTintCans, blueTintTotal, stirSticks, tintAmount);
            buttonUI.getPaintCans();
            buttonUI.resetCans();
            buttonUI.addCans(paintCans);
            npcChanceController.loadData(customerChance, customerPossibility, firstCustomerPatience);
            LoanController.loadData(loanBalanceOwed);
            

            Debug.Log("Game Successfully Loaded");
        }
        else
        {
            Debug.Log("No saved game found");
            wipeData();
        }
    }

    public void wipeData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        using (StreamWriter writer = new StreamWriter(destination))
        {
            writer.WriteLine(1);
            writer.WriteLine(50);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(2);
            writer.WriteLine(0);
            writer.WriteLine(255);
            writer.WriteLine(255);
            writer.WriteLine(255);
            writer.WriteLine(0);
            writer.WriteLine(730);
            writer.WriteLine(255);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(false);
            writer.WriteLine(0);
            writer.WriteLine(25);
            writer.WriteLine(20);
            writer.WriteLine(100);
            writer.WriteLine(0);
            }
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(1);
    }

    public void moveWorlds()
    {
        Save();
        Debug.Log("Saved");
        WaitForFunction();
        Load();
        Debug.Log("Loaded," + balance);
        WaitForFunction();
        string destination = Application.persistentDataPath + "/save.dat";
        using (StreamWriter writer = new StreamWriter(destination))
        {
            writer.WriteLine(2);
            writer.WriteLine(balance);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(1);
            writer.WriteLine(2);
            writer.WriteLine(0);
            writer.WriteLine(255);
            writer.WriteLine(255);
            writer.WriteLine(255);
            writer.WriteLine(dayNumbers);
            writer.WriteLine(730);
            writer.WriteLine(paintOptions);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(tipJarLevel);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(false);
            writer.WriteLine(0);
            writer.WriteLine(10);
            writer.WriteLine(30);
            writer.WriteLine(100);
            writer.WriteLine(loanBalanceOwed);
            }
            WaitForFunction();
        Load();
        Debug.Log("Move successful");
        WaitForFunction();
        inventoryManager.loadWorld(2);
    }
    
}
