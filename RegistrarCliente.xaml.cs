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
using System.Data.SqlClient;


namespace PC1
{
    public partial class RegistrarCliente : Window
    {
        public RegistrarCliente()
        {
            InitializeComponent();
        }

        private void ButtonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string idCliente = txtIdCliente.Text;
            string nombreCompañia = txtNombreCompañia.Text;
            string nombreContacto = txtNombreContacto.Text;
            string cargoContacto = txtCargoContacto.Text;
            string direccion = txtDireccion.Text;
            string ciudad = txtCiudad.Text;
            string region = txtRegion.Text;
            string codPostal = txtCodPostal.Text;
            string pais = txtPais.Text;
            string telefono = txtTelefono.Text;
            string fax = txtFax.Text;

            string connectionString = "Data Source=LAB1504-32\\SQLEXPRESS; Initial Catalog=Neptuno;User Id=user01; Password=123456;";
            string uspInsertarCliente = "USP_InsertarCliente";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(uspInsertarCliente, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@NombreCompañia", nombreCompañia);
                    command.Parameters.AddWithValue("@NombreContacto", nombreContacto);
                    command.Parameters.AddWithValue("@CargoContacto", cargoContacto);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Ciudad", ciudad);
                    command.Parameters.AddWithValue("@Region", region);
                    command.Parameters.AddWithValue("@CodPostal", codPostal);
                    command.Parameters.AddWithValue("@Pais", pais);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Fax", fax);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cliente registrado correctamente.");
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

