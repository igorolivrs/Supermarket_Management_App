using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Final_1
{
    [Serializable]
    public enum Pagamento
    {
        Dinheiro = 1,
        Cartao = 2
    }
    [Serializable]
    public class Fatura
    {
        public int id;
        public string cliente;
        public Pagamento pagamento;
        public string vendedor;
        public List<Stock> Produtos { get; set; }

        public Fatura(int id, string cliente, string pagamento, string vendedor, List<Stock> stocks)
        {
            this.id = id;
            this.cliente = cliente;
            this.vendedor = vendedor;
            this.Produtos = stocks;
            if (!Enum.TryParse(pagamento, out this.pagamento))
            {
                this.pagamento = Pagamento.Dinheiro;
            }
        }

        public override string ToString()
        {
            string rV = "Numero da Fatura: " + id +
                        "\nComprador: " + cliente +
                        "\nMétodo de Pagamento: " + pagamento +
                        "\nVendedor: " + vendedor + "\n";
            foreach (var item in Produtos)
            {
                rV += "\n" + item;
            }
            double total = 0;
            foreach (var item in Produtos)
            {
                total += item.preco * item.quantidade;
            }
            rV += $"\nTotal: {total} Euros" + "\n----------------------------------------------------------------------------------------------------" + "\n"; 

            return rV;
        }
    }
}
