using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CAR_AMI_LIB
{
    public class Print
    {
        public bool print(string value)
        {
            string line = value;
            File.WriteAllText(@"C:\Users\pledezma\Documents\escri\record.txt", line);
            //File.WriteAllText(@"C:\Users\Administrator\Desktop\print\record.txt", line);
            return true;
        }
    }
}
