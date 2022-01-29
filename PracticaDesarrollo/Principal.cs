using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriasP;

namespace PracticaDesarrollo
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        //Este metodo se utiliza para llamar la tabla clientes y llenar el DataGrid 
        public DataSet LlenaDGV(string tabla) 
        {
            string cmd = "SELECT * FROM Clientes ";
            DataSet ds = Herramientas.SetConsultas(cmd);

            return ds;
        }

        //En el evento Load de la ventana principal se llama el metodo LLenaDGV 
        private void Principal_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LlenaDGV("Clientes").Tables[0];
       
        }

        //Cuando se preciona este boton muestra la ventana de detalle de los clientes
        public static string idCliente;
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            idCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString().Trim();           

            InfoClientes ic = new InfoClientes();

            ic.ShowDialog();

            dataGridView1.DataSource = LlenaDGV("Clientes").Tables[0];
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Con este metodo se llama la ventana de creacion de clientes
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CreaClientes cc = new CreaClientes();
            cc.ShowDialog();
            dataGridView1.DataSource = LlenaDGV("Clientes").Tables[0];

        }

        //metodo para borrar los clientes y sus direcciones desde la ventana principal
        private void btnBorrar_Click(object sender, EventArgs e)
        {    
            //Se le pregunta al usuario si desea realmente eliminar el Cliente, de ser positivo se proceder a la eliminacion de los registros
            DialogResult Respuesta = MessageBox.Show("Realmente desea eliminar este Cliente ", "Eliminado Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try
            {
                if (Respuesta == DialogResult.Yes)
                {
                    //Como hay dos tablas (Clientes y Domicilios), realice dos sentencias para eliminar los registros de clientes y una para las direcciones
                    //En la tabla domicilios esta referenciado el cliente mediante su id de cliente
                    string cmdcliente = string.Format("DELETE FROM Clientes WHERE idCliente='{0}'", dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    Herramientas.SetConsultas(cmdcliente);

                    string cmddir = string.Format("DELETE FROM Domicilios WHERE idCliente='{0}'", dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    Herramientas.SetConsultas(cmddir);

                    MessageBox.Show("Cliente Eliminado!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView1.DataSource = LlenaDGV("Clientes").Tables[0];

                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de eliminarar el cliente " + ex.Message, "Error en la operacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
    }

