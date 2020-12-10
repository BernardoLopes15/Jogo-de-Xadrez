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

					Console.Write("Origem: ");
					Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
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
