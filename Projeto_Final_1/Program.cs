using System;
using System.Collections.Generic;

namespace Projeto_Final_1
{
    class Program
    {

        static void Main(string[] args)
        {
            Menu(); // Traz o método responsável por carregar o menu para o método main 
        }

        private static void Menu() // MENU
        {
            Funcionario funcionarioAtual = new Funcionario(); // Criação do novo funcionario.

            bool sair = false; // Variável para auxiliar no encerramento do programa
            do
            {
                string escolhaDoUtilizador = ""; // Declara uma variável vazia cujo nome é "escolhaDoUtilizador"
                do // Faz o programa executar os próximos comandos, até que certa condição seja atingida
                {   // MENU
                    Console.Clear();
                    Console.WriteLine("########## BEM VINDO - MENU PRINCIPAL ###########");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#          1 - Criar Funcionário                #");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#-----------------------------------------------#");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#          2 - Fazer Login                      #");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#-----------------------------------------------#");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#          0 - Encerrar Aplicação               #");
                    Console.WriteLine("#                                               #");
                    Console.WriteLine("#################################################");
                    Console.WriteLine();
                    Console.Write("Digite a opção desejada..:");

                    escolhaDoUtilizador = Console.ReadLine(); // Atribui um valor à variável "escolhaDoUtilizador"

                    switch (escolhaDoUtilizador)
                    {
                        case "0": // Encerrar o Programa
                            sair = true;
                            break;
                        case "1": // Chama o metodo "CriarFuncionario"
                            CriarFuncionario(); 
                            break;
                        case "2": // Testa o Login do funcionario, caso seja uma credencial valida, o programa redireciona para o Menu do respectivo funcionario
                            try // Tratamento de exceção para uma operação critica 
                            {
                                funcionarioAtual = Login();
                            }
                            catch (Exception err) // Caso apresente algum erro ao testar o login, o console apresentará a mensagem do erro e não finaliza o andamento do programa
                            {
                                escolhaDoUtilizador = "";
                                Console.WriteLine(err.Message);
                                Console.ReadKey();
                            }
                            break;
                        default: // Caso o utilizador não escolha uma das opções apresentadas no console, o programa apresenta a mensagem de "Opção inválida!".
                            Console.WriteLine("Opção inválida!");
                            Console.ReadKey();
                            break;
                    }

                } while (escolhaDoUtilizador != "1" && escolhaDoUtilizador != "2" && escolhaDoUtilizador != "0"); // Enquanto o utilizado não digitar uma opção fornecida pelo programa, utilizador continua no Menu.

                if (sair) // Encerra o programa caso escolha opcao '0' de encerrar o programa.
                {
                    break;
                }

                switch (funcionarioAtual.cargo) // Apos o usuario entrar com o Login , utiliza-se da variavel Cargo para determinar qual Menu mostrar.
                {                               // 
                    case 1: // Apresenta o Menu do Gerente
                        Console.Clear();
                        sair = MostraMenuGerente(funcionarioAtual.user);
                        break;
                    case 2: // Apresenta o Menu do Repositor
                        Console.Clear();
                        sair = MostraMenuRepositor();
                        break;
                    case 3: // Apresenta o Menu do Caixa
                        Console.Clear();
                        sair = MostraMenuCaixa(funcionarioAtual.user); // É utilizado o "funcionarioAtual.user" para ter acesso ao nome do funcionario que efetuou o login
                        break;                                         
                    default:
                        break;
                }
            } while (!sair); // Enquanto a varial sair continuar true o programa continua a funcionar.
        }

        // MENU CAIXA
        private static bool MostraMenuCaixa(string nomeFuncionario) // "nomeFuncionario" recebe o nome do funcionario a partir do login
        {
            string escolha = "";
            bool sair = false; // Variável para auxiliar no encerramento do programa
            do
            {
                Console.Clear();
                Console.WriteLine("############### Menu Caixa ###############");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#      1 - Vendas                        #");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#----------------------------------------#");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#      2 - Consultar Faturas             #");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#----------------------------------------#");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#      8 - Terminar sessão               #");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#----------------------------------------#");
                Console.WriteLine("#                                        #");
                Console.WriteLine("#      9 - Fechar aplicação              #");
                Console.WriteLine("#                                        #");
                Console.WriteLine("##########################################");
                Console.WriteLine();
                Console.Write("Digite a opção desejada..:");
                escolha = Console.ReadLine();
                switch (escolha)
                {
                    case "1":// Apresenta o metodo Vendas
                        Vendas(nomeFuncionario);
                        break;
                    case "2":// Apresenta o metodo ConsultarFatura
                        ConsultarFatura();
                        break;
                    case "9":// Fechar aplicação
                        sair = true;
                        break;
                    default:
                        break;
                }
            } while (escolha != "8" && escolha != "9"); // 4 - Finaliza a sessão do caixa e retorna para o menu principal , 5 - Finaliza o programa

            return sair; // caso o usuario escolha a opcao 5, o que muda a variavel sair para true, encerrando assim o programa
        }

        private static void Vendas(string nomeFuncionario) 
        {
            List<Stock> carrinho = new List<Stock>(); // Cria uma nova lista em stock, chamada carrinho (Será o nosso carrinho de compras)
            Console.Clear();
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#           Vendas           #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            Console.Write("Insira o nome do cliente..: ");
            string nomeCliente = Console.ReadLine(); // Recebe e armazena o nome do cliente na variavel
            string pagamento = "";
            do
            {
                Console.Write("Informe o método de pagamento: (1 - Dinheiro / 2 - Cartão )..: ");
                pagamento = Console.ReadLine();

                if (pagamento != "1" && pagamento != "2") // Limita o usuario a escolher uma das opções validas
                {
                    Console.WriteLine("Erro! Insira um valor aceitável.");
                }

            } while (pagamento != "1" && pagamento != "2");
            string escolha = "";

            do
            {
                Console.WriteLine("########## Escolha um produto para adicionar a lista de compras ########## ");
                ConsultarStock(); // Chama o metodo "ConsultarStock", para exibir a lista no console
                Console.Write("Digite o ID do produto que deseja adicionar a sua lista de compras: ");
                string addProduto = Console.ReadLine(); // Recebe e armazena o ID selecionado na variavel
                bool isValidQtd = true; // Variavel para auxiliar no loop
                
                FileStock fileStock2 = new FileStock(); 
                var x = fileStock2.GetStockByID(addProduto,false); // Salva na variavel o protudo encontrado atraves do "GetStockByID" que obtem o produto pelo parametro ID, e o parametro bool que verifica 
                do                                                 // se o produto esta deletado
                {
                    Console.Write("Digite a quantidade do produto que deseja adicionar a sua lista de compras: ");
                    int qtd = int.Parse(Console.ReadLine()); // Recebe e armazena a quatidade informada na variavel
                    if (x.quantidade - qtd < 0) // Condição para impossibilitar o usuario de inserir a quantidade superior ao disponivel no stock
                    {
                        Console.WriteLine($"A quantidade excede o limite do stock.\nExiste(m) {x.quantidade}, {x.nome}.");
                        isValidQtd = false;
                    }
                    else
                    {
                        isValidQtd = true;
                        x.quantidade -= qtd; // subtrai a quantidade "comprada" pelo usuario, da quantidade disponivel no stock.
                        fileStock2.Alterar(addProduto, x);// Utiliza o medo "Alterar", para realizar a mudança da quantidade
                        x.quantidade = qtd; // inf
                        carrinho.Add(x); // Adiciona o produto no "Carrinho de Compras" 
                    }
                } while (!isValidQtd); // Enquanto o usuario nao fornecer um valor(quantidade) que esteja disponivel no stock para "qtd", ocorre o loop
                Console.WriteLine("Para Finalizar Compra Digite \"0\" para adicionar um novo produto ao carrinho de compras digite, \"1\"");
                escolha = Console.ReadLine(); // Recebe e armazena a escolha informada na variavel
            } while (escolha != "0"); // Enquanto a escolha for diferente de zero o loop continua

            FileFatura fileFatura = new FileFatura(); // Cria a fatura
            int id = fileFatura.GetFaturaToMain().Count + 1; // Cria o ID da fatura , Utiliza o metodo "GetFaturaToMain" para trazer a lista criada, adicionar +1 ao gerar um novo id
            Fatura fatura1 = new Fatura(id, nomeCliente, pagamento, nomeFuncionario,carrinho);
            fileFatura.GerarFatura(fatura1); // Utiliza o metodo "GerarFatura" para criar a fatura
            Console.WriteLine(fatura1); // exibe para o utilizador a fatura
            Console.ReadKey();

        }

        private static void ConsultarFatura()
        {
            Console.Clear();
            FileFatura fileFatura = new FileFatura();

            var x = fileFatura.GetFaturaToMain(); // Armazena na variavel a Lista Fatura
            Console.Clear(); // limpa o console
            Console.WriteLine("# ----------------------------  Lista de Faturas  ---------------------------- #");
            Console.WriteLine();
            foreach (var item in x)
            {
                Console.WriteLine(item); // apresenta a lista de faturas
            }
            Console.WriteLine();
            Console.Write("Aperte <Enter> para retornar ao Menu.");
            Console.ReadKey();
        }

        // MENU REPOSITOR
        private static bool MostraMenuRepositor() // Apresenta o Menu do Repositor
        {
            string escolha = ""; // Variável para auxiliar na opção de escolha do utilizador
            bool sair = false; // Variável para auxiliar no encerramento do programa
            do
            {
                Console.Clear();
                Console.WriteLine("############### Menu Repositor #############");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    1 - Adicionar Produto ao Stock        #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    2 - Consultar Stock                   #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    3 - Excluir Produto                   #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    4 - Alterar Stock                     #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    5 - Recuperar Produto                 #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    8 - Terminar Sessão                   #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#------------------------------------------#");
                Console.WriteLine("#                                          #");
                Console.WriteLine("#    9 - Fechar Aplicação                  #");
                Console.WriteLine("#                                          #");
                Console.WriteLine("############################################");
                Console.WriteLine();
                Console.Write("Digite a opção desejada..:");
                escolha = Console.ReadLine(); // Recebe e armazena a opção do utilizador
                try // Tratamento de exceção para uma operação critica 
                {
                    switch (escolha)
                    {
                        case "1":// Adicionar Produtos ao Stock
                            AdicionarProdutoAoStock();
                            break;
                        case "2":// Consultar Stock
                            ConsultarStock();
                            Console.ReadKey();
                            break;
                        case "3":// Excluir um Produto
                            ExcluirProduto();
                            break;
                        case "4":// Alterar um produto do stock
                            AlterarStock();
                            break;
                        case "5":// Recuperar um produto
                            RecuperarStock();
                            break;
                        case "9":// Fechar Aplicação
                            sair = true;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception err) // Caso apresente algum erro, o console apresentará a mensagem do erro sem finalizar o andamento do programa
                {
                    Console.WriteLine(err.Message); // Exibi o erro
                    Console.ReadKey();
                }

            } while (escolha != "8" && escolha != "9"); // Enquanto o utilizado não digitar uma opção fornecida pelo programa, utilizador continua no Menu.

            return sair; // Enquanto a varial sair continuar true o programa continua a funcionar.
        }

        private static void AlterarStock()
        {
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#        AlterarStock        #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            

            FileStock fileStock2 = new FileStock();
            Console.WriteLine("Escolha o produto que deseja Alterar.");
            Console.WriteLine();
            var x = fileStock2.GetStockToMain(); // Salva na variavel o a lista Stock
            foreach (var item in x)
            {
                Console.WriteLine(item); // exibi a lista Stock
            }
            Console.WriteLine();
            Console.WriteLine("Digite o ID do produto que deseja alterar..: ");
            string id = Console.ReadLine(); // Recebe e armazena o ID informado na variavel
            FileStock fileStock1 = new FileStock();
            var stk = CreateStock(true); // Adiciona o produto ao stock
            try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
            {
                fileStock1.Alterar(id, stk); // utiliza o metodo "Alterar" 
                Console.WriteLine("Produto: {0} alterado com sucesso.", stk.nome); // apresenta o nome do produto alterado
                Console.WriteLine();
                Console.Write("Para voltar ao Menu Principal digite '0' ..:");
            }
            catch (Exception err) // Caso não seja possivel salvar o produto alterado, e apresentar alguma mensagem de erro
            {
                Console.WriteLine(err.Message);
            }   
                Console.ReadKey();           
        }

        private static void RecuperarStock()
        {
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#      Recuperar Produto     #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            

            FileStock fileStock2 = new FileStock();
            Console.WriteLine("Escolha o produto que deseja recuperar.");
            Console.WriteLine();
            var x = fileStock2.GetDeletedStockToMain(); // Salva na variavel os produtos que ja foram excluidos
            foreach (var item in x)
            {
                Console.WriteLine(item); // Apresenta a lista dos produtos excluidos
            }
            Console.WriteLine();
            Console.WriteLine("Digite o ID do produto que deseja recuperar..: ");
            string id = Console.ReadLine(); // Recebe e armazena na variavel o ID digitado
            FileStock fileStock1 = new FileStock();
            string nome = "";
            try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
            {
                nome = fileStock1.RecuperarProduto(id).nome; // Utiliza o metodo "RecuperarProduto", que recupera um produto 
                Console.WriteLine("Produto: {0} alterado com sucesso.", nome);
                Console.WriteLine();
                Console.Write("Para voltar ao Menu Principal digite '0' ..:");
            }
            catch (Exception err) // Caso aconteça alguma falha, apresenta a mensagem do erro
            {
                Console.WriteLine(err.Message);
            }
            Console.ReadKey();

        }

        private static void ExcluirProduto()
        {
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#       Excluir Produto      #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            Console.WriteLine();

            FileStock fileStock2 = new FileStock();
            Console.WriteLine("Escolha o produto que deseja excluir..:");
            var x = fileStock2.GetStockToMain(); // Salva na variavel a lista Stock
            foreach (var item in x)
            {
                Console.WriteLine(item); // apresenta a lista Stock
            }
            string nome = Console.ReadLine();
            FileStock fileStock1 = new FileStock();
            try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
            {
                fileStock1.Delete(nome); // utiliza o metodo "Delete"
                Console.WriteLine("Produto excluido com sucesso!");
            }
            catch (Exception err) // Caso aconteça alguma falha, apresenta a mensagem do erro
            {
                Console.WriteLine(err.Message);
            }
            Console.ReadKey();
        }

        private static void ConsultarStock()
        {
            string escolha = "";
           
                Console.Clear();
                FileStock fileStock2 = new FileStock();
                Console.WriteLine("Escolha a categoria de produto que deseja acessar: ");
                Console.WriteLine();
                Console.WriteLine(fileStock2.GetCategoriasDescription()); // Apresenta a lista stock por categorias
                Console.WriteLine();
                Console.Write("Categoria selecionada: ");
                escolha = Console.ReadLine();
                Console.WriteLine();

                var x = fileStock2.GetStockToMain(escolha); // Filtra a escolha de categoria
                foreach (var item in x)
                {
                    Console.WriteLine(item); // apresenta a lista stock, filtrada com a catehoria selecionada pelo usuario
                }
                Console.ReadKey();
         
        }

        private static Stock CreateStock(bool toEdit) // Metodo para criar um produto 
        {

            string titleDiff = toEdit ? "Alterar" : "Adicionar"; // Essa variavel permite apresentar 2 palavras diferentes, a depender do parametro informado nos metodos "Alterar Stock" e "AdicionarProdutoAoStock"
            FileStock fileStock = new FileStock();
            Stock stock1 = new Stock();
            string nome = "";
            Console.Clear();
            Console.WriteLine("########################################");
            Console.WriteLine("#                                      #");
            Console.WriteLine("# " + titleDiff + " Produto ao Stock   #"); // A palavra no campo "titleDiff" aparece a partir do parametro definido na hora da chamada do metodo
            Console.WriteLine("#                                      #");
            Console.WriteLine("########################################");
            Console.WriteLine();

            string categoria;
            do
            {
                Console.Write("Escreva a categoria do Produto (1 - Congelados / 2 - Prateleira / 3 - Enlatados)..: ");
                categoria = Console.ReadLine();

                if (categoria != "1" && categoria != "2" && categoria != "3") // Condição para limitar o usuario as opções disponiveis
                {
                    Console.WriteLine("Erro! Insira um valor aceitável."); // aparece a mensagem em caso da condição nao ser atendida
                }
            } while (categoria != "1" && categoria != "2" && categoria != "3"); // cria um loop enquanto o usuario nao escolher uma opção valida

            string nCategoria = categoria; // recebe categoria

            do
            {
                Console.Write("Escreva o nome do Produto..: ");
                nome = Console.ReadLine();
                if (fileStock.ProdutoExists(nome) && !toEdit) // 
                {
                    Console.WriteLine($"Erro! Esse produto {nome} já está cadastrado.");
                }

            } while (fileStock.ProdutoExists(nome) && !toEdit);

            string input;
            int quantidade;
            do
            {
                Console.Write("Escreva a quantidade do produto a ser adicionado..: ");
                input = Console.ReadLine();

            } while (int.TryParse(input, out quantidade) == false); // Recebe a string input e convert para int quatidade

            int id = fileStock.GetStockToMain().Count + 1; // Salva a lista Stock na variavel

            Console.Write("Escreva o preço do produto..: ");
            double preco = Convert.ToDouble(Console.ReadLine()); 

            stock1 = new Stock(nCategoria, nome, quantidade, preco, id); // Cria o novo produto
            return stock1;
        }

        public static void AdicionarProdutoAoStock() // Salva a criação dos produtos no ficheiro
        {
            var stock1 = CreateStock(false); // Atraves do metodo "CreateStock" salva os dados preenchidos na variavel
            FileStock fileStock = new FileStock();
            fileStock.AdicionarProduto(stock1); // Atraves do metodo "AdicionarProduto" adiciona a lista
            Console.WriteLine();
            Console.WriteLine("Novo Produto: {0} adicionado com sucesso.", stock1.nome);
            Console.WriteLine();
            Console.Write("Para voltar ao Menu Principal digite '0' ..:");
            Console.ReadKey();
        }

        // MENU GERENTE
        private static bool MostraMenuGerente(string nomeFuncionario)
        {
            FileStock fileStock = new FileStock();
            string escolha = "";
            bool sair = false;  // Variável para auxiliar no encerramento do programa
            do
            {
                try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
                {
                    Console.Clear();
                    Console.WriteLine("############### Menu Gerente ###############");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    1 - Consultar Lista de Funcionários   #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#------------------------------------------#");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    2 - Excluir Funcionário               #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#------------------------------------------#");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    3 - Vendas                            #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#------------------------------------------#");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    4 - Limpar Stock                      #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#------------------------------------------#");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    8 - Terminar sessão                   #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#------------------------------------------#");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("#    9 - Fechar aplicação                  #");
                    Console.WriteLine("#                                          #");
                    Console.WriteLine("############################################");
                    Console.WriteLine();
                    Console.Write("Digite a opção desejada..:");
                    escolha = Console.ReadLine();
                    switch (escolha)
                    {
                        case "1":// Consultar Lista de Funcionários
                            ConsultarListaDeFuncionarios();
                            break;
                        case "2":// Excluir um Funcionário
                            Delete();
                            break;
                        case "3":// Vendas
                            Vendas(nomeFuncionario);
                            break;
                        case "4":
                            Console.WriteLine("##############################");
                            Console.WriteLine("#                            #");
                            Console.WriteLine("#        Limpar Stock        #");
                            Console.WriteLine("#                            #");
                            Console.WriteLine("#          ATENÇÃO           #");
                            Console.WriteLine("#                            #");
                            Console.WriteLine("#      Ação irreversivel!!   #");
                            Console.WriteLine("#                            #");
                            Console.WriteLine("##############################");
                            
                            Random random = new Random();
                            var cod = random.Next(10000, 99999); // Gera um numero aleatorio e armazena na variavel
                            Console.WriteLine($"Digite o código: {cod}, para eliminar todos os produtos.\n\n");
                            Console.Write("Codigo: ");
                            string codigo = Console.ReadLine(); // Recebe e armazena na variavel
                            if (codigo == cod.ToString()) // Compara o numero aleatorio gerado com o a variavel que recebeu o codigo digitado
                            {
                                fileStock.HardDelete(); // Utiliza o metodo "HardDelete" para Limpar a lista Stock
                            }
                            else // Caso o condição acima não se cumpra
                            {
                                Console.WriteLine("Codigo errado.\nVoltar para menu digite (0)..."); 
                                Console.ReadKey();
                            }
                            break;
                        case "9":// Fechar aplicação
                            sair = true;
                            break;
                        default:
                            break;
                    }
                }catch(Exception err) // Caso aconteça algum erro na execução do programa, apresenta a mensagem do erro
                {
                    Console.Clear();
                    Console.WriteLine("ERRO!!!");
                    Console.WriteLine(err.Message);
                    Console.ReadKey();
                }
                
            } while (escolha != "8" && escolha != "9"); // Encerra o loop caso a condição se cumpra

            return sair;
        } // Apresenta o Menu do Gerente

        private static void Delete()
        {
            FileFuncionario fileFuncionario = new FileFuncionario();
            var x = fileFuncionario.GetFuncionariosToMain(); // Salva a lista Funcionarios na variavel
            Console.Clear(); // limpa o console
            Console.WriteLine("# --- Lista de Funcionários --- #");
            foreach (var item in x)
            {
                Console.WriteLine(item.user); // Exibi os funcionarios presente na lista Funcionarios
            }
            Console.WriteLine("Escolha o utilizador a eliminar..:");
            string user = Console.ReadLine();
            try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
            {
                fileFuncionario.Delete(user); // Utiliza o metodo "Delete" para apagar um funcionario da lista
                Console.WriteLine("Utilizador eliminado com sucesso!");
                Console.WriteLine("Aperte <Enter> para retornar ao Menu do Gerente");
            }
            catch (Exception err) // Caso aconteça algum erro, a mensagem do erro aparece no console
            {
                Console.WriteLine(err.Message);
            }
            Console.ReadKey();
        }

        private static void ConsultarListaDeFuncionarios()
        {
            FileFuncionario fileFuncionario = new FileFuncionario();
            var x = fileFuncionario.GetFuncionariosToMain(); // Salva a lista Funcionarios na variavel
            Console.Clear(); // limpa o console
            Console.WriteLine("# -------------------  Lista de Funcionários  ------------------ #");
            Console.WriteLine();
            foreach (var item in x)
            {
                Console.WriteLine(item); // Exibi os funcionarios presente na lista Funcionarios 
            }
            Console.WriteLine();
            Console.Write("Aperte <Enter> para retornar ao Menu Gerente.");
            Console.ReadKey();
        }

        // METODOS MENU PRINCIPAL
        private static Funcionario Login()
        {
            FileFuncionario fileFuncionario = new FileFuncionario();
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#      Login Funcionário     #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            Console.Write("Digite seu Nome de Funcionário..: ");
            string user = Console.ReadLine();
            Console.Write("Digite sua Senha..: ");
            string password = Console.ReadLine();
            try // Tratamento de exceção para uma operação critica(que pode acontecer erros)
            {
                var x = fileFuncionario.Login(user, password);
                Console.WriteLine("{0}, Login efetuado com sucesso.", user);
                return x;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

        }
        private static void CriarFuncionario()
        {
            int escolhaDoUtilizador = -1;
            while (escolhaDoUtilizador != 0)
            {
                Console.Clear();
                Console.WriteLine("##############################");
                Console.WriteLine("#                            #");
                Console.WriteLine("#      Criar Funcionário     #");
                Console.WriteLine("#                            #");
                Console.WriteLine("##############################");
                FileFuncionario fileFuncionario = new FileFuncionario();
                string user = "";
                do
                {
                    Console.Write("Escreva um nome de usuário..: ");
                    user = Console.ReadLine();
                    if (fileFuncionario.FuncionarioExists(user)) // Confere se já existe o funcionário com mesmo nome cadastrado
                    {
                        Console.WriteLine($"Erro! Funcionário {user} já existe.");
                    }

                } while (fileFuncionario.FuncionarioExists(user)); // Enquanto o usuario nao inserir um novo nome, o loop continua

                Console.Write("Escreva sua senha..: ");
                string password = Console.ReadLine();
                string cargo;
                do
                {
                    Console.Write("Escreva seu cargo (1- Gerente / 2-Repositor / 3-Caixa)..: ");
                    cargo = Console.ReadLine();

                    if (cargo != "1" && cargo != "2" && cargo != "3") // Limita o usuario a escolher uma das opções validas
                    {
                        Console.WriteLine("Erro! Insira um valor aceitável.");
                    }

                } while (cargo != "1" && cargo != "2" && cargo != "3"); // Continua no loop ate escolher uma opção valida

                int nCargo = Convert.ToInt32(cargo);
                // Gravação dos dados no ficheiro
                Funcionario funcionario1 = new Funcionario(user, password, nCargo);
                fileFuncionario.AddFuncionario(funcionario1);
                Console.WriteLine();
                Console.WriteLine("Novo Funcionário: {0} adicionado com sucesso.", user);
                Console.WriteLine();
                Console.Write("Para voltar ao Menu Principal digite '0'..:");
                Console.ReadKey();
                break;
            }
        }
    }
}
