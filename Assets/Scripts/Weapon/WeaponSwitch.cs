using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;
    public float timeSwitch = 1f;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedWeapon = 2;
        /*if (Input.GetKeyDown(KeyCode.G))
            animator.SetBool("Switching", true);
        selectedWeapon = 3;
        */

        if (previousSelectedWeapon != selectedWeapon)
        {
            /*transform.GetChild(previousSelectedWeapon).GetComponent<Weapon>().weaponReady = false;
            animator.SetBool("Switching", true);
            //SwitchOff();
            transform.GetChild(previousSelectedWeapon).gameObject.SetActive(false);
            */
            //Debug.Log("prevWeapon" + previousSelectedWeapon);
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.GetComponent<Weapon>().weaponReady = true;
                weapon.gameObject.SetActive(true);
                //animator.SetBool("Switching", false);
                //Debug.Log("selectedWeapon" + selectedWeapon);
            }
            else
            {
                weapon.GetComponent<Weapon>().weaponReady = false;
                //animator.SetBool("Switching", false);
                weapon.gameObject.SetActive(false);
                //Debug.Log("not selected");
            }
            i++;
        }
    }
}
