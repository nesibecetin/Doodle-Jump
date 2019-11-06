using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Blue_Controller : MonoBehaviour
{
    float direction = -1;
    public float time = 1;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Hareket());
    }
    void Update()
    {
        this.gameObject.transform.Translate(new Vector2((speed * direction * Time.deltaTime), 0)); 
    }

    IEnumerator Hareket()
    {
        direction = +1;
        yield return new WaitForSeconds(time);
        direction = -1;
        yield return new WaitForSeconds(time);

        StartCoroutine(Hareket());
    }
  
}
