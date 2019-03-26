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
        int xMin = -100;
        int xMax = 100;
        int xInterval = 10;
        int yMin = -100;
        int yMax = 100;
        int yInterval = 10;

        Graphics g;
        Pen pen;

        Color oneColor = Color.Red;
        Color twoColor = Color.Red;
        Color threeColor = Color.Red;
        Color fourColor = Color.Red;

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
            amountOfTicksPosX = Math.Abs(xMax) / Math.Abs(xInterval);

            // Determine how many pixels between ticks on the positive X-axis.
            pixelsBetweenTicksPosX = halfWidth / amountOfTicksPosX;

            // Get how many ticks need to be drawn on negative X-axis.
            amountOfTicksNegX = Math.Abs(xMin) / Math.Abs(xInterval);

            // Determine how many pixels between ticks on the negative X-axis.
            pixelsBetweenTicksNegX = halfWidth / amountOfTicksNegX;


            // Get how many ticks need to be drawn on positive Y-axis.
            amountOfTicksPosY = Math.Abs(yMax) / Math.Abs(yInterval);

            // Determine how many pixels between ticks on the positive Y-axis.
            pixelsBetweenTicksPosY = halfHeight / amountOfTicksPosY;

            // Get how many ticks need to be drawn on negative Y-axis.
            amountOfTicksNegY = Math.Abs(yMin) / Math.Abs(yInterval);

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

            // If user inputted a non-number, then yell at them and don't perform any more action!
            if (!int.TryParse(textOneM.Text, out parsedValue)) {
                textOneM.Clear();
                textOneB.Clear();

                richTextMessage.AppendText("Numbers only!");

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

            // Find the max (x, y) coordinate point of the positive X-axis line.
            Point maxPositive = new Point(pictureBoxGrid.Width, b - (m * pictureBoxGrid.Width));
            Point minPositive = new Point(halfWidth, halfHeight - (b * pixelsBetweenTicksPosY));

            //MessageBox.Show(maxPositive.X.ToString() + " " + maxPositive.Y.ToString());
            //MessageBox.Show(minPositive.X.ToString() + " " + minPositive.Y.ToString());

            g.DrawLine(pen, minPositive, maxPositive);

            // Draw the line for the positive X-axis side.
            //g.DrawLines(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX];

            // For every negative X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksNegX; i++) {
                points[i] = new Point(halfWidth - i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) + m * (i * pixelsBetweenTicksNegY));
            }

            // Find the max (x, y) coordinate point of the negative X-axis line.
            Point maxNegative = new Point(halfWidth, halfHeight - (b * pixelsBetweenTicksNegY));
            Point minNegative = new Point(0, b + (m * pictureBoxGrid.Width));

            //Point minNegative = new Point(0, halfHeight - (b * pixelsBetweenTicksPosY));

            //MessageBox.Show(minNegative.X.ToString() + " " + minNegative.Y.ToString());

            // Draw the line for the negative X-axis side.
            g.DrawLine(pen, minNegative, maxNegative);
            //g.DrawLines(pen, points);
        }

        private void textOneM_KeyPress(object sender, KeyPressEventArgs e) {
        
        }

        private void calcCubic_Click(object sender, EventArgs e) {
            drawGrid();

            pen.Width = 2;
            pen.Color = colorThree.Color;

            Point[] points;

            float a = 1f;
            float b = 1f;
            float c = 1f;
            float d = 0f;

            float parsedValue;

            // Check to see if user inputted only numbers.
            if (float.TryParse(textThreeA.Text, out parsedValue)) {
                a = parsedValue;
            } else {
                textThreeA.Clear();
                richTextMessage.Text = "Only Numbers!";
                return;
            }

            // Check to see if user inputted only numbers.
            if (float.TryParse(textThreeB.Text, out parsedValue)) {
                b = parsedValue;
            } else {
                textThreeB.Clear();
                richTextMessage.Text = "Only Numbers!";
                return;
            }

            // Check to see if user inputted only numbers.
            if (float.TryParse(textThreeC.Text, out parsedValue)) {
                c = parsedValue;
            } else {
                textThreeC.Clear();
                richTextMessage.Text = "Only Numbers!";
                return;
            }

            // Check to see if user inputted only numbers.
            if (float.TryParse(textThreeD.Text, out parsedValue)) {
                d = parsedValue;
            } else {
                textThreeD.Clear();
                richTextMessage.Text = "Only Numbers!";
                return;
            }

            // Get the total amount of X-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i != amountOfTicksPosX; i++) {
                //y = ax3 + bx2 + cx + d
                double x = (i * pixelsBetweenTicksPosX);
                double y = ((a * pixelsBetweenTicksPosX) * Math.Pow(i, 3) + ((b * pixelsBetweenTicksPosX) * Math.Pow(i, 2) + ((c * pixelsBetweenTicksPosX) * i) + (d * pixelsBetweenTicksPosX)));
                points[i] = new Point(halfWidth + (int) x, halfHeight - (int) y);
                //Console.WriteLine(i + " | " + (a * Math.Pow(i, 3) + b * Math.Pow(i,2) + c * i + d) + " || " + x + " | " + y + " || " + points[i].X + " | " + points[i].Y);//+ " || " + i + " | " + ((a) * Math.Pow(i, 3) + ((b) * Math.Pow(i, 2) + ((c) * i) + (d))));
            }

            // Draw the line for the positive X-axis side.
            g.DrawCurve(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i != amountOfTicksNegX; i++) {
                //y = ax3 + bx2 + cx + d
                double x = (i * pixelsBetweenTicksNegX);
                double y = ((a * pixelsBetweenTicksNegX) * Math.Pow(i, 3) + ((b * pixelsBetweenTicksNegX) * Math.Pow(i, 2) + ((c * pixelsBetweenTicksNegX) * i)));
                points[i] = new Point(halfWidth - (int)x, halfHeight + (int)y - (int) (d * pixelsBetweenTicksNegX));
                //Console.WriteLine(i + " | " + (a * Math.Pow(i, 3) + b * Math.Pow(i, 2) + c * i + d) + " || " + x + " | " + y + " || " + points[i].X + " | " + points[i].Y);//+ " || " + i + " | " + ((a) * Math.Pow(i, 3) + ((b) * Math.Pow(i, 2) + ((c) * i) + (d))));
            }

            // Draw the line for the negative X-axis side.
            g.DrawCurve(pen, points);
        }

        private void ButtonQuadraticCalculate(object sender, EventArgs e) {
            drawGrid();

            pen.Width = 2;
            pen.Color = Color.Black;

            Point[] points;

            int a = 0;
            int b = 1;
            int c = 2;

            int parsedValue;

            // Check to see if user inputted only numbers.
            if (!int.TryParse(textTwoA.Text, out parsedValue)) {
                textTwoA.Clear();
                textTwoB.Clear();
                textTwoC.Clear();

                richTextMessage.AppendText("Numbers only!");

                return;
            }

            // Get what the user typed into the inputs for the linear equation.
            a = Convert.ToInt32(textTwoA.Text);
            b = Convert.ToInt32(textTwoB.Text);
            c = Convert.ToInt32(textTwoC.Text);

            // Get the total amount of X-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksPosX; i++) {
                points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (c * pixelsBetweenTicksPosY) - (b * (i * pixelsBetweenTicksPosY)) - (a * ((i * i) * pixelsBetweenTicksNegY)));
            }

            // Draw the line for the positive X-axis side.
            g.DrawLines(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX];

            // For every negative X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksNegX; i++) {
                points[i] = new Point(halfWidth - i * pixelsBetweenTicksPosX, halfHeight - (c * pixelsBetweenTicksPosY) - (b * (i * pixelsBetweenTicksPosY)) - (a * ((i * i) * pixelsBetweenTicksPosY)));
            }
            // Draw the line for the negative X-axis side
            g.DrawCurve(pen, points);
        }

        private void picColorThree_Click(object sender, EventArgs e) {
            // Show the color dialog.
            DialogResult result = colorThree.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK) {
                // Set form background to the selected color.
                this.threeColor = colorThree.Color;
            }
        }

    }
}
