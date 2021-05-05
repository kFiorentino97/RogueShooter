using UnityEngine;

namespace Abilities.Helpers
{
    /// <summary>
    ///     <c>LaserShot</c> is a script for extending the <c>LaserBlast</c> ability's laser.
    /// </summary>
    public class LaserShot : MonoBehaviour
    {
        public float sizeIncrease;
        public GameObject laserEnd, laserBase;  // References to the end and base of the laser.
        private BoxCollider2D _boxCollider2d;

        private bool _expand = true;            // Flag for whether laser should be expanding or not.
        private bool _inside;
        private Transform _location;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _col;


        /// <summary>
        ///     Gets components and sets starting position of laser end and base.
        /// </summary>
        private void Start()
        {
            laserBase.SetActive(true);
            var position = gameObject.transform.position;
            laserBase.transform.position = position;
            laserEnd.transform.position = position;
            laserEnd.SetActive(false);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2d = GetComponent<BoxCollider2D>();
            _spriteRenderer.size = new Vector2(_spriteRenderer.size.x, 0.01f);
        }

        /// <summary>
        ///     Updates the size and scale of laser and laser's hitbox. Stops when it enters an enemy object. When
        ///     entering an enemy object it enables the end.
        /// </summary>
        private void Update()
        {
            var tmpSize = _spriteRenderer.size;
            if (_inside)
            {
                _location = gameObject.transform;
                Vector2 tmpPositon = _spriteRenderer.gameObject.transform.position;
                var colTmp = _col.gameObject.transform;
                tmpSize.y = _col.gameObject.GetComponent<BoxCollider2D>().bounds.center.y - tmpPositon.y;
                _spriteRenderer.size = tmpSize;
                var endLocation = new Vector2(_location.position.x, _location.position.y + tmpSize.y);
                laserEnd.transform.position = endLocation;
            }

            if (!_expand) return;
            tmpSize = new Vector2(tmpSize.x, tmpSize.y + sizeIncrease * Time.deltaTime);
            _boxCollider2d.size = _spriteRenderer.size;
            var tmp = _boxCollider2d.offset;
            tmp.y = _boxCollider2d.size.y / 2;
            _boxCollider2d.offset = tmp;
            _spriteRenderer.size = tmpSize;
        }

        /// <summary>
        ///     Checks if laser entered an enemy.
        /// </summary>
        /// <param name="col"></param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                _col = col;
                laserEnd.SetActive(true);
                _expand = false;
                _inside = true;
            }
        }

        
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                laserEnd.SetActive(false);
                _expand = true;
                _inside = false;
            }
        }
    }
}