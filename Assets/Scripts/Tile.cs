using UnityEngine;

namespace Boomsweeper
{
    public class Tile : MonoBehaviour
    {
        private Board _parentBoard;
        private TileVisuals _visual;
        private Collider _collider;

        private int _id;
        public int id => _id;

        private Vector2Int _boardPosition;
        private bool _hidden = true;

        [SerializeField] private bool _hasMine;
        [SerializeField] private int _neighboursWithMines = 0;
        public bool isFlippableNeighbour => !_hasMine && _hidden;

        public void Init( int id, bool hasMine )
        {
            _parentBoard = GetComponentInParent<Board>();
            _visual = GetComponent<TileVisuals>();
            _collider = GetComponent<Collider>();

            _id = id;
            _hasMine = hasMine;
        }

        public void Flip()
        {
            _hidden = false;

            if( !_hasMine && _neighboursWithMines == 0 )
            {
                var neighbours = _parentBoard.GetTileNeighbours( _boardPosition );
                foreach( var tile in neighbours )
                {
                    if( tile.isFlippableNeighbour )
                        tile.Flip();
                }
            }

            GameState.Instance.OnTileFlip( _hasMine );
            _visual.Flip();
        }

        public void CountNearbyMines()
        {
            var neighbours = _parentBoard.GetTileNeighbours( _boardPosition );
            foreach( var tile in neighbours )
            {
                if( tile._hasMine )
                    _neighboursWithMines++;
            }

            _visual.Init( _hasMine, _neighboursWithMines );
        }

        public Vector2 GetSizeOnBoard()
        {
            return new Vector2( _collider.bounds.size.x, _collider.bounds.size.z );
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
                Flip();
        }
    }
}
