using tabuleiro;

namespace xadrez
{
	class Rei : Peca
	{
		public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
		{
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

			return mat;
		}
	}
}
