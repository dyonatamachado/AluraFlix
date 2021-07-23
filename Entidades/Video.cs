using System;

namespace AluraFlix.Entidades
{
    public class Video
    {
        public Video(string titulo, string descricao, string url)
        {
            Titulo = titulo;
            Descricao = descricao;
            Url = url;
            Ativo = true;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Url { get; private set; }
        public bool Ativo { get; private set; }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Reativar()
        {
            Ativo = true;
        }

        public void AtualizarVideo(string titulo, string descricao, string url)
        {
            Titulo = titulo;
            Descricao = descricao;
            Url = url;
        } 
    }
}