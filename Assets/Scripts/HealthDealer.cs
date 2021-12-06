using UnityEngine;

public class HealthDealer : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health => health;

    public void Hit()
    {
        Destroy(gameObject);
    }
}