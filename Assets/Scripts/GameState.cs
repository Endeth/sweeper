using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boomsweeper
{
    public class GameState : Singleton<GameState>
    {
        enum State
        {
            Active,
            Finished
        }
        private State _currentState = State.Active;
        public bool active => _currentState == State.Active;

        [SerializeField] private TMPro.TMP_Text _victoryText;
        [SerializeField] private TMPro.TMP_Text _defeatText;

        [SerializeField] private int _tilesToUncover;

        override protected void Awake()
        {
            base.Awake();

            var minesCount = GameOptions.Instance.minesCount;
            var boardSize = GameOptions.Instance.boardSize;
            _tilesToUncover = boardSize.x * boardSize.y - minesCount;
        }

        IEnumerator DelayedQuit()
        {
            yield return new WaitForSeconds( 1 );

            LoadMainMenu();
        }

        public void OnTileFlip( bool hasMine )
        {
            if( hasMine )
            {
                _currentState = State.Finished;
                _defeatText.alpha = 255;
            }
            else
            {
                _tilesToUncover--;
                if( _tilesToUncover == 0 )
                {
                    _currentState = State.Finished;
                    _victoryText.alpha = 255;
                }
            }

            if( _currentState == State.Finished )
                StartCoroutine( DelayedQuit() );
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene( "MainMenu" );
        }
    }
}
