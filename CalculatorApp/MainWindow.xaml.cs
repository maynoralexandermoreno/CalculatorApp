using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] storedValues = new int[2];
        private readonly int LHS_VALUE = 0;
        private readonly int RHS_VALUE = 1;
        private readonly int NUMBER_BASE = 10;

        private bool isRHSInitiated = false;
        private string displayString = "";
        private char operation = ' ';
        private int INDEX = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickNum(object sender, RoutedEventArgs e)
        {
            string s = (sender as Button).Content.ToString();
            int buttonValue = -1;
            int.TryParse(s, out buttonValue);
            if(buttonValue >= 0)
            {
                if(INDEX == RHS_VALUE)
                {
                    isRHSInitiated = true;
                }
                
                storedValues[INDEX] = storedValues[INDEX] * NUMBER_BASE + buttonValue;
                if (displayString.Equals("0"))
                {
                    displayString = s;
                }
                else
                {
                    displayString += s;
                }
                DisplayBox.Text = displayString;
            }
            else
            {
                displayString = "";
                storedValues[RHS_VALUE] = 0;
                storedValues[LHS_VALUE] = 0;
                INDEX = LHS_VALUE;
                isRHSInitiated = false;
                DisplayBox.Text = "ERROR";
            }
        }

        private void ClickDiv(object sender, RoutedEventArgs e)
        {
            INDEX = RHS_VALUE;
            operation = '/';
            displayString += "/";
            DisplayBox.Text = displayString;

        }

        private void ClickMult(object sender, RoutedEventArgs e)
        {
            INDEX = RHS_VALUE;
            operation = '*';
            displayString += "*";
            DisplayBox.Text = displayString;
        }

        private void ClickSub(object sender, RoutedEventArgs e)
        {
            INDEX = RHS_VALUE;
            operation = '-';
            displayString += "-";
            DisplayBox.Text = displayString;
        }

        private void ClickPlus(object sender, RoutedEventArgs e)
        {
            INDEX = RHS_VALUE;
            operation = '+';
            displayString += "+";
            DisplayBox.Text = displayString;
        }

        private void ClickCalculate(object sender, RoutedEventArgs e)
        {
            if(isRHSInitiated == false)
            {
                if(operation == ' ')
                {
                    DisplayBox.Text = displayString;
                }
                else
                {
                    displayString = "";
                    storedValues[RHS_VALUE] = 0;
                    storedValues[LHS_VALUE] = 0;
                    INDEX = LHS_VALUE;
                    isRHSInitiated = false;
                    DisplayBox.Text = "ERROR";
                    return;
                }
            }

            switch (operation)
            {
                case '*':
                    {
                        storedValues[LHS_VALUE] = storedValues[LHS_VALUE] * storedValues[RHS_VALUE];
                        break;
                    }
                case '/':
                    {
                        if(storedValues[RHS_VALUE] == 0)
                        {
                            displayString = "";
                            storedValues[RHS_VALUE] = 0;
                            storedValues[LHS_VALUE] = 0;
                            INDEX = LHS_VALUE;
                            isRHSInitiated = false;
                            DisplayBox.Text = "ERROR";
                            return;
                        }
                        storedValues[LHS_VALUE] = storedValues[LHS_VALUE] / storedValues[RHS_VALUE];
                        break;
                    }
                case '+':
                    {
                        storedValues[LHS_VALUE] = storedValues[LHS_VALUE] + storedValues[RHS_VALUE];
                        break;
                    }
                case '-':
                    {
                        storedValues[LHS_VALUE] = storedValues[LHS_VALUE] - storedValues[RHS_VALUE];
                        break;
                    }

            }

            displayString = storedValues[LHS_VALUE].ToString();
            DisplayBox.Text = displayString;
            storedValues[RHS_VALUE] = 0;
            INDEX = LHS_VALUE;
            isRHSInitiated = false;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            switch (displayString[displayString.Length - 1])
            {
                case '*':
                case '/':
                case '+':
                case '-':
                    {
                        INDEX = LHS_VALUE;
                        isRHSInitiated = false;
                        storedValues[RHS_VALUE] = 0;
                        operation = ' ';
                        break;
                    }
                default:
                    {
                        storedValues[INDEX] = storedValues[INDEX] / NUMBER_BASE;
                        break;
                    }

            }
            displayString = displayString.Substring(0, displayString.Length - 1);
            DisplayBox.Text = displayString;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            storedValues[LHS_VALUE] = 0;
            storedValues[RHS_VALUE] = 0;
            INDEX = LHS_VALUE;
            isRHSInitiated = false;
            operation = ' ';
            displayString = storedValues[LHS_VALUE].ToString();
            DisplayBox.Text = displayString;
        }
    }
}
