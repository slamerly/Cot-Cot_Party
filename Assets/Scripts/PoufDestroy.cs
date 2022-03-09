using UnityEngine;
using System.Collections;

public class PoufDestroy : MonoBehaviour
{

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(timeDestroy());
    }
    IEnumerator timeDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
