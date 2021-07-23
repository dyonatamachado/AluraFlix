namespace AluraFlix.Models
{
    public class ViewVideo
    {
        public ViewVideo(int id, string titulo, string descricao, string url)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Url = url;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Url { get; private set; }
    }
}