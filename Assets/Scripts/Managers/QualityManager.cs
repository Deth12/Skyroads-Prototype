using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class QualityManager : MonoBehaviour
{
    public static QualityManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private PostProcessVolume ppVolume;
    
    // Settings
    private DepthOfField dof;
    private ChromaticAberration ca;
    
    private void Start()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        ppVolume.profile.TryGetSettings<DepthOfField>(out dof);
        ppVolume.profile.TryGetSettings<ChromaticAberration>(out ca);
    }

    public void SmoothToggleDOF(bool status, float time = 1f)
    {
        StartCoroutine(SmoothDOF(status ? 5:1, status ? 1:5, time));
    }

    public void SmoothToggleCA(bool status, float waitTime = 0.5f)
    {
        StartCoroutine(SmoothCA(status ? 0:1, status ? 1:0, waitTime));
    }

    private IEnumerator SmoothDOF(float from, float to, float waitTime)
    {
        if (dof != null)
        {
            float elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                dof.focusDistance.value = Mathf.Lerp(from, to, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
    
    private IEnumerator SmoothCA(float from, float to, float waitTime)
    {
        if (ca != null)
        {
            float elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                ca.intensity.value = Mathf.Lerp(from, to, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
