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
    public partial class Cell : UserControl
    {
        
        public EventHandler OnCellClick;
        
        Panel myPanel;
        Button myButton;
        Label myPanelLabel = new Label();
        int size = 32;
        int row; // refactor encapsilate and use prooperty **1
        int col;
         
        
        public Cell()
        {
            InitializeComponent();
            SetButton();
            myPanelLabel.Text = "0";
            this.Controls.Add(myPanelLabel);
            SetPanel();
            this.Size = new Size(size, size);
        }

        //**1 created from this command
        public int X { get => row; set => row = value; }
        public int Y { get => col; set => col = value; }
        //sets and gets the color for the cell
        public Color CellColor { get => myPanel.BackColor; set => myPanel.BackColor = value; }
        public Button MyButton { get => myButton; }
        public Panel MyPanel { get => myPanel; }

        // getter for button
        public Button CellButton { get => myButton; }
        public Label MyPanelLabel { get => myPanelLabel; set => myPanelLabel = value; }


        private void SetButton()
        {
            myButton = new Button();
            myButton.Size = new Size(size, size); //create the size of the button
            myButton.Location = this.Location;  // set the button to the cells location
            this.Controls.Add(myButton);
            CellButton.Click += OnButtonClick; // registers click event to cells button
        }

        private void SetPanel()
        {
            myPanel = new Panel();
            Label myPanelLabel = new Label();
            myPanelLabel.Text = " ";
            myPanel.Size = new Size(size, size); //create the size of the button
            myPanel.Location = this.Location;  // set the button to the cells location
            myPanel.BackColor = Color.Beige;
            this.Controls.Add(myPanel);
        }

       
        // decides what happens on click
        public void OnButtonClick(object sender, EventArgs e)
        {
            
            Button temp = (Button)sender;
            temp.Visible = false; // can be combined to ((Button)sender).Visible = false;

            if(OnCellClick != null)
            {
                OnCellClick(this, EventArgs.Empty);
            }
            
        }

    }
}
