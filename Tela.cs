using System;
using tabuleiro;

namespace Xadrez
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
					if(tab.Pea(i, j) == null)
					{
						Console.Write("- ");
					}
					else
					{
						ImprimirPeca(tab.Pea(i, j));
						Console.Write(" ");
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}

		public static void ImprimirPeca(Peca peca)
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
		}
	}
}
