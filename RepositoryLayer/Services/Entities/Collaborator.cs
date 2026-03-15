using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Collaborator
    {
        [Key]
        public int CollabId { get; set; }
        public string CollabEmailId { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
