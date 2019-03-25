using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign4 {
    public partial class Form1 : Form {
        int xMin = 100;
        int xMax = 100;
        int xInterval = 5;
        int yMin = 100;
        int yMax = 100;
        int yInterval = 5;

        Graphics g;
        Pen pen;

        int amountOfTicksPosX = 0;          // Number of tick marks on the positive X-axis.
        int pixelsBetweenTicksPosX = 0;     // The number of pixels between each tick on the positive X-axis.
        int amountOfTicksNegX = 0;          // Number of tick marks on the negative X-axis.
        int pixelsBetweenTicksNegX = 0;     // The number of pixels between each tick on the negative X-axis.

        int amountOfTicksPosY = 0;          // Number of tick marks on the positive Y-axis.
        int pixelsBetweenTicksPosY = 0;     // The number of pixels between each tick on the positive Y-axis.
        int amountOfTicksNegY = 0;          // Number of tick marks on the negative Y-axis.
        int pixelsBetweenTicksNegY = 0;     // The number of pixels between each tick on the negative Y-axis.

        int halfHeight = 0;                 // The middle of the picture box's height.
        int halfWidth = 0;                  // The middle of the picture box's width.

        public Form1() {
            InitializeComponent();
        }

        /*  
         *  Method:     drawGrid
         *  
         *  Purpose:    Draws out the Cartesian coordinate graph in the picture box component.
         * 
         *  Arguments:  none
         *  
         *  Return:     void
         */
        private void drawGrid() {
            // Create Graphics object for the pictureBox (where the graph will be drawn).
            g = pictureBoxGrid.CreateGraphics();

            // Make the pen to draw the x and y-axis.
            pen = new Pen(new SolidBrush(Color.Black));
            pen.Width = 3;
            
            // Determine where the middle of each axis of the graphics box is.
            halfHeight = pictureBoxGrid.Height / 2;
            halfWidth = pictureBoxGrid.Width / 2;

            // Draw horizontal line.
            g.DrawLine(pen, 0, halfHeight, pictureBoxGrid.Width, halfHeight);

            // Draw vertical line.
            g.DrawLine(pen, halfWidth, 0, halfWidth, pictureBoxGrid.Height);

            // Set pen width for a tick mark.
            pen.Width = 1;


            // Get how many ticks need to be drawn on positive X-axis.
            amountOfTicksPosX = xMax / xInterval;

            // Determine how many pixels between ticks on the positive X-axis.
            pixelsBetweenTicksPosX = halfWidth / amountOfTicksPosX;

            // Get how many ticks need to be drawn on negative X-axis.
            amountOfTicksNegX = xMin / xInterval;

            // Determine how many pixels between ticks on the negative X-axis.
            pixelsBetweenTicksNegX = halfWidth / amountOfTicksNegX;


            // Get how many ticks need to be drawn on positive Y-axis.
            amountOfTicksPosY = yMax / yInterval;

            // Determine how many pixels between ticks on the positive Y-axis.
            pixelsBetweenTicksPosY = halfHeight / amountOfTicksPosY;

            // Get how many ticks need to be drawn on negative Y-axis.
            amountOfTicksNegY = yMin / yInterval;

            // Determine how many pixels between ticks on the negative Y-axis.
            pixelsBetweenTicksNegY = halfHeight / amountOfTicksNegY;


            // Draw each tick mark on the positive X-axis.
            for (int i = 0; i <= amountOfTicksPosX; i++) {
                    g.DrawLine(pen, halfWidth + (i * pixelsBetweenTicksPosX), halfHeight + 3, halfWidth + (i * pixelsBetweenTicksPosX), halfHeight - 3);
            }

            // Draw each tick mark on the negative X-axis.
            for (int i = 0; i <= amountOfTicksNegX; i++) {
                g.DrawLine(pen, halfWidth - (i * pixelsBetweenTicksNegX), halfHeight + 3, halfWidth - (i * pixelsBetweenTicksNegX), halfHeight - 3);
            }

            // Draw each tick mark on the positive Y-axis.
            for (int i = 0; i <= amountOfTicksPosY; i++) {
                g.DrawLine(pen, halfWidth + 3, halfHeight - (i * pixelsBetweenTicksPosY), halfWidth - 3, halfHeight - (i * pixelsBetweenTicksPosY));
            }

            // Draw each tick mark on the negative Y-axis.
            for (int i = 0; i <= amountOfTicksNegY; i++) {
                g.DrawLine(pen, halfWidth + 3, halfHeight + (i * pixelsBetweenTicksNegY), halfWidth - 3, halfHeight + (i * pixelsBetweenTicksNegY));
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void pictureBox2_Click(object sender, EventArgs e) {

        }

        /*  
         *  Method:     buttonLinearCalculate
         *  
         *  Purpose:    Calculates a linear equation and draws the line on the Cartesian graph.
         * 
         *  Arguments:  object      The publisher of the event.e
         *              EventArgs   Event data from the publisher.
         *              
         *  Return:     void
         */
        private void buttonLinearCalculate(object sender, EventArgs e) {
            drawGrid();

            pen.Width = 2;
            pen.Color = Color.Black;

            Point[] points;

            int m = 0;
            int b = 1;

            int parsedValue;

            // Check to see if user inputted only numbers.
            if (int.TryParse(textOneM.Text, out parsedValue)) {
                MessageBox.Show("Processing...");
            } else {
                textOneM.Clear();
                textOneB.Clear();
                MessageBox.Show("Numbers only!");
                return;
            }

            // Get what the user typed into the inputs for the linear equation.
            m = Convert.ToInt32(textOneM.Text);
            b = Convert.ToInt32(textOneB.Text);

            // Get the total amount of X-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksPosX; i++) {
                points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) - m * (i * pixelsBetweenTicksPosY));
            }

            // Draw the line for the positive X-axis side.
            g.DrawLines(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX];

            // For every negative X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksNegX; i++) {
                points[i] = new Point(halfWidth - i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) + m * (i * pixelsBetweenTicksNegY));
            }
            // Draw the line for the negative X-axis side.
            g.DrawLines(pen, points);
        }

        private void textOneM_KeyPress(object sender, KeyPressEventArgs e) {
        
        }

        private void calcCubic_Click(object sender, EventArgs e) {
            drawGrid();

            pen.Width = 2;
            pen.Color = Color.Black;

            Point[] points;

            float a = .5f;
            float b = .5f;
            float c = .5f;
            float d = 0f;

            int parsedValue;

            //// Check to see if user inputted only numbers.
            //if (int.TryParse(textThreeA.Text, out parsedValue)) {
            //    //MessageBox.Show("Processing...");
            //} else {
            //    textOneM.Clear();
            //    textOneB.Clear();
            //    //MessageBox.Show("Numbers only!");
            //    return;
            //}

            // Get what the user typed into the inputs for the linear equation.
            //a = Convert.ToInt32(textThreeA.Text);
            //b = Convert.ToInt32(textThreeB.Text);
            //c = Convert.ToInt32(textThreeC.Text);
            //d = Convert.ToInt32(textThreeD.Text);

            // Get the total amount of X-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i != amountOfTicksPosX; i++) {
                //points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) - m * (i * pixelsBetweenTicksPosY));
                //y = ax3 + bx2 + cx + d
                double x = (i * pixelsBetweenTicksPosX);
                double y = ((a * pixelsBetweenTicksPosX) * Math.Pow(i, 3) + ((b * pixelsBetweenTicksPosX) * Math.Pow(i, 2) + ((c * pixelsBetweenTicksPosX) * i) + (d * pixelsBetweenTicksPosX)));
                points[i] = new Point(halfWidth + (int) x, halfHeight - (int) y * (i * pixelsBetweenTicksPosY));
                if (i % 5 == 0) {
                    //MessageBox.Show(points[i].X + " | " + points[i].Y + " | " + amountOfTicksPosX + " | " + pixelsBetweenTicksPosX + " | " + pixelsBetweenTicksPosY);
                }
            }

            //MessageBox.Show(points.Length + "");

            // Draw the line for the positive X-axis side.
            g.DrawCurve(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[halfWidth];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i != amountOfTicksPosX; i++) {
                //points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) - m * (i * pixelsBetweenTicksPosY));
                //y = ax3 + bx2 + cx + d
                double x = (i * pixelsBetweenTicksPosX);
                double y = ((a * pixelsBetweenTicksPosX) * Math.Pow(i, 3) + ((b * pixelsBetweenTicksPosX) * Math.Pow(i, 2) + ((c * pixelsBetweenTicksPosX) * i) + (d * pixelsBetweenTicksPosX)));
                points[i] = new Point(halfWidth - (int)x, halfHeight + (int)y * (i * pixelsBetweenTicksPosY));
                if (i % 5 == 0) {
                    //MessageBox.Show(points[i].X + " | " + points[i].Y + " | " + amountOfTicksPosX + " | " + pixelsBetweenTicksPosX + " | " + pixelsBetweenTicksPosY);
                }
            }

            // Draw the line for the negative X-axis side.
            g.DrawCurve(pen, points);
        }
    }
}
