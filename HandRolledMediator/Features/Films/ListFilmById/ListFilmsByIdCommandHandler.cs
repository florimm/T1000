﻿namespace HandRolledMediator.Features.Films.ListFilmById
{
    using HandRolledMediator.Features.CastMembers.GetCastByFilmIdQuery;
    using HandRolledMediator.Features.Directors.GetDirectorByIdQuery;
    using HandRolledMediator.Features.Films.ListFilmByIdQuery;

    public class ListFilmsByIdCommandHandler : CommandHandler<ListFilmsByIdCommand>
    {
        private readonly IListFilmByIdQuery listFilmByIdQuery;

        private readonly IGetDirectorByIdQuery getDirectorByIdQuery;

        private readonly IGetCastByFilmIdQuery getCastByFilmIdQuery;

        public ListFilmsByIdCommandHandler(IListFilmByIdQuery listFilmByIdQuery, IGetDirectorByIdQuery getDirectorByIdQuery, IGetCastByFilmIdQuery getCastByFilmIdQuery)
        {
            this.listFilmByIdQuery = listFilmByIdQuery;
            this.getDirectorByIdQuery = getDirectorByIdQuery;
            this.getCastByFilmIdQuery = getCastByFilmIdQuery;

            //No need to inject IPermissionService as we don't need it
        }

        protected override object Execute(ListFilmsByIdCommand command)
        {
            //Use shared query to get film
            var film = this.listFilmByIdQuery.Execute(command.Id);
            
            if (film == null)
            {
                return null;
            }

            var director = this.getDirectorByIdQuery.Execute(film.DirectorId);
            film.Director = director;

            var cast = this.getCastByFilmIdQuery.Execute(command.Id);
            film.Cast = cast;

            return film;
        }
    }
}
