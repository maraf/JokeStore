using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JokeStore.Core.Entity
{
    public class Entry
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage="Please fill content")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }

        public bool Approved { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ThumbsUp { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ThumbsDown { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Votes { get; set; }

        [Required(ErrorMessage = "Please fill category")]
        public string Category { get; set; }

        public string CategoryUrl { get; set; }

        public virtual Domain Domain { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageContentType { get; set; }
    }
}
