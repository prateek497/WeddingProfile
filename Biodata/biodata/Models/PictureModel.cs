﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class PictureModel
    {
        public bool IsProfile { get; set; }

        public byte[] PicBytes { get; set; }

        public int Id { get; set; }
    }

    public class PictureList
    {
        public List<PictureModel> PicList { get; set; }
    }
}