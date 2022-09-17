
using System.Data.SqlClient;
using System;
using System.Data;
using System.Configuration;

namespace Datos
{
    public static class Conexiones
    {
        public static DataSet EjecutaQuerySQL(string cnn, string sql)
        {

            SqlConnection conn = new SqlConnection(cnn); // C#
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sqlDa = new SqlDataAdapter();

                sqlDa.SelectCommand = cmd;

                DataSet DsRetorno = new DataSet();

                sqlDa.Fill(DsRetorno);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return DsRetorno;
            }
            catch (Exception ex)

            {
                return null;
            }
        }

        public static DataSet EjecutaSPSQL(string cnn, string sp)
        {

            SqlConnection conn = new SqlConnection(cnn); // C#
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDa = new SqlDataAdapter();

                sqlDa.SelectCommand = cmd;

                DataSet DsRetorno = new DataSet();

                sqlDa.Fill(DsRetorno);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return DsRetorno;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        internal static DataSet EjecutaSPSQL(object cnn, string v, SqlParameter[] parameter)
        {
            throw new NotImplementedException();
        }

        public static DataSet EjecutaSPSQL(string cnn, string sp, SqlParameter[] sqlParameters)
        {

            SqlConnection conn = new SqlConnection(cnn); // C#
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(sqlParameters);

                SqlDataAdapter sqlDa = new SqlDataAdapter();

                sqlDa.SelectCommand = cmd;

                DataSet DsRetorno = new DataSet();

                sqlDa.Fill(DsRetorno);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return DsRetorno;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
