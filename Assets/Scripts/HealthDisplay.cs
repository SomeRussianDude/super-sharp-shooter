using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    // Cached references
    private TextMeshProUGUI healthDisplay;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        healthDisplay = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = player.Health.ToString();
        TextColorChange();
    }

    private void TextColorChange()
    {
        if (player.Health <= 200)
        {
            healthDisplay.color = Color.red;
        }
    }
}
