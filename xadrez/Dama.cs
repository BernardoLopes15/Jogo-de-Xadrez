﻿using tabuleiro;

namespace xadrez
{
	class Dama : Peca
	{
		public Dama(Cor cor, Tabuleiro tab) : base(cor, tab)
		{
		}

		public override string ToString()
		{
			return "D";
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.Peca(pos);
			return p == null || p.Cor != Cor;
		}

		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			//Nordeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha - 1;
				pos.Coluna = pos.Coluna + 1;
			}

			//Noroeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha - 1;
				pos.Coluna = pos.Coluna - 1;
			}

			//Sudeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha + 1;
				pos.Coluna = pos.Coluna + 1;
			}

			//Sudoeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha + 1;
				pos.Coluna = pos.Coluna - 1;
			}

			//acima
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha - 1;
			}

			//abaixo
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Linha = pos.Linha + 1;
			}

			//Direita
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Coluna = pos.Coluna + 1;
			}

			//Esquerda
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
				{
					break;
				}
				pos.Coluna = pos.Coluna - 1;
			}

			return mat;
		}
	}
}