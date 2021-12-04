using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Cached References
    private TextMeshProUGUI scoreText;
    private GameSession gameSession;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.PlayerScore.ToString();
    }
}
