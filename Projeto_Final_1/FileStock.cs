using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Projeto_Final_1
{
    class FileStock:MainFile<Stock>
    {
        private List<Stock> Stock;
        public FileStock()
        {
            FileName = "StockGravado.txt";
            Update();
        }

        public Stock RecuperarProduto(string id)
        {
            var stock = GetStockByID(id,true);
            int count = 0;
            stock.nome = stock.nome.Replace("(Apagado : " + stock.ProductID + ")", "");
            
            Stock existingProd = GetStockByName(stock.nome);
            while (existingProd != null)
            {
                try
                {
                    existingProd = null;
                existingProd = GetStockByName(stock.nome);
                }
                catch (Exception)
                {

                }
                if (existingProd != null)
                {
                    count++;
                    stock.nome = stock.nome.Replace((count - 1).ToString(), "") + count;
                }
            }
            stock.IsDeleted = false;
            SaveToFile(Stock);
            Update();
            return stock;
        }

        public void AdicionarProduto(Stock stock) // Adcionar dados na lista
        {
            stock.ProductID = Stock.Count+1;
            Stock.Add(stock);
            SaveToFile(Stock);
            Update();
        }

        public void HardDelete()
        {
            Stock = new List<Stock>();
            SaveToFile(Stock);
            Update();
        }

        private void Update() // Atualiza o ficheiro
        {
            Stock = GetFromFile();
        }

        public bool ProdutoExists(string user) // Checa se existe o produto no ficheiro
        {
            var x = Stock.FirstOrDefault(p => p.nome == user);
            return x != null;
        }

        public List<Stock> GetStockToMain()
        {
            return Stock.Where(p => !p.IsDeleted).ToList();
        }
        public List<Stock> GetDeletedStockToMain()
        {
            return Stock.Where(p => p.IsDeleted).ToList();
        }

        public List<Stock> GetStockToMain(string categoria)
        {
            var x = Stock.Where(stock => stock.categoria == categoria && !stock.IsDeleted).ToList();

            if (x != null) return x;
            else
            {
                throw new Exception("Categoria Inexistente");
            }
        }

        public string GetCategoriasDescription()
        {
            return "1 - Congelados\n2 - Prateleira\n3 - Enlatada";
        }

        public Stock GetStockByID(string id, bool isDeleted)
        {
            Stock stock = null;
            foreach (var item in Stock)
            {
                if (item.ProductID.ToString() == id && item.IsDeleted == isDeleted)
                {
                    stock = item;
                    break;
                }
            }
            if (stock == null)
            {
                throw new Exception($"id: {id} inexistente!");
            }
            return stock;
        }
        public Stock GetStockByName(string nome)
        {
            Stock stock = null;
            foreach (var item in Stock)
            {
                if (item.nome == nome && !item.IsDeleted)
                {
                    stock = item;
                    break;
                }
            }
            if (stock == null)
            {
                throw new Exception($"nome: {nome} inexistente!");
            }
            return stock;
        }

        public void Alterar(string id, Stock paraAlterar)
        {
            var stock = GetStockByID(id,false);
            stock.categoria = paraAlterar.categoria;
            stock.nome = paraAlterar.nome;
            stock.preco = paraAlterar.preco;
            stock.quantidade = paraAlterar.quantidade;
            paraAlterar.ProductID = int.Parse(id);
            SaveToFile(Stock);
            Update();
        }


        public void Delete(string id) // Apagar produto do ficheiro
        {
            var stock = GetStockByID(id,false);
            stock.nome += "(Apagado : " + stock.ProductID + ")";
            stock.IsDeleted = true;
            SaveToFile(Stock);
            Update();
        }
    }
}
