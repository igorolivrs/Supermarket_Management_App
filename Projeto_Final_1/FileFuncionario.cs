using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Projeto_Final_1
{
    public class FileFuncionario:MainFile<Funcionario>
    {
        private List<Funcionario> Funcionarios;
        public FileFuncionario()
        {
            FileName = "users.txt";
            Update();
        }
        private void Update()
        {
            Funcionarios = GetFromFile();
        }

        public void Delete(string user)
        {
            Funcionario funcionario = null;
            foreach (var item in Funcionarios)
            {
                if (item.user == user)
                {
                    funcionario = item;
                }
            }

            if (funcionario == null)
            {
                throw new Exception("Funcionário não existe");
            }
            else
            {
                Funcionarios.Remove(funcionario);
            }
            SaveToFile(Funcionarios);
            Update();
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            Funcionarios.Add(funcionario);
            SaveToFile(Funcionarios);
            Update();
        }

        public Funcionario Login(string user, string password)
        {
            var x = Funcionarios.FirstOrDefault(p => p.user == user && p.password == password);
            if (x == null)
            {
                throw new Exception("Login inválido!");
            }
            else
            {
                return x;
            }
        }

        public bool FuncionarioExists(string user)
        {
            var x = Funcionarios.FirstOrDefault(p => p.user == user);
            return x != null;
        }

        public List<Funcionario> GetFuncionariosToMain()
        {
            return Funcionarios;
        }
    }
}
