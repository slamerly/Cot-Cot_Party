using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public ParticleSystem pouf;
    public GameObject gameRule;

    private void Start()
    {
        gameRule = GameObject.Find("GameManager");
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<TargetController>().destinationSelected = transform.position;
        Instantiate(pouf, transform.position, Quaternion.Euler(-90, 0, 0));
        gameRule.GetComponent<GameRule>().currentScore += 10; 
        Destroy(gameObject);   
    }
}
