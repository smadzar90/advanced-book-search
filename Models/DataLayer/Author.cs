﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdvancedBookSearch.Models.DataLayer;

public partial class Author
{
    [Key]
    public int AuthorId { get; set; }

    [StringLength(200)]
    public string FirstName { get; set; } = null!;

    [StringLength(200)]
    public string LastName { get; set; } = null!;

    [ForeignKey("AuthorId")]
    [InverseProperty("Authors")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
