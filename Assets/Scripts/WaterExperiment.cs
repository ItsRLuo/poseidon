using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExperiment : MonoBehaviour
{
    public WaterSegment WaterSegment;
    public float SegmentWidth = 10;
    public int WaterStart = -100;
    public int WaterEnd = 100;
    public int WaterOffset = -3;
    public float DecayTime = 10;

    private List<WaterSegment> waterSegments = new List<WaterSegment>();
    private WaterSegment activeSegment;
    private float mouseLastY;
    private List<Wave> waves = new List<Wave>();

    void Start()
    {
        WaterSegment lastSegment = null;

        for (float x = WaterStart; x < WaterEnd; x += SegmentWidth)
        {
            var segment = GameObject.Instantiate(WaterSegment);
            segment.transform.position = new Vector3(x, WaterOffset, 0);
            if (lastSegment != null) lastSegment.Next = segment;
            segment.Previous = lastSegment;
            segment.Initialize();
            lastSegment = segment;
            waterSegments.Add(segment);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnTouchUp();
        }

        if (Input.GetMouseButton(0) && activeSegment != null)
        {
            var mouseCurrentY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            var translation = new Vector3(0, mouseCurrentY - mouseLastY, 0);
            activeSegment.transform.position = activeSegment.NaturalPosition + translation;
            PropogateFromWave(activeSegment, translation);
        }

        UpdateWaves();
    }

    private void UpdateWaves()
    {
        foreach (var wave in waves)
        {
            wave.T += Time.deltaTime;
            wave.Decay = 1 - (wave.T / DecayTime);
            wave.Oscilator = Mathf.Sin((wave.T % 1) * 2 * Mathf.PI);
            if (wave.T > DecayTime)
            {
                wave.Done = true;
            }
        }

        foreach (var waterSegment in this.waterSegments)
        {
            if (waterSegment.IsBeingControlled)
            {
                continue;
            }

            float yOffset = 0;

            foreach (var wave in waves)
            {
                if (wave.Done) continue;
                yOffset += wave.ComputeValue(waterSegment.transform.position.x);
            }

            waterSegment.transform.position = waterSegment.NaturalPosition + new Vector3(0, yOffset, 0);
        }
    }

    private float falloff = 0.0002f;

    private void PropogateFromWave(WaterSegment activeSegment, Vector3 translation)
    {
        var currentWave = activeSegment;
        float influence = 1.0f;
        int numSegments = 0;

        while (currentWave != null && influence > 0.1f)
        {
            currentWave.IsBeingControlled = true;
            currentWave = currentWave.Previous;
            currentWave.transform.position = currentWave.NaturalPosition + influence * translation;

            numSegments++;
            influence = 1f - falloff * (1f * numSegments * numSegments);
        }

        currentWave = activeSegment;
        influence = 1.0f;
        numSegments = 0;

        while (currentWave != null && influence > 0.1f)
        {
            currentWave.IsBeingControlled = true;
            currentWave = currentWave.Next;
            currentWave.transform.position = currentWave.NaturalPosition + influence * translation;

            numSegments++;
            influence = 1f - falloff * (1f * numSegments * numSegments);
        }
    }

    private void OnTouchDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            var clickedObject = hit.transform.gameObject;
            if (clickedObject.GetComponent<WaterSegment>() != null)
            {
                activeSegment = clickedObject.GetComponent<WaterSegment>();
                mouseLastY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            }
        }
    }

    private void OnTouchUp()
    {
        if (activeSegment == null)
        {
            return;
        }

        var mouseCurrentY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        waves.Add(new Wave()
        {
            X = activeSegment.NaturalPosition.x,
            T = 0,
            InitialAmplitude = mouseCurrentY - mouseLastY
        });
        foreach (var segment in waterSegments)
        {
            segment.IsBeingControlled = false;
        }
        activeSegment = null;
    }
}
