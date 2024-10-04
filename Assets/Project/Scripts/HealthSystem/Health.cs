public class Health
{
    private int maxHealth = 100;
    public int currentHealth = 0;

    public int MaxHealth
    {
        get { return maxHealth; }
        set 
        {
            maxHealth = (value < currentHealth) ? currentHealth : value;
        }
    }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = (value > maxHealth) ? maxHealth : (value < 0) ? 0 : value;
        }
    }
    public bool IsDead
    {
        get { return currentHealth <= 0; }
    }
    

    public Health(int max = 100)
    {
        MaxHealth = max;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
    public void HealDamage(int amount)
    {
        CurrentHealth += amount;
    }
    public void RestHealth()
    {
        CurrentHealth = MaxHealth;
    }
    public void Dead()
    {
        CurrentHealth = 0;
    }
}