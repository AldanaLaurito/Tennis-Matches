using System;
using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Persistence;

namespace Baufest.Tennis.Business
{
    public class TableroBusiness : ITableroBusiness
    {
        private readonly IPartidoRepository partidoRepository;

        public TableroBusiness()
        {
            this.partidoRepository = new PartidoRepository();
        }

        public TableroBusiness(IPartidoRepository partidoRepository)
        {
            this.partidoRepository = partidoRepository;
        }

        public Partido IniciarPartido(int id)
        {
            var partido = this.partidoRepository.GetPartido(id);
            if (partido.Estado == EstadoPartido.NoIniciado)
            {
                partido.PuntosGameLocal = "0";
                partido.GamesLocal = 0;
                partido.ScoreLocal = 0;

                partido.PuntosGameVisitante = "0";
                partido.GamesVisitante = 0;
                partido.ScoreVisitante = 0;

                partido.Estado = EstadoPartido.EnCurso;
                this.partidoRepository.UpdatePartido(partido);
            }

            return partido;
        }

        public Partido SumarPuntoLocal(int id)
        {
            Partido partido = this.partidoRepository.GetPartido(id);

            partido.ScoreLocal += 1;

            this.ActualizarScore(partido);

            this.partidoRepository.UpdatePartido(partido);

            return partido;
        }

        public Partido SumarPuntoVisitante(int id)
        {
            Partido partido = this.partidoRepository.GetPartido(id);

            partido.ScoreVisitante += 1;

            this.ActualizarScore(partido);

            this.partidoRepository.UpdatePartido(partido);

            return partido;
        }

        private void ActualizarScore(Partido partido)
        {
            if (partido.ScoreLocal < 4 && partido.ScoreVisitante < 4)
            {
                partido.PuntosGameLocal = this.TranslateScore(partido.ScoreLocal);
                partido.PuntosGameVisitante = this.TranslateScore(partido.ScoreVisitante);

                return;
            }

            if (partido.ScoreLocal >= 3 && partido.ScoreVisitante >= 3 && Math.Abs(partido.ScoreLocal - partido.ScoreVisitante) < 2)
            {
                if (partido.ScoreLocal == partido.ScoreVisitante)
                {
                    partido.PuntosGameLocal = "40";
                    partido.PuntosGameVisitante = "40";
                }
                else if (partido.ScoreLocal > partido.ScoreVisitante)
                {
                    partido.PuntosGameLocal = "Adv";
                }
                else
                {
                    partido.PuntosGameVisitante = "Adv";
                }

                return;
            }

            if (partido.ScoreLocal > partido.ScoreVisitante && partido.ScoreLocal >= 4)
            {
                this.GameLocal(partido);

                return;
            }

            if (partido.ScoreVisitante > partido.ScoreLocal && partido.ScoreVisitante >= 4)
            {
                this.GameVisitante(partido);

                return;
            }
        }

        private string TranslateScore(int points)
        {
            switch (points)
            {
                case 3:
                    return "40";
                case 2:
                    return "30";
                case 1:
                    return "15";
                case 0:
                    return "0";
                default:
                    return string.Empty;
            }
        }

        private void GameLocal(Partido partido)
        {
            partido.ScoreLocal = 0;
            partido.ScoreVisitante = 0;
            partido.PuntosGameLocal = this.TranslateScore(partido.ScoreLocal);
            partido.PuntosGameVisitante = this.TranslateScore(partido.ScoreVisitante);

            partido.GamesLocal++;
            if (partido.GamesLocal == 6)
            {
                partido.Estado = EstadoPartido.Finalizado;
            }
            
        }

        private void GameVisitante(Partido partido)
        {
            partido.ScoreLocal = 0;
            partido.ScoreVisitante = 0;
            partido.PuntosGameLocal = this.TranslateScore(partido.ScoreLocal);
            partido.PuntosGameVisitante = this.TranslateScore(partido.ScoreVisitante);

            partido.GamesVisitante++;
            if (partido.GamesVisitante == 6)
            {            
                partido.Estado = EstadoPartido.Finalizado;
            }

        }
    }
}
