using System;
using tabuleiro;

namespace xadrez
{
	class PartidaDeXadrez
	{
		public Tabuleiro Tab { get; private set; }
		public int Turno { get; private set; }
		public Cor JogadorAtual { get; private set; }
		public bool Terminada { get; private set; }

		public PartidaDeXadrez()
		{
			Tab = new Tabuleiro(8, 8);
			Turno = 1;
			JogadorAtual = Cor.Branca;
			Terminada = false;
			ColocarPecas();
		}

		public void ExecutaMovimento(Posicao origem, Posicao Destino)
		{
			Peca p = Tab.RetirarPeca(origem);
			p.IncrementarQMovimentos();
			Peca pecaCap = Tab.RetirarPeca(Destino);
			Tab.ColocarPeca(p, Destino);
		}

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			ExecutaMovimento(origem, destino);
			Turno++;
			MudaJogador();
		}

		public void ValidarPosicaoOrigem(Posicao pos)
		{
			if(Tab.Peca(pos) == null)
			{
				throw new TabuleiroException("Não existe peça na posição de origem escolhida");
			}
			if(JogadorAtual != Tab.Peca(pos).Cor)
			{
				throw new TabuleiroException("O turno é da peça " + JogadorAtual);
			}
			if(!Tab.Peca(pos).ExisteMovimentosPossiveis())
			{
				throw new TabuleiroException("A peça está bloqueada");
			}
		}

		public void ValidarPosicaoDestino(Posicao origem, Posicao Destino)
		{
			if (!Tab.Peca(origem).PodeMoverPara(Destino))
			{
				throw new TabuleiroException("Posicao de destino invalida");
			}
		}

		private void MudaJogador()
		{
			if(JogadorAtual == Cor.Preta)
			{
				JogadorAtual = Cor.Branca;
			}
			else
			{
				JogadorAtual = Cor.Preta;
			}
		}

		private void ColocarPecas()
		{
			Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 1).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 2).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('d', 2).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 2).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 1).ToPosicao());
			Tab.ColocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('d', 1).ToPosicao());

			Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 8).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 7).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('d', 7).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 7).ToPosicao());
			Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 8).ToPosicao());
			Tab.ColocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('d', 8).ToPosicao());
		}
	}
}
