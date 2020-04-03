using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SqlClient;
namespace CAR_AMI_LIB
{
    public class Print
    {
        public SqlConnection conectar = new SqlConnection();
        public bool print(string value, string d)
        {
             
        string line = value;
            if (d == null)
            {

            }
            else if (d == "lectura")
            {
                File.WriteAllText(@"C:\Users\TI\Documents\AMI\lectura.txt", line);
            }
            else if (d == "token")
            {
                File.WriteAllText(@"C:\Users\TI\Documents\AMI\token.txt", line);
            }
            model.cnx cnx = new model.cnx();
            if (cnx.abrir())
            {
                string hola = "abireto";
            }
            //File.WriteAllText(@"C:\Users\Administrator\Desktop\print\record.txt", line);
            return true;
        }
    }
}
