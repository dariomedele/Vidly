﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
       
        public int Id { get; set; }

       
        public string Name { get; set; }

       
        public DateTime? ReleaseDate { get; set; }

       
        public DateTime? DateAdded { get; set; }

         
        [Range(1, 20)]
        public int NumberInStock { get; set; }

      
        public int GenreId { get; set; }

    }
}