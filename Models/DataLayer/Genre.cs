using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdvancedBookSearch.Models.DataLayer;

public partial class Genre
{
    [Key]
    [StringLength(10)]
    public string GenreId { get; set; } = null!;

    [StringLength(25)]
    public string Name { get; set; } = null!;

    [InverseProperty("Genre")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
