using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHUD : MonoBehaviour {

    [SerializeField]
    GameObject Player, BoostTimerBox, ForceTimerBox, LevitateTimerBox;

    [SerializeField]
    Image boostImage;

    Sprite transparentSprite, boostSprite, inactiveBoostSprite;

    [SerializeField]
    Text BoostText, ForceText, LevitateText;

    bool boostUnlocked = false;

    private float endTime;

    private Powerup activePowerup = Powerup.None;
    

	// Use this for initialization
	void Start ()
    {
        this.BoostText = this.BoostTimerBox.GetComponent<Text>();
        this.ForceText = this.ForceTimerBox.GetComponent<Text>();
        this.LevitateText = this.LevitateTimerBox.GetComponent<Text>();

        this.transparentSprite = Resources.Load<Sprite>("Transparent");
        this.boostSprite = Resources.Load<Sprite>("Boost");
        this.inactiveBoostSprite = Resources.Load<Sprite>("Inactive_Boost");

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (this.activePowerup != Powerup.None)
        {
            if (this.activePowerup == Powerup.Boost)
            {
                this.BoostText.text = Mathf.RoundToInt((this.endTime - Time.time)).ToString();

                if (this.endTime - Time.time <= 0)
                {
                    this.activePowerup = Powerup.None;
                    this.boostImage.sprite = this.inactiveBoostSprite;
                }

            }
            else if (this.activePowerup == Powerup.Force)
            {
                this.ForceText.text = Mathf.RoundToInt((this.endTime - Time.time)).ToString();

                if (this.endTime - Time.time <= 0)
                {
                    this.activePowerup = Powerup.None;
                }

            }
            else if (this.activePowerup == Powerup.Levitate)
            {
                this.ForceText.text = Mathf.RoundToInt((this.endTime - Time.time)).ToString();

                if (this.endTime - Time.time <= 0)
                {
                    this.activePowerup = Powerup.None;
                }

            }



        }
	}

    public void unlockBoost()
    {
        this.boostImage.sprite = this.inactiveBoostSprite;

        wait(.2f);

        this.boostImage.sprite = this.transparentSprite;

        wait(.2f);

        this.boostImage.sprite = this.inactiveBoostSprite;

        wait(.2f);

        this.boostImage.sprite = this.transparentSprite;

        wait(.2f);

        this.boostImage.sprite = this.inactiveBoostSprite;

        boostUnlocked = true;
    }

    private void wait(float duration)
    {
        float initTime = Time.time;

        while(Time.time - initTime < duration)
        {

        }
    }

    public void changePowerup(Powerup powerup)
    {
        if (powerup == Powerup.Boost)
        {
            if (boostUnlocked)
            {
                this.activePowerup = powerup;
                this.endTime = Time.time + 10;
            }
        }

    }

    public enum Powerup
    {
        Boost,
        Force,
        Levitate,
        None
    }

}

