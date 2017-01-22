using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Camera Camera;
    public Text ScoreFloatingPrefab;

    private int currentScore;
    private int currentMultiplyer;
    private Coroutine currentMultiplyerCoroutine;

    public int CurrentScore
    {
        get { return currentScore; }
    }

    private Vector2 canvasPointFromWorldPoint(Vector3 worldPoint)
    {
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = GetComponent<RectTransform>();

        Vector2 viewportPosition = Camera.WorldToViewportPoint(worldPoint);
        Vector2 screenPos = new Vector2(
        ((viewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((viewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        return screenPos;
    }

    public void ScoreAtPoint(Vector3 point, int score)
    {
        var screenPoint = canvasPointFromWorldPoint(point);
        var newScore = Instantiate(ScoreFloatingPrefab);
        newScore.transform.position = screenPoint;
        newScore.GetComponent<ScoreBehavior>().InitialPos = screenPoint;
        newScore.text = "" + score * currentMultiplyer;
        newScore.rectTransform.parent = GetComponent<RectTransform>();
        if (currentMultiplyer > 1)
        {
            newScore.GetComponent<ScoreBehavior>().Color = Color.green;
        }
        currentScore += score * currentMultiplyer;
    }

    public void MultiplyerAtPointForDuration(Vector3 point, int multiplyer, float duration)
    {
        if (currentMultiplyerCoroutine != null)
        {
            StopCoroutine(currentMultiplyerCoroutine);
        }

        currentMultiplyer = multiplyer;
        currentMultiplyerCoroutine = StartCoroutine(endMultiplyer(duration));
    }

    private IEnumerator endMultiplyer(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentMultiplyer = 1;
    }
}
