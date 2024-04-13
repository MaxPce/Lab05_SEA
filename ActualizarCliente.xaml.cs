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
using System.Data;

namespace PC1
{
    public partial class ActualizarCliente : Window
    {
        private string idCliente;

        public ActualizarCliente(String clienteId)
        {
            InitializeComponent();
            idCliente = clienteId;
            CargarDatosCliente();
        }

        private void CargarDatosCliente()
        {
            string connectionString = "Data Source=LAB1504-32\\SQLEXPRESS; Initial Catalog=Neptuno;User Id=user01; Password=123456;";
            string query = "SELECT * FROM clientes WHERE idCliente = @idCliente";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombreCompañia.Text = reader["NombreCompañia"].ToString();
                        txtNombreContacto.Text = reader["NombreContacto"].ToString();
                        txtCargoContacto.Text = reader["CargoContacto"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();
                        txtCiudad.Text = reader["Ciudad"].ToString();
                        txtRegion.Text = reader["Region"].ToString();
                        txtCodPostal.Text = reader["CodPostal"].ToString();
                        txtPais.Text = reader["Pais"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();
                        txtFax.Text = reader["Fax"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtonActualizar_Click(object sender, RoutedEventArgs e)
        {
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
            string uspActualizarCliente = "USP_ActualizarCliente";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(uspActualizarCliente, connection);
                    command.CommandType = CommandType.StoredProcedure;

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
                MessageBox.Show("Cliente actualizado correctamente.");
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

