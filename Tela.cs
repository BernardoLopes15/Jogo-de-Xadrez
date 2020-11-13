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
				for (int j = 0; j < tab.Colunas; j++)
				{
					if(tab.Pea(i, j) == null)
					{
						Console.Write("- ");
					}
					else
					{
						Console.Write(tab.Pea(i, j) + " ");
					}
				}
				Console.WriteLine();
			}
		}
	}
}
