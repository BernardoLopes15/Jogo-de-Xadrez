﻿using tabuleiro;


namespace xadrez
{
	class Peao : Peca
	{
		private PartidaDeXadrez Partida;

		public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
		{
			Partida = partida;
		}

		public override string ToString()
		{
			return "P";
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.Peca(pos);
			return p == null;
		}

		private bool PodeMoverInimigo(Posicao pos)
		{
			Peca p = Tab.Peca(pos);
			return p != null && p.Cor != Cor;
		}

		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			if (Cor == Cor.Branca)
			{
				//acima
				pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
				Posicao p1 = new Posicao(0, 0);
				p1.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && PodeMover(pos) && PodeMover(p1) && QuantMoviment == 0)
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//acima
				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && PodeMover(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//NE
				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
				if (Tab.PosicaoValida(pos) && PodeMoverInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//NO
				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
				if (Tab.PosicaoValida(pos) && PodeMoverInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}

				// #JogadaEspecial En Passant
				if(Posicao.Linha == 3)
				{
					Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

					//NE
					pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
					if (Tab.PosicaoValida(direita) && PodeMoverInimigo(direita) && Partida.VulEnPassant != null)
					{
						mat[direita.Linha - 1, direita.Coluna] = true;
					}
					//NO
					pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
					if (Tab.PosicaoValida(esquerda) && PodeMoverInimigo(esquerda) && Partida.VulEnPassant != null)
					{
						mat[esquerda.Linha - 1, esquerda.Coluna] = true;
					}
				}

			}
			else
			{
				//acima
				pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
				Posicao p1 = new Posicao(0, 0);
				p1.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && PodeMover(pos) && PodeMover(p1) && QuantMoviment == 0)
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//abaixo
				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && PodeMover(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//NE
				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
				if (Tab.PosicaoValida(pos) && PodeMoverInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				//NO
				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
				if (Tab.PosicaoValida(pos) && PodeMoverInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}

				// #JogadaEspecial En Passant
				if (Posicao.Linha == 4)
				{
					Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

					//NE
					pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
					if (Tab.PosicaoValida(direita) && PodeMoverInimigo(direita) && Partida.VulEnPassant != null)
					{
						mat[direita.Linha + 1, direita.Coluna] = true;
					}
					//NO
					pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
					if (Tab.PosicaoValida(esquerda) && PodeMoverInimigo(esquerda) && Partida.VulEnPassant != null)
					{
						mat[esquerda.Linha + 1, esquerda.Coluna] = true;
					}
				}
			}

			return mat;
		}
	}
}
