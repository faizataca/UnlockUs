using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private SwitchHandler[] andSwitches;
    [SerializeField] private SwitchHandler[] orSwitches;
    [SerializeField] private bool useOrSwitches;

    [SerializeField] private Vector2 initialPosition, currentPosition, calledPosition;
    private float elapsedTime, scaledDuration;
    private bool movingToFinal;

    private void Start()
    {
        initialPosition = transform.position;
        scaledDuration = duration;
    }

    private void Update()
    {
        if ((andSwitches.Length != 0 && !useOrSwitches) || (orSwitches.Length != 0 && useOrSwitches))
        {
            DoMovementWithSwitchArrays();
        }

        currentPosition = transform.position;

        if (movingToFinal && currentPosition != finalPosition)
        {
            Move(finalPosition);
        }
        else if (!movingToFinal && currentPosition != initialPosition)
        {
            Move(initialPosition);
        }
    }

    private void DoMovementWithSwitchArrays()
    {
        if ((useOrSwitches && OneOrSwitchTrue()) || (!useOrSwitches && AndSwitchesTrue()))
        {
            if (!movingToFinal && currentPosition != finalPosition)
            {
                MoveToFinal();
            }
        }
        else
        {
            if (movingToFinal && currentPosition != initialPosition)
            {
                MoveToInitial();
            }
        }
    }

    private bool AndSwitchesTrue()
    {
        for (int i = 0; i < andSwitches.Length; i++)
        {
            if (!andSwitches[i].IsOn()) return false;
        }

        return true;
    }

    private bool OneOrSwitchTrue()
    {
        for (int i = 0; i < orSwitches.Length; i++)
        {
            if (orSwitches[i].IsOn()) return true;
        }

        return false;
    }

    public void MoveToInitial()
    {
        elapsedTime = Time.deltaTime;
        calledPosition = currentPosition;

        scaledDuration = duration - duration * (1 - InverseLerp(initialPosition, finalPosition, calledPosition));
        movingToFinal = false;
    }

    public void MoveToFinal()
    {
        elapsedTime = Time.deltaTime;
        calledPosition = currentPosition;

        scaledDuration = duration - duration * InverseLerp(initialPosition, finalPosition, calledPosition);
        movingToFinal = true;
    }

    private static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
    {
        Vector2 AB = b - a;
        Vector2 AV = value - a;
        return Vector2.Dot(AV, AB) / Vector2.Dot(AB, AB);
    }

    private void Move(Vector2 destinationPosition)
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = Mathf.Clamp01(elapsedTime / scaledDuration);

        Vector2 newPos2D = Vector2.Lerp(calledPosition, destinationPosition, curve.Evaluate(percentageComplete));
        transform.position = new Vector3(newPos2D.x, newPos2D.y, transform.position.z);
        
        // DEBUG di sini:
        Debug.Log($"{gameObject.name} | Target X: {destinationPosition.x}, Now at X: {transform.position.x}");
    }
}
