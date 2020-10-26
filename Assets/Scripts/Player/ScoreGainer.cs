using System;
using System.Collections;
using UnityEngine;

public class ScoreGainer : MonoBehaviour
{
    [SerializeField] private int scorePerObstacle = 5;
    [SerializeField] private int defaultScorePerSecond = 1;
    [SerializeField] private int boostScorePerSecond = 2;
    
    private void Awake()
    {
        GameManager.Instance.OnGameStart += EnableScoreGain;
    }

    private void EnableScoreGain()
    {
        InvokeRepeating("ScoreGain", 1f, 1f);
    }

    private void ScoreGain()
    {
        GameProfile.Score += InputManager.Instance.Boost ? boostScorePerSecond : defaultScorePerSecond;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameProfile.Score += scorePerObstacle;
            GameProfile.Asteroids++;
            StartCoroutine(DestroyObstacle(other.gameObject));
        }
    }

    private IEnumerator DestroyObstacle(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStart -= EnableScoreGain;
    }
}
