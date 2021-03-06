using System.Collections.Generic;
using System.Linq;
using AluraFlix.Entidades;
using AluraFlix.Models;
using AluraFlix.Persistencia;
using AluraFlix.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly VideosContext _dbContext;
        public VideosController(VideosContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetVideos()
        {
            var videos = _dbContext.Videos.Where(v => v.Ativo).ToList();

            if(videos.Count == 0)
                return NoContent();
            else
            {
                var listaViewVideos = new List<ViewVideo>();

                foreach(var video in videos)
                {
                    var viewVideo = new ViewVideo(video.Id, video.Titulo, 
                        video.Descricao, video.Url);
                    
                    listaViewVideos.Add(viewVideo);
                }

                return Ok(listaViewVideos);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetVideoById(int id)
        {
            var video = _dbContext.Videos.SingleOrDefault(v => (v.Id == id) && v.Ativo);

            if(video == null)
                return NotFound();
            else
            {
                var viewVideo = new ViewVideo(video.Id, video.Titulo, 
                        video.Descricao, video.Url);
                
                return Ok(viewVideo);
            }
        }

        [HttpPost]
        public IActionResult CreateVideo([FromBody] InputVideo input)
        {
            var videoService = new VideoService(_dbContext);
            var videoCadastrado = videoService.VerificaSeVideoJaEstaCadastrado(input);
            var isUrl = videoService.VerificarUrl(input.Url);

            if(videoCadastrado == null)
            {
                if(!isUrl)
                    return BadRequest("Url informada est?? com formato inv??lido.");
                else
                {
                    var video = new Video(input.Titulo, input.Descricao, input.Url);

                    _dbContext.Add(video);
                    _dbContext.SaveChanges();

                    return CreatedAtAction(nameof(GetVideoById), new {id = video.Id}, input);
                }
            }
            else
            {
                if(videoCadastrado.Ativo)
                {
                    return BadRequest($"V??deo j?? est?? cadastrado com ID: {videoCadastrado.Id}.");
                }
                else
                    return BadRequest($"V??deo j?? cadastrado com ID: {videoCadastrado.Id}," 
                    + "por??m est?? inativo. "
                    + "Para ativ??-lo novamente utilize o m??todo PATCH");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id,[FromBody] InputVideo input)
        {
            var video = _dbContext.Videos.SingleOrDefault(v => v.Id == id);
            var videoService = new VideoService(_dbContext);
            var isUrl = videoService.VerificarUrl(input.Url);

            if(video == null)
                return NotFound();
            else if(!video.Ativo)
                return BadRequest("V??deo informado est?? desativado. Se quiser reativ??-lo " +
                "utilize o m??todo PATCH e depois atualize os demais dados.");
            else
            {
                if(!isUrl)
                    return BadRequest("Url informada est?? com formato inv??lido.");
                    
                video.AtualizarVideo(input.Titulo, input.Descricao, input.Url);
                _dbContext.SaveChanges();

                return NoContent();
            }
        }
        
        [HttpPatch("{id}")]
        public IActionResult ReactivateVideo(int id)
        {
            var video = _dbContext.Videos.SingleOrDefault(v => v.Id == id);

            if(video == null)
                return NotFound();
            else if(video.Ativo)
                return BadRequest("V??deo informado j?? est?? com status ativo.");
            else
            {
                video.Reativar();
                _dbContext.SaveChanges();

                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(int id)
        {
            var video = _dbContext.Videos.SingleOrDefault(v => v.Id == id);

            if(video == null)
                return NotFound();
            else if(!video.Ativo)
                return BadRequest("V??deo j?? est?? desativado.");
            else
            {
                video.Desativar();
                _dbContext.SaveChanges();

                return NoContent();
            }
        }
    }
}