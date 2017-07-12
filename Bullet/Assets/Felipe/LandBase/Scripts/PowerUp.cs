using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    public Text PowerUpTextPrice;
    public int PowerUpPriceNumber;

    public GameObject[] powerLevelGlow;
    public static int CurrentPowerLevel;
    public bool timeToChangePowerLevel;

    //true= add // false= subtract
    public bool powerLevelAddSubtract ;
    public int NotConfirmedPowerLevel;


    public int varCurentPower;
    public int Lvl_0PowerUpPriceNumber=10;

    // Use this for initialization
    public void Start()
    {
        timeToChangePowerLevel = false;
        NotConfirmedPowerLevel = 0;
        //CurrentPowerLevel = testvarCurentPower;

        //PowerUpPriceNumber = testvarPowerUpPriceNumber;
        if (CurrentPowerLevel == 0)
        {
            PowerUpPriceNumber = Lvl_0PowerUpPriceNumber;
        }
        else{
            PowerUpPriceNumber = Lvl_0PowerUpPriceNumber;
            for (int x = 0; x < CurrentPowerLevel; x++)
            {
                PowerUpPriceNumber = PowerUpPriceNumber * 2;
            }
        }
            PowerUpTextPrice.text = PowerUpPriceNumber.ToString();
        for (int x = 0; x <= 10; x++)
        {
            if (x <= (CurrentPowerLevel - 1))
            {
                powerLevelGlow[x].SetActive(true);
            }else
            powerLevelGlow[x].SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        varCurentPower = CurrentPowerLevel;
        if (timeToChangePowerLevel)
        {
            timeToChangePowerLevel = false;
            if (powerLevelAddSubtract)
            {
                NotConfirmedPowerLevel ++;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel - 1)].SetActive(true);
                PowerUpPriceNumber = PowerUpPriceNumber * 2;
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString();
            }
            else
            {
                NotConfirmedPowerLevel --;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel )].SetActive(false);
                PowerUpPriceNumber = PowerUpPriceNumber / 2;
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString();

            }
        }
	}
    public void SavePoints()
    {   
        CurrentPowerLevel = CurrentPowerLevel + NotConfirmedPowerLevel;
        NotConfirmedPowerLevel = 0;
    }
    public void AddLevelPowerUp()
    {
        if (CurrentPowerLevel <10 && NotConfirmedPowerLevel < 10 - CurrentPowerLevel)
        {
            powerLevelAddSubtract = true;
            timeToChangePowerLevel = true;
        }

    }
    public void SubtractLevelPowerUp()
    {
        if (NotConfirmedPowerLevel > 0 && NotConfirmedPowerLevel > 0)
        {
            powerLevelAddSubtract = false;
            timeToChangePowerLevel = true;
        }

    }
}
