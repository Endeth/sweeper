using UnityEngine;

namespace Boomsweeper
{
    public class Tile : MonoBehaviour
    {
        private Vector2Int _boardPosition;
        private bool _hidden = true;

        private Board _parentBoard;

        private int _id;
        public int id => _id;

        private TextMesh _text;

        [SerializeField] private bool _hasMine;
        public bool hasMine => _hasMine;

        [SerializeField] private int _neighboursWithMines = 0;

        public bool isFlippableNeighbour => !_hasMine && _hidden;

        public void Init( int id, bool hasMine )
        {
            _parentBoard = GetComponentInParent<Board>();
            _text = GetComponentInChildren<TextMesh>();

            _id = id;
            _hasMine = hasMine;

            if( _hasMine )
                _text.text = "B";
        }

        public void Flip( bool clicked )
        {
            _hidden = false;

            if( !_hasMine && _neighboursWithMines == 0 )
            {
                var neighbours = _parentBoard.GetTileNeighbours( _boardPosition );
                transform.localScale = new Vector3( 0.5f, 0.1f, 0.5f );
                foreach( var tile in neighbours )
                {
                    if( tile.isFlippableNeighbour )
                        tile.Flip( false );
                } 
            }

            GameState.Instance.OnTileFlip( _hasMine );

            _text.color = Color.black;
        }

        public void CountNearbyMines()
        {
            var neighbours = _parentBoard.GetTileNeighbours( _boardPosition );
            foreach( var tile in neighbours )
            {
                if( tile._hasMine )
                    _neighboursWithMines++;
            }
            if( !_hasMine && _neighboursWithMines > 0 )
                _text.text = _neighboursWithMines.ToString();
        }

        public Vector2 GetSizeOnBoard()
        {
            return new Vector2( transform.localScale.x, transform.localScale.z );
        }

        public void SetBoardPosition( int x, int y )
        {
            _boardPosition = new Vector2Int( x, y );
        }

        public void SetPosition( Vector2 pos )
        {
            transform.position = new Vector3( pos.x, 0, pos.y );
        }

        private void OnMouseUp()
        {
            if( _hidden && GameState.Instance.active )
                Flip( true );
        }
    }
}
