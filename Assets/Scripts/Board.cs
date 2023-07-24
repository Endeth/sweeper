using UnityEngine;
using System.Collections.Generic;

namespace Boomsweeper
{
    public class Board : MonoBehaviour
    {
        private static System.Random rng = new System.Random();

        [SerializeField] private Vector2Int _size = new( 8, 8 );

        [SerializeField] private Tile _tileToGenerate;
        private Tile[,] _tilesBoard;
        private Vector2 _tileSizeOnBoard;
        [SerializeField] private float _spaceBetweenTiles = 0.1f;

        public int tilesCount => _size.x * _size.y;

        [SerializeField] private int _minesCount = 10;
        public int minesCount
        {
            set => _minesCount = Mathf.Clamp( value, 1, tilesCount );
            get => _minesCount;
        }

        public List<Tile> GetTileNeighbours( Vector2Int tilePosition )
        {
            var neighbours = new List<Tile>();

            for( int x = -1; x < 2; x++ )
            {
                for( int y = -1; y < 2; y++ )
                {
                    if( ( x == 0 && y == 0 ) || !CheckBounds( tilePosition.x + x, tilePosition.y + y ) )
                        continue;

                    neighbours.Add( _tilesBoard[tilePosition.x + x, tilePosition.y + y] );
                }
            }

            return neighbours;
        }

        private bool CheckBounds( int x, int y )
        {
            bool valid = true;
            if( x < 0 || x >= _size.x )
                valid = false;

            if( y < 0 || y >= _size.y )
                valid = false;

            return valid;
        }

        void Awake()
        {
            _size = GameOptions.Instance.boardSize;
            _tilesBoard = new Tile[_size.x, _size.y];
            _minesCount = GameOptions.Instance.minesCount;

            Populate();
        }

        private void Populate()
        {
            var newTiles = new List<Tile>( tilesCount );
            for( int i = 0; i < tilesCount; i++ )
            {
                var tile = Instantiate( _tileToGenerate, transform );
                tile.Init( rng.Next(), i < _minesCount );
                newTiles.Add( tile );
            }

            newTiles.Sort( delegate(Tile lhs, Tile rhs)
            {
                if( lhs.id == rhs.id )
                    return 0;
                else if( lhs.id > rhs.id )
                    return 1;
                else
                    return -1;
            });

            _tileSizeOnBoard = newTiles[0].GetSizeOnBoard();
            for( int x = 0; x < _size.x; x++ )
            {
                for( int y = 0; y < _size.y; y++ )
                {
                    var tile = newTiles[0];
                    _tilesBoard[x, y] = tile;

                    var pos = CalculateTilePosition( x, y );
                    tile.SetBoardPosition( x, y );
                    tile.SetPosition( pos );

                    newTiles.Remove( tile );
                }
            }

            foreach( var tile in _tilesBoard )
                tile.CountNearbyMines();
        }

        private Vector2 CalculateTilePosition( int x, int y )
        {
            float spaceX = _spaceBetweenTiles * x - 1;
            float spaceY = _spaceBetweenTiles * y - 1;
            float tileX = x * _tileSizeOnBoard.x + spaceX;
            float tileY = y * _tileSizeOnBoard.y + spaceY;

            return new Vector2( tileX, tileY );
        }
    }
}
