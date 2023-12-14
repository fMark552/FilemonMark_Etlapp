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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for Felvetel.xaml
	/// </summary>
	public partial class Felvetel : Window
	{
		private FoodService foodService;
		private Food food;
		public Felvetel(FoodService foodService)
		{
			InitializeComponent();
			this.foodService = foodService;
		}

		public Felvetel(FoodService foodService, Food food)
		{
			InitializeComponent();
			this.foodService = foodService;
			this.food = food;
			this.btnadd.Visibility = Visibility.Collapsed;

			tbname.Text = food.Nev;
			cbcat.Text = food.Kategoria;
			tbprice.Text = food.Ar.ToString();
		}

		private void Add_Button(object sender, RoutedEventArgs e)
		{
			try
			{
				Food food = CreateFoodFromInputData();
				if (foodService.Create(food))
				{
					MessageBox.Show("Sikeres hozzáadás");
					tbname.Text = "";
					tbdesc.Text = "";
					tbprice.Text = "";
				}
				else
				{
					MessageBox.Show("Hiba történt a hozzáadás során");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Food CreateFoodFromInputData()
		{
			string nev = tbname.Text.Trim();
			string kategoria = cbcat.Text.Trim();
			string ar = tbprice.Text.Trim();

			if (string.IsNullOrEmpty(nev))
			{
				throw new Exception("Név megadása kötelező");
			}

			if (string.IsNullOrEmpty(ar))
			{
				throw new Exception("Ár megadása kötelező");
			}
			if (!int.TryParse(ar, out int age))
			{
				throw new Exception("Ár csak szám lehet");
			}


			Food food = new Food();
			food.Nev = nev;
			food.Kategoria = kategoria;
			food.Ar = Convert.ToInt32(ar);
			return food;
		}
	}
}
