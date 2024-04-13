    using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using PC1;
using System.Linq;


namespace PC1
{
    public partial class MainWindow : Window
    {
        private DataTable clientesTable;

        public MainWindow()
        {
            InitializeComponent();
            clientesTable = new DataTable();
        }

        private void ButtonListarClientes_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-32\\SQLEXPRESS; Initial Catalog=Neptuno;User Id=user01; Password=123456;";
            string uspListarClientes = "USP_ListarClientes";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(uspListarClientes, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(clientesTable);
                }

                dgClientes.ItemsSource = clientesTable.AsEnumerable().Take(5).CopyToDataTable().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void ButtonRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente registrarCliente = new RegistrarCliente();
            registrarCliente.ShowDialog();
        }

        private void ButtonActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dgClientes.SelectedItem;
            string idCliente = selectedRow["idCliente"].ToString();
            ActualizarCliente actualizarClienteWindow = new ActualizarCliente(idCliente);
            actualizarClienteWindow.ShowDialog();
        }




        private void ButtonEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            
                DataRowView selectedRow = (DataRowView)dgClientes.SelectedItem;
                string clienteId = selectedRow["idCliente"].ToString();

                string connectionString = "Data Source=LAB1504-32\\SQLEXPRESS; Initial Catalog=Neptuno;User Id=user01; Password=123456;";
                string uspEliminarCliente = "USP_EliminarCliente";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(uspEliminarCliente, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idCliente", clienteId);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Cliente eliminado correctamente.");
                    ButtonListarClientes_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
           
        }

    }

