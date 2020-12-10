namespace tabuleiro
{
	abstract class Peca
	{
		public Posicao Posicao { get; set; }
		public Cor Cor { get; protected set; }
		public int QuantMoviment { get; protected set; }
		public Tabuleiro Tab { get; protected set; }

		public Peca(Cor cor, Tabuleiro tab)
		{
			Posicao = null;
			Cor = cor;
			Tab = tab;
			QuantMoviment = 0;
		}

		public void IncrementarQMovimentos()
		{
			QuantMoviment++;
		}

		public abstract bool[,] MovimentosPossiveis();
	}
}
