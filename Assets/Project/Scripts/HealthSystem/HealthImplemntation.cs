using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthImplemntation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentHealth;
    [SerializeField] TextMeshProUGUI maxHealth;
    [SerializeField] TextMeshProUGUI isDead;

    private Health health = new Health();


    private void Start()
    {
        maxHealth.text = "/ " + health.MaxHealth;
        currentHealth.text = health.CurrentHealth.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            health.TakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.HealDamage(5);
        }

        if (health.currentHealth == 0)
        {
            isDead.gameObject.SetActive(true);
        }
        else
        {
            isDead.gameObject.SetActive(false);
        }

        currentHealth.text = health.CurrentHealth.ToString();
        currentHealth.color = (health.IsDead) ? Color.red : Color.white;
    }

}
