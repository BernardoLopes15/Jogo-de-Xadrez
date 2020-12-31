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
		public Peca VulEnPassant { get; private set; }
		public bool Promocao;

		public PartidaDeXadrez()
		{
			Tab = new Tabuleiro(8, 8);
			Turno = 1;
			JogadorAtual = Cor.Branca;
			Terminada = false;
			Xeque = false;
			VulEnPassant = null;
			Promocao = false;
			Pecas = new HashSet<Peca>();
			PecasCapturadas = new HashSet<Peca>();
			ColocarPecas();
		}

		public Peca ExecutaMovimento(Posicao origem, Posicao destino)
		{
			Peca p = Tab.RetirarPeca(origem);
			p.IncrementarQMovimentos();
			Peca pecaCap = Tab.RetirarPeca(destino);
			Tab.ColocarPeca(p, destino);
			if (pecaCap != null)
			{
				PecasCapturadas.Add(pecaCap);
			}

			//#JogadaEspecial Roque Pequeno
			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
				Peca t = Tab.RetirarPeca(origemT);
				t.IncrementarQMovimentos();
				Tab.ColocarPeca(t, destinoT);
			}

			//#JogadaEspecial Roque Grande
			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca t = Tab.RetirarPeca(origemT);
				t.IncrementarQMovimentos();
				Tab.ColocarPeca(t, destinoT);
			}


			// #JogadaEspecial En Passant
			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && pecaCap == null)
				{
					Posicao posP;
					if (p.Cor == Cor.Branca)
					{
						posP = new Posicao(destino.Linha + 1, destino.Coluna);
					}
					else
					{
						posP = new Posicao(destino.Linha - 1, destino.Coluna);
					}
					pecaCap = Tab.RetirarPeca(posP);
					PecasCapturadas.Add(pecaCap);
				}
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

			//#JogadaEspecial Roque Pequeno
			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
				Peca t = Tab.RetirarPeca(destinoT);
				t.DecrementarQMovimentos();
				Tab.ColocarPeca(t, origemT);
			}

			//#JogadaEspecial Roque Grande
			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca t = Tab.RetirarPeca(destinoT);
				t.IncrementarQMovimentos();
				Tab.ColocarPeca(t, origemT);
			}

			// #JogadaEspecial En Passant
			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && cap == VulEnPassant)
				{
					Peca peao = Tab.RetirarPeca(destino);
					Posicao posP;
					if (p.Cor == Cor.Preta)
					{
						posP = new Posicao(4, destino.Coluna);
					}
					else
					{
						posP = new Posicao(3, destino.Coluna);
					}
					Tab.ColocarPeca(peao, posP);
				}
			}
		}

		public void PromocaoJog(Cor cor, string c, Posicao destino)
		{
			if (c == "d" || c == "D")
			{
				Peca peca = new Dama(Adversaria(cor), Tab);
				Tab.ColocarPeca(peca, destino);
				Pecas.Add(peca);
			}
			else if (c == "t" || c == "T")
			{
				Peca peca = new Torre(Adversaria(cor), Tab);
				Tab.ColocarPeca(peca, destino);
				Pecas.Add(peca);
			}
			else if (c == "b" || c == "B")
			{
				Peca peca = new Bispo(Adversaria(cor), Tab);
				Tab.ColocarPeca(peca, destino);
				Pecas.Add(peca);
			}
			else if (c == "c" || c == "C")
			{
				Peca peca = new Cavalo(Adversaria(cor), Tab);
				Tab.ColocarPeca(peca, destino);
				Pecas.Add(peca);
			}
		}

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			Peca pecaCap = ExecutaMovimento(origem, destino);

			if (EstaXeque(JogadorAtual))
			{
				DesfazMovimento(origem, destino, pecaCap);
				throw new TabuleiroException("Voce não pode se colocar em xeque!");
			}

			Peca p = Tab.Peca(destino);

			// #JogadaEspecial Promocao

			if (p is Peao)
			{
				if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
				{
					Promocao = true;
					p = Tab.RetirarPeca(destino);
					Pecas.Remove(p);
				}
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

			// #JogadaEspecial En Passant
			if (p is Peao && (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2))
			{
				VulEnPassant = p;
			}
			else
			{
				VulEnPassant = null;
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
			if (!Tab.Peca(origem).MovimentoPossivel(Destino))
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

		public bool EstaXeque(Cor cor)
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
			ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
			ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
			ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
			ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
			ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
			ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
			ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));

			ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
			ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

			ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
			//ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
			ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
			ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
			ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
			ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
			ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
			ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));

			ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
			ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
			//Colocar preta depois abaixo
			ColocarNovaPeca('h', 7, new Peao(Cor.Branca, Tab, this));

		}
	}
}
