using System;
using System.Windows.Forms;
using MethodLib;
using System.Windows.Forms.DataVisualization.Charting;

namespace ODU
{
    public partial class Form1 : Form
    {

        TextBox[] txtbox;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            txtbox = new TextBox[]
            {
                y11_textBox, y12_textBox, y13_textBox, y14_textBox,
                y21_textBox, y22_textBox, y23_textBox, y24_textBox,
                y31_textBox, y32_textBox, y33_textBox, y34_textBox,
                y41_textBox, y42_textBox, y43_textBox, y44_textBox,
                y1x0_textBox, y2x0_textBox, y3x0_textBox, y4x0_textBox,
                x0_textBox, xn_textBox, h_textBox, result_x_textBox,
                result_y1_textBox, result_y2_textBox, result_y3_textBox, result_y4_textBox
            };
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                #region Enabled
                StartButton.Enabled = false;
                ClearButton.Enabled = true;
                comboBox1.Enabled = false;
                MethodsBox.Enabled = false;
                for (int i = 0; i < txtbox.Length - 5; i++)
                {
                    txtbox[i].Enabled = false;
                }
                #endregion
                #region ConvertToDouble
                int order = Convert.ToInt32(comboBox1.Text);
                Vector coefficients = new Vector(order * order);
                Vector y = new Vector(order);
                switch (order)
                {
                    case (2):
                        {
                            coefficients[0] = Convert.ToDouble(y11_textBox.Text);
                            coefficients[1] = Convert.ToDouble(y12_textBox.Text);
                            coefficients[2] = Convert.ToDouble(y21_textBox.Text);
                            coefficients[3] = Convert.ToDouble(y22_textBox.Text);
                            y[0] = Convert.ToDouble(y1x0_textBox.Text);
                            y[1] = Convert.ToDouble(y2x0_textBox.Text);

                            break;
                        }
                    case (3):
                        {
                            coefficients[0] = Convert.ToDouble(y11_textBox.Text);
                            coefficients[1] = Convert.ToDouble(y12_textBox.Text);
                            coefficients[2] = Convert.ToDouble(y13_textBox.Text);
                            coefficients[3] = Convert.ToDouble(y21_textBox.Text);
                            coefficients[4] = Convert.ToDouble(y22_textBox.Text);
                            coefficients[5] = Convert.ToDouble(y23_textBox.Text);
                            coefficients[6] = Convert.ToDouble(y31_textBox.Text);
                            coefficients[7] = Convert.ToDouble(y32_textBox.Text);
                            coefficients[8] = Convert.ToDouble(y33_textBox.Text);
                            y[0] = Convert.ToDouble(y1x0_textBox.Text);
                            y[1] = Convert.ToDouble(y2x0_textBox.Text);
                            y[2] = Convert.ToDouble(y3x0_textBox.Text);

                            break;
                        }
                    case (4):
                        {
                            coefficients[0] = Convert.ToDouble(y11_textBox.Text);
                            coefficients[1] = Convert.ToDouble(y12_textBox.Text);
                            coefficients[2] = Convert.ToDouble(y13_textBox.Text);
                            coefficients[3] = Convert.ToDouble(y14_textBox.Text);
                            coefficients[4] = Convert.ToDouble(y21_textBox.Text);
                            coefficients[5] = Convert.ToDouble(y22_textBox.Text);
                            coefficients[6] = Convert.ToDouble(y23_textBox.Text);
                            coefficients[7] = Convert.ToDouble(y24_textBox.Text);
                            coefficients[8] = Convert.ToDouble(y31_textBox.Text);
                            coefficients[9] = Convert.ToDouble(y32_textBox.Text);
                            coefficients[10] = Convert.ToDouble(y33_textBox.Text);
                            coefficients[11] = Convert.ToDouble(y34_textBox.Text);
                            coefficients[12] = Convert.ToDouble(y41_textBox.Text);
                            coefficients[13] = Convert.ToDouble(y42_textBox.Text);
                            coefficients[14] = Convert.ToDouble(y43_textBox.Text);
                            coefficients[15] = Convert.ToDouble(y44_textBox.Text);
                            y[0] = Convert.ToDouble(y1x0_textBox.Text);
                            y[1] = Convert.ToDouble(y2x0_textBox.Text);
                            y[2] = Convert.ToDouble(y3x0_textBox.Text);
                            y[3] = Convert.ToDouble(y4x0_textBox.Text);

                            break;
                        }
                }
                double x0 = Convert.ToDouble(x0_textBox.Text);
                double xn = Convert.ToDouble(xn_textBox.Text);
                double h = Convert.ToDouble(h_textBox.Text);
                #endregion
                if (x0 >= xn)
                    throw new ArgumentException();
                Method method = new Method();
                int n = (int)((xn - x0) / h);
                double[,] Matrix = new double[n + 1, order + 1];
                if (EulerRadioButton.Checked == true)
                    Matrix = Method.Euler(coefficients, x0, xn, h, y, n + 1);
                if (RungeKuttaRadioButton.Checked == true)
                    Matrix = Method.RungeKutta(coefficients, x0, xn, h, y, n + 1);
                if (AdamsRadioButton.Checked == true)
                    Matrix = Method.Adams(coefficients, x0, xn, h, y, n + 1);
                #region Output
                for (int i = 0; i < n + 1; i++)
                {
                    result_x_textBox.Text += String.Format("{0:0.###}", Matrix[i, 0]) + Environment.NewLine;
                    result_y1_textBox.Text += String.Format("{0:0.###}", Matrix[i, 1]) + Environment.NewLine;
                    result_y2_textBox.Text += String.Format("{0:0.###}", Matrix[i, 2]) + Environment.NewLine;
                    if(comboBox1.Text == "3")
                        result_y3_textBox.Text += String.Format("{0:0.###}", Matrix[i, 3]) + Environment.NewLine;
                    if (comboBox1.Text == "4")
                    {
                        result_y3_textBox.Text += String.Format("{0:0.###}", Matrix[i, 3]) + Environment.NewLine;
                        result_y4_textBox.Text += String.Format("{0:0.###}", Matrix[i, 4]) + Environment.NewLine;
                    }
                }
                #endregion
                #region Graphics
                chart1.Series.Clear();
                chart1.Series.Add("y1");
                chart1.Series.Add("y2");
                if (comboBox1.Text == "3")
                    chart1.Series.Add("y3");
                if (comboBox1.Text == "4")
                {
                    chart1.Series.Add("y3");
                    chart1.Series.Add("y4");
                }
                for (int j = 0; j < order; j++)
                {
                    chart1.Series[j].ChartType = SeriesChartType.Line;
                    for (int i = 0; i < n + 1; i++)
                    {
                        chart1.Series[j].Points.AddXY(Matrix[i, 0], Matrix[i, j + 1]);
                    }
                }
                #endregion
            }
            #region Exceptions
            catch (ArgumentException)
            {
                MessageBox.Show("x0 должен быть меньше xn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for (int i = 0; i < txtbox.Length; i++)
                {
                    txtbox[i].Text = "";
                    txtbox[i].Enabled = true;
                }
                StartButton.Enabled = true;
                ClearButton.Enabled = false;
                comboBox1.Enabled = true;
                MethodsBox.Enabled = true;
                EulerRadioButton.Checked = true;
                RungeKuttaRadioButton.Checked = false;
                AdamsRadioButton.Checked = false;
                chart1.Series.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for (int i = 0; i < txtbox.Length; i++)
                {
                    txtbox[i].Text = "";
                    txtbox[i].Enabled = true;
                }
                StartButton.Enabled = true;
                ClearButton.Enabled = false;
                comboBox1.Enabled = true;
                MethodsBox.Enabled = true;
                EulerRadioButton.Checked = true;
                RungeKuttaRadioButton.Checked = false;
                AdamsRadioButton.Checked = false;
                chart1.Series.Clear();
            }
            #endregion
        }

        #region Clear
        private void ClearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < txtbox.Length; i++)
            {
                txtbox[i].Text = "";
                txtbox[i].Enabled = true;
            }
            StartButton.Enabled = true;
            ClearButton.Enabled = false;
            comboBox1.Enabled = true;
            MethodsBox.Enabled = true;
            EulerRadioButton.Checked = true;
            RungeKuttaRadioButton.Checked = false;
            AdamsRadioButton.Checked = false;
            chart1.Series.Clear();
        }
        #endregion

        #region Visible
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.Text)
            {
                case "2":
                    {
                        y11_textBox.Visible = true;                       
                        y12_textBox.Visible = true;
                        y13_textBox.Visible = false;
                        y14_textBox.Visible = false;
                        y1_label.Visible = true;
                        y11_label.Visible = true;
                        label1.Visible = true;
                        y12_label.Visible = true;
                        label2.Visible = false;
                        y13_label.Visible = false;
                        label3.Visible = false;
                        y14_label.Visible = false;
                        y11_textBox.Text = "";
                        y12_textBox.Text = "";

                        y21_textBox.Visible = true;
                        y22_textBox.Visible = true;
                        y23_textBox.Visible = false;
                        y24_textBox.Visible = false;
                        y2_label.Visible = true;
                        y21_label.Visible = true;
                        label4.Visible = true;
                        y22_label.Visible = true;
                        label5.Visible = false;
                        y23_label.Visible = false;
                        label6.Visible = false;
                        y24_label.Visible = false;
                        y21_textBox.Text = "";
                        y22_textBox.Text = "";

                        y31_textBox.Visible = false;
                        y32_textBox.Visible = false;
                        y33_textBox.Visible = false;
                        y34_textBox.Visible = false;
                        y3_label.Visible = false;
                        y31_label.Visible = false;
                        label7.Visible = false;
                        y32_label.Visible = false;
                        label8.Visible = false;
                        y33_label.Visible = false;
                        label9.Visible = false;
                        y34_label.Visible = false;

                        y41_textBox.Visible = false;
                        y42_textBox.Visible = false;
                        y43_textBox.Visible = false;
                        y44_textBox.Visible = false;
                        y4_label.Visible = false;
                        y41_label.Visible = false;
                        label10.Visible = false;
                        y42_label.Visible = false;
                        label11.Visible = false;
                        y43_label.Visible = false;
                        label12.Visible = false;
                        y44_label.Visible = false;

                        y1x0_textBox.Visible = true;
                        y2x0_textBox.Visible = true;
                        y3x0_textBox.Visible = false;
                        y4x0_textBox.Visible = false;
                        y1x0_label.Visible = true;
                        y2x0_label.Visible = true;
                        y3x0_label.Visible = false;
                        y4x0_label.Visible = false;
                        y1x0_textBox.Text = "";
                        y2x0_textBox.Text = "";

                        result_x_textBox.Visible = true;
                        result_y1_textBox.Visible = true;
                        result_y2_textBox.Visible = true;
                        result_y3_textBox.Visible = false;
                        result_y4_textBox.Visible = false;
                        result_x_label.Visible = true;
                        result_y1_label.Visible = true;
                        result_y2_label.Visible = true;
                        result_y3_label.Visible = false;
                        result_y4_label.Visible = false;
                        result_x_textBox.Text = "";
                        result_y1_textBox.Text = "";
                        result_y2_textBox.Text = "";

                        break;
                    }
                case "3":
                    {
                        y11_textBox.Visible = true;
                        y12_textBox.Visible = true;
                        y13_textBox.Visible = true;
                        y14_textBox.Visible = false;
                        y1_label.Visible = true;
                        y11_label.Visible = true;
                        label1.Visible = true;
                        y12_label.Visible = true;
                        label2.Visible = true;
                        y13_label.Visible = true;
                        label3.Visible = false;
                        y14_label.Visible = false;
                        y11_textBox.Text = "";
                        y12_textBox.Text = "";
                        y13_textBox.Text = "";

                        y21_textBox.Visible = true;
                        y22_textBox.Visible = true;
                        y23_textBox.Visible = true;
                        y24_textBox.Visible = false;
                        y2_label.Visible = true;
                        y21_label.Visible = true;
                        label4.Visible = true;
                        y22_label.Visible = true;
                        label5.Visible = true;
                        y23_label.Visible = true;
                        label6.Visible = false;
                        y24_label.Visible = false;
                        y21_textBox.Text = "";
                        y22_textBox.Text = "";
                        y23_textBox.Text = "";

                        y31_textBox.Visible = true;
                        y32_textBox.Visible = true;
                        y33_textBox.Visible = true;
                        y34_textBox.Visible = false;
                        y3_label.Visible = true;
                        y31_label.Visible = true;
                        label7.Visible = true;
                        y32_label.Visible = true;
                        label8.Visible = true;
                        y33_label.Visible = true;
                        label9.Visible = false;
                        y34_label.Visible = false;
                        y31_textBox.Text = "";
                        y32_textBox.Text = "";
                        y33_textBox.Text = "";

                        y41_textBox.Visible = false;
                        y42_textBox.Visible = false;
                        y43_textBox.Visible = false;
                        y44_textBox.Visible = false;
                        y4_label.Visible = false;
                        y41_label.Visible = false;
                        label10.Visible = false;
                        y42_label.Visible = false;
                        label11.Visible = false;
                        y43_label.Visible = false;
                        label12.Visible = false;
                        y44_label.Visible = false;

                        y1x0_textBox.Visible = true;
                        y2x0_textBox.Visible = true;
                        y3x0_textBox.Visible = true;
                        y4x0_textBox.Visible = false;
                        y1x0_label.Visible = true;
                        y2x0_label.Visible = true;
                        y3x0_label.Visible = true;
                        y4x0_label.Visible = false;
                        y1x0_textBox.Text = "";
                        y2x0_textBox.Text = "";
                        y3x0_textBox.Text = "";

                        result_x_textBox.Visible = true;
                        result_y1_textBox.Visible = true;
                        result_y2_textBox.Visible = true;
                        result_y3_textBox.Visible = true;
                        result_y4_textBox.Visible = false;
                        result_x_label.Visible = true;
                        result_y1_label.Visible = true;
                        result_y2_label.Visible = true;
                        result_y3_label.Visible = true;
                        result_y4_label.Visible = false;
                        result_x_textBox.Text = "";
                        result_y1_textBox.Text = "";
                        result_y2_textBox.Text = "";
                        result_y3_textBox.Text = "";

                        break;
                    }
                case "4":
                    {
                        y11_textBox.Visible = true;
                        y12_textBox.Visible = true;
                        y13_textBox.Visible = true;
                        y14_textBox.Visible = true;
                        y1_label.Visible = true;
                        y11_label.Visible = true;
                        label1.Visible = true;
                        y12_label.Visible = true;
                        label2.Visible = true;
                        y13_label.Visible = true;
                        label3.Visible = true;
                        y14_label.Visible = true;
                        y11_textBox.Text = "";
                        y12_textBox.Text = "";
                        y13_textBox.Text = "";
                        y14_textBox.Text = "";

                        y21_textBox.Visible = true;
                        y22_textBox.Visible = true;
                        y23_textBox.Visible = true;
                        y24_textBox.Visible = true;
                        y2_label.Visible = true;
                        y21_label.Visible = true;
                        label4.Visible = true;
                        y22_label.Visible = true;
                        label5.Visible = true;
                        y23_label.Visible = true;
                        label6.Visible = true;
                        y24_label.Visible = true;
                        y21_textBox.Text = "";
                        y22_textBox.Text = "";
                        y23_textBox.Text = "";
                        y24_textBox.Text = "";

                        y31_textBox.Visible = true;
                        y32_textBox.Visible = true;
                        y33_textBox.Visible = true;
                        y34_textBox.Visible = true;
                        y3_label.Visible = true;
                        y31_label.Visible = true;
                        label7.Visible = true;
                        y32_label.Visible = true;
                        label8.Visible = true;
                        y33_label.Visible = true;
                        label9.Visible = true;
                        y34_label.Visible = true;
                        y31_textBox.Text = "";
                        y32_textBox.Text = "";
                        y33_textBox.Text = "";
                        y34_textBox.Text = "";

                        y41_textBox.Visible = true;
                        y42_textBox.Visible = true;
                        y43_textBox.Visible = true;
                        y44_textBox.Visible = true;
                        y4_label.Visible = true;
                        y41_label.Visible = true;
                        label10.Visible = true;
                        y42_label.Visible = true;
                        label11.Visible = true;
                        y43_label.Visible = true;
                        label12.Visible = true;
                        y44_label.Visible = true;
                        y41_textBox.Text = "";
                        y42_textBox.Text = "";
                        y43_textBox.Text = "";
                        y44_textBox.Text = "";

                        y1x0_textBox.Visible = true;
                        y2x0_textBox.Visible = true;
                        y3x0_textBox.Visible = true;
                        y4x0_textBox.Visible = true;
                        y1x0_label.Visible = true;
                        y2x0_label.Visible = true;
                        y3x0_label.Visible = true;
                        y4x0_label.Visible = true;
                        y1x0_textBox.Text = "";
                        y2x0_textBox.Text = "";
                        y3x0_textBox.Text = "";
                        y4x0_textBox.Text = "";

                        result_x_textBox.Visible = true;
                        result_y1_textBox.Visible = true;
                        result_y2_textBox.Visible = true;
                        result_y3_textBox.Visible = true;
                        result_y4_textBox.Visible = true;
                        result_x_label.Visible = true;
                        result_y1_label.Visible = true;
                        result_y2_label.Visible = true;
                        result_y3_label.Visible = true;
                        result_y4_label.Visible = true;
                        result_x_textBox.Text = "";
                        result_y1_textBox.Text = "";
                        result_y2_textBox.Text = "";
                        result_y3_textBox.Text = "";
                        result_y4_textBox.Text = "";

                        break;
                    }
            }
        }
        #endregion

        #region KeyPress
        private void y11_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y12_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y13_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y14_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y21_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y22_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y23_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y24_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y31_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y32_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y33_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y34_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y41_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y42_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y43_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y44_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y1x0_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y2x0_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y3x0_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void y4x0_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void x0_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void xn_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }

        private void h_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45 && number != 44)
                e.Handled = true;
        }
        
        private void result_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void result_y1_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void result_y2_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void result_y3_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void result_y4_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region(Info)
        private void InfoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Программу составил студент 303 гр. ФМФ Лира Р. В. " +
                "\nПрограмма написана для курсовой работы по соответствующей теме." +
                "\nКраткое руководство можно прочитать по вкладке «Помощь».",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Программа решает системы ОДУ 2-4 порядков на выбор." +
                "\nНа выбор даётся 3 метода графического решения систем ОДУ." +
                "\nДля решения необходимо заполнить все текстовые поля." +
                "\nВ противном случае программа выдаст ошибку." +
                "\nНачало отрезка интегрирования должно быть меньше конца." +
                "\nШаг интегрирования может быть любым, но при очень маленьких значениях программа может работать долго. Очень долго.",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
