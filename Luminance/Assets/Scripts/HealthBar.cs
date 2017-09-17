using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image healthBarPrefab;
    public Canvas canvas;

    public float offset = 100;

    public Image hpBar;
    public Image hpBarActual;

    private EnemyStats stats;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas> ();
        stats = GetComponent<EnemyStats>();
        hpBar = Instantiate(healthBarPrefab);
        hpBar.transform.SetParent(canvas.transform, false);


        hpBarActual = hpBar.transform.Find("HealthBar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint((Vector3.up * offset) + transform.position);

        hpBar.transform.position = wantedPos;

        hpBarActual.fillAmount = (stats.currentHp / stats.maxHp);
	}

    private void OnDestroy()
    {
        if (hpBar != null)
        {
            Destroy(hpBar.gameObject);
        }
    }
}
