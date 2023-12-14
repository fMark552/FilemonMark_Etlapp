﻿using System;
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

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		FoodService foodService;
		public MainWindow(FoodService foodService)
		{
			InitializeComponent();
			this.foodService = foodService;
			Read();
		}

		private void Read()
		{
			foodTable.ItemsSource = foodService.GetAll();
		}

		private void felvetel_Click(object sender, RoutedEventArgs e)
		{
			Felvetel form = new Felvetel(foodService);
			form.Closed += (_, _) =>
			{
				Read();
			};
			form.ShowDialog();
		}

		private void torol_Click(object sender, RoutedEventArgs e)
		{
			Food selected = foodTable.SelectedItem as Food;
			if (selected == null)
			{
				MessageBox.Show("A törléshez válassz ételt!");
				return;
			}
			MessageBoxResult selectedButton =
				MessageBox.Show($"Biztos, hogy törölöd az alábbi ételt: {selected.Nev}?",
					"Tuti?", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if (foodService.Delete(selected.Id))
				{
					MessageBox.Show("Siker!");
				}
				else
				{
					MessageBox.Show("Hiba!");
				}
				Read();
			}
		}
	}
}
