using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _TreasureHunt._Scripts.GameplayRelated
{
    [ExecuteInEditMode]
    public class GenerateMaze : MonoBehaviour
    {
        public static GenerateMaze instance;
        [SerializeField] private MazeCellElement _mazeCellPrafab;
        private int _mazeWidth;
        private int _mazeDepth;

        private MazeCellElement[,] _mazeGrid;
        public GameObject _existingMaze;

        private void Awake()
        {
            instance = this;
        }

        public void MazeGenerator(int width, int dephth)
        {
            CheckForExistingMaze();
            _mazeWidth = width;
            _mazeDepth = dephth;
            _mazeGrid = new MazeCellElement[_mazeWidth, _mazeDepth];
            for (int i = 0; i < _mazeWidth; i++)
            {
                for (int j = 0; j < _mazeDepth; j++)
                {
                    _mazeGrid[i, j] = Instantiate(_mazeCellPrafab, new Vector3(i, 0, j), quaternion.identity);
                    _mazeGrid[i, j].transform.parent = _existingMaze.transform;
                }
            }
            GenerateMyMaze(null, _mazeGrid[0, 0]);
        }

        void CheckForExistingMaze()
        {
            if(_existingMaze == null)
            {
                _existingMaze = new GameObject("MAZE");
            }
            else
            {
                DestroyImmediate(_existingMaze);
                _existingMaze = new GameObject("MAZE");
            }
        }

        private void GenerateMyMaze(MazeCellElement previousCell, MazeCellElement currentCell)
        {
            currentCell.Visit();
            ClearWalls(previousCell, currentCell);

            MazeCellElement nextCell;
            do
            {
                nextCell = GetNextUnvisitedCell(currentCell);
                if (nextCell != null)
                    GenerateMyMaze(currentCell, nextCell);
            } while (nextCell != null);
        }

        private MazeCellElement GetNextUnvisitedCell(MazeCellElement currentCell)
        {
            var unvisitedCells = GetUnvisitedCells(currentCell);
            return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
        }

        private IEnumerable<MazeCellElement> GetUnvisitedCells(MazeCellElement currentCell)
        {
            int x = (int) currentCell.transform.position.x;
            int z = (int) currentCell.transform.position.z;

            if (x + 1 < _mazeWidth)
            {
                var cellToRight = _mazeGrid[x + 1, z];
                if (!cellToRight.isVisited)
                    yield return cellToRight;
            }

            if (x - 1 >= 0)
            {
                var cellToLeft = _mazeGrid[x - 1, z];
                if (!cellToLeft.isVisited)
                    yield return cellToLeft;
            }

            if (z + 1 < _mazeDepth)
            {
                var cellToFront = _mazeGrid[x, z + 1];
                if (!cellToFront.isVisited)
                    yield return cellToFront;
            }

            if (z - 1 >= 0)
            {
                var cellToBack = _mazeGrid[x, z - 1];
                if (!cellToBack.isVisited)
                    yield return cellToBack;
            }
        }

        private void ClearWalls(MazeCellElement previousCell, MazeCellElement currentCell)
        {
            if (previousCell == null) return;
            if (previousCell.transform.position.x < currentCell.transform.position.x)
            {
                previousCell.ClearRightWall();
                currentCell.ClearLeftWall();
                return;
            }

            if (previousCell.transform.position.x > currentCell.transform.position.x)
            {
                previousCell.ClearLeftWall();
                currentCell.ClearRightWall();
                return;
            }

            if (previousCell.transform.position.z < currentCell.transform.position.z)
            {
                previousCell.ClearFrontWall();
                currentCell.ClearBackWall();
                return;
            }

            if (previousCell.transform.position.z > currentCell.transform.position.z)
            {
                previousCell.ClearBackWall();
                currentCell.ClearFrontWall();
                return;
            }
        }

        void Update()
        {
        }
    }
}