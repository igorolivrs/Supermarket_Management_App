using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Final_1
{
    [Serializable]
    public class Funcionario
    {
        public string user;
        public string password;
        public int cargo;
        public string CargoDescription
        {
            get
            {
                var x = "";
                switch (cargo)
                {
                    case 1:
                        x = "Gerente";
                        break;
                    case 2:
                        x = "Repositor";
                        break;
                    case 3:
                        x = "Caixa";
                        break;

                    default:
                        x = "Não defenido";
                        break;
                }

                return x;
            }
        }
        public Funcionario()
        {

        }
        public Funcionario(string user, string password, int cargo)
        {
            this.user = user;
            this.password = password;
            this.cargo = cargo;
        }

        public override string ToString()
        {
            return "--------------------------------------------------------------------------------\n" +
                   "Nome: " + user + " | Password: " + password + " | Cargo: " + CargoDescription + 
                   "\n--------------------------------------------------------------------------------";
        }

    }
}
