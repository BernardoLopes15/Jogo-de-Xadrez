﻿using System;
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

				while (!partidaDeXadrez.Terminada)
				{
					try
					{
						Console.Clear();
						Tela.ImprimirPartida(partidaDeXadrez);

						Console.WriteLine();
						Console.Write("Origem: ");
						Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
						partidaDeXadrez.ValidarPosicaoOrigem(origem);

						bool[,] pm = partidaDeXadrez.Tab.Peca(origem).MovimentosPossiveis();

						Console.Clear();
						Tela.ImprimirTel(partidaDeXadrez.JogadorAtual, partidaDeXadrez.Tab, pm);

						Console.WriteLine();
						Console.Write("Destino: ");
						Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
						partidaDeXadrez.ValidarPosicaoDestino(origem, destino);

						partidaDeXadrez.RealizaJogada(origem, destino);
						if (partidaDeXadrez.Promocao)
						{
							Console.WriteLine("Escolha entre as peças: D, T, B, C");
							string c = Console.ReadLine();
							partidaDeXadrez.PromocaoJog(partidaDeXadrez.JogadorAtual, c, destino);
							partidaDeXadrez.Promocao = false;
						}
					}
					catch (TabuleiroException e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
					catch (IndexOutOfRangeException e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
					catch(Exception e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
					Console.Clear();
					Tela.ImprimirPartida(partidaDeXadrez);
				}
			}
			catch (TabuleiroException e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
