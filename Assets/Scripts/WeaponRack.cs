using UnityEngine;
using System.Collections;

public class WeaponRack : MonoBehaviour
{
    public GameObject weapon;
    public float rotationSpeed = 0.1f;
    public float timeRespawnWeapon = 5f;
    public Transform weaponsPlayer;

    // Start is called before the first frame update
   /* void Start()
    {
        //weapon = gameObject.transform.GetChild(0).gameObject;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
                weapon = gameObject.transform.GetChild(i).gameObject;
        }
        //weaponsPlayer = gameObject.transform.Find("Gun");
    }*/

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
                weapon = gameObject.transform.GetChild(i).gameObject;
        }
        if (weapon != null)
            weapon.transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && weapon != null)
        {
            switch (weapon.name)
            {
                case "Pistol":
                    weaponsPlayer.GetChild(0).GetComponent<Weapon>().currentBullet = weaponsPlayer.GetChild(0).GetComponent<Weapon>().nbMaxBullet;
                    break;
                case "Rifle":
                    weaponsPlayer.GetChild(1).GetComponent<Weapon>().currentBullet = weaponsPlayer.GetChild(1).GetComponent<Weapon>().nbMaxBullet;
                    break;
                case "Heavy":
                    weaponsPlayer.GetChild(2).GetComponent<Weapon>().currentBullet = weaponsPlayer.GetChild(2).GetComponent<Weapon>().nbMaxBullet;
                    break;
            }
            Debug.Log("Reaload");
            weapon.SetActive(false);
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(timeRespawnWeapon);
        int wNumber = Random.Range(0,3);
        gameObject.transform.GetChild(wNumber).gameObject.SetActive(true);
    }
}
