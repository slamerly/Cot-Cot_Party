using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxNbBullet;
    public int tNbBullet;

    private GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        weapons = GameObject.FindGameObjectsWithTag("Gun");
        //Debug.Log(weapons.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
