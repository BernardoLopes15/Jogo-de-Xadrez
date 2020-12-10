using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
	class Tela
	{
		public static void ImprimirTela(Tabuleiro tab)
		{
			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");
				for (int j = 0; j < tab.Colunas; j++)
				{
					ImprimirPeca(tab.Pea(i, j));
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}

		public static void ImprimirTel(Tabuleiro tab, bool[,] pm)
		{
			ConsoleColor FundoOriginal = Console.BackgroundColor;
			ConsoleColor FundoAlterado = ConsoleColor.DarkCyan;

			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");
				for (int j = 0; j < tab.Colunas; j++)
				{
					if(pm[i, j])
					{
						Console.BackgroundColor = FundoAlterado;
					}
					else
					{
						Console.BackgroundColor = FundoOriginal;
					}
					ImprimirPeca(tab.Pea(i, j));
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
			Console.BackgroundColor = FundoOriginal;
		}

		public static PosicaoXadrez LerPosicaoXadrez()
		{
			string s = Console.ReadLine();
			char coluna = s[0];
			int linha = int.Parse(s[1] + "");
			return new PosicaoXadrez(coluna, linha);
		}

		public static void ImprimirPeca(Peca peca)
		{
			if (peca == null)
			{
				Console.Write("- ");
			}
			else
			{
				if (peca.Cor == Cor.Branca)
				{
					Console.Write(peca);
				}
				else
				{
					ConsoleColor aux = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(peca);
					Console.ForegroundColor = aux;
				}
				Console.Write(" ");
			}
		}
	}
}
