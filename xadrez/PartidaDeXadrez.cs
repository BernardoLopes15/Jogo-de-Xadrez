using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
	class PartidaDeXadrez
	{
		public Tabuleiro Tab { get; private set; }
		public int Turno { get; private set; }
		public Cor JogadorAtual { get; private set; }
		public bool Terminada { get; private set; }
		private HashSet<Peca> Pecas;
		private HashSet<Peca> PecasCapturadas;

		public PartidaDeXadrez()
		{
			Tab = new Tabuleiro(8, 8);
			Turno = 1;
			JogadorAtual = Cor.Branca;
			Terminada = false;
			Pecas = new HashSet<Peca>();
			PecasCapturadas = new HashSet<Peca>();
			ColocarPecas();
		}

		public void ExecutaMovimento(Posicao origem, Posicao Destino)
		{
			Peca p = Tab.RetirarPeca(origem);
			p.IncrementarQMovimentos();
			Peca pecaCap = Tab.RetirarPeca(Destino);
			Tab.ColocarPeca(p, Destino);
			if(pecaCap != null)
			{
				PecasCapturadas.Add(pecaCap);
			}
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

		public HashSet<Peca> PecasCap(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();
			foreach(Peca x in PecasCapturadas)
			{
				if(x.Cor == cor)
				{
					aux.Add(x);
				}
			}
			return aux;
		}

		public HashSet<Peca> PecasEmJogo(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();
			foreach (Peca x in Pecas)
			{
				if (x.Cor == cor)
				{
					aux.Add(x);
				}
			}
			aux.ExceptWith(PecasCap(cor));
			return aux;
		}

		public void ColocarNovaPeca(char coluna, int linha, Peca peca)
		{
			Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			Pecas.Add(peca);
		}
		private void ColocarPecas()
		{
			ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

			ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));

			
		}
	}
}
