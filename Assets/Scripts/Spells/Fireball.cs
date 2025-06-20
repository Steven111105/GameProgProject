using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage = 10f;
    public int level;
    public float size = 1f; // Size of the fireball
    public GameObject explosionEffectPrefab;
    public void SetupSize(int level)
    {
        this.level = level;
        switch (level)
        {
            case 1:
                this.size = 1f;
                damage = 5f; // Set damage for level 1
                break;
            case 2:
                this.size = 1.5f;
                damage = 10f;
                break;
            default:
                Debug.Log("Invalid size level for fireball: " + level);
                break;
        }
        transform.localScale = new Vector3(size, size, 1f);
    }
    private void OnDestroy()
    {
        //create a fire explosion effect when the fireball is destroyed using physics overlap circle
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, size*1.5f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Assuming the enemy has a method to take damage
                hitCollider.GetComponent<Enemy>()?.TakeDamage(damage);
            }
        }
        GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(size*1.5f, size*1.5f, 1);
    }
}
