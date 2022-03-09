using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameRule : MonoBehaviour
{

    //public GameObject timer;
    public float currentTime;
    public float startingTime = 180;
    public int currentScore;
    public Text currentTimeText;
    public Text currentAmmoText;
    public Text scoreText;
    public Text endGameText;
    public GameObject guns;
    public GameObject UIInGame;
    public GameObject endscreen;

    private int weaponSelect;
    private int currentAmmo;
    private int maxAmmo;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        weaponSelect = guns.GetComponent<WeaponSwitch>().selectedWeapon;
        Timer();
        Ammo();
        Score();
    }

    void Timer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            EndGame();
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":";
        if (time.Seconds < 10)
            currentTimeText.text += "0" + time.Seconds.ToString();
        else
            currentTimeText.text += time.Seconds.ToString();
        //currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    void Ammo()
    {
        maxAmmo = guns.transform.GetChild(weaponSelect).gameObject.GetComponent<Weapon>().nbMaxBullet + 1;
        currentAmmo = guns.transform.GetChild(weaponSelect).gameObject.GetComponent<Weapon>().currentBullet + 1;

        currentAmmoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    void Score()
    {
        scoreText.text = currentScore.ToString();
    }

    void EndGame()
    {
        endGameText.text = "Your Score : " + currentScore.ToString();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        UIInGame.SetActive(false);
        endscreen.SetActive(true);
    }
}
