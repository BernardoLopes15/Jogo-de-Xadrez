﻿namespace tabuleiro
{
	class Peca
	{
		public Posicao Posicao { get; set; }
		public Cor Cor { get; protected set; }
		public int QuantMoviment { get; protected set; }
		public Tabuleiro Tabuleiro { get; protected set; }

		public Peca(Cor cor, Tabuleiro tabuleiro)
		{
			Posicao = null;
			Cor = cor;
			Tabuleiro = tabuleiro;
			QuantMoviment = 0;
		}

		public void IncrementarQMovimentos()
		{
			QuantMoviment++;
		}
	}
}
