using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{

    private bool isMoving;
    private bool isMovingBlock = false;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    public MovementManager movementManager;
    private float timeToMove = 0.2f;
    public float shakeDuration = 0.3f;
    //[SerializeField]
    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        movementManager = new MovementManager();
        isMovingBlock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving) {
            if (movementManager.CanMove(isMovingBlock, (-1, 0))) {
                movementManager.Move((-1, 0));
                StartCoroutine(MoveCursor(Vector3.up));
            } else {
                StartCoroutine(Shake());
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) && !isMoving) {

            if (movementManager.CanMove(isMovingBlock, (1, 0))) {
                movementManager.Move((1, 0));
                StartCoroutine(MoveCursor(Vector3.down));
            } else {
                StartCoroutine(Shake());
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving) {
            if (movementManager.CanMove(isMovingBlock, (0, -1))) {
                movementManager.Move((0, -1));
                StartCoroutine(MoveCursor(Vector3.left));
            } else {
                StartCoroutine(Shake());
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isMoving) {
            if (movementManager.CanMove(isMovingBlock, (0, 1))) {
                movementManager.Move((0, 1));
                StartCoroutine(MoveCursor(Vector3.right));
            } else {
                StartCoroutine(Shake());
            }
        }
    }

    private IEnumerator MoveCursor(Vector3 direction) {
        isMoving = true;

        float elapsedTime = 0.0f;

        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        while (elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        originalPosition = transform.position;

        isMoving = false;
    }


    private IEnumerator Shake() {
        isMoving = true;
        
        Vector3 startPosition = transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = originalPosition;

        isMoving = false;
    }
}
