using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{
    private bool died = false;
    public GameObject LightningPrefab;

    public Slider TowerHealthSlider;
    public Transform TowerLocationStart;
    public Transform TowerLocationEnd;
    public Camera Camera;
    public RectTransform GameOverPanel;
    public BoatSpawner BoatSpawner;
    public Catapult Catapult;
    public GameObject MeterContainer;
    public ScoreManager ScoreManager;
    public Text ScoreText;

    public GameObject LightningMeterPrefab;
    Meter lightningChargeMeter;

    public void Start()
    {
        this.TowerHealthSlider.value = 1;

        lightningChargeMeter = LightningMeterPrefab.GetComponentsInChildren<Meter>()[0];

        this.ScoreManager.OnScoreChanged +=
            score => this.ScoreText.text = score.ToString();
    }

    public void Update()
    {
        #region Place tower health slider
        Vector2 viewportPosition_start = this.Camera.WorldToScreenPoint(this.TowerLocationStart.position);
        Vector2 viewportPosition_end = this.Camera.WorldToScreenPoint(this.TowerLocationEnd.position);

        RectTransform rt = this.TowerHealthSlider.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(viewportPosition_end.x - viewportPosition_start.x, rt.sizeDelta.y);
        rt.anchoredPosition3D = new Vector3(viewportPosition_start.x + rt.sizeDelta.x / 2, viewportPosition_start.y, 0);
        #endregion

        if (lightningChargeMeter.Full() && !this.died) {
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
                        boat.Sink(false);

                        this.Camera.clearFlags = CameraClearFlags.Color;
                        StartCoroutine(RestoreSkyBox());
                    }
                }
            }
        }
    }

    private IEnumerator RestoreSkyBox()
    {
        yield return new WaitForSeconds(0.25f);
        this.Camera.clearFlags = CameraClearFlags.Skybox;
    }

    public void AddDamage(float damage)
    {
        if (this.died)
        {
            return;
        }

        this.TowerHealthSlider.value = Mathf.Max(0, this.TowerHealthSlider.value - damage);
        if (this.TowerHealthSlider.value == 0)
        {
            this.died = true;
            this.GameOverPanel.gameObject.SetActive(true);
            this.BoatSpawner.gameObject.SetActive(false);
            this.Catapult.gameObject.SetActive(false);
            this.MeterContainer.SetActive(false);
            this.ScoreManager.gameObject.SetActive(false);
            StartCoroutine(QuitToMainMenu());
        }
    }

    public IEnumerator QuitToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
