public abstract class ItemBiblioteca : IEmprestavel
{
    public string Titulo { get; set; }
    public string Codigo { get; set; }

    public ItemBiblioteca(string Titulo) {

        this.Titulo = Titulo;
        Codigo = "";

    }

    public abstract void Emprestimo(Usuario usuario, int dias);
    public abstract void Devolucao(Usuario usuario);
    public abstract void ExibirInfo();

}