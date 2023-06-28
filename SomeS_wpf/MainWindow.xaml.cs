using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SomeS_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> arr = new List<int>();
        private MyStack<int> myIntStack = new MyStack<int>();
        private MyList<double> doubles = new MyList<double>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /////////////  Лабораторная работа #6  /////////////

        private void txtArrayInt(object sender, KeyEventArgs e)
        {
            string text = (sender as TextBox).Text;

            if (e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                e.Key >= Key.D0 && e.Key <= Key.D9)
                return;

            else if (e.Key == Key.OemMinus)
            {
                if (text == "") return;
                else if (text[text.Length - 1] == ',' || text[text.Length - 1] == ' ') return;
            }
            else if (text != "")
                if (text[text.Length - 1] >= '0' && text[text.Length - 1] <= '9' && e.Key == Key.OemComma ||
                    (text[text.Length - 1] >= '0' && text[text.Length - 1] <= '9' || text[text.Length - 1] == ',') && 
                    e.Key == Key.Space)
                {
                    if(e.Key == Key.OemComma) return;
                    else if(e.Key == Key.Space || (e.Key == Key.Space && text[text.Length - 1] == ',')) return;
                }

            e.Handled = true;
        }

        private void CreateIntArray(object sender, RoutedEventArgs e)
        {
            Clear(sender, e);
            try
            {
                string str = txtArray.Text;
                string[] strs = str.Split(',', ' ');

                foreach (string i in strs)
                {
                    if (i != "" && int.Parse(i)%2 != 0)
                    {
                        arr.Add(int.Parse(i));
                        txbArray.Text += i + "; ";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnFindMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int res = arr.Where(x=>x>=0).Sum();
                txbResult.Text = "Сумма положительных элементов: " + res;
            }
            catch
            {
                MessageBox.Show("Массив пуст", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            txbArray.Text = null;
            txbResult.Text = null;
            arr.Clear();
        }

        /////////////  Лабораторная работа #7  /////////////

        private void btnPush_Click(object sender, RoutedEventArgs e)
        {
            if(txtInputInt.Text != "") myIntStack.Push(int.Parse(txtInputInt.Text));
            txtInputInt.Clear();
            lbStack_Update();
        }

        private void btnPop_Click(object sender, RoutedEventArgs e)
        {
            if (!myIntStack.IsEmpty) myIntStack.Pop();
            lbStack_Update();
        }

        private void lbStack_Update()
        {
            lbStack.Items.Clear();
            foreach(int item in myIntStack) lbStack.Items.Add(item);
        }

        private void btnSum_Click(object sender, RoutedEventArgs e)
        {
            if (!myIntStack.IsEmpty) txtRes.Text = myIntStack.Sum().ToString();
            else MessageBox.Show("Складывать нечего", "Список пуст", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /////////////  Лабораторная работа #8  /////////////

        private void btnAddDouble_Click(object sender, RoutedEventArgs e)
        {
            if (txtInputDouble.Text != "") doubles.Add(double.Parse(txtInputDouble.Text));
            txtInputDouble.Clear();
            lbDoubleList_Update();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!doubles.IsEmpty && lbList.SelectedItem != null) 
                doubles.Remove(double.Parse(lbList.SelectedItem.ToString()!));
            lbDoubleList_Update();
        }

        private void btnLess_Click(object sender, RoutedEventArgs e)
        {
            double temp = doubles.MinBy(x=>x%5);
            doubles.Remove(temp);
            lbDoubleList_Update();
        }

        private void lbDoubleList_Update()
        {
            lbList.Items.Clear();
            foreach (double item in doubles) lbList.Items.Add(item);
        }
    }
}
