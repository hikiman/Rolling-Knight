using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpPotion : MonoBehaviour
{
    public float durationEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEffects effectsManager = collision.gameObject.GetComponent<PlayerEffects>();
            if (effectsManager == null)
            {
                effectsManager = collision.gameObject.AddComponent<PlayerEffects>();
            }

            effectsManager.ActivateJumpBoost(durationEffect);

            Destroy(gameObject);
        }
    }
}

public class PlayerEffects : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // ��������� ��� ����� ������
    private float jumpBoostEndTime = 0f;
    private float originalJumpValue;
    private bool jumpBoostActive = false;

    // ��������� ��� ����� ��������
    private float speedBoostEndTime = 0f;
    private float originalSpeedValue;
    private bool speedBoostActive = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateJumpBoost(float duration)
    {
        if (!jumpBoostActive)
        {
            originalJumpValue = playerMovement.jump;
            playerMovement.jump *= 2;
            jumpBoostActive = true;
        }

        jumpBoostEndTime = Time.time + duration;
    }

    public void ActivateSpeedBoost(float duration)
    {
        if (!speedBoostActive)
        {
            originalSpeedValue = playerMovement.speed;
            playerMovement.speed *= 1.5f; // ����������� �������� � 1.5 ����
            speedBoostActive = true;
        }

        speedBoostEndTime = Time.time + duration;
    }

    private void Update()
    {
        // ��������� ��������� ������� ������
        if (jumpBoostActive && Time.time >= jumpBoostEndTime)
        {
            playerMovement.jump = originalJumpValue;
            jumpBoostActive = false;
            Debug.Log("������ ����� ������ ����������!");
        }

        // ��������� ��������� ������� ��������
        if (speedBoostActive && Time.time >= speedBoostEndTime)
        {
            playerMovement.speed = originalSpeedValue;
            speedBoostActive = false;
            Debug.Log("������ ����� �������� ����������!");
        }
    }
}
