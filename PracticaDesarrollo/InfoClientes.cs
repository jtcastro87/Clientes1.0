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
    public partial class InfoClientes : Form
    {
        public InfoClientes()
        {
            InitializeComponent();
        }

        public DataSet LlenaDGVDirecciones(string tabla)
        {
            string cmd = String.Format("SELECT * FROM "+tabla+" WHERE idCliente='{0}' ",Principal.idCliente);
            DataSet ds = Herramientas.SetConsultas(cmd);

            return ds;
        }
      /*   private void  Llenar()
        {
            string cmd = String.Format("SELECT * FROM Domicilios WHERE idCliente='{0}' ", Principal.idCliente);
            DataSet ds = Herramientas.SetConsultas(cmd);

            dgvCliente dg = 
        }*/




        private void InfoClientes_Load(object sender, EventArgs e)
        {  

            try
            {
                string cmd = string.Format("SELECT * FROM Clientes WHERE idCliente='{0}'", Principal.idCliente);

                DataSet DS = Herramientas.SetConsultas(cmd);

                tbNombre.Text = DS.Tables[0].Rows[0]["Nombre"].ToString().Trim();

                tbApellido.Text = DS.Tables[0].Rows[0]["Apellido"].ToString().Trim();

                tbCedula.Text = DS.Tables[0].Rows[0]["Cedula"].ToString().Trim();

                tbTelefono.Text = DS.Tables[0].Rows[0]["Telefono"].ToString().Trim();

                dgvCliente.DataSource = LlenaDGVDirecciones("Domicilios").Tables[0];

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: "+ ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            tbNombre.Enabled = true;
            tbApellido.Enabled = true;
            tbCedula.Enabled = true;
            tbTelefono.Enabled = true;
            btnEditar.Enabled = false;
        }

        private void InfoClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnActualizar.Enabled = true;
        }

        private void tbApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnActualizar.Enabled = true;
        }

        private void tbCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnActualizar.Enabled = true;
        }

        private void tbTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnActualizar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string cmd = string.Format("EXEC ActualizaClientes '{0}','{1}','{2}','{3}','{4}' ", Principal.idCliente,tbNombre.Text.Trim(),tbApellido.Text.Trim(),tbCedula.Text.Trim(),tbTelefono.Text.Trim() );
          
            try
            {
                Herramientas.SetConsultas(cmd);

                MessageBox.Show("Datos Actualizados!","Guardando",MessageBoxButtons.OK,MessageBoxIcon.Information);

                tbNombre.Enabled = false;
                tbApellido.Enabled = false;
                tbCedula.Enabled = false;
                tbTelefono.Enabled = false;
                btnEditar.Enabled = true;
                btnActualizar.Enabled = false;

            }catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos "+ex.Message,"Error en la operacion",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        public static string idDir;
        private void btnEditarDir_Click(object sender, EventArgs e)
        {
            // idDir = dgvCliente.SelectedRows[dgvCliente.CurrentRow.Index].Cells[0].Value.ToString().Trim();
            idDir = dgvCliente.CurrentRow.Cells[0].Value.ToString();

            ActualizaDom ad = new ActualizaDom();

            ad.ShowDialog();


            dgvCliente.DataSource = LlenaDGVDirecciones("Domicilios").Tables[0];
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            Domicilio d = new Domicilio();

            d.ShowDialog();

            dgvCliente.DataSource = LlenaDGVDirecciones("Domicilios").Tables[0];
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tbNombre.Enabled = false;
            tbApellido.Enabled = false;
            tbCedula.Enabled = false;
            tbTelefono.Enabled = false;
            btnEditar.Enabled = true;
            btnActualizar.Enabled = false;
        }


        private void btnBorrar_Click(object sender, EventArgs e)
        {
            idDir = dgvCliente.CurrentRow.Cells[0].Value.ToString();

            try
            {
                DialogResult Respuesta = MessageBox.Show("Realmente desea eliminar este domicilio? ","Eliminado domicilio",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                
                if(Respuesta == DialogResult.Yes)
                {
                    string cmd = string.Format("EXEC EliminaDomicilios '{0}' ", idDir);

                    Herramientas.SetConsultas(cmd);

                    MessageBox.Show("Direccion Eliminada!", "Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvCliente.DataSource = LlenaDGVDirecciones("Domicilios").Tables[0];
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminarar los domicilio " + ex.Message, "Error en la operacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
  
}

