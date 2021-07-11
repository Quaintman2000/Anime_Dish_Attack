using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;


public class Health : MonoBehaviourPun
{
    // Stores max health.
    [Header("Health Stats:"), SerializeField, Tooltip("The maximum health the player could have.")]
    float maxHealth = 100;
    // Stores current health.
    [Tooltip("The health the player has.")]
    public float health;

    public GameManager gameManager;
    public HealthBar healthBar;

    public bool isPlayer;

    [Tooltip("This will take action when the pawn heals.")]
    public UnityEvent OnHeal;
    [Tooltip("This will take action when the pawn takes damage.")]
    public UnityEvent OnDamaged;
    [Tooltip("This will take action when the pawn Dies.")]
    public UnityEvent OnDie;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (isPlayer)
        {
            gameManager.playerHealths.Add(this);
        }
        else
        {
            gameManager.enemyHealths.Add(this);
        }
        // On start, set player health to max health.
        health = maxHealth;
    }
    /// <summary>
    /// Deals damage to the pawn's health by set value.
    /// </summary>
    /// <param name="damage">The amount of damage dealt the player.</param>
    public void TakeDamage(float damage)
    {
        // Damage can't be < 0, else it turns into 0;
        damage = Mathf.Max(damage, 0f);
        // Clamps the damage to be greater than 0 while less than the max health.
        health -= Mathf.Clamp(damage, 0f, maxHealth);
        // Invokes other functions needed when damage is taken.
        OnDamaged.Invoke();

        if (healthBar != null)
        {
            healthBar.AdjustHealthBarTo((health / maxHealth));
        }
        // If health is below 0, you die.
        if (health <= 0)
        {
            // Invokes functions when you die.
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Heals the pawn's health by set value.
    /// </summary>
    /// <param name="healthGain">The amount health the pawn gains.</param>
    public void Heal(float healthGain)
    {
        // health gain can't be less than 0 or greater than amount needed to heal..
        healthGain = Mathf.Clamp(healthGain, 0f, (maxHealth - health));
        // Heals the pawn.
        health += healthGain;
        // Just in case of an overheal, revert health to the maximum value.
        health = Mathf.Min(health, maxHealth);

        if (healthBar != null)
        {
            healthBar.AdjustHealthBarTo((health / maxHealth));
        }
        // Invoke other functions when healed.
        OnHeal.Invoke();
    }

    private void OnDestroy()
    {
        if (isPlayer)
        {
            gameManager.playerHealths.Remove(this);
        }
        else
        {
            gameManager.enemyHealths.Remove(this);
        }
    }
}
