using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Final_1
{
    [Serializable]
    public class Stock
    {
        public string categoria;
        public string nome;
        public int quantidade;
        public double preco;
        public bool IsDeleted { get; set; }

        public int ProductID { get; set; }
        public string CatDescription
        {
            get
            {
                var x = "";
                switch (categoria)
                {
                    case "1":
                        x = "Congelados";
                        break;
                    case "2":
                        x = "Prateleira";
                        break;
                    case "3":
                        x = "Enlatados";
                        break;
                    default:
                        x = "Não defenido";
                        break;
                }
                return x;
            }
        }
        public Stock()
        {

        }
        public Stock(string categoria, string nome,int quantidade, double preco,int id)
        {
            this.ProductID = id;
            this.categoria = categoria;
            this.nome = nome;
            this.quantidade = quantidade;
            this.preco = preco;
        }

        public override string ToString()
        {
            return "----------------------------------------------------------------------------------------------------\n" + 
                    "ID: " +ProductID + " | Categoria: " + CatDescription + " | Produto: " + nome + " | Quantidade: " + quantidade + " unidades " + "  | Preço: " + preco + " Euros" +
                    "\n----------------------------------------------------------------------------------------------------";
        }
    }
}
