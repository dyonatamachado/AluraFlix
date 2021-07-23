using System;
using System.Linq;
using System.Text.RegularExpressions;
using AluraFlix.Entidades;
using AluraFlix.Models;
using AluraFlix.Persistencia;

namespace AluraFlix.Servicos
{
    public class VideoService
    {
        private readonly VideosContext _dbContext;
        public VideoService(VideosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Video VerificaSeVideoJaEstaCadastrado(InputVideo input)
        {
            var game = _dbContext.Videos.SingleOrDefault(v => v.Url == input.Url);

            return game;
        }

        public bool VerificarUrl(string url)
        {
            string padrao = @"(https?\:/{2})?\w+\.(\w+)?(\.com)?(\.[a-z]{2})?/?";
            var regex = new Regex(padrao);
            
            var isUrl = regex.IsMatch(url);

            return isUrl;
        }
    }
}