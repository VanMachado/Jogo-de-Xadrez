using tabuleiro;
using Xadrez_Console;
using xadrez;
using tabuleiro.exceptions;

try
{
    PartidaDeXadrez partida = new PartidaDeXadrez();

    while(!partida.Terminada)
    {        
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab);

        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

        bool[,] posicoesPossiveis = partida.Tab.PosicaoPeca(origem).MovimentosPosiveis();

        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

        Console.WriteLine();
        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partida.ExecutaMovimento(origem, destino);
    }    
}
catch (TabuleiroExceptions e)
{
    Console.WriteLine(e.Message);
}