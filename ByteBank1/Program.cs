using System.Reflection.Metadata.Ecma335;

namespace ByteBank1 {

    public class Program {

        static void ShowMenu() {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas , List<double> saldos) {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);
          
            if(indexParaDeletar == -1) {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos) {
            for(int i = 0; i < cpfs.Count; i++) {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1) {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos) {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
            // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
        }

        static void ApresentaConta( int index, List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        static void LoginUsuario(List<string> cpfs, List<string> senhas, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaLogin = Console.ReadLine();
            int cpfIndex = cpfs.FindIndex(cpf => cpf == cpfParaLogin);
            Console.Write("Digite a senha: ");
            string senhaParaLogin = Console.ReadLine();

            if (cpfIndex == -1)
            {
                Console.WriteLine("Usuário ou senha inválidos!");
            }
            else
            {
                if (senhas[cpfIndex] == senhaParaLogin)
                {
                    Console.WriteLine($"Seja bem vindo {titulares[cpfIndex]}");
                    MenuUsuario(cpfIndex, cpfs, senhas, titulares,saldos);
                }
                else {
                    Console.WriteLine("Usuário ou senha inválidos!");
                }
            }
        }

        static void ShowMenuUsuario()
        {
            Console.WriteLine("1 - Saque");
            Console.WriteLine("2 - Depósito");
            Console.WriteLine("3 - Transferência");
            Console.WriteLine("0 - Para Logout");
            Console.Write("Digite a opção desejada: ");
        }

        static void SacarConta(int indexUsuario, List<double> saldos, List<string> senhas) {

            
            Console.Write("Digite valor para sacar: ");
            double valor = double.Parse(Console.ReadLine());
            Console.Write("Digite senha da conta: ");
            string senha = Console.ReadLine();

            if (senha == senhas[indexUsuario])
            {
                if (valor > saldos[indexUsuario])
                {
                    Console.WriteLine($"Saldo insuficiente! Seu saldo na conta é de R$ {saldos[indexUsuario]}");
                }
                else
                {
                    saldos[indexUsuario] -= valor;
                    Console.WriteLine($"Saque efetuado com sucesso! Seu saldo agora é de R$ {saldos[indexUsuario]}");
                }
            }
            else
            {
                Console.WriteLine("SENHA INVÁLIDA!");
            }
        }

        static void DepositarConta(int indexUsuario, List<double> saldos)
        {
            Console.Write("Insira valor do depósito: ");
            double valor = double.Parse(Console.ReadLine());

            saldos[indexUsuario] += valor;
            Console.WriteLine($"Depósito efetuado com sucesso! Seu saldo agora é de R$ {saldos[indexUsuario]}");
        }

        static void ValidarTransferencia(int indexUsuario, List<string> cpfs, List<string> senhas, List<string> titulares, List<double> saldos)
        {
            Console.Write("Insira CPF da conta para qual deseja realizar a transferência: ");
            string contaTransferencia = Console.ReadLine();
            int indexParaTransferir = cpfs.FindIndex(cpf => cpf == contaTransferencia);

            if(indexParaTransferir == -1)
            {
                Console.WriteLine("Conta Inválida!");
                return;
            }

            Console.Write("Digite senha da conta: ");
            string senha = Console.ReadLine();

            if (senha != senhas[indexUsuario])
            {
                Console.WriteLine("SENHA INVÁLIDA!");
                return;
            }

            Console.WriteLine();
            Console.Write("Digite valor para transferir: ");
            double valor = double.Parse(Console.ReadLine());

            if (valor > saldos[indexUsuario])
            {
                Console.WriteLine($"Saldo insuficiente! Seu saldo na conta é de {saldos[indexUsuario]}");
            }
            else
            {
                Transferir(indexUsuario, indexParaTransferir, saldos, valor);
            }
        }
            
            

        static void Transferir(int indexUsuario, int indexParaTransferir, List<double> saldos, double valor)
        {
            saldos[indexUsuario] -= valor;
            saldos[indexParaTransferir] += valor;
            Console.WriteLine($"Transferência realizada com sucesso! Seu saldo na conta é de {saldos[indexUsuario]}");
        }


        static void MenuUsuario(int indexUsuario, List<string> cpfs, List<string> senhas, List<string> titulares, List<double> saldos)
        {
            ApresentaConta(indexUsuario, cpfs, titulares, saldos);
            
            int optionUsuario;

            do
            {
                ShowMenuUsuario();
                optionUsuario = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (optionUsuario)
                {
                    case 0:
                        Console.WriteLine("Logout efetuado com sucesso!");
                        break;
                    case 1:
                        SacarConta(indexUsuario, saldos, senhas);
                        break;
                    case 2:
                        DepositarConta(indexUsuario, saldos);
                        break;
                    case 3:
                        ValidarTransferencia(indexUsuario, cpfs, senhas, titulares, saldos);
                        break;
                }

                Console.WriteLine("-----------------");
            } while (optionUsuario != 0);
        }

        public static void Main(string[] args) {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option) {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        LoginUsuario(cpfs, senhas, titulares, saldos);
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);
            

        }

    }

}