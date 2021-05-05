using System;
using UnityEngine;

namespace Abilities.Helpers
{
    public class DestroyBullets : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D Col)
        {
            if (Col.gameObject.CompareTag("EnemyBullet"))
            {
                Destroy(Col.gameObject);
            }
        }
    }
}