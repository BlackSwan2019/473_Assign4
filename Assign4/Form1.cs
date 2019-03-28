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
        int xMin = -10;
        int xMax = 10;
        int xInterval = 1;
        int yMin = -10;
        int yMax = 10;
        int yInterval = 1;

        Graphics g;     // The graphics object for the picture box containing the Cartesian graph.
        Pen pen;        // The object for drawing things on the graphics object (the graph).

        // The colors for the color selector. Determines color of a line.
        Color oneColor = Color.Black;
        Color twoColor = Color.Red;
        Color threeColor = Color.Green;
        Color fourColor = Color.Blue;

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
         *  Method:     calcLinear
         *  
         *  Purpose:    Calculates a linear equation and draws the line on the Cartesian graph.
         * 
         *  Arguments:  none
         *              
         *  Return:     void
         */
        private void calcLinear() {
            pen.Width = 2;
            pen.Color = colorOne.Color;

            Graphics g = pictureBoxGrid.CreateGraphics();
            Point[] points;

            int m = 1;      // Slope of line.
            int b = 1;      // Y-intercept.

            // Get what the user typed into the inputs for the linear equation.
            m = Convert.ToInt32(textOneM.Text);
            b = Convert.ToInt32(textOneB.Text);

            // Get the total amount of X and Y-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;
            int numberOfYAxisTicks = amountOfTicksPosY + amountOfTicksNegY;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX + 2];

            // For every positive X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksPosX + 2; i++) {
                points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) - m * (i * pixelsBetweenTicksPosY));
            }

            // Find (x, y) coordinate of right side of line.
            Point rightSideOfLine = points[points.Length - 1];

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX + 2];

            // For every negative X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksNegX + 2; i++) {
                points[i] = new Point(halfWidth - i * pixelsBetweenTicksPosX, halfHeight - (b * pixelsBetweenTicksPosY) + m * (i * pixelsBetweenTicksNegY));
            }

            // Find (x, y) coordinate of left side of line.
            Point leftSideOfLine = points[points.Length - 1];

            // Draw the line.
            g.DrawLine(pen, leftSideOfLine, rightSideOfLine);
        }

        private void calcQuadratic() {
            pen.Width = 2;
            pen.Color = colorTwo.Color;

            Graphics g = pictureBoxGrid.CreateGraphics();
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
                double x = i * pixelsBetweenTicksPosX;
                double y = (a * pixelsBetweenTicksPosX) * Math.Pow(i, 2) + (b * pixelsBetweenTicksPosX) * i;

                //points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (c * pixelsBetweenTicksPosY) - (b * (i * pixelsBetweenTicksPosY)) - (a * ((i * i) * pixelsBetweenTicksNegY)));
                points[i] = new Point(halfWidth + (int)x, halfHeight - (int)y - (c * pixelsBetweenTicksPosY));

                Console.WriteLine(x.ToString() + ", " + y.ToString() + " | " + points[i].X + ", " + points[i].Y);

            }

            // Draw the line for the positive X-axis side.
            g.DrawCurve(pen, points);

            // Construct an array of points. The number of elements in this array is the number of ticks on the negative X-axis.
            points = new Point[amountOfTicksNegX];

            // For every negative X-axis tick, make an (x,y) point.
            for (int i = 0; i < amountOfTicksNegX; i++) {
                //points[i] = new Point(halfWidth - i * pixelsBetweenTicksPosX, halfHeight - (c * pixelsBetweenTicksPosY) - (b * (i * pixelsBetweenTicksPosY)) - (a * ((i * i) * pixelsBetweenTicksPosY)));
                double x = i * pixelsBetweenTicksPosX;
                double y = a * (Math.Pow(i, 2) * pixelsBetweenTicksPosX) + b * (i * pixelsBetweenTicksPosX);

                //points[i] = new Point(halfWidth + i * pixelsBetweenTicksPosX, halfHeight - (c * pixelsBetweenTicksPosY) - (b * (i * pixelsBetweenTicksPosY)) - (a * ((i * i) * pixelsBetweenTicksNegY)));
                points[i] = new Point(halfWidth - (int)x, halfHeight - (int)y - (c * pixelsBetweenTicksPosY));
            }
            // Draw the line for the negative X-axis side
            g.DrawCurve(pen, points);
        }

        private void calcCubic() {
            pen.Width = 2;
            pen.Color = colorThree.Color;

            Graphics g = pictureBoxGrid.CreateGraphics();
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
                points[i] = new Point(halfWidth + (int)x, halfHeight - (int)y);
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
                points[i] = new Point(halfWidth - (int)x, halfHeight + (int)y - (int)(d * pixelsBetweenTicksNegX));
                //Console.WriteLine(i + " | " + (a * Math.Pow(i, 3) + b * Math.Pow(i, 2) + c * i + d) + " || " + x + " | " + y + " || " + points[i].X + " | " + points[i].Y);//+ " || " + i + " | " + ((a) * Math.Pow(i, 3) + ((b) * Math.Pow(i, 2) + ((c) * i) + (d))));
            }

            // Draw the line for the negative X-axis side.
            g.DrawCurve(pen, points);
        }

        private void calcCircle() {
            pen.Width = 2;
            pen.Color = colorFour.Color;

            Graphics g = pictureBoxGrid.CreateGraphics();
            Point[] points;

            int h = 1;      // X-axs location of center of circle.
            int k = 1;      // Y-axis location of center of circle.
            int r = 1;      // Radius of circle.

            int parsedValue;    // Used for input validation.

            // If user inputted a non-number for H, then yell at them and don't perform any more action!
            if (!int.TryParse(textFourH.Text, out parsedValue)) {
                textFourH.Clear();
                textFourK.Clear();
                textFourR.Clear();

                richTextMessage.AppendText("Numbers only!");

                return;
            }

            // If user inputted a non-number for K, then yell at them and don't perform any more action!
            if (!int.TryParse(textFourK.Text, out parsedValue)) {
                textFourH.Clear();
                textFourK.Clear();
                textFourR.Clear();

                richTextMessage.AppendText("Numbers only!");

                return;
            }
            // If user inputted a non-number for R, then yell at them and don't perform any more action!
            if (!int.TryParse(textFourR.Text, out parsedValue)) {
                textFourH.Clear();
                textFourK.Clear();
                textFourR.Clear();

                richTextMessage.AppendText("Numbers only!");

                return;
            }

            // Get what the user typed into the inputs for the linear equation.
            h = Convert.ToInt32(textFourH.Text);
            k = Convert.ToInt32(textFourK.Text);
            r = Convert.ToInt32(textFourR.Text);

            // Get the total amount of X and Y-axis ticks (positive and negative).
            int numberOfXAxisTicks = amountOfTicksPosX + amountOfTicksNegX;
            int numberOfYAxisTicks = amountOfTicksPosY + amountOfTicksNegY;

            // Construct an array of points. The number of elements in this array is the number of ticks on the positive X-axis.
            points = new Point[amountOfTicksPosX];

            Point centerOfCircle = new Point(halfWidth + (h * pixelsBetweenTicksPosX), halfHeight - (k * pixelsBetweenTicksPosY));

            int circleCenterX = halfWidth + (h * pixelsBetweenTicksPosX);
            int circleCenterY = halfHeight - (k * pixelsBetweenTicksPosY);

            int radiusOfCircle = r * pixelsBetweenTicksPosX;

            //Rectangle circleBox = new Rectangle(circleCenterX - radiusOfCircle, circleCenterY - radiusOfCircle, radiusOfCircle * 2, radiusOfCircle * 2);
            Rectangle circleBox = new Rectangle(centerOfCircle.X - radiusOfCircle, centerOfCircle.Y - radiusOfCircle, radiusOfCircle * 2, radiusOfCircle * 2);

            g.DrawEllipse(pen, circleBox);
        }

        private void pictureBoxGrid_Paint(object sender, PaintEventArgs e) {
            // Create Graphics object for the pictureBox (where the graph will be drawn).
            g = e.Graphics;

            // Make the pen to draw the x and y-axis.
            pen = new Pen(new SolidBrush(Color.White));
            pen.Width = 1;

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

        private void drawGraph() {
            // Create Graphics object for the pictureBox (where the graph will be drawn).
            g = pictureBoxGrid.CreateGraphics();

            // Make the pen to draw the x and y-axis.
            pen = new Pen(new SolidBrush(Color.White));
            pen.Width = 1;

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

        private void picColorOne_Click(object sender, EventArgs e) {
            // Show the color dialog.
            DialogResult result = colorOne.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK) {
                // Set form background to the selected color.
                this.oneColor = colorOne.Color;

                // Set the color of the color selector.
                picColorOne.BackColor = colorOne.Color;
            }
        }

        private void picColorTwo_Click(object sender, EventArgs e) {
            // Show the color dialog.
            DialogResult result = colorTwo.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK) {
                // Set form background to the selected color.
                this.twoColor = colorTwo.Color;

                // Set the color of the color selector.
                picColorTwo.BackColor = colorTwo.Color;
            }
        }

        private void picColorThree_Click(object sender, EventArgs e) {
            // Show the color dialog.
            DialogResult result = colorThree.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK) {
                // Set form background to the selected color.
                this.threeColor = colorThree.Color;

                // Set the color of the color selector.
                picColorThree.BackColor = colorThree.Color;
            }
        }

        private void picColorFour_Click(object sender, EventArgs e) {
            // Show the color dialog.
            DialogResult result = colorFour.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK) {
                // Set form background to the selected color.
                this.fourColor = colorFour.Color;

                // Set the color of the color selector.
                picColorFour.BackColor = colorFour.Color;
            }
        }              

        private void button_Parameters(object sender, EventArgs e) {
            
            xMin = Convert.ToInt32(textXMin.Text);
            xMax = 100;
            xInterval = 10;
            yMin = -100;
            yMax = 100;
            yInterval = 10;

            Refresh();
        }

        private void buttonCalcAll_Click(object sender, EventArgs e) {
            int parsedValue;    // Used for input validation.

            // Only change graph parameters if input fields are not blank and they have numbers in them.
            if (!textXMax.Text.Equals("") && int.TryParse(textXMax.Text, out parsedValue)) {
                xMax = Convert.ToInt32(textXMax.Text);
            }
            
            if (!textXMin.Text.Equals("") && int.TryParse(textXMin.Text, out parsedValue)) {
                xMin = Convert.ToInt32(textXMin.Text);
            }

            if (!textXInt.Text.Equals("") && int.TryParse(textXInt.Text, out parsedValue)) {
                xInterval = Convert.ToInt32(textXInt.Text);
            }

            if (!textYMax.Text.Equals("") && int.TryParse(textYMax.Text, out parsedValue)) {
                yMax = Convert.ToInt32(textYMax.Text);
            }

            if (!textYMin.Text.Equals("") && int.TryParse(textYMin.Text, out parsedValue)) {
                yMin = Convert.ToInt32(textYMin.Text);
            }

            if (!textYInt.Text.Equals("") && int.TryParse(textYInt.Text, out parsedValue)) {
                yInterval = Convert.ToInt32(textYInt.Text);
            }

            // Get user's graph parameters.
            //xMax = Convert.ToInt32(textXMax.Text);
            //xMin = Convert.ToInt32(textXMin.Text);
            //xInterval = Convert.ToInt32(textXInt.Text);
            //yMax = Convert.ToInt32(textYMax.Text);
            //yMin = Convert.ToInt32(textYMin.Text);
            //yInterval = Convert.ToInt32(textYInt.Text);

            // Redraw the graph.
            drawGraph();

            // Only process formulas if 1. Fields aren't blank and 2. There are numbers in the fields.
            if (!textOneB.Text.Equals("") && !textOneM.Text.Equals("")) {
                // Only if user inputted a number, then perform calculation and drawing.
                if (int.TryParse(textOneM.Text, out parsedValue) && int.TryParse(textOneB.Text, out parsedValue)) {
                    calcLinear();
                }
            }

            if (!textTwoA.Text.Equals("") && !textTwoB.Text.Equals("") && !textTwoC.Text.Equals("")) {
                // Only if user inputted a number, then perform calculation and drawing.
                if (int.TryParse(textTwoA.Text, out parsedValue) && int.TryParse(textTwoB.Text, out parsedValue) && int.TryParse(textTwoC.Text, out parsedValue)) {
                    calcQuadratic();
                }
            }

            if (!textThreeA.Text.Equals("") && !textThreeB.Text.Equals("") && !textThreeC.Text.Equals("") && !textThreeD.Text.Equals("")) {
                // Only if user inputted a number, then perform calculation and drawing.
                if (int.TryParse(textThreeA.Text, out parsedValue) && int.TryParse(textThreeB.Text, out parsedValue) && int.TryParse(textThreeC.Text, out parsedValue) && int.TryParse(textThreeD.Text, out parsedValue)) {
                    calcCubic();
                }
            }

            if (!textFourH.Text.Equals("") && !textFourK.Text.Equals("") && !textFourR.Text.Equals("") && !textFourR.Text.Equals("")) {
                // Only if user inputted a number, then perform calculation and drawing.
                if (int.TryParse(textFourH.Text, out parsedValue) && int.TryParse(textFourK.Text, out parsedValue) && int.TryParse(textFourR.Text, out parsedValue)) {
                    calcCircle();
                }
            }

            
        }
    }
}
