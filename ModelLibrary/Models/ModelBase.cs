using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLibrary.Models
{
    public class ModelBase
    {
        [Key]
        public int Id { get; private set; }

        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo 3 caracteres.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }

        public ModelBase()
        {
            if (Id == 0)
                Created = DateTime.UtcNow;
            
            Updated = DateTime.UtcNow;
        }
    }
}
