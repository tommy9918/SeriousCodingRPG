using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion
{
    private float duration;
    public float current;
    private Vector2 startPosition;
    private Vector2 finalPosition;
    private float Xdifference;
    private float Ydifference;


    public Motion(float length, Vector2 start, Vector2 end)
    {
        duration = length;
        current = 0;
        startPosition = start;
        finalPosition = end;
        Xdifference = finalPosition.x - startPosition.x;
        Ydifference = finalPosition.y - startPosition.y;

    }

    public Motion(float length, float start, float end)
    {
        duration = length;
        current = 0;
        startPosition.x = start;
        finalPosition.x = end;
        Xdifference = finalPosition.x - startPosition.x;

    }

    public Vector2 UpdatePositionValue()
    {

        current++;
        float x = startPosition.x + BezierCurveValue(current, duration) * Xdifference;
        float y = startPosition.y + BezierCurveValue(current, duration) * Ydifference;

        return new Vector2(x, y);


    }

    public Vector2 DeUpdatePositionValue()
    {

        current++;
        float x = startPosition.x + DeBezierCurveValue(current, duration) * Xdifference;
        float y = startPosition.y + DeBezierCurveValue(current, duration) * Ydifference;
        return new Vector2(x, y);


    }

    public Vector2 AcUpdatePositionValue()
    {

        current++;
        float x = startPosition.x + AcBezierCurveValue(current, duration) * Xdifference;
        float y = startPosition.y + AcBezierCurveValue(current, duration) * Ydifference;
        return new Vector2(x, y);


    }

    public float UpdateValue()
    {

        current++;
        float x = startPosition.x + BezierCurveValue(current, duration) * Xdifference;

        return x;


    }


    public float BezierCurveValue(float now, float length)
    {
        Vector2 P0 = new Vector2(0f, 0f);
        Vector2 P1 = new Vector2(0.08f, 0.885f);
        Vector2 P2 = new Vector2(0.2f, 1f);
        Vector2 P3 = new Vector2(1f, 1f);
        float t = now / length;
        Vector2 a = (1 - t) * (1 - t) * (1 - t) * P0 + 3 * (1 - t) * (1 - t) * t * P1 + 3 * (1 - t) * t * t * P2 + t * t * t * P3;
        return a.y;
    }

    public float DeBezierCurveValue(float now, float length)
    {
        Vector2 P0 = new Vector2(0f, 0f);
        Vector2 P1 = new Vector2(0f, 0f);
        Vector2 P2 = new Vector2(0.2f, 1f);
        Vector2 P3 = new Vector2(1f, 1f);
        float t = now / length;
        Vector2 a = (1 - t) * (1 - t) * (1 - t) * P0 + 3 * (1 - t) * (1 - t) * t * P1 + 3 * (1 - t) * t * t * P2 + t * t * t * P3;
        return a.y;
    }

    public float AcBezierCurveValue(float now, float length)
    {
        Vector2 P0 = new Vector2(0f, 0f);
        Vector2 P1 = new Vector2(0.4f, 0f);
        Vector2 P2 = new Vector2(1f, 1f);
        Vector2 P3 = new Vector2(1f, 1f);
        float t = now / length;
        Vector2 a = (1 - t) * (1 - t) * (1 - t) * P0 + 3 * (1 - t) * (1 - t) * t * P1 + 3 * (1 - t) * t * t * P2 + t * t * t * P3;
        return a.y;
    }

    public void initializeMotion(float length, Vector2 start, Vector2 end)
    {
        duration = length;
        current = 0;
        startPosition = start;
        finalPosition = end;
        Xdifference = finalPosition.x - startPosition.x;
        Ydifference = finalPosition.y - startPosition.y;

    }

    public void initializeMotion(float length, float start, float end)
    {
        duration = length;
        current = 0;
        startPosition.x = start;
        finalPosition.x = end;
        Xdifference = finalPosition.x - startPosition.x;

    }







}
