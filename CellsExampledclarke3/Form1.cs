using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellsExampledclarke3
{
    public partial class Form1 : Form
    {
        
        int numBombs = 10;
        Cell[,] grid = new Cell[10,10];
        Random rand = new Random();
        bool firstClick = false;

        
        public Form1()
        {
            InitializeComponent();
            CreateGrid();

        }
        private void CreateGrid()
        {
            // creates grid of cells 
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Cell temp = new Cell();
                    temp.X = col;
                    temp.Y = row;
                    //temp.CellColor = (rand.Next(2) % 2 == 0) ? Color.MediumOrchid : Color.Wheat; // turinary statement for cell color
                    temp.Location = new Point(col * temp.Size.Width, row * temp.Size.Height);
                    temp.OnCellClick += OnCellClick;
                    this.Controls.Add(temp);
                    //temp.CellButton.Click += OnCellClick; //subscribes cell to its button click
                    grid[col, row] = temp;
                }
            }
        }

        public void PlaceBombs()
        {
            for (int i = 0; i < numBombs; i++)
            {
                int x = rand.Next(9);
                int y = rand.Next(9);
                
                while(grid[x, y].MyPanelLabel.Text == "M")
                {
                    x = rand.Next(9);
                    y = rand.Next(9);
                }
                grid[x, y].MyPanelLabel.Text = "M";
            }
        }
        // create helper methods for this
        public void PlaceNums()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    int numBombs = 0;

                    // check around the grid and count the M's
                    numBombs = CheckUpNums(row, col, numBombs);
                    numBombs = CheckDownNums(row, col, numBombs);
                    numBombs = CheckSizesNums(row, col, numBombs);
                    // set the num in the label
                    if (grid[col, row].MyPanelLabel.Text != "M")
                    {
                        grid[col, row].MyPanelLabel.Text = numBombs.ToString();
                    }
                }
            }
        }

        private int CheckSizesNums(int row, int col, int numBombs)
        {
            // check left
            if (col > 0)
            {
                if (grid[col - 1, row].MyPanelLabel.Text == "M")
                {
                    numBombs++;
                }
            }
            //check right
            if (col < grid.GetLength(1) - 1)
            {
                if (grid[col + 1, row].MyPanelLabel.Text == "M")
                {
                    numBombs++;
                }
            }

            return numBombs;
        }

        private int CheckDownNums(int row, int col, int numBombs)
        {
            if (row < grid.GetLength(0) - 1)
            {
                if (grid[col, row + 1].MyPanelLabel.Text == "M")
                {
                    numBombs++;
                }
                // check left and down
                if (col > 0)
                {
                    if (grid[col - 1, row + 1].MyPanelLabel.Text == "M")
                    {
                        numBombs++;
                    }
                }
                //check to the right and down
                if (col < grid.GetLength(1) - 1)
                {
                    if (grid[col + 1, row + 1].MyPanelLabel.Text == "M")
                    {
                        numBombs++;
                    }
                }
            }

            return numBombs;
        }

        private int CheckUpNums(int row, int col, int numBombs)
        {
            if (row > 0)
            {
                //check up
                if (grid[col, row - 1].MyPanelLabel.Text == "M")
                {
                    numBombs++;
                }
                //check to the left and up
                if (col > 0)
                {
                    if (grid[col - 1, row - 1].MyPanelLabel.Text == "M")
                    {
                        numBombs++;
                    }
                }
                //check to the right and up
                if (col < grid.GetLength(1) - 1)
                {
                    if (grid[col + 1, row - 1].MyPanelLabel.Text == "M")
                    {
                        numBombs++;
                    }
                }
            }

            return numBombs;
        }

        public void OnCellClick(object sender, EventArgs e)
        {
            if (firstClick == false)
            {
                PlaceBombs();
                PlaceNums();
                firstClick = true;
            }

            
            int row = ((Cell)sender).Y;
            int col = ((Cell)sender).X;

            // this is the cascade event for 0
            
            CheckUpCascade(row, col);
            CheckDownCascade(row, col);
            CheckSidesCascade(row, col);
        }

        private void CheckSidesCascade(int row, int col)
        {
            // check left
            if (col > 0)
            {
                if (grid[col - 1, row].MyPanelLabel.Text == "0")
                {
                    grid[col - 1, row].MyButton.PerformClick();
                }
            }
            //check right
            if (col < grid.GetLength(1) - 1)
            {
                if (grid[col + 1, row].MyPanelLabel.Text == "0")
                {
                    grid[col + 1, row].MyButton.PerformClick();
                }
            }
        }

        private void CheckDownCascade(int row, int col)
        {
            if (row < grid.GetLength(0) - 1)
            {
                // check down
                if (grid[col, row + 1].MyPanelLabel.Text == "0")
                {
                    grid[col, row + 1].MyButton.PerformClick();
                }
                // check left and down
                if (col > 0)
                {
                    if (grid[col - 1, row + 1].MyPanelLabel.Text == "0")
                    {
                        grid[col - 1, row + 1].MyButton.PerformClick();
                    }
                }
                //check to the right and down
                if (col < grid.GetLength(1) - 1)
                {
                    if (grid[col + 1, row + 1].MyPanelLabel.Text == "0")
                    {
                        grid[col + 1, row + 1].MyButton.PerformClick();
                    }
                }
            }
        }

        private void CheckUpCascade(int row, int col)
        {
            if (row > 0)
            {
                //check up
                if (grid[col, row - 1].MyPanelLabel.Text == "0")
                {
                    numBombs++;
                }
                //check to the left and up
                if (col > 0)
                {
                    if (grid[col - 1, row - 1].MyPanelLabel.Text == "0")
                    {
                        grid[col - 1, row - 1].MyButton.PerformClick();
                    }
                }
                //check to the right and up
                if (col < grid.GetLength(1) - 1)
                {
                    if (grid[col + 1, row - 1].MyPanelLabel.Text == "0")
                    {
                        grid[col + 1, row - 1].MyButton.PerformClick();
                    }
                }
            }
        }
    }  
    
}
