﻿namespace FunctionalProject.Features.FuncFilms.ListFilmById
{
    using System;
    using System.Collections.Generic;
    using Models;

    public static class ListFilmByIdRoute
    {
        public static Film Handle(int id, Func<int,Film> listFilmById, Func<int,Director> getDirectorById, Func<int,IEnumerable<CastMember>> getCastByFilmIdQuery)
        {
            var film = listFilmById(id);
            
            if (film == null)
            {
                return null;
            }

            var director = getDirectorById(film.DirectorId);
            film.Director = director;

            var cast = getCastByFilmIdQuery(id);
            film.Cast = cast;

            return film;
        }
    }
}
