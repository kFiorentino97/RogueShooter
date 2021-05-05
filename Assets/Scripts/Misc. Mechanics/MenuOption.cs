using UnityEngine;
using UnityEngine.UI;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>MenuOption</c> deals with the animation of menu options, ie sliding out when hovered over, and sliding
    ///     back when mouse exits, as well as changing opacity.
    /// </summary>
    public class MenuOption : MonoBehaviour
    {
        public Color hoverColor, neutralColor, holdColor;       // Colors based on button status
                                                                // hover: mouse hover,
                                                                // neutral: no cursor on option,
                                                                // hold: mouse hover and mouse down.
        public float slideDistance, extendDelay, holdDistance;  // extendDelay is the amount of time it takes to extend.
        
        protected Color CurrentColor;                           // Current color of menu option
        protected Vector3 InitialPosition, HoverPosition, currentPosition;
        protected bool Inside;                                  // Flag for when mouse is inside.

        protected Image MenuGraphic;
        protected Vector3 SlideDistance;

        /// <summary>
        ///     Gets components and sets defaults.
        /// </summary>
        private void Start()
        {
            SlideDistance = new Vector3(slideDistance, 0, 0);
            MenuGraphic = gameObject.GetComponent<Image>();
            MenuGraphic.color = neutralColor;
            InitialPosition = MenuGraphic.gameObject.transform.position;
            HoverPosition = InitialPosition + SlideDistance;
            currentPosition = InitialPosition;
            Inside = false;
        }
        
        /// <summary>
        ///     Moves the menu option inside of the screen if the mouse is in the option, otherwise moves the option
        ///     out.
        /// </summary>
        private void Update()
        {
            if (!Inside)
            {
                currentPosition.x -= SlideDistance.x * (Time.deltaTime / extendDelay);
                currentPosition.x = currentPosition.x > InitialPosition.x ? InitialPosition.x : currentPosition.x;
            }

            else if (Inside)
            {
                currentPosition.x += SlideDistance.x * (Time.deltaTime / extendDelay);
                currentPosition.x = currentPosition.x < HoverPosition.x ? HoverPosition.x : currentPosition.x;
            }

            gameObject.transform.position = currentPosition;
        }

        public void OnMouseDown()
        {
            MenuGraphic.color = holdColor;
        }

        public void OnMouseEnter()
        {
            Inside = true;
            MenuGraphic.color = hoverColor;
        }

        public void OnMouseExit()
        {
            Inside = false;
            MenuGraphic.color = neutralColor;
        }

        public void OnMouseUp()
        {
            MenuGraphic.color = Inside ? hoverColor : neutralColor;
        }
    }
}