using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{

    [Header("UI")]
    public TextMeshProUGUI numText;
    private int total = -1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] pickups;
        pickups = GameObject.FindGameObjectsWithTag("pickup");
        total = pickups.Length;
        numText.text = (total).ToString();

    }

    private void UpdateItems()
    {
        string totalStr = numText.text;
        int totalNum;
        int.TryParse(totalStr, out totalNum);
        numText.text = (totalNum - 1).ToString();

    }

    private void OnCollisionEnter(Collision other)
    {
        UpdateItems();
        Destroy(gameObject);
    }
}
