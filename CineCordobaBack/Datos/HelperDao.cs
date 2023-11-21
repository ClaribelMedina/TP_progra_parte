using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CineCordobaBack.Datos
{
    public class HelperDao
    {
        private static HelperDao instancia;
        private SqlConnection cnn;

        private HelperDao()
        {
            cnn= new SqlConnection("Data Source=CLARI\\SQLEXPRESS01;Initial Catalog=Cordoba_Cine_GRUPO_N9;Integrated Security=True");
            //cnn = new SqlConnection("Data Source=Tomas;Initial Catalog=Cordoba_Cine_GRUPO_N9;Integrated Security=True");
        }

        public static HelperDao ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDao();

            }
            return instancia;
        }

        public DataTable Consultar(string nombreSP)
        {
            cnn.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            cnn.Close();
            return tabla;
        }



        public DataTable ConsultaTabla(string spNombre, List<Parametro> listaParámetros)  //metodo para sp con parametros de entrada
        {
            DataTable dt = new DataTable();
            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (listaParámetros != null)
            {
                foreach (Parametro oParametro in listaParámetros)
                {
                    cmd.Parameters.AddWithValue(oParametro.Nombre, oParametro.Valor);
                }
            }
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }

        public int ConsultaEscalar(string spNombre, string pOutNombre)  //para prox numero de comprobante por ej, sp con parametros de salida
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = pOutNombre;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public int ConsultaEscalarConParametros(string spNombre, string pOutNombre, List<Parametro> listaParámetros)
        {
            if (cnn != null && cnn.State == ConnectionState.Open)    //este metodo sirve para consultar sp con p de entrada y p salida
                cnn.Close();
            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (listaParámetros != null)
            {
                foreach (Parametro oParametro in listaParámetros)
                {
                    cmd.Parameters.AddWithValue(oParametro.Nombre, oParametro.Valor);
                }
            }

            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = pOutNombre;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public int EjecutarSp(string spNombre, List<Parametro> listaParámetros)
        {
            int filasAfectadas = 0;
            SqlTransaction t = null;

            if (cnn != null && cnn.State == ConnectionState.Open)
                cnn.Close();                                             //esto esta bien????


            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(spNombre, cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                if (listaParámetros != null)
                {
                    foreach (Parametro oParametro in listaParámetros)
                    {
                        cmd.Parameters.AddWithValue(oParametro.Nombre, oParametro.Valor);
                    }
                }
                filasAfectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                { t.Rollback(); }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }
            return filasAfectadas;
        }

        public SqlConnection ObtenerConexion()
        {
            return this.cnn;
        }

    }
}
