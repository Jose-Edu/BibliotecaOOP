public abstract class ItemBiblioteca : IEmprestavel, IPesquisavel
{
    public string Titulo { get; protected set; }

    public ItemBiblioteca(string Titulo) {

        this.Titulo = Titulo;

    }

    public abstract void Emprestimo(int dias);
    public abstract void Devolucao();
    public abstract void ExibirInfo();

}