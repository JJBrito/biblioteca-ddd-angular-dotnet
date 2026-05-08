using System;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Cliente
    {
        public int ClientId { get; set; }
        public string Nome { get; set; }
        public int Sobrenome { get; set; }
        public int Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo {  get; set; }    

        public bool ClienteEspecial(Cliente cliente) 
        {
            return cliente.Ativo && DateTime.Now.Year - cliente.DataCadastro.Year >= 5;
        }
    }
}
