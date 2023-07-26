using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Boomsweeper
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size;

        private BoardBehaviour _boardBehaviour;
        [SerializeField] private Tile _tileToGenerate;
        private Tile[,] _tilesBoard;

        public int tilesCount => _size.x * _size.y;
        void Awake()
        {
            _boardBehaviour = GetComponent<BoardBehaviour>();
            _size = GameOptions.Instance.boardSize;

            _boardBehaviour.Populate( _tileToGenerate, tilesCount, _size, out _tilesBoard );
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
    }
}
