using UnityEngine;

namespace Boomsweeper
{
    public class GameOptions : PersistentSingleton<GameOptions>
    {
        [SerializeField] Vector2Int _boardSize = new Vector2Int( 8, 8 );
        [SerializeField] private int _minesCount = 10;

        [SerializeField] private int _minDimension = 5;
        [SerializeField] private int _maxDimension = 10;

        public int minesCount => _minesCount;
        public Vector2Int boardSize => _boardSize;

        public int GetMinesCount()
        {
            return _minesCount;
        }

        public void SetBoardWidth( string w )
        {
            var parsedValue = int.Parse( w );
            _boardSize.x = Mathf.Clamp( parsedValue, _minDimension, _maxDimension );
        }

        public void SetBoardHeight( string h )
        {
            var parsedValue = int.Parse( h );
            _boardSize.y = Mathf.Clamp( parsedValue, _minDimension, _maxDimension );
        }

        public void SetMinesCount( string count )
        {
            var parsedValue = int.Parse( count );
            _minesCount = Mathf.Clamp( parsedValue, 0, _boardSize.x * _boardSize.y );
        }
    }
}
