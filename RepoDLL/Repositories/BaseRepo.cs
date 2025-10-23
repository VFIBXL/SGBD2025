using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepo
    {
        private Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
        protected string GetFileFromAssemblyAsync(string fileName)
        {
            var assemblyFiles =
                System
                .Reflection
                .Assembly
                .GetExecutingAssembly()
                .GetManifestResourceNames();

            var assemblyFile = assemblyFiles.FirstOrDefault(f => f.EndsWith(fileName));

            if (assemblyFile == null)
            {
                throw new Exception($"File {fileName} not found in assembly");
            }
            else
            {
                string content = new System.IO.StreamReader(_assembly.GetManifestResourceStream(assemblyFile) ?? throw new Exception("Erreur dans la récupération du fichier " + fileName)).ReadToEnd();
                return content;
            }
        }
    }
}
