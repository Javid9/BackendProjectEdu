using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Helpers
{
    public class Helper
    {
        public static void DeleteImage(string root, string folder, string fileName)
        {
            string filePath = Path.Combine(root, folder, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

    }
}
