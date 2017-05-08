﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestASP.NetIdentity.DomainEntities
{
    [Table("AnswerType", Schema = "dbo")]
    public class AnswerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string AnswerTypeText { get; set; }
    }
}
