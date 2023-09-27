using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace front_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string BaseUrl = "https://localhost:7188/agenda";
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            _httpClient = new HttpClient();

            InitializeComponent();
            LoadDataFromApi();

        }

        private async void LoadDataFromApi()
        {

            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                DataGridDisplay.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                MessageBox.Show("Error na comunicação com API");
            }
        }

        private async void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {

            var newRecord = new
            {
                nome = TextBoxNome.Text,
                sobrenome = TextBoxSobrenome.Text,
                telefone = TextBoxTelefone.Text
            };

            var json = JsonConvert.SerializeObject(newRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Contato Adicionado com Sucesso");
                LoadDataFromApi();
            }
            else
            {
                MessageBox.Show("Error ao adicionar contato");
            }
        }

        private async void AtualizarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(TextBoxId.Text, out int id))
            {
                MessageBox.Show("ID invalido.");
                return;
            }

            var updatedRecord = new
            {
                id = TextBoxId.Text,
                nome = TextBoxNome.Text,
                sobrenome = TextBoxSobrenome.Text,
                telefone = TextBoxTelefone.Text
            };

            {
                var json = JsonConvert.SerializeObject(updatedRecord);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{BaseUrl}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Contato atualizado com sucesso");
                    LoadDataFromApi();
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar contato");
                }
            }

        }

        private async void ExcluirButton_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(TextBoxId.Text, out int id))
            {
                MessageBox.Show("Id invalido.");
                return;
            }

            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Contato deletado com sucesso");
                    LoadDataFromApi();
                }
                else
                {
                    MessageBox.Show("Error ao deletar contato");
                }
            }
        }
    }
}


