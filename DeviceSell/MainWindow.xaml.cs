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
using System.ComponentModel;
using MySql.Data.MySqlClient;
namespace DeviceSell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class Category
    {

        public int id { get; set; }
        public string name { get; set; }
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    class CategoryRepository
    {
        private BindingList<Category> categoriesList;
        public string connectionString = "server=localhost; user=root; password=Password; database=devices; ";
        public CategoryRepository()
        {
            categoriesList = new BindingList<Category>();
        }
        public void fetchCategories(DataGrid dataGrid, string query)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categoriesList.Add(new Category(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"])));
                }

                connection.Close();
                dataGrid.ItemsSource = categoriesList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public partial class MainWindow : Window
    {
        CategoryRepository categoryRepository;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "SELECT id,name FROM categories;";
            categoryRepository = new CategoryRepository();

            categoryRepository.fetchCategories(CategoriesGrid, query);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategoryWindow = new AddCategory();
            addCategoryWindow.Show();
            this.Close();
        }
    }
}
