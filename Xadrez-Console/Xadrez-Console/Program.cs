using tabuleiro;
using Xadrez_Console;
using xadrez;

Tabuleiro tab = new Tabuleiro(8, 8);
Rei rei = new Rei(tabuleiro.enums.Cor.Branca, tab);
Torre torre = new Torre(tabuleiro.enums.Cor.Branca, tab);   

tab.ColocarPeca(rei, new Posicao(0,4));
tab.ColocarPeca(torre, new Posicao(0,0));
tab.ColocarPeca(new Torre(tabuleiro.enums.Cor.Preta, tab), new Posicao(1, 3));
tab.ColocarPeca(new Rei(tabuleiro.enums.Cor.Preta, tab), new Posicao(2, 4));

Tela.ImprimirTabuleiro(tab);