using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace practica_14._1
{
    public partial class MainWindow : Window
    {
        private List<Flat> flats;

        public MainWindow()
        {
            InitializeComponent();

            // Добавление данных для пяти квартир
            flats = new List<Flat>()
            {
                new Flat
                {
                    Area = 75.5,
                    Rooms = 3,
                    Options = new List<string> { "кухня", "ванна" },
                    YearBuilt = 2005,
                    MaterialType = "кирпич",
                    Floor = 4,
                    Address = new Address
                    {
                        Country = "Россия",
                        City = "Москва",
                        Street = "Ленина",
                        HouseNumber = "10",
                        ApartmentNumber = "25"
                    }
                },
                new Flat
                {
                    Area = 65,
                    Rooms = 2,
                    Options = new List<string> { "балкон", "туалет" },
                    YearBuilt = 2010,
                    MaterialType = "панель",
                    Floor = 7,
                    Address = new Address
                    {
                        Country = "Россия",
                        City = "Санкт-Петербург",
                        Street = "Пушкина",
                        HouseNumber = "5",
                        ApartmentNumber = "12"
                    }
                },
                new Flat
                {
                    Area = 85,
                    Rooms = 4,
                    Options = new List<string> { "подвал", "балкон" },
                    YearBuilt = 2000,
                    MaterialType = "кирпич",
                    Floor = 2,
                    Address = new Address
                    {
                        Country = "Россия",
                        City = "Екатеринбург",
                        Street = "Кирова",
                        HouseNumber = "15",
                        ApartmentNumber = "3"
                    }
                },
                new Flat
                {
                    Area = 60,
                    Rooms = 2,
                    Options = new List<string> { "ванна", "туалет" },
                    YearBuilt = 2015,
                    MaterialType = "панель",
                    Floor = 5,
                    Address = new Address
                    {
                        Country = "Россия",
                        City = "Новосибирск",
                        Street = "Гагарина",
                        HouseNumber = "20",
                        ApartmentNumber = "7"
                    }
                },
                new Flat
                {
                    Area = 70,
                    Rooms = 3,
                    Options = new List<string> { "кухня", "балкон" },
                    YearBuilt = 2008,
                    MaterialType = "кирпич",
                    Floor = 3,
                    Address = new Address
                    {
                        Country = "Россия",
                        City = "Краснодар",
                        Street = "Лермонтова",
                        HouseNumber = "8",
                        ApartmentNumber = "14"
                    }
                }
            };
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка наличия данных в полях ввода
            if (!string.IsNullOrWhiteSpace(RoomsTextBox.Text) &&
                !string.IsNullOrWhiteSpace(YearTextBox.Text) &&
                !string.IsNullOrWhiteSpace(StreetTextBox.Text) &&
                !string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                int rooms;
                int year;
                if (int.TryParse(RoomsTextBox.Text, out rooms) && int.TryParse(YearTextBox.Text, out year))
                {
                    string district = StreetTextBox.Text;
                    string city = CityTextBox.Text;

                    var matchingFlats = flats.Where(flat =>
                        flat.Rooms == rooms &&
                        flat.YearBuilt == year &&
                        flat.Address != null && flat.Address.District == district &&
                        flat.Address.City == city
                    ).ToList();

                    // Отображение результатов поиска
                    SearchResultsListBox.ItemsSource = matchingFlats;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректные значения для комнат и года.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (SortComboBox.SelectedValue != null)
            {
                string sortBy = SortComboBox.SelectedValue.ToString();
                bool ascending = AscendingCheckBox.IsChecked ?? false;

                if (sortBy == "Area")
                {
                    flats = ascending ? flats.OrderBy(flat => flat.Area).ToList() : flats.OrderByDescending(flat => flat.Area).ToList();
                }
                else if (sortBy == "Price")
                {
                    flats = ascending ? flats.OrderBy(flat => flat.Price).ToList() : flats.OrderByDescending(flat => flat.Price).ToList();
                }

                // Отображение отсортированных квартир
                SortResultsListBox.ItemsSource = flats;
            }
        }
    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }

    public class Flat
    {
        public double Area { get; set; }
        public int Rooms { get; set; }
        public List<string> Options { get; set; }
        public int YearBuilt { get; set; }
        public string MaterialType { get; set; }
        public int Floor { get; set; }
        public decimal Price { get; set; }
        public Address Address { get; set; }
    }
}
