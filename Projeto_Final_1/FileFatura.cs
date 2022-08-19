using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Projeto_Final_1
{
    class FileFatura : MainFile<Fatura>
    {
        private List<Fatura> Fatura;
        public FileFatura()
        {
            FileName = "fatura.txt";
            Update();
        }

        public void GerarFatura(Fatura fatura) // Gerar uma Fatura
        {
            Fatura.Add(fatura);
            SaveToFile(Fatura);
            Update();
        }

        private void Update() // Atualizar o ficheiro
        {
            Fatura = GetFromFile();
        }

        public List<Fatura> GetFaturaToMain()
        {
            return Fatura;
        }

    }
}
