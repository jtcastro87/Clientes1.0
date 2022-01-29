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
    public partial class Domicilio : Form
    {
        public Domicilio()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string idDir = string.Format("SELECT  TOP 1 idDir FROM Domicilios ORDER BY idDir DESC");
            
            int indice;

            try
            {
                DataSet DSdir = Herramientas.SetConsultas(idDir);

                indice = Convert.ToInt32(DSdir.Tables[0].Rows[0]["idDir"].ToString())+1;            

                string cmd = string.Format("EXEC CreaDomicilios '{0}','{1}','{2}','{3}','{4}' ",indice,tbCalle.Text,tbSector.Text,tbCiudad.Text,Principal.idCliente);

                Herramientas.SetConsultas(cmd);

                MessageBox.Show("Se agrego la direccion","Guardado",MessageBoxButtons.OK,MessageBoxIcon.Information);


            }catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar la direccion " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tbCalle.Text = "";
            tbSector.Text ="";
            tbCiudad.Text = "";
            tbCalle.Focus();

        }
    }
}
