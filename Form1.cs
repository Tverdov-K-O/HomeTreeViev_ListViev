using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTreeViev_ListViev
{
    public partial class Form1 : Form
    {
        List<Product> products; // лист продуктов
        public int sotrColumn { get; set; } = -1; // индек колоны которая сортируется
        public Form1()
        {
            InitializeComponent();
            products = new List<Product>
            {
                new Product("Картошка", 12.70, new DateTime(2019,01,10),    new DateTime(2020,06,11)),
                new Product("Морковка", 33.10, new DateTime(2020,03,4),     new DateTime(2021,11,17)),
                new Product("Лук",      13.80, new DateTime(2021,01,24),    new DateTime(2021,04,30)),
                new Product("Капуста",  12.60, new DateTime(2021,03,07),    new DateTime(2021,05,19))
            };
            UpdateList();
        }
        // создание/обновление списка
        void UpdateList()
        {
            listView1.Items.Clear();
            foreach (var item in products)
            {
                ListViewItem i = new ListViewItem(item._Name);
                i.SubItems.Add(item._Price.ToString());
                i.SubItems.Add(item._DateCreation.ToShortDateString());
                i.SubItems.Add(item._ExpirationDate.ToShortDateString());
                listView1.Items.Add(i);
            }
        }
        // сортировка 
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sotrColumn)
            {
                foreach (var item in listView1.Columns)
                {
                    if ((item as ColumnHeader).Index == e.Column)
                    {
                        (item as ColumnHeader).Text = (item as ColumnHeader).Text.TrimEnd(new char[] { ' ', '↓', '↑' });
                        (item as ColumnHeader).Text += " ↑";
                    }
                    else (item as ColumnHeader).Text = (item as ColumnHeader).Text.TrimEnd(new char[] { ' ', '↓', '↑' });
                }
                sotrColumn = e.Column;
                if (e.Column == 0)      products.Sort();
                else if (e.Column == 1) products.Sort(new ProductPriceComparer());
                else if (e.Column == 2) products.Sort(new DateCreateComparer());
                else if (e.Column == 3) products.Sort(new DateExpirationComparer());
            }
            else
            {
                foreach (var item in listView1.Columns)
                {
                    sotrColumn = -1;
                    if ((item as ColumnHeader).Index == e.Column)
                    {
                        (item as ColumnHeader).Text = (item as ColumnHeader).Text.TrimEnd(new char[] { ' ', '↑', '↓' });
                        (item as ColumnHeader).Text += " ↓";
                    }
                }
                products.Reverse();
            }
            UpdateList();
        }
        // удаление 
        private void dellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if(listView1.Items[i].Selected)
                {
                    listView1.Items.RemoveAt(i);
                    products.RemoveAt(i);
                }
            };
            UpdateList();
        }
        // добавление 
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("Добавление");
            if (form2.ShowDialog() == DialogResult.OK)
            {
                products.Add(new Product(form2._Name, form2._Price, form2._CreateDate, form2._ExpDate));
                UpdateList();
            }
        }
        // редактирование
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2("Редактирование");
            int ind = -1;
            int countSel = 0; 
            for (int i = 0; i < listView1.Items.Count; i++)
            {

                if (listView1.Items[i].Selected)
                {
                    countSel++;
                    form2._Name         = listView1.Items[i].Text;
                    form2._Price        = Convert.ToDouble(listView1.Items[i].SubItems[1].Text);
                    form2._CreateDate   = Convert.ToDateTime(listView1.Items[i].SubItems[2].Text);
                    form2._ExpDate      = Convert.ToDateTime(listView1.Items[i].SubItems[3].Text);
                    ind = i;
                }
            }
            if (ind != -1 && countSel == 1)
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    products[ind]._Name = form2._Name;
                    products[ind]._Price = form2._Price;
                    products[ind]._DateCreation = form2._CreateDate;
                    products[ind]._ExpirationDate = form2._ExpDate;
                    UpdateList();
                }
            }
            else
                MessageBox.Show("Выбрано более 1 элемента!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // сласс продукт
    class Product : IComparable
    {
        
        public string _Name { get; set; }
        public double _Price { get; set; }
        public DateTime _DateCreation { get; set; }
        public DateTime _ExpirationDate { get; set; }
        public Product(string n, double p, DateTime dt1, DateTime dt2)
        {
            _Name           = n;
            _Price          = p;
            _DateCreation   = dt1;
            _ExpirationDate = dt2;
        }

        public int CompareTo(object obj)
        {
            return String.Compare(this._Name, (obj as Product)._Name);
        }

    }

    // сортировка по дате
    class DateCreateComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return DateTime.Compare(x._DateCreation, y._DateCreation);
        }
    }
    // сортировка по дате 2
    class DateExpirationComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return DateTime.Compare(x._ExpirationDate, y._ExpirationDate);
        }
    }
    // сортировка по цене
    class ProductPriceComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            if (x._Price > y._Price) return 1;
            else if (x._Price < y._Price) return -1;
            return 0;
        }
    }
}
