using System.Collections.Generic;
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
		private HashSet<Peca> Pecas;
		private HashSet<Peca> PecasCapturadas;
		public bool Xeque { get; private set; }

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

		public Peca ExecutaMovimento(Posicao origem, Posicao Destino)
		{
			Peca p = Tab.RetirarPeca(origem);
			p.IncrementarQMovimentos();
			Peca pecaCap = Tab.RetirarPeca(Destino);
			Tab.ColocarPeca(p, Destino);
			if (pecaCap != null)
			{
				PecasCapturadas.Add(pecaCap);
			}
			return pecaCap;
		}

		public void DesfazMovimento(Posicao origem, Posicao destino, Peca cap)
		{
			Peca p = Tab.RetirarPeca(destino);
			p.DecrementarQMovimentos();

			if (cap != null)
			{
				Tab.ColocarPeca(cap, destino);
				PecasCapturadas.Remove(cap);
			}
			Tab.ColocarPeca(p, origem);
		}

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			Peca pecaCap = ExecutaMovimento(origem, destino);

			if (EstaXeque(JogadorAtual))
			{
				DesfazMovimento(origem, destino, pecaCap);
				throw new TabuleiroException("Voce não pode se colocar em xeque!");
			}

			if (EstaXeque(Adversaria(JogadorAtual)))
			{
				Xeque = true;
			}
			else
			{
				Xeque = false;
			}

			if (EstaXequeMate(Adversaria(JogadorAtual)))
			{
				Terminada = true;
			}
			else
			{
				Turno++;
				MudaJogador();
			}
		}

		public void ValidarPosicaoOrigem(Posicao pos)
		{
			if (Tab.Peca(pos) == null)
			{
				throw new TabuleiroException("Não existe peça na posição de origem escolhida");
			}
			if (JogadorAtual != Tab.Peca(pos).Cor)
			{
				throw new TabuleiroException("O turno é da peça " + JogadorAtual);
			}
			if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
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
			if (JogadorAtual == Cor.Preta)
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
			foreach (Peca x in PecasCapturadas)
			{
				if (x.Cor == cor)
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

		private Cor Adversaria(Cor cor)
		{
			if (cor == Cor.Branca)
			{
				return Cor.Preta;
			}
			else
			{
				return Cor.Branca;
			}
		}

		private Peca Rei(Cor cor)
		{
			foreach (Peca x in PecasEmJogo(cor))
			{
				if (x is Rei)
				{
					return x;
				}
			}
			return null;
		}

		private bool EstaXeque(Cor cor)
		{
			Peca rei = Rei(cor);
			if (rei == null)
			{
				throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro!");
			}
			bool[,] mat;
			foreach (Peca x in PecasEmJogo(Adversaria(cor)))
			{
				mat = x.MovimentosPossiveis();
				if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
				{
					return true;
				}
			}
			return false;
		}

		public bool EstaXequeMate(Cor cor)
		{
			if (!EstaXeque(cor))
			{
				return false;
			}
			
			foreach (Peca x in PecasEmJogo(cor))
			{
				bool[,] mat = x.MovimentosPossiveis();
				for (int i = 0; i < Tab.Linhas; i++)
				{
					for (int j = 0; j < Tab.Colunas; j++)
					{
						if (mat[i, j])
						{
							Posicao origem = x.Posicao;
							Posicao destino = new Posicao(i, j);
							Peca pCap = ExecutaMovimento(x.Posicao, destino);
							bool xequeTeste = EstaXeque(cor);
							DesfazMovimento(origem, destino, pCap);

							if (!xequeTeste)
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		public void ColocarNovaPeca(char coluna, int linha, Peca peca)
		{
			Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			Pecas.Add(peca);
		}
		private void ColocarPecas()
		{
			ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('h', 7, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

			ColocarNovaPeca('b', 8, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('a', 8, new Rei(Cor.Preta, Tab));


		}
	}
}
