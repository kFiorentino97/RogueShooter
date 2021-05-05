using UnityEngine;
using static Namespaces.Helpers;

namespace Enemy.BulletPatterns
{
    /// <summary>
    ///     Spawns bullets at a velocity aimed towards the player.
    /// </summary>
    public class EpAim : EnemyPattern
    {
        public Vector3 endOffset, startOffset; // endOffset: offset based on target, startOffset: offset based on self.
        public float rotationalOffset;
        
        private void Start()
        {
            GetElements();
        }

        public override void Fire()
        {
            if (!Life.IsDead && !Life.IsDown)
            {
                FinalVelocity = DrawLineTo(transform.position + startOffset, target.transform.position + endOffset,
                    velocity);
                SingleFire(FinalVelocity, startOffset);
            }
        }
    }
}