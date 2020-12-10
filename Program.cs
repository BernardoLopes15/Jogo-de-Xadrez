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
					try
					{
						Console.Clear();
						Tela.ImprimirTela(partidaDeXadrez.Tab);
						Console.WriteLine();
						Console.WriteLine("Turno: " + partidaDeXadrez.Turno);
						Console.WriteLine("Aguardando a jogada da " + partidaDeXadrez.JogadorAtual);

						Console.WriteLine();
						Console.Write("Origem: ");
						Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
						partidaDeXadrez.ValidarPosicaoOrigem(origem);

						bool[,] pm = partidaDeXadrez.Tab.Peca(origem).MovimentosPossiveis();

						Console.Clear();
						Tela.ImprimirTel(partidaDeXadrez.Tab, pm);

						Console.WriteLine();
						Console.Write("Destino: ");
						Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
						partidaDeXadrez.ValidarPosicaoDestino(origem, destino);

						partidaDeXadrez.RealizaJogada(origem, destino);
					}
					catch(TabuleiroException e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
				}
			}
			catch (TabuleiroException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
