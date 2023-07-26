using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Boomsweeper
{
    public class BoardBehaviour : MonoBehaviour
    {
        private static System.Random rng = new System.Random();

        private Vector2 _tileSizeOnBoard;
        [SerializeField] private float _spaceBetweenTiles = 0.1f;

        public void Populate( Tile tileToGenerate, int tilesCount, Vector2Int boardSize, out Tile[,] board )
        {
            var newTiles = new List<Tile>( tilesCount );
            for( int i = 0; i < tilesCount; i++ )
            {
                var tile = Instantiate( tileToGenerate, transform );
                tile.Init( rng.Next(), i < GameOptions.Instance.minesCount );
                newTiles.Add( tile );
            }

            newTiles.Sort( delegate ( Tile lhs, Tile rhs )
            {
                if( lhs.id == rhs.id )
                    return 0;
                else if( lhs.id > rhs.id )
                    return 1;
                else
                    return -1;
            } );

            _tileSizeOnBoard = newTiles[0].GetSizeOnBoard();
            board = new Tile[boardSize.x, boardSize.y];
            for( int x = 0; x < boardSize.x; x++ )
            {
                for( int y = 0; y < boardSize.y; y++ )
                {
                    var tile = newTiles[0];
                    board[x, y] = tile;

                    var pos = CalculateTilePosition( x, y );
                    tile.SetBoardPosition( x, y );
                    tile.SetPosition( pos );

                    newTiles.Remove( tile );
                }
            }

            foreach( var tile in board )
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
