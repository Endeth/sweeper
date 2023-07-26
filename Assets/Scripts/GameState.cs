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

        [SerializeField] private int _tilesToUncover;

        private FinishVisuals _finishVfx;

        override protected void Awake()
        {
            base.Awake();
            _finishVfx = GetComponent<FinishVisuals>();

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
                _finishVfx.OnFinish( false );
            }
            else
            {
                _tilesToUncover--;
                if( _tilesToUncover == 0 )
                {
                    _currentState = State.Finished;
                    _finishVfx.OnFinish( true );
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
