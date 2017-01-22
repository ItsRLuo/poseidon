﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    public GameObject LightningPrefab;

    public Slider TowerHealthSlider;
    public Transform TowerLocationStart;
    public Transform TowerLocationEnd;
    public Camera Camera;
    public RectTransform GameOverPanel;
    public BoatSpawner BoatSpawner;
    public Catapult Catapult;
    public GameObject MeterContainer;

    public GameObject LightningMeterPrefab;
    Meter lightningChargeMeter;

    public void Start()
    {
        #region Initialize tower health slider
        Vector2 viewportPosition_start = this.Camera.WorldToScreenPoint(this.TowerLocationStart.position);
        Vector2 viewportPosition_end = this.Camera.WorldToScreenPoint(this.TowerLocationEnd.position);

        this.TowerHealthSlider.value = 1;
        RectTransform rt = this.TowerHealthSlider.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(viewportPosition_end.x - viewportPosition_start.x, rt.sizeDelta.y);
        rt.anchoredPosition3D = new Vector3(viewportPosition_start.x + rt.sizeDelta.x / 2, viewportPosition_start.y, 0);
        #endregion

        lightningChargeMeter = LightningMeterPrefab.GetComponentsInChildren<Meter>()[0];
    }

    public void Update()
    {
        if (lightningChargeMeter.Full()) {
            bool executeAll = false;

            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f)) {
                    if (hit.transform.name.Contains("Cube")) {
                        executeAll = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) || executeAll) {
                lightningChargeMeter.Deplete();
                BoatController[] boats = GameObject.FindObjectsOfType<BoatController>();

                foreach (BoatController boat in boats)
                {
                    if (!boat.Sinking)
                    {
                        GameObject go = GameObject.Instantiate(LightningPrefab);
                        Lightning l = go.GetComponent<Lightning>();
                        var p = boat.transform.position;
                        p.x -= 4; // not sure why needed
                        p.y -= 1; // not sure why needed
                        l.EndPosition = p;
                        p.y += 40;
                        l.StartPosition = p;

                        boat.Smoke();
                        boat.Sink();
                    }
                }
            }
        }
    }

    public void AddDamage(float damage)
    {
        this.TowerHealthSlider.value = Mathf.Max(0, this.TowerHealthSlider.value - damage);
        if (this.TowerHealthSlider.value == 0)
        {
            this.GameOverPanel.gameObject.SetActive(true);
            this.BoatSpawner.gameObject.SetActive(false);
            this.Catapult.gameObject.SetActive(false);
            this.MeterContainer.SetActive(false);
        }
    }
}
