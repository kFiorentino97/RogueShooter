using EventSystem;
using Misc._Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueScenes
{
    /// <summary>
    ///     <c>CutsceneHandler</c> handles the visual display of <c>DialogueScene</c>s
    /// </summary>
    public class CutsceneHandler : MonoBehaviour
    {
        public Cutscene scn;                // The scene to be displayed
        public Sprite[] sprites;            // Array of all in-game sprites of characters
        public Color spriteChange;          // Color shift of non-speaking characters
        public float nonSpeakingOffset;     // Spacial offset of non-speaking characters
        public bool endOnLast;              // Flag for whether the cutscene should end at the last message.
        public int optionChose;             // The choice from the player
        public bool ended, started;
        public float textCrawlTime;
        public bool hasScene;               // Flag for whether scn is null or not.
        public Canvas canvas;
        
        // References to various canvas elements.
        private Text _body, _lName, _rName;
        private DialogueTree _dS;
        private Image _lSprite, _rSprite, _lPanel, _rPanel;

        private int _maxChars, _currChar;   // The index of the current character and the max characters in a text crawl
        private Pause _pause;               // Reference to pause script to pause game during cutscenes
        private Vector2 _rSpritePos, _lSpritePos;       // Default positions of sprites.
        private string _shownText;          // Current amount of text shown during text crawl.
        private float _textCrawlTimer;
        private LevelTransition _levelTransition;       // Used to tell if transition animation is still playing

        private void Start()
        {
            GetUIElements();
            started = false;
        }

        /// <summary>
        ///     Sets sprite positions and text. Gets user input and updates text crawl.
        /// </summary>
        private void Update()
        {
            if (!hasScene)
                return;
            
            _textCrawlTimer += Time.unscaledDeltaTime;
            if (!started)
                SetupCanvas();
            else if (Input.GetButtonDown("fire") && scn.dS.GetNext() != null)
                AdvanceDialogue();
            else if (endOnLast && Input.GetButtonDown("fire") && scn.dS.GetNext() == null)
                DisableCutscene();

            if (_textCrawlTimer >= textCrawlTime && _currChar < _maxChars)
                UpdateTextCrawl();
        }

        /// <summary>
        ///     Sets cutscene handler to defaults and disables components.
        /// </summary>
        public void DisableCutscene()
        {
            _shownText = "";
            _currChar = 0;
            _maxChars = 0;
            canvas.enabled = false;
            Time.timeScale = 1f;
            _pause.isPaused = false;
            ended = true;
            _lSprite.transform.position = _lSpritePos;
            _rSprite.transform.position = _rSpritePos;
        }

        /// <summary>
        ///     Enables and disables components and sets up components based on which character is speaking.
        /// </summary>
        private void HideNonSpeaker()
        {
            if (scn.dS.curr.leftSpeaking)
            {
                _rSprite.color = spriteChange;
                _lSprite.color = Color.white;
                _rPanel.enabled = false;
                _lPanel.enabled = true;
                _rName.enabled = false;
                _lName.enabled = true;
                _rSprite.transform.position = new Vector2(_rSpritePos.x + nonSpeakingOffset, _rSpritePos.y);
                _lSprite.transform.position = _lSpritePos;
            }
            else
            {
                _lSprite.color = spriteChange;
                _rSprite.color = Color.white;
                _lPanel.enabled = false;
                _rPanel.enabled = true;
                _lName.enabled = false;
                _rName.enabled = true;
                _lSprite.transform.position = new Vector2(_lSpritePos.x - nonSpeakingOffset, _lSpritePos.y);
                _rSprite.transform.position = _rSpritePos;
            }
        }

        /// <summary>
        ///     Sets up canvas for start of a cutscene.
        /// </summary>
        private void SetupCanvas()
        {
            _maxChars = scn.dS.curr.body.Length;
            canvas.enabled = true;
            _rSpritePos = _rSprite.transform.position;
            _lSpritePos = _lSprite.transform.position;
            Time.timeScale = 0f;
            _pause.isPaused = true;
            started = true;
            _shownText = "";
            _currChar = 0;
            _body.text = _shownText;
            _lName.text = scn.dS.curr.lName;
            _rName.text = scn.dS.curr.rName;
            _lSprite.sprite = sprites[(int) scn.dS.curr.lSprite];
            _rSprite.sprite = sprites[(int) scn.dS.curr.rSprite];
            HideNonSpeaker();
        }

        /// <summary>
        ///     Advances cutscene to next message.
        /// </summary>
        private void AdvanceDialogue()
        {
            if (!_levelTransition.Finished)
                return;
            // TODO: Add option functionality.
            _shownText = "";
            _maxChars = scn.dS.curr.body.Length;
            _currChar = 0;
            _textCrawlTimer = 0;
            _body.text = _shownText;
            _lName.text = scn.dS.curr.lName;
            _rName.text = scn.dS.curr.rName;
            _lSprite.sprite = sprites[(int) scn.dS.curr.lSprite];
            _rSprite.sprite = sprites[(int) scn.dS.curr.rSprite];
            HideNonSpeaker();
        }

        /// <summary>
        ///     Updates text crawl and timer.
        /// </summary>
        private void UpdateTextCrawl()
        {
            _textCrawlTimer = 0;
            _shownText += scn.dS.curr.body[_currChar];
            _currChar++;
            _body.text = _shownText;
        }
        
        /// <summary>
        ///     Get's elements, components, and scripts.
        /// </summary>
        private void GetUIElements()
        {
            _body = GameObject.FindGameObjectWithTag("DialogueBody").GetComponent<Text>();
            _lPanel = GameObject.FindGameObjectWithTag("NamePanelLeft").GetComponent<Image>();
            _rPanel = GameObject.FindGameObjectWithTag("NamePanelRight").GetComponent<Image>();
            _lSprite = GameObject.FindGameObjectWithTag("leftSprite").GetComponent<Image>();
            _rSprite = GameObject.FindGameObjectWithTag("rightSprite").GetComponent<Image>();
            _lName = GameObject.FindGameObjectWithTag("NameLeft").GetComponent<Text>();
            _rName = GameObject.FindGameObjectWithTag("Nameright").GetComponent<Text>();
            canvas = GameObject.FindGameObjectWithTag("CutsceneUI").GetComponent<Canvas>();
            _pause = GameObject.FindGameObjectWithTag("Manager").GetComponent<Pause>();
            _levelTransition = GameObject.Find("FadeCanvas").GetComponentInChildren<LevelTransition>();
        }
    }
}