using System.Collections.Generic;
using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
	class Tela
	{
		public static void ImprimirPartida(PartidaDeXadrez partida)
		{
			ImprimirTela(partida.Tab);
			Console.WriteLine();
			Console.WriteLine("Turno: " + partida.Turno);
			Console.WriteLine("Aguardando a jogada da " + partida.JogadorAtual);
			ImprimirPecasCap(partida);
		}

		public static void ImprimirPecasCap(PartidaDeXadrez partida)
		{
			Console.WriteLine("Peças capturadas: ");
			Console.Write("Brancas: ");
			ImprimirConjunto(partida.PecasCap(Cor.Branca));
			Console.WriteLine();
			ConsoleColor aux = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Pretas: ");
			ImprimirConjunto(partida.PecasCap(Cor.Preta));
			Console.ForegroundColor = aux;
		}

		public static void ImprimirConjunto(HashSet<Peca> conjunto)
		{
			foreach (Peca x in conjunto)
			{
				Console.Write(x + " ");
			}
		}

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

		public static void ImprimirTel(Cor jogadorAtual,Tabuleiro tab, bool[,] pm)
		{
			ConsoleColor FundoOriginal = Console.BackgroundColor;
			ConsoleColor FundoDasBrancas = ConsoleColor.White;
			ConsoleColor FundoDasPretas = ConsoleColor.Red;

			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");
				for (int j = 0; j < tab.Colunas; j++)
				{
					if (pm[i, j] == true && jogadorAtual == Cor.Branca)
					{
						Console.BackgroundColor = FundoDasBrancas;
					}
					else if(pm[i, j] == true && jogadorAtual == Cor.Preta)
					{
						Console.BackgroundColor = FundoDasPretas;
					}
					else
					{
						Console.BackgroundColor = FundoOriginal;
					}
					ImprimirPeca(tab.Pea(i, j));
					Console.BackgroundColor = FundoOriginal;
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
