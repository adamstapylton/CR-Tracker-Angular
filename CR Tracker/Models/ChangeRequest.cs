﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Models
{
    public class ChangeRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string ChangeRequestId { get; set; }

        [Required]
        public string Description { get; set; }

        
        public DateTime DateRequired { get; set; }


        public User AssignedToUser { get; set; }


        public User RaisedByUser { get; set; }


        public IEnumerable<Worktype> Worktypes { get; set; }


        public bool BillingRulesRequired { get; set; }


        public bool OnHold { get; set; }


        public IEnumerable<Note> Notes { get; set; }


        public Stage Stage { get; set; }

    }
}
