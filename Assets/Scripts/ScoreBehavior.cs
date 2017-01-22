using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehavior : MonoBehaviour
{
    public float Lifetime;
    public float MoveUpDistance = 50f;
    public Vector2 InitialPos;
    public Color Color;
    private float timeElapsed;

    void Start()
    {

    }

    void Update()
    {
        if (timeElapsed > Lifetime)
        {
            Destroy(gameObject);
            return;
        }

        float t = timeElapsed / Lifetime;

        GetComponent<RectTransform>().anchoredPosition = InitialPos + t * new Vector2(0, InitialPos.y + MoveUpDistance);
        var newColor = Color;
        newColor.a = 1 - t;
        GetComponent<Text>().color = newColor;

        timeElapsed += Time.deltaTime;
    }
}
