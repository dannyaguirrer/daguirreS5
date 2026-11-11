using System;
using System.Collections.Generic;
using System.Text;

namespace daguirreS5.Utilis
{
    public class FileAccessHelper
    {
        public static string getFilepath(string fileName)
        {
            
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
