using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitWiggle : MonoBehaviour
{
    private float duration = 1.0f;
    public int direction = -1;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) {
            StartCoroutine(Wiggle());
            isMoving = true;
        }
    }

    IEnumerator Wiggle() {
        isMoving = true;

        Vector3 rotation = transform.rotation.eulerAngles;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        transform.Rotate(new Vector3(0, 0, 50 * direction));

        isMoving = false;
        direction *= -1;
    }
}
