using tabuleiro;

namespace xadrez
{
	class Cavalo : Peca
	{
		public Cavalo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
		{
		}

		public override string ToString()
		{
			return "C";
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

			//acima Esquerda
			pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//acima Direita
			pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Nordeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Noroeste
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Abaixo Esquerda
			pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Abaixo Direita
			pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Sudeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			//Sudoeste
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			return mat;
		}
	}
}
