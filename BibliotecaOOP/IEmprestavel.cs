public interface IEmprestavel
{
    void Emprestimo(Usuario usuario, int dias);
    void Devolucao(Usuario usuario);
}