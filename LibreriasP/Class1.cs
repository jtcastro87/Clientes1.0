using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//Esto me sierve para la conexion con la base de datos

namespace LibreriasP
{
    public class Herramientas
    {
        private static DataSet DS;
        private static SqlDataAdapter DA;
        private static SqlConnection Conexion;
        public static DataSet SetConsultas(String cmd)
        {
            Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conUnica"].ConnectionString);

            try
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                }
                else
                {
                    Conexion.Open();

                }
                DS = new DataSet(cmd);

                DA = new SqlDataAdapter(cmd, Conexion);

                DA.Fill(DS);

                Conexion.Close();

                return DS;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
