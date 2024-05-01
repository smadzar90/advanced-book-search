using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdvancedBookSearch.Models.DataLayer;

[Index("GenreId", Name = "IX_Books_GenreId")]
public partial class Book
{
    [Key]
    [DisplayName("Item #")]
    public int BookId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public double Price { get; set; }

    [StringLength(10)]
    public string GenreId { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Books")]
    public virtual Genre Genre { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    [DisplayName("Author(s)")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
