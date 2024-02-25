using System.Drawing;
using System.Numerics;
using Task7.Data.Enums;

namespace Task7.Data.GameModels.TicTacToe
{
    public enum TicTacToeGameState
    {
        None = 0,
        Draw,
        CrossWon,
        NoughtsWon,
    }
    public class TicTacToeGameModel
    {
        public readonly int FIELD_WIDTH = 3;
        public readonly int FIELD_HEIGHT = 3;

        public readonly int WIN_ROW_AMOUNT = 3;

        private int[,] gameField;

        public TicTacToeGameState GameState;

        public List<Point> winLine;

        public int this[int x, int y]
        {
            get { return gameField[x, y]; }
        }

        public bool IsCrossToMove { get; private set; }

        public delegate void TicTacToeGameResultHandler(List<Point> WinLine);
        public event TicTacToeGameResultHandler OnCrossWin;
        public event TicTacToeGameResultHandler OnNoughtWin;
        public event Action OnDraw;

        public TicTacToeGameModel(int winRowAmount = 3, int width = 3, int height = 3)
        {
            WIN_ROW_AMOUNT = winRowAmount;
            FIELD_WIDTH = width;
            FIELD_HEIGHT = height;
            gameField = new int[FIELD_WIDTH, FIELD_HEIGHT];
            IsCrossToMove = true;
            GameState = TicTacToeGameState.None;
            winLine = new List<Point>();
        }

        public bool TryMakeMove(int x, int y, bool isCross)
        {
            if (GameState != TicTacToeGameState.None)
                return false;
            if (!IsValidMove(x, y, isCross))
                return false;
            gameField[x, y] = isCross ? 1 : 10;
            CheckForWin(x, y, isCross);
            IsCrossToMove = !IsCrossToMove;
            return true;
        }


        private void CheckForWin(int x, int y, bool isCross)
        {
            int curSign = isCross ? 1 : 10;
            int emptyCellCount = 0;
            for (int i = 0; i < FIELD_WIDTH; i++)
            {
                for (int j = 0; j < FIELD_HEIGHT; j++)
                {
                    if (gameField[i, j] == curSign)
                    {
                        bool pointResult = CheckRightLine(i, j, curSign) || CheckRightBottomLine(i, j, curSign) || CheckBottomLine(i, j, curSign) || CheckLeftBottomLine(i, j, curSign);
                        if (pointResult)
                        {
                            if (isCross)
                            {
                                GameState = TicTacToeGameState.CrossWon;
                                OnCrossWin(winLine);
                            }
                            else
                            {
                                GameState = TicTacToeGameState.NoughtsWon;
                                OnNoughtWin(winLine);
                            }
                            return;
                        }
                    }
                    if (gameField[i, j] == 0)
                        emptyCellCount++;
                }
            }
            if (emptyCellCount == 0)
                OnDraw();
        }

        private bool CheckRightLine(int x, int y, int curSign)
        {
            if (x + WIN_ROW_AMOUNT > FIELD_WIDTH)
                return false;
            for (int i = 0; i < WIN_ROW_AMOUNT; i++)
            {
                if (gameField[x + i, y] != curSign)
                    return false;
            }
            return true;
        }
        private bool CheckRightBottomLine(int x, int y, int curSign)
        {
            if (x + WIN_ROW_AMOUNT > FIELD_WIDTH || y + WIN_ROW_AMOUNT > FIELD_HEIGHT)
                return false;
            for (int i = 0; i < WIN_ROW_AMOUNT; i++)
            {
                if (gameField[x + i, y + i] != curSign)
                    return false;
            }
            return true;
        }
        private bool CheckBottomLine(int x, int y, int curSign)
        {
            if (y + WIN_ROW_AMOUNT > FIELD_HEIGHT)
                return false;
            for (int i = 0; i < WIN_ROW_AMOUNT; i++)
            {
                if (gameField[x, y + i] != curSign)
                    return false;
            }
            return true;
        }
        private bool CheckLeftBottomLine(int x, int y, int curSign)
        {
            if (y + WIN_ROW_AMOUNT > FIELD_HEIGHT || x - (WIN_ROW_AMOUNT - 1) < 0)
                return false;
            for (int i = 0; i < WIN_ROW_AMOUNT; i++)
            {
                if (gameField[x - i, y + i] != curSign)
                    return false;
            }
            return true;
        }

        private bool IsValidMove(int x, int y, bool isCross)
        {
            if (IsCrossToMove != isCross)
                return false;
            if (gameField[x, y] != 0)
                return false;
            if (x < 0 || y < 0 || x >= FIELD_WIDTH || y >= FIELD_HEIGHT)
                return false;
            return true;
        }

    }
}
