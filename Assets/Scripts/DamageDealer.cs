using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Config params
    [SerializeField] private int damage = 100;
    
    public int Damage => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }

}
