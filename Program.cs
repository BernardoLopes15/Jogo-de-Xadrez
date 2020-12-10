using System;
using tabuleiro;
using xadrez;

namespace Xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

				while(!partidaDeXadrez.Terminada)
				{
					Console.Clear();
					Tela.ImprimirTela(partidaDeXadrez.Tab);

					Console.WriteLine();
					Console.Write("Origem: ");
					Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

					bool[,] pm = partidaDeXadrez.Tab.Peca(origem).MovimentosPossiveis();

					Console.Clear();
					Tela.ImprimirTel(partidaDeXadrez.Tab, pm);

					Console.Write("Destino: ");
					Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

					partidaDeXadrez.ExecutaMovimento(origem, destino);
				}
			}
			catch (TabuleiroException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
