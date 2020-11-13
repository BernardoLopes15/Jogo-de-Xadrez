﻿namespace tabuleiro
{
	class Tabuleiro
	{
		public int Linhas { get; set; }
		public int Colunas { get; set; }
		private Peca[,] Pecas;

		public Tabuleiro()
		{
		}

		public Tabuleiro(int linhas, int colunas)
		{
			Linhas = linhas;
			Colunas = colunas;
			Pecas = new Peca[Linhas, Colunas];
		}

		public Peca Pea(int linha, int coluna)
		{
			return Pecas[linha, coluna];
		}
	}
}
