using System;

namespace DIO.Videos
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
		static FilmeRepositorio repositorioFilme = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						Console.Clear();
						MostrarSerie();
						Console.Clear();
						break;
					case "2":
						Console.Clear();
						MostrarFilme();
						Console.Clear();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }


        private static void MostrarSerie()
		{
            string opcaoSerie = ObterOpcaoTipo('S');

			while (opcaoSerie.ToUpper() != "V")
			{
				switch (opcaoSerie)
				{
					case "1":
						Listar('S');
						break;
					case "2":
						Inserir('S');
						break;
					case "3":
						Atualizar('S');
						break;
					case "4":
						Excluir('S');
						break;
					case "5":
						Visualizar('S');
						break;
					case "C":
						Console.Clear();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				opcaoSerie = ObterOpcaoTipo('S');
			}		
		}

        private static void MostrarFilme()
		{
            string opcaoFilme = ObterOpcaoTipo('F');

			while (opcaoFilme.ToUpper() != "V")
			{
				switch (opcaoFilme)
				{
					case "1":
						Listar('F');
						break;
					case "2":
						Inserir('F');
						break;
					case "3":
						Atualizar('F');
						break;
					case "4":
						Excluir('F');
						break;
					case "5":
						Visualizar('F');
						break;
					case "C":
						Console.Clear();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				opcaoFilme = ObterOpcaoTipo('F');
			}		
		}		

        private static void Excluir(char tipo)
		{
			Console.Write("Digite o id {0}: ",((tipo=='S') ? "da série" : "do filme"));
			int indice = int.Parse(Console.ReadLine()!);
			
			Console.Write("Confima exclusão? (s/n): ");
			if (Console.ReadLine()!.ToUpper() == "S")
			{
				if (tipo=='S')
					repositorio.Exclui(indice);
				else
					repositorioFilme.Exclui(indice);
			}
		}

        private static void Visualizar(char tipo)
		{
			Console.Write("Digite o id {0}: ",((tipo=='S') ? "da série" : "do filme"));
			int indice = int.Parse(Console.ReadLine()!);
			if (tipo=='S')
			{
				var serie = repositorio.RetornaPorId(indice);
				Console.WriteLine(serie);
			}
			else
			{
				var filme = repositorioFilme.RetornaPorId(indice);
				Console.WriteLine(filme);				
			}
		}

        private static void Atualizar(char tipo)
		{
			Console.Write("Digite o id {0}: ",((tipo=='S') ? "da série" : "do filme"));
			int indice = int.Parse(Console.ReadLine()!);

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o Gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine()!);

			Console.Write("Digite o Título {0}: ",((tipo=='S') ? "da série" : "do filme"));
			string? entradaTitulo = Console.ReadLine()!;

			Console.Write("Digite o Ano de Início {0}: ",((tipo=='S') ? "da série" : "do filme"));
			int entradaAno = int.Parse(Console.ReadLine()!);

			Console.Write("Digite a Descrição {0}: ",((tipo=='S') ? "da série" : "do filme"));
			string entradaDescricao = Console.ReadLine()!;

			if (tipo=='S')
			{
				Serie atualizaSerie = new Serie(id: indice,
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorio.Atualiza(indice, atualizaSerie);
			}
			else
			{
				Filme atualizaFilme = new Filme(id: indice,
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorioFilme.Atualiza(indice, atualizaFilme);				
			}
		}
        private static void Listar(char tipo)
		{
			Console.WriteLine("Listar {0}",((tipo=='S') ? "série" : "filme"));

			if (tipo=='S')
			{
				var lista = repositorio.Lista();
				if (lista.Count == 0)
				{
					Console.WriteLine("Nenhuma série cadastrada.");
					return;
				}

				foreach (var serie in lista)
				{
					var excluido = serie.retornaExcluido();
					
					Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
				}
			}
			else
			{
				var lista = repositorioFilme.Lista();
				if (lista.Count == 0)
				{
					Console.WriteLine("Nenhuma filme cadastrado.");
					return;
				}

				foreach (var filme in lista)
				{
					var excluido = filme.retornaExcluido();
					
					Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? "*Excluído*" : ""));
				}				
			}
		}

        private static void Inserir(char tipo)
		{
			Console.WriteLine("Inserir {0}",((tipo=='S') ? "nova série" : "novo filme"));

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o Gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine()!);

			Console.Write("Digite o Título {0}: ",((tipo=='S') ? "da série" : "do filme"));
			string entradaTitulo = Console.ReadLine()!;

			Console.Write("Digite o Ano de Início {0}: ",((tipo=='S') ? "da série" : "do filme"));
			int entradaAno = int.Parse(Console.ReadLine()!);

			Console.Write("Digite a Descrição {0}: ",((tipo=='S') ? "da série" : "do filme"));
			string entradaDescricao = Console.ReadLine()!;

			if (tipo=='S')
			{
				Serie novaSerie = new Serie(id: repositorio.ProximoId(),
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorio.Insere(novaSerie);
			}
			else
			{
				Filme novoFilme = new Filme(id: repositorioFilme.ProximoId(),
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorioFilme.Insere(novoFilme);				
			}
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries e Filmes a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Série");
			Console.WriteLine("2- Filme");
			Console.WriteLine("C- Limpar Tela");			
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine()!.ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

        private static string ObterOpcaoTipo(char tipo)
		{
			Console.WriteLine();
			Console.WriteLine("DIO {0} a seu dispor!!!",((tipo=='S') ? "série" : "filme"));
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar {0}",((tipo=='S') ? "série" : "filme"));
			Console.WriteLine("2- Inserir {0}",((tipo=='S') ? "nova série" : "novo filme"));
			Console.WriteLine("3- Atualizar {0}",((tipo=='S') ? "série" : "filme"));
			Console.WriteLine("4- Excluir {0}",((tipo=='S') ? "série" : "filme"));
			Console.WriteLine("5- Visualizar {0}",((tipo=='S') ? "série" : "filme"));
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("V- Voltar");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine()!.ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}		
    }
}
