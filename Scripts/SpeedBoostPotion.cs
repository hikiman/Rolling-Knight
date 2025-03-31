using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPotion : MonoBehaviour
{
    public float durationEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEffects effectsManager = collision.gameObject.GetComponent<PlayerEffects>();
            if (effectsManager == null)
            {
                effectsManager = collision.gameObject.AddComponent<PlayerEffects>();
            }

            effectsManager.ActivateSpeedBoost(durationEffect);

            Destroy(gameObject);
        }
    }
}
