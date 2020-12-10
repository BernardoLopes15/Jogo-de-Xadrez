using System;
using tabuleiro;

namespace xadrez
{
	class PartidaDeXadrez
	{
		public Tabuleiro Tab { get; private set; }
		private int _turno;
		private Cor _jogadorAtual;
		public bool Terminada { get; private set; }

		public PartidaDeXadrez()
		{
			Tab = new Tabuleiro(8, 8);
			_turno = 1;
			_jogadorAtual = Cor.Branca;
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
