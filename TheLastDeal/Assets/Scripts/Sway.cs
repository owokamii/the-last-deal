using System;
using System.Collections;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float swayTime;
    [SerializeField] private float swayDegree;

    private Quaternion baseRotation;
    private Coroutine swayCoroutine;
    
    private void Awake() => baseRotation = transform.localRotation;

    private void OnEnable() => StartSway();

    private void StartSway()
    {
        if (swayCoroutine != null) StopCoroutine(swayCoroutine);
        swayCoroutine = StartCoroutine(SwayCoroutine());
    }

    private void StopSway()
    {
        if (swayCoroutine != null)
        {
            StopCoroutine(swayCoroutine);
            swayCoroutine = null;
        }
    }
    
    private IEnumerator SwayCoroutine()
    {
        float omega = 2f * Mathf.PI * swayTime; // omega is the angular frequency: the phase-advance rate of your sine wave, measured in radians per second.
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;
            float angle = Mathf.Sin(omega * time) * swayDegree;
            transform.localRotation = baseRotation * Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }
    }
}
