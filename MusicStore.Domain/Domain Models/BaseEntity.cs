﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.Domain_Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
