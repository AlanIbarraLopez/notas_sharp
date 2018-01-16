using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 author: Alan Ibarra
*/

namespace app_notas
{
    class Data
    {
        private string config;
        public Data()
        {
            //config = "server = DESKTOP-KFHSANA ; database = NOTAS_DB ; integrated security = true"; // conexion con sql server 
            config = "server = (localdb)\\MSSQLLocalDB ; database = NOTAS_DB ; integrated security = true"; //conexion con localdb

        }

        public SqlConnection conexion()
        {
            SqlConnection conexion = new SqlConnection(config);
            try
            {
                conexion.Open();
                Console.WriteLine("Conectado");
             
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al conectar");
            }

            return conexion;

        }//conexion

        public Boolean insertarNota(string titulo,string contenido)
        {
            Boolean res = true;
            try
            {
                SqlConnection con= conexion(); //obtenemos conexion a la db
                string sentencia = "insert into notas(titulo,contenido) values('"+titulo+"','"+contenido+"')";
                SqlCommand comando = new SqlCommand(sentencia,con);
                int col = comando.ExecuteNonQuery();
                Console.WriteLine("Insert "+col);
                con.Close();
            }catch(SqlException e)
            {
                res = false;
                Console.WriteLine("Error al guardar"+e.Message);
            }

            return res;
        }//insertar

        public List<Nota> getNotas()
        {
            List<Nota> notas = new List<Nota>(); //una lista de notas

            try
            {
                SqlConnection con = conexion();
                string query = "select * from notas";
                SqlCommand comando = new SqlCommand(query, con); //creamos el comando
                SqlDataReader registros = comando.ExecuteReader(); // guardar registros de la consulta en un reader
                while (registros.Read())
                {
                    //obtenemos los registros
                    var id = int.Parse(registros["id_nota"].ToString());
                    var titulo = registros["titulo"].ToString();
                    var contenido = registros["contenido"].ToString();
                    //los guardamos en la lista
                    notas.Add(new Nota(id,titulo,contenido)); //le pasamos un objeto nota
                }

                con.Close();

            }catch(SqlException ex)
            {
                Console.WriteLine("Error al actualizar "+ex.Message);
            }

            return notas;

        }//getNotas 

        public Boolean updateNota(int id,string titulo,string contenido)
        {
            Boolean res = false;
            try
            {
                SqlConnection con = conexion();
                string query = "update notas set titulo = '"+titulo+"', contenido = '"+contenido+"' where id_nota  = "+id;
                SqlCommand comando = new SqlCommand(query,con);
                int check = comando.ExecuteNonQuery();
                if(check == 1)
                {
                    res = true;
                }

                con.Close();    


            }catch(SqlException ex)
            {
                Console.WriteLine("Error al actualizar "+ex.Message);
            }

            return res;
        }//update 

        public Boolean deleteNota(int id)
        {
            Boolean res = false;
            try
            {
                SqlConnection con = conexion();
                string query = "delete from notas where id_nota = "+id+";";
                SqlCommand comando = new SqlCommand(query,con);
                int answer = comando.ExecuteNonQuery();
                if(answer > 0)
                {
                    res = true;
                }

                con.Close();
            }catch(SqlException ex)
            {
                Console.WriteLine("Error al eliminar "+ex.Message);
            }
            return res;
        }//delete
       
    }//class

}//namespace
