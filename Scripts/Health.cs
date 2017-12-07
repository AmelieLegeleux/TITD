using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
    private float healFactor = 0.1f;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage(int amount)
	{
		if (isServer) {
			currentHealth -= amount;
			if (currentHealth <= 0) {
				mourir ();
			}
		}
	}

    private void Update()
    {
        if(currentHealth< maxHealth)
        {
            currentHealth = (int)(currentHealth + healFactor);
        }
    }

    void OnChangeHealth (int health)
	{
		Debug.Log (healthBar.sizeDelta);
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}

	void mourir(){
		Destroy(gameObject);
	}
}
