using System;
using System.Security.Cryptography;
using Abilities.Helpers;
using Player;
using UnityEngine;
using static Namespaces.Helpers;
using Random = System.Random;

namespace Abilities
{
    /// <summary>
    ///     Teleports player to random location and causes small bullet destroying explosion at location
    /// </summary>
    public class BombTeleport : Ability
    {
        public GameObject bombPrefab;
        public Vector3 offset;
        public Vector2 rightBound, leftBound;   // Screen limits.
        public Vector3 teleportLocation;

        private Random _random = new Random();
        private Movement _movement;
        private Lives _lives;
        private bool _onCooldown;
        
        /// <summary>
        ///     Gets components
        /// </summary>
        private void Start()
        {
            bombPrefab = Instantiate(bombPrefab, transform.position, transform.rotation);
            bombPrefab.SetActive(false);
            _movement = gameObject.GetComponent<Movement>();
        }

        /// <summary>
        ///     lowers cooldown timer.
        /// </summary>
        private void Update()
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
                cooldownTimer = 0;
            if (cooldownTimer <= 0) _onCooldown = false;
        }

        /// <summary>
        ///     Randomly selects location and changes players position.
        /// </summary>
        public override void Begin()
        {
            if(_onCooldown) return;
            teleportLocation.x = (float) RandomDouble(rightBound.x, leftBound.x, _random);
            teleportLocation.y = (float) RandomDouble(rightBound.y, leftBound.y, _random);
            bombPrefab.SetActive(true);
            bombPrefab.transform.position = teleportLocation + offset;
            _movement._posTarget = teleportLocation;
            cooldownTimer = cooldown;
            _onCooldown = true;
        }
        
        
    }
}
