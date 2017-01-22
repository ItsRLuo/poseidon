using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    public GameObject LightningPrefab;

    public Slider TowerHealthSlider;
    public Transform TowerLocation;
    public Camera Camera;
    public int TowerHealth = 1;

    public GameObject LightningMeterPrefab;
    Meter lightningChargeMeter;

    public void Start()
    {
        #region Initialize tower health slider
        Vector2 viewportPosition = this.Camera.WorldToScreenPoint(this.TowerLocation.position);

        Debug.Log(viewportPosition);
        this.TowerHealthSlider.value = this.TowerHealth;
        RectTransform rt = this.TowerHealthSlider.GetComponent<RectTransform>();
        rt.anchoredPosition3D = new Vector3(viewportPosition.x, viewportPosition.y, 0);
        #endregion

        lightningChargeMeter = LightningMeterPrefab.GetComponentsInChildren<Meter>()[0];
    }

    public void Update() {
        if (lightningChargeMeter.Full()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                lightningChargeMeter.Deplete();

                BoatController[] boats = GameObject.FindObjectsOfType<BoatController>();

                foreach (BoatController boat in boats) {
                    if (!boat.Sinking) {
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
}
