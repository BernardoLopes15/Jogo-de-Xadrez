using tabuleiro;

namespace xadrez
{
	class Rei : Peca
	{
		private PartidaDeXadrez Partida;
		public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
		{
			Partida = partida;
		}

		public override string ToString()
		{
			return "R";
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.Peca(pos);
			return p == null || p.Cor != Cor;
		}

		private bool TesteRoque(Posicao pos)
		{
			Peca p = Tab.Peca(pos);
			return p != null && p is Torre && p.Cor == Cor && QuantMoviment == 0;
		}

		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			//acima
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
			if(Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//abaixo
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Direita
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Esquerda
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Nordeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Noroeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Sudeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Sudoeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//#JogadaEspecial Roque
			if(QuantMoviment == 0 && !Partida.Xeque)
			{
				//#JogadaEspecial Roque Pequeno
				Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
				if (TesteRoque(posT1))
				{
					Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
					Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
					if(Tab.Peca(p1) == null && Tab.Peca(p2) == null)
					{
							mat[Posicao.Linha, Posicao.Coluna + 2] = true;
					}
				}

				//#JogadaEspecial Roque Grande
				Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
				if (TesteRoque(posT2))
				{
					Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
					Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
					if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null)
					{
						mat[Posicao.Linha, Posicao.Coluna - 2] = true;
					}
				}
			}

			return mat;
		}
	}
}
